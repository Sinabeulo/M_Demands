using Main_UWP.Model;
using Main_UWP.Request;
using MVVM;
using MVVM.Base;
using System.Windows.Input;

namespace Main_UWP.ViewModel
{
    public class LanguageControlViewModel : ViewModelBase
    {
        private System.Collections.ObjectModel.ObservableCollection<ComboBinding> _flagList;
        //private string _languageKey;
        //private string _languageValue;
        private LanguageControlModel _applyModel;
        public ICommand ApplyCommand { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<ComboBinding> FlagList
        {
            get => _flagList;
            set => SetProperty(ref _flagList, nameof(FlagList), value);
        }

        //public string LanguageKey
        //{
        //    get => _languageKey;
        //    set => SetProperty(ref _languageKey, nameof(LanguageKey), value);
        //}

        //public string LanguageValue
        //{
        //    get => _languageValue;
        //    set => SetProperty(ref _languageValue, nameof(LanguageValue), value);
        //}

        public LanguageControlModel ApplyModel
        {
            get => _applyModel;
            set => SetProperty(ref _applyModel, nameof(ApplyModel), value);
        }

        public LanguageControlViewModel()
        {
            FlagList = new System.Collections.ObjectModel.ObservableCollection<ComboBinding>();
            FlagList.Add(new ComboBinding { ComboValue = "U", ComboDisplay = "Update" });
            FlagList.Add(new ComboBinding { ComboValue = "I", ComboDisplay = "Insert" });
            FlagList.Add(new ComboBinding { ComboValue = "DELETE", ComboDisplay = "DELETE" });

            ApplyCommand = new RelayCommand(ExecuteApplyCommand);
        }

        private void ExecuteApplyCommand()
        {
            RequestWebApi.Request.PostRequest()
        }
    }
}