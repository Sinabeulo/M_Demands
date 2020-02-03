using Main_UWP.Model;
using Main_UWP.Request;
using Main_UWP.View;
using MVVM;
using MVVM.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Main_UWP.ViewModel
{
    public class CommonFeatureControlVIewModel : ViewModelBase
    {
        private UserControl _pageContent;
        private IList<ConnectionModel> _canConnectList;
        private IList<ConnectionModel> _selectedList;
        public IList<ConnectionModel> CanConnectList
        {
            get => _canConnectList;
            set => SetProperty(ref _canConnectList, nameof(CanConnectList), value);
        }
        public IList<ConnectionModel> SelectedList
        {
            get => _selectedList;
            set => SetProperty(ref _selectedList, nameof(SelectedList), value);
        }

        public UserControl PageContent
        {
            get => _pageContent;
            set => SetProperty(ref _pageContent, nameof(PageContent), value);
        }
        public ConnectionModel SelectedCanConnect { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand SetLanguageDataCommand { get; set; }


        public CommonFeatureControlVIewModel()
        {
            SetLanguageDataCommand = new RelayCommand(ExecuteSetLanguageDataCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand);
            SearchCommand = new RelayCommand(ExecuteSearchCommand);

            SelectedList = new System.Collections.ObjectModel.ObservableCollection<ConnectionModel>();
        }

        private void ExecuteSearchCommand()
        {
            string result = RequestWebApi.Request.GetRequest("List");
            var conList = JsonConvert.DeserializeObject<IList<ConnectionModel>>(result);

            CanConnectList = conList.ToObservableCollection();
        }

        private void ExecuteAddCommand()
        {
            SelectedList.Add(SelectedCanConnect);
            //SelectedCanConnect;
        }

        private void ExecuteSetLanguageDataCommand()
        {
            //PageContent = new LanguageControlView(this);
        }
    }
}
