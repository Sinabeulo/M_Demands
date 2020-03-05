using BizCommon_Std.Models;
using Main_UWP.Request;
using MVVM;
using MVVM.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Main_UWP.ViewModel
{
    public class LanguageControlViewModel : ViewModelBase
    {
        private ObservableCollection<ComboBinding> _flagList;
        private LanguageControlModel _applyModel;
        private IList<ConnectionModel> _selectedList;
        private IList<ConnectionModel> _canConnectList;
        private ConnectionModel _selectedCanConnect;

        public ObservableCollection<ComboBinding> FlagList
        {
            get => _flagList;
            set => SetProperty(ref _flagList, nameof(FlagList), value);
        }

        public LanguageControlModel ApplyModel
        {
            get => _applyModel;
            set => SetProperty(ref _applyModel, nameof(ApplyModel), value);
        }
        public ConnectionModel SelectedCanConnect
        {
            get => _selectedCanConnect;
            set => SetProperty(ref _selectedCanConnect, nameof(SelectedCanConnect), value);
        }

        public IList<ConnectionModel> SelectedList
        {
            get => _selectedList;
            set => SetProperty(ref _selectedList, nameof(SelectedList), value);
        }
        public IList<ConnectionModel> CanConnectList
        {
            get => _canConnectList;
            set => SetProperty(ref _canConnectList,nameof(CanConnectList), value);
        }


        public ICommand ApplyCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        

        public LanguageControlViewModel()
        {
            FlagList = new ObservableCollection<ComboBinding>();
            CanConnectList = new ObservableCollection<ConnectionModel>();
            SelectedList = new ObservableCollection<ConnectionModel>();

            ApplyModel = new LanguageControlModel();

            FlagList.Add(new ComboBinding { ComboValue = "U", ComboDisplay = "Update" });
            FlagList.Add(new ComboBinding { ComboValue = "I", ComboDisplay = "Insert" });
            FlagList.Add(new ComboBinding { ComboValue = "DELETE", ComboDisplay = "DELETE" });

            ApplyCommand = new RelayCommand(ExecuteApplyCommand);
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
        }

        private void ExecuteApplyCommand()
        {
            ApplyModel.TargetTitle = SelectedCanConnect.Title;

            if (SelectedCanConnect.Password == null)
            {
                CommonFeature.Feature.ShowMessage("Password null");
                return;
            }

            var retData = RequestWebApi.Request.PostRequest("Language", ApplyModel);

            // 익명클래스로 된 JSON 데이터로 받아 컨버팅이 어려워 강제로 데이터 추출
            var resultmsg = retData.Split(":")[1]?.Split("\"")[1];

            CommonFeature.Feature.ShowMessage(resultmsg);
        }

        private void ExecuteSearchCommand()
        {
            try
            {
                string result = RequestWebApi.Request.GetRequest("Login");
                CanConnectList = JsonConvert.DeserializeObject<ObservableCollection<ConnectionModel>>(result);
            }
            catch(Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
            }
        }

        private void ExecuteAddCommand()
        {
            if (SelectedCanConnect == null) return;
            
            SelectedList.Add(SelectedCanConnect);
        }
    }
}