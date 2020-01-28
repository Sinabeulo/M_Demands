using Main_UWP.Model;
using MVVM;
using MVVM.Base;
using MVVM.ItemType;
using Newtonsoft.Json;
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
        private IList<ConnectionModel> _connectionList;
        private ConnectionModel _selectedConnection;
        private ConnectionModel _newConnectionModel;
        private string _folderPath;
        private bool _canSave;
        private bool _isFromFile;
        private HttpClient client;
        private Windows.Web.Http.HttpClient httpClient;


        private readonly string fileName = "ServList.txt";
        #endregion

        #region Properties
        /// <summary>
        /// 서버 리스트
        /// </summary>
        public IList<ConnectionModel> ConnectionList
        {
            get => _connectionList;
            set => SetProperty(ref _connectionList, nameof(ConnectionList), value);
        }
        /// <summary>
        /// 선택된 서버
        /// </summary>
        public ConnectionModel SelectedConnection
        {
            get => _selectedConnection;
            set => SetProperty(ref _selectedConnection, nameof(SelectedConnection), value);
        }
        /// <summary>
        /// 새로 입력된 서버
        /// </summary>
        public ConnectionModel NewConnectionModel
        {
            get => _newConnectionModel;
            set => SetProperty(ref _newConnectionModel, nameof(NewConnectionModel), value);
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
        /// <summary>
        /// 파일에서 연결 목록 데이터 가져오는지 여부
        /// </summary>
        public bool IsFromFile
        {
            get => _isFromFile;
            set => SetProperty(ref _isFromFile, nameof(IsFromFile), value);
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

            ConnectionList = new System.Collections.ObjectModel.ObservableCollection<ConnectionModel>();

            AddCommand = new RelayCommand(ExecuteAddCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            ConnectCommand = new RelayCommand(ExecuteConnectCommand);
            ApiGetCommand = new RelayCommand(GetConnectionListAsync);
            GetConnectionListCommand = new RelayCommand(ExecuteGetConnectionListCommand);

            InitializeHttpClient();

            NewConnectionModel = new ConnectionModel();

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
            //QueryManager queryManager = new QueryManager(SelectedConnection.DataSource,SelectedConnection.InitialCatalog,SelectedConnection.UserID,SelectedConnection.Password);
            //bool result = await queryManager.ConnectionTest();
            //if(result == false)
            //{
            //    CommonFeature.Feature.ShowMessageAsync("Connection Failed");
            //}
            //
            //SelectedConnection.Password = string.Empty;
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
            if(NewConnectionModel == null)
            {
                CommonFeature.Feature.ShowMessage("저장 할 항목 없음");
                return;
            }
            try
            {
                if (IsFromFile)
                {
                    SaveNewConnectionToFile();
                }
                else
                {
                    //List<ConnectionModel> saveList = ConnectionList.Where(w => w.EditType == EditType.New).ToList();
                    ConnectionList.Where(w => w.EditType == EditType.New).ToList().ForEach(s => SaveNewConnectionToServer(s));

                    //SaveNewConnectionToServer();
                }
            }
            catch(Exception ex)
            {
                CommonFeature.Feature.ShowMessageAsync(ex.Message);
            }
        }

        /// <summary>
        /// 새로운 연결 입력
        /// </summary>
        private void ExecuteAddCommand()
        {
            if(string.IsNullOrEmpty(NewConnectionModel.DataSource) || string.IsNullOrEmpty(NewConnectionModel.InitialCatalog) || 
                string.IsNullOrEmpty(NewConnectionModel.UserID) || string.IsNullOrEmpty(NewConnectionModel.Title))
            {
                CommonFeature.Feature.ShowMessage("전부 입력되지 않았습니다.");
                return;
            }

            NewConnectionModel.EditType = EditType.New;
            ConnectionList.Add(NewConnectionModel);
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

            ConnectionList = StringToConnectionModelList(servDataString).ToObservableCollection();
        }

        /// <summary>
        /// 문자열 데이터를 ConnectionModel 리스트로 반환
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private IList<ConnectionModel> StringToConnectionModelList(string strData)
        {
            List<string> lineData = strData?.SplitToList('\n');
            if(lineData == null || lineData.Count == 0)
            {
                return null;
            }

            IList<ConnectionModel> infos = lineData.Where(w => w != "").Select(s => s.Split('#')).Select(s1 => new ConnectionModel()
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

                //var connectList = await response.Content.ReadAsAsync<IEnumerable<ConnectionModel>>();
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

        private async void SaveNewConnectionToFile()
        {
            //if (NewConnectionModel != null)
            //{
            //    ConnectionList.Add(NewConnectionModel);
            //    NewConnectionModel = null;
            //}

            List<string> saveList = ConnectionList.Select(s => s.ToString()).ToList();

            await FileManager.SingleFileManager.FileWriteAsync(fileName, saveList, FileManager.ReadWriteWay.Buffer);

            ReadServerListAysnc();
            CanSave = false;  
        }

        private async void SaveNewConnectionToServer(ConnectionModel newConnection)
        {
            //List<ConnectionModel> saveList = ConnectionList.Where(w => w.EditType == EditType.New).ToList();

            //foreach(ConnectionModel info in saveList)
            //{
            //    // 데이터 Json Serialize
            //    var convertedData = JsonConvert.SerializeObject(info);
            //    StringContent content = new StringContent(convertedData);
            //
            //    // 요청
            //    var response = await client.PostAsync("api/list", content);
            //    response.EnsureSuccessStatusCode();
            //
            //
            //    // 반환
            //    var result = await response.Content.ReadAsStringAsync();
            //    var tmp = JsonConvert.DeserializeObject<ConnectionModel>(result);
            //}

            var convertedData = JsonConvert.SerializeObject(newConnection);
            StringContent content = new StringContent(convertedData, System.Text.Encoding.UTF8,"application/json");

            // 요청
            var response = await client.PostAsync("api/List", content);
            response.EnsureSuccessStatusCode();


            // 반환
            var result = await response.Content.ReadAsStringAsync();
            var tmp = JsonConvert.DeserializeObject<ConnectionModel>(result);

            //ConnectionList = JsonConvert.DeserializeObject<ConnectionModel>(result);
            //string convertedData = JsonConvert.SerializeObject(newConnection);
            //StringContent content = new StringContent(convertedData);

            //System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://localhost:44373/list");
            //request.Method = "POST";
            //request.ContentType = "application/json";
            //request.Timeout = 30 * 1000;

            //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(convertedData);
            //request.ContentLength = bytes.Length;

            //using (System.IO.Stream reqStream = request.GetRequestStream())
            //{
            //    reqStream.Write(bytes, 0, bytes.Length);
            //}

            //string responseText = string.Empty;
            //using(System.Net.WebResponse resp = request.GetResponse())
            //{
            //    System.IO.Stream respStream = resp.GetResponseStream();
            //    using(System.IO.StreamReader sr = new System.IO.StreamReader(respStream))
            //    {
            //        responseText = sr.ReadToEnd();
            //    }
            //}
        }
        #endregion
    }
}
