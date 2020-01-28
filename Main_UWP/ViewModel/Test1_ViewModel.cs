using MVVM;
using MVVM.Base;
using System.Windows.Input;

namespace Main_UWP.ViewModel
{
    public class Test1_ViewModel : ViewModelBase
    {
        public ICommand ShowHelloPopup { get; set; }

        public Test1_ViewModel()
        {
            ShowHelloPopup = new RelayCommand(ExecuteShowHelloPopup);
        }

        private void ExecuteShowHelloPopup()
        {
            Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog("Hello");
            messageDialog.ShowAsync();
        }
    }
}
