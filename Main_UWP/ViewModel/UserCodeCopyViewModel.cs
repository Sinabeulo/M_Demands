using Main_UWP.Request;
using Main_UWP.View;
using MVVM;
using MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Main_UWP.ViewModel
{
    public class UserCodeCopyViewModel : ViewModelBase
    {
        private string _headerCode;
        private string _receivedQuery;
        private UserControl _connectionList_v;

        public string HeaderCode
        {
            get => _headerCode;
            set => SetProperty(ref _headerCode, nameof(HeaderCode), value);
        }
        public string ReceivedQuery
        {
            get => _receivedQuery;
            set => SetProperty(ref _receivedQuery, nameof(ReceivedQuery), value);
        }
        public UserControl ConnectionList_v
        {
            get => _connectionList_v;
            set => SetProperty(ref _connectionList_v, nameof(ConnectionList_v), value);
        }

        public ICommand SendQueryCommand { get; set; }

        public UserCodeCopyViewModel()
        {
            ConnectionList_v = new ConnectionListView();
            
            //ConnectionList_v.RegisterPropertyChangedCallback(ConnectionListView.TestPoperty, PropChanged);
            //TestP = 

            SendQueryCommand = new RelayCommand(ExecuteSendQueryCommand);
        }

        private void PropChanged(DependencyObject sender, DependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        private void ExecuteSendQueryCommand()
        {
            if(string.IsNullOrEmpty(HeaderCode))
            {
                CommonFeature.Feature.ShowMessage("HeadCode 입력 오류");
                return;
            }
            var received = RequestWebApi.Request.PostRequest("TestData", HeaderCode);
        }
    }
}
