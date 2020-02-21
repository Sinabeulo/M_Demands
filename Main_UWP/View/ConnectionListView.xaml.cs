using BizCommon_Std.Models;
using Main_UWP.Request;
using MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Main_UWP.View
{
    public sealed partial class ConnectionListView : UserControl
    {
        //public static readonly DependencyProperty TestPoperty = 
        //    DependencyProperty.Register("TestProp", typeof(object), typeof(UserControl), new PropertyMetadata(null));
        //
        //public object TestP
        //{
        //    get { return (object)GetValue(TestPoperty); }
        //    set { SetValue(TestPoperty, value); }
        //}
        

        public ConnectionModel SelectedCanConnect { get; set; }
        public IList<ConnectionModel> CanConnectList { get; set; }

        public ConnectionListView()
        {
            this.InitializeComponent();

            CanConnectList = new ObservableCollection<ConnectionModel>();
            //lb_Connection.ItemsSource = CanConnectList;
            //lb_Connection.SelectedItem = SelectedCanConnect;
            
        }

        private void SearchCommand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = RequestWebApi.Request.GetRequest("List");
                CanConnectList = JsonConvert.DeserializeObject<ObservableCollection<ConnectionModel>>(result);
                lb_Connection.ItemsSource = CanConnectList;
            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
            }
        }

    }
}
