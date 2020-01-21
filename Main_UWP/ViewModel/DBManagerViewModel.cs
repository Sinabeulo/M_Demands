using DACLayer.Connection;
using DACLayer.Querys;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace Main_UWP.ViewModel
{
    public class DBManagerViewModel : ViewModelBase
    {

        #region Field
        private IList<ConnectionInfo> _connectionList;
        private ConnectionInfo _selectedConnection;
        private ConnectionInfo _newConnectionInfo;
        private string _folderPath;
        private bool _canSave;
        private HttpClient client;
        Windows.Web.Http.HttpClient httpClient;

        private readonly string fileName = "ServList.txt";
        #endregion

        #region Properties
        /// <summary>
        /// 서버 리스트
        /// </summary>
        public IList<ConnectionInfo> ConnectionList
        {
            get => _connectionList;
            set => SetProperty(ref _connectionList, nameof(ConnectionList), value);
        }
        /// <summary>
        /// 선택된 서버
        /// </summary>
        public ConnectionInfo SelectedConnection
        {
            get => _selectedConnection;
            set => SetProperty(ref _selectedConnection, nameof(SelectedConnection), value);
        }
        /// <summary>
        /// 새로 입력된 서버
        /// </summary>
        public ConnectionInfo NewConnectionInfo
        {
            get => _newConnectionInfo;
            set => SetProperty(ref _newConnectionInfo, nameof(NewConnectionInfo), value);
        }
        /// <summary>
        /// 서버 목록 파일의 폴더 경로
        /// </summary>
        public string FolderPath
        {
            get => _folderPath;
            set => SetProperty(ref _folderPath, nameof(FolderPath), value);
        }
        /// <summary>
        /// 저장 가능여부
        /// </summary>
        public bool CanSave
        {
            get => _canSave;
            set => SetProperty(ref _canSave, nameof(CanSave), value);
        }
        #endregion

        #region Command
        /// <summary>
        /// 연결 커맨드
        /// </summary>
        public ICommand ConnectCommand { get; set; }
        /// <summary>
        /// 테스트 커맨드
        /// </summary>
        public ICommand ApiGetCommand { get; set; }
        /// <summary>
        /// [GET] 접속리스트 조회
        /// </summary>
        public ICommand GetConnectionListCommand { get; set; }
        #endregion

        public DBManagerViewModel()
        {
            CanSave = false;

            ConnectionList = new System.Collections.ObjectModel.ObservableCollection<ConnectionInfo>();

            AddCommand = new RelayCommand(ExecuteAddCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            ConnectCommand = new RelayCommand(ExecuteConnectCommand);
            ApiGetCommand = new RelayCommand(GetConnectionListAsync);
            GetConnectionListCommand = new RelayCommand(ExecuteGetConnectionListCommand);

            InitializeHttpClient();

            NewConnectionInfo = new ConnectionInfo();

            PropertyChanged += DBManagerViewModel_PropertyChanged;
        }

        private void DBManagerViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(e.PropertyName):
                    //SelectedDS = SelectedServer.DataSource;
                    //SelectedIC = SelectedServer.InitialCatalog;
                    //SelectedID = SelectedServer.UserID;
                    break;
            }
        }

        #region Execute
        private async void ExecuteConnectCommand()
        {
            QueryManager queryManager = new QueryManager(SelectedConnection.DataSource,SelectedConnection.InitialCatalog,SelectedConnection.UserID,SelectedConnection.Password);
            bool result = await queryManager.ConnectionTest();
            if(result == false)
            {
                CommonFeature.Feature.ShowMessageAsync("Connection Failed");
            }

            SelectedConnection.Password = string.Empty;
            // 다음 코드...
        }

        /// <summary>
        /// 선택된 아이템 삭제 후 저장
        /// </summary>
        private void ExecuteDeleteCommand()
        {
            if(SelectedConnection == null)
            {
                //선택된 아이템 없음.
                CommonFeature.Feature.ShowMessageAsync("There is no selected item.");
                return;
            }

            ConnectionList.Remove(SelectedConnection);
            SelectedConnection = null;

            ExecuteSaveCommand();
        }

        /// <summary>
        /// 서버 목록 저장
        /// </summary>
        private async void ExecuteSaveCommand()
        {
            try
            {
                if (NewConnectionInfo != null)
                {
                    ConnectionList.Add(NewConnectionInfo);
                    NewConnectionInfo = null;
                }

                var saveList = ConnectionList.Select(s => s.ToString()).ToList();

                await FileManager.SingleFileManager.FileWriteAsync(fileName, saveList, FileManager.ReadWriteWay.Buffer);

                ReadServerListAysnc();
                CanSave = false;
            }
            catch(Exception e)
            {
                CommonFeature.Feature.ShowMessageAsync(e.Message);
            }
        }

        /// <summary>
        /// 새로운 서버 입력
        /// </summary>
        private void ExecuteAddCommand()
        {
            //NewConnectionInfo = new ConnectionInfo();
            if(string.IsNullOrEmpty(NewConnectionInfo.DataSource) || string.IsNullOrEmpty(NewConnectionInfo.InitialCatalog) || 
                string.IsNullOrEmpty(NewConnectionInfo.UserID) || string.IsNullOrEmpty(NewConnectionInfo.Title))
            {
                CommonFeature.Feature.ShowMessage("전부 입력되지 않았습니다.");
                return;
            }

            ConnectionList.Add(NewConnectionInfo);
            NewConnectionInfo = null;
            CanSave = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private async void ExecuteGetConnectionListCommand()
        {
            try
            {
                var response = await client.GetAsync("api/List");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var connectList = await response.Content.ReadAsStringAsync();


                //Uri requestUri = new Uri("https://localhost:44373/api/list");
                //Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();

                //httpResponse = await httpClient.GetAsync(requestUri);
                //httpResponse.EnsureSuccessStatusCode();
                //var results = await httpResponse.Content.ReadAsStringAsync();

            }
            catch (Exception e)
            {
                CommonFeature.Feature.ShowMessage(e.Message);
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 서버목록 파일 읽기
        /// </summary>
        private async void ReadServerListAysnc()
        {
            string servDataString =  await FileManager.SingleFileManager.FileReadAsync(fileName, FileManager.ReadWriteWay.Buffer);

            ConnectionList = StringToConnectionInfoList(servDataString).ToObservableCollection();
        }

        /// <summary>
        /// 문자열 데이터를 ConnectionInfo 리스트로 반환
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private IList<ConnectionInfo> StringToConnectionInfoList(string strData)
        {
            List<string> lineData = strData?.SplitToList('\n');
            if(lineData == null || lineData.Count == 0)
            {
                return null;
            }

            IList<ConnectionInfo> infos = lineData.Where(w => w != "").Select(s => s.Split('#')).Select(s1 => new ConnectionInfo()
            {
                DataSource = s1[0],
                InitialCatalog = s1[1],
                UserID = s1[2],
                Title = s1[3]
            }).ToList();

            return infos;
        }

        /// <summary>
        /// GET 테스트
        /// </summary>
        private async void GetConnectionListAsync()
        {
            try
            {
                var response = await client.GetAsync("api/connection");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                //var connectList = await response.Content.ReadAsAsync<IEnumerable<ConnectionInfo>>();
                var connectList = await response.Content.ReadAsStringAsync();
                // 데이터 가져와서 정리해야하는데.. 오류남

            }
            catch (Exception e)
            {
                
            }
        }

        /// <summary>
        /// Web Api 와 통신할 HttpClient 초기화
        /// </summary>
        private void InitializeHttpClient()
        {
            if (client == null)
            {
                client = new HttpClient();
            }

            client.BaseAddress = new Uri("https://localhost:44373");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient = new Windows.Web.Http.HttpClient();

        }
        #endregion
    }
}
