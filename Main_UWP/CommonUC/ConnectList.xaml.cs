using BizCommon_Std.Models;
using Main_UWP.Request;
using MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Main_UWP.CommonUC
{
    public sealed partial class ConnectList : UserControl
    {
        public ConnectList()
        {
            this.InitializeComponent();
        }

        private void SearchCommand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = RequestWebApi.Request.GetRequest("Login");
                lb_connect.ItemsSource = JsonConvert.DeserializeObject<ObservableCollection<ConnectionModel>>(result);
            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
            }

        }

        private void Lb_connect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ListBox list)) return;

            if (!(list.SelectedItem is ConnectionModel connection)) return;

            CommonProperties.Properties.SelectedConnection = connection;

            tb_SelectedTitle.Text = connection.Title;
        }
    }
}
