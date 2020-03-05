using BizCommon_Std.Enums;
using BizCommon_Std.Models;
using MVVM;
using MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using BizCommon_Std.Extension;

namespace Main_UWP.ViewModel
{
    public class TestDataMakerViewModel : ViewModelBase
    {
        private string _resultQuery;
        private string _headerCode;
        private UserControl _content_V;
        public TestDataMakerModel _applyModel;

        public string ResultQuery
        {
            get => _resultQuery;
            set => SetProperty(ref _resultQuery, nameof(ResultQuery), value);
        }
        public string HeaderCode
        {
            get => _headerCode;
            set => SetProperty(ref _headerCode, nameof(HeaderCode), value);
        }
        public UserControl Content_V
        {
            get => _content_V;
            set => SetProperty(ref _content_V, nameof(Content_V), value);
        }

        public TestDataMakerModel ApplyModel
        {
            get => _applyModel;
            set => SetProperty(ref _applyModel, nameof(ApplyModel), value);
        }


        public ICommand UserCodeMakerCommand { get; set; }
        

        public TestDataMakerViewModel()
        {
            ApplyModel = new TestDataMakerModel();

            UserCodeMakerCommand = new RelayCommand(ExecuteUserCodeMakerCommand);


        }

        private void ExecuteUserCodeMakerCommand()
        {
            if (string.IsNullOrEmpty(HeaderCode))
            {
                CommonFeature.Feature.ShowMessage("HeaderCode Empty");
                return;
            }

            var connection = CommonProperties.Properties.SelectedConnection;
            ApplyModel.TargetTitle = connection.Title;
            ApplyModel.DataElement = HeaderCode;
            ApplyModel.TargetFeature = Features.USER_CODE;
            var retData = Request.RequestWebApi.Request.PostRequest("TestDataMaker", ApplyModel);

            StringBuilder sbStr = new StringBuilder();
            List<string> strList = retData.JsonToListString();

            if (strList == null || strList.Count == 0)
                return;

            foreach (string str in strList)
            {
                sbStr.Append(str + "\r\n");
            }

            ResultQuery = sbStr.ToString();
        }
    }
}
