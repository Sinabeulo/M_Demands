using BizCommon_Std.Models;
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


        public UserControl PageContent
        {
            get => _pageContent;
            set => SetProperty(ref _pageContent, nameof(PageContent), value);
        }

        public ICommand SearchCommand { get; set; }
        public ICommand SetLanguageDataCommand { get; set; }
        public ICommand UserCodeCopyCommand { get; set; }
        public ICommand PO_BLCopyCommand { get; set; }


        public CommonFeatureControlVIewModel()
        {
            SetLanguageDataCommand = new RelayCommand(ExecuteSetLanguageDataCommand);
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            UserCodeCopyCommand = new RelayCommand(ExecuteUserCodeCopyCommand);
            PO_BLCopyCommand = new RelayCommand(ExecutePO_BLCopyCommand);
        }

        private void ExecuteSearchCommand()
        {
            string result = RequestWebApi.Request.GetRequest("List");
            var conList = JsonConvert.DeserializeObject<IList<ConnectionModel>>(result);
        }

        private void ExecuteSetLanguageDataCommand()
        {
            PageContent = new LanguageControlView();
        }

        private void ExecuteUserCodeCopyCommand()
        {
            PageContent = new TestDataMakerView();
        }

        private void ExecutePO_BLCopyCommand()
        {
            PageContent = new PO_BLMakerView();
        }
    }
}
