﻿using Main_UWP.View;
using MVVM;
using MVVM.Base;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Main_UWP.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public static bool CanUseFeatures = false;

        #region Field

        private UserControl _pageContent;
        private bool _canActiveFeature;

        #endregion Field

        public ICommand MoveTest1Page { get; set; }
        public ICommand DBConnectionCommand { get; set; }
        public ICommand FileWriteTestCommand { get; set; }

        public UserControl PageContent
        {
            get => _pageContent;
            set => SetProperty(ref _pageContent, nameof(PageContent), value);
        }

        public bool CanActiveFeature
        {
            get => _canActiveFeature;
            set => SetProperty(ref _canActiveFeature, nameof(CanActiveFeature), value);
        }

        public MainViewModel()
        {
            MoveTest1Page = new RelayCommand(ExecuteMoveTest1Page);
            DBConnectionCommand = new RelayCommand(ExecuteDBConnectionCommand);
            FileWriteTestCommand = new RelayCommand(ExecuteFileWriteTestCommand);
            PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CanUseFeatures):
                    CanActiveFeature = CanUseFeatures;
                    break;
            }
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