using BizCommon_Std.Enums;
using BizCommon_Std.Models;
using Main_UWP.Request;
using MVVM;
using MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BizCommon_Std.Extension;

namespace Main_UWP.ViewModel
{
    public class PO_BLMakerViewModel : ViewModelBase
    {
        private readonly string controller = "TestDataMaker";

        private int _selectedTab;
        private string _orderGroupKey;
        private string _bLNumber;
        private string _resultQuery;
        private TestDataMakerModel _applyModel;

        public int SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, nameof(SelectedTab), value);
        }
        public string OrderGroupKey
        {
            get => _orderGroupKey;
            set => SetProperty(ref _orderGroupKey, nameof(OrderGroupKey), value);
        }
        public string BLNumber
        {
            get => _bLNumber;
            set => SetProperty(ref _bLNumber, nameof(BLNumber), value);
        }
        public string ResultQuery
        {
            get => _resultQuery;
            set => SetProperty(ref _resultQuery, nameof(ResultQuery), value);
        }
        public TestDataMakerModel ApplyModel
        {
            get => _applyModel;
            set => SetProperty(ref _applyModel, nameof(ApplyModel), value);
        }

        public ICommand PO_CopyCommand { get; set; }
        public ICommand BL_CopyCommand { get; set; }

        public PO_BLMakerViewModel()
        {
            PO_CopyCommand = new RelayCommand(ExecutePO_CopyCommand);
            BL_CopyCommand = new RelayCommand(ExecuteBL_CopyCommand);

            ApplyModel = new TestDataMakerModel();
        }

        private void ExecuteBL_CopyCommand()
        {
            ApplyModel.TargetTitle = CommonProperties.Properties.SelectedConnection.Title;
            ApplyModel.TargetFeature = Features.PO_COPY;
            ApplyModel.DataElement = BLNumber;

            if(string.IsNullOrEmpty(ApplyModel.TargetTitle) || ApplyModel.DataElement == null)
            {
                CommonFeature.Feature.ShowMessage("Input Error");
                return;
            }

            string result = RequestWebApi.Request.PostRequest(controller, ApplyModel);

            SetResultQuery(result);
        }

        private void ExecutePO_CopyCommand()
        {
            ApplyModel.TargetTitle = CommonProperties.Properties.SelectedConnection.Title;
            ApplyModel.TargetFeature = Features.PO_COPY;
            ApplyModel.DataElement = OrderGroupKey;

            if (string.IsNullOrEmpty(ApplyModel.TargetTitle) || ApplyModel.DataElement == null)
            {
                CommonFeature.Feature.ShowMessage("Input Error");
                return;
            }

            string result = RequestWebApi.Request.PostRequest(controller, ApplyModel);

            SetResultQuery(result);
        }

        private void SetResultQuery(string result)
        {
            StringBuilder sbStr = new StringBuilder();
            List<string> strList = result.JsonToListString();

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
