using Main_UWP.View;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Main_UWP.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Field
        
        private UserControl _pageContent;
        #endregion

        public ICommand MoveTest1Page { get; set; }
        public ICommand DBConnectionCommand { get; set; }
        public ICommand FileWriteTestCommand { get; set; }
        public UserControl PageContent
        {
            get => _pageContent;
            set => SetProperty(ref _pageContent ,nameof(PageContent), value);
        }

        public MainViewModel()
        {
            MoveTest1Page = new RelayCommand(ExecuteMoveTest1Page);
            DBConnectionCommand = new RelayCommand(ExecuteDBConnectionCommand);
            FileWriteTestCommand = new RelayCommand(ExecuteFileWriteTestCommand);
        }

        private void ExecuteFileWriteTestCommand()
        {
            PageContent = new FileIOTestView();
        }

        private void ExecuteMoveTest1Page()
        {
            PageContent = new Test1_View();
        }

        private void ExecuteDBConnectionCommand()
        {
            PageContent = new DBManagerView();
            //PageContent = new DB_ConnectionView();
            //PageContent = new ConnectionManagerView();
        }
    }
}
