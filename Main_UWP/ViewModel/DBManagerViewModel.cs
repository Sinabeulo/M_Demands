using Main_UWP.Model;
using MVVM;
using MVVM.Base;
using MVVM.ItemType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace Main_UWP.ViewModel
{
    public class DBManagerViewModel : ViewModelBase
    {
        #region Field

        private IList<ConnectionModel> _connectionList;
        private IList<ConnectionModel> _readyToConnectedList;
        private ConnectionModel _selectedConnection;
        private ConnectionModel _newConnectionModel;
        private string _folderPath;
        private bool _canSave;
        private bool _isFromFile;
        private HttpClient client;
        private Windows.Web.Http.HttpClient httpClient;
        private MainViewModel _mainViewModel;

        private readonly string fileName = "ServList.txt";

        #endregion Field

        #region Properties

        /// <summary>
        /// 연결 목록
        /// </summary>
        public IList<ConnectionModel> ConnectionList
        {
            get => _connectionList;
            set => SetProperty(ref _connectionList, nameof(ConnectionList), value);
        }

        /// <summary>
        /// 연결가능한 목록
        /// </summary>
        public IList<ConnectionModel> ReadyToConnectList
        {
            get => _readyToConnectedList;
            set => SetProperty(ref _readyToConnectedList, nameof(ReadyToConnectList), value);
        }

        /// <summary>
        /// 선택된 연결
        /// </summary>
        public ConnectionModel SelectedConnection
        {
            get => _selectedConnection;
            set => SetProperty(ref _selectedConnection, nameof(SelectedConnection), value);
        }

        /// <summary>
        /// 새로 입력된 연결
        /// </summary>
        public ConnectionModel NewConnectionModel
        {
            get => _newConnectionModel;
            set => SetProperty(ref _newConnectionModel, nameof(NewConnectionModel), value);
        }

        /// <summary>
        /// 연결 목록 파일의 폴더 경로
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


        #endregion Properties

        #region Command

        /// <summary>
        /// 연결 커맨드
        /// </summary>
        public ICommand ConnectCommand { get; set; }

        /// <summary>
        /// [GET] 접속리스트 조회
        /// </summary>
        public ICommand GetConnectionListCommand { get; set; }

        /// <summary>
        /// 연결가능 목록 선택 변경 이벤트 커맨드
        /// </summary>
        public ICommand LoadedCommand { get; set; }


        #endregion Command

        public DBManagerViewModel()
        {
            CanSave = false;

            ConnectionList = new System.Collections.ObjectModel.ObservableCollection<ConnectionModel>();
            ReadyToConnectList = new System.Collections.ObjectModel.ObservableCollection<ConnectionModel>();

            AddCommand = new RelayCommand(ExecuteAddCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            ConnectCommand = new RelayCommand(ExecuteConnectCommand);
            GetConnectionListCommand = new RelayCommand(ExecuteGetConnectionListCommand);
            LoadedCommand = new RelayCommand(ExecuteLoadedCommand);

            //InitializeHttpClient();

            NewConnectionModel = new ConnectionModel();
        }

        #region Execute

        private async void ExecuteConnectCommand()
        {
            if (SelectedConnection == null)
            {
                CommonFeature.Feature.ShowMessage("선택된 항목이 없음");
                return;
            }
            if (SelectedConnection.Password == null || SelectedConnection.Password == string.Empty)
            {
                CommonFeature.Feature.ShowMessage("비밀번호 미입력");
                return;
            }

            HttpWebRequest request = null;
            string result = string.Empty;

            try
            {
                Uri uri = new Uri("https://localhost:44373/api/Login");
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Post;
                request.Timeout = 5000;

                // 인코딩 UTF-8
                byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(SelectedConnection));
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                // 데이터 전송
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(data, 0, data.Length);

                    // 전송 응답
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                            {
                                result = streamReader.ReadToEnd();
                                ReadyToConnectList.Add(JsonConvert.DeserializeObject<ConnectionModel>(result));
                                // 다른 기능 사용 가능하게 하기
                                _mainViewModel.CanActiveFeature = ReadyToConnectList.Any();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
            }
            finally
            {
                SelectedConnection.Password = string.Empty;
            }
        }

        /// <summary>
        /// 선택된 아이템 삭제 후 저장
        /// </summary>
        private void ExecuteDeleteCommand()
        {
            if (SelectedConnection == null)
            {
                //선택된 아이템 없음.
                CommonFeature.Feature.ShowMessageAsync("There is no selected item.");
                return;
            }

            ConnectionList.Remove(SelectedConnection);
            SelectedConnection = null;
        }

        /// <summary>
        /// 서버 목록 저장
        /// </summary>
        private async void ExecuteSaveCommand()
        {
            if (NewConnectionModel == null)
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
                    ConnectionList.Where(w => w.EditType == EditType.New)
                                  .ToList().ForEach(s => SaveNewConnectionToServerHttpWebRequest(s));
                }
            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessageAsync(ex.Message);
            }
        }

        /// <summary>
        /// 새로운 연결 입력
        /// </summary>
        private void ExecuteAddCommand()
        {
            if (string.IsNullOrEmpty(NewConnectionModel.DataSource) || string.IsNullOrEmpty(NewConnectionModel.InitialCatalog) ||
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
            if (IsFromFile)
            {
                await FileManager.SingleFileManager.OpenFolderPicker();

                ReadServerListAysnc();
            }
            else
            {
                GetConnectionsFromServer();
            }
        }

        #endregion Execute

        #region Method

        /// <summary>
        /// 서버목록 파일 읽기
        /// </summary>
        private async void ReadServerListAysnc()
        {
            string servDataString = await FileManager.SingleFileManager.FileReadAsync(fileName, FileManager.ReadWriteWay.Buffer);

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
            if (lineData == null || lineData.Count == 0)
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
        /// Web Api 와 통신할 HttpClient 초기화
        /// </summary>
        private void InitializeHttpClient()
        {
            // HttpClient POST 전송을 해결하지 못해 사용하지 않음.
            // HttpWebRequest를 사용하여 처리
            if (client == null)
            {
                client = new HttpClient();
            }

            client.BaseAddress = new Uri("https://localhost:44373");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// 파일로 연결 목록 저장
        /// </summary>
        private async void SaveNewConnectionToFile()
        {
            List<string> saveList = ConnectionList.Select(s => s.ToString()).ToList();

            await FileManager.SingleFileManager.FileWriteAsync(fileName, saveList, FileManager.ReadWriteWay.Buffer);

            ReadServerListAysnc();
            CanSave = false;
        }

        /// <summary>
        /// 서버에서 연결목록 가져오기
        /// </summary>
        private async void GetConnectionsFromServer()
        {
            HttpWebRequest request = null;
            string result = string.Empty;
            try
            {
                Uri uri = new Uri("https://localhost:44373/api/List");
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Get;
                request.Timeout = 5000;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                        {
                            result = streamReader.ReadToEnd();
                            ConnectionList = JsonConvert.DeserializeObject<List<ConnectionModel>>(result).ToObservableCollection();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// HttpWebRequest를 사용하여 api/list 에 Post
        /// </summary>
        /// <param name="newConnection"></param>
        private void SaveNewConnectionToServerHttpWebRequest(ConnectionModel newConnection)
        {
            HttpWebRequest request = null;
            string result = string.Empty;
            ConnectionModel savedData = null;
            try
            {
                Uri uri = new Uri("https://localhost:44373/api/List");
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Post;
                request.Timeout = 5000;

                // 인코딩 UTF-8
                byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newConnection));
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                // 데이터 전송
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(data, 0, data.Length);

                    // 전송 응답
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                            {
                                result = streamReader.ReadToEnd();
                                savedData = JsonConvert.DeserializeObject<ConnectionModel>(result);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// MainView 상태 변경을 위해 로드 시 MainViewModel 객체 가져옴
        /// </summary>
        /// <param name="obj"></param>
        public void ExecuteLoadedCommand(object obj)
        {
            _mainViewModel =
            ((Main_UWP.ViewModel.MainViewModel)((Windows.UI.Xaml.FrameworkElement)((Windows.UI.Xaml.FrameworkElement)((Windows.UI.Xaml.FrameworkElement)((Windows.UI.Xaml.FrameworkElement)((Windows.UI.Xaml.FrameworkElement)obj).Parent).Parent).Parent).Parent).DataContext);
        }

        #endregion Method
    }
}