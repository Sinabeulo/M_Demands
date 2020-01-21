using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Main_Form.Models;

namespace Main_Form
{
    public partial class DataBaseConnection : UserControl
    {
        private HttpClient client = new HttpClient();
        //private IList<ConnectionInfo> _connectList = new List<ConnectionInfo>();
        private ConnectionInfo _selectedConInfo;

        public DataBaseConnection()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:44373");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            SetEnableBtn(false);

            
            //this.TodoList.ItemsSource = _todoItems;
        }       

        private async void btn_Connect_Click(object sender, EventArgs e)
        {
            btn_Connect.Enabled = false;

            try
            {
                var conItem = new ConnectionInfo()
                {
                    DataSource = tb_DataSource.Text,
                    InitialCatalog = tb_InitialCatalog.Text,
                    UserID = tb_UserId.Text,
                    Password = tb_Password.Text,
                    Title = tb_Title.Text
                };
                // 연결 요청
                var response = await client.PostAsJsonAsync("api/Connections", conItem);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                //listB_ConnectionList.Items.Add(conItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btn_Connect.Enabled = true;
            }
        }

        private async void btn_Add_Click(object sender, EventArgs e)
        {
            btn_Add.Enabled = false;

            try
            {
                var conItem = new ConnectionInfo()
                {
                    DataSource = tb_DataSource.Text,
                    InitialCatalog = tb_InitialCatalog.Text,
                    UserID = tb_UserId.Text,
                    Password = tb_Password.Text,
                    Title = tb_Title.Text
                };
                var response = await client.PostAsJsonAsync("api/List", conItem);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                listB_ConnectionList.Items.Add(conItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btn_Add.Enabled = true;
            }
        }

        private async void btn_GetList_Click(object sender, EventArgs e)
        {
            try
            {
                btn_GetList.Enabled = false;

                var response = await client.GetAsync("api/List");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var todoItems = await response.Content.ReadAsAsync<IEnumerable<ConnectionInfo>>();

                //_connectList.Clear();
                listB_ConnectionList.Items.Clear();
                foreach (var item in todoItems)
                {
                    //_connectList.Add(item);
                    listB_ConnectionList.Items.Add(item);
                }

                SetEnableBtn(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btn_GetList.Enabled = true;
            }
        }

        private void SetEnableBtn(bool canUse)
        {
            btn_Add.Enabled = btn_Connect.Enabled = btn_Delete.Enabled= canUse;
        }

        private void listB_ConnectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is ListBox listBox)) return;
                _selectedConInfo = listBox.SelectedItem as ConnectionInfo;
                string[] items = _selectedConInfo.ToString().Split('#');

                tb_DataSource.Text = items[0];
                tb_InitialCatalog.Text = items[1];
                tb_UserId.Text = items[2];
                tb_Title.Text = items[3];
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(">> IndexChanged : " + ex.Message);
            }
        }

        private async void btn_Delete_Click(object sender, EventArgs e)
        {
            btn_Delete.Enabled = false;

            try
            {
                if (listB_ConnectionList.Items.Count == 0)
                {
                    throw new Exception("항목이 없습니다.");
                }

                var conItem = new ConnectionInfo()
                {
                    DataSource = tb_DataSource.Text,
                    InitialCatalog = tb_InitialCatalog.Text,
                    UserID = tb_UserId.Text,
                    Password = tb_Password.Text,
                    Title = tb_Title.Text
                };
                var response = await client.PostAsJsonAsync("api/List", conItem);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                listB_ConnectionList.Items.Add(conItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btn_Delete.Enabled = true;
            }
        }
    }
}
