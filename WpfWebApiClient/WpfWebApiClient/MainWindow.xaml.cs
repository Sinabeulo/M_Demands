using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfWebApiClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        TodoItemCollection _todoItems = new TodoItemCollection();

        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:44345");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            this.TodoList.ItemsSource = _todoItems;
        }

        #region GET
        /// <summary>
        /// GET 비동기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetTodoListAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGetTodoList.IsEnabled = false;

                var response = await client.GetAsync("api/todoitems");
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                var todoItems = await response.Content.ReadAsAsync<IEnumerable<TodoItem>>();
                _todoItems.CopyFrom(todoItems);
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 이 예외는 요청 본문을 역직렬화 할 때, 문제가 발생했음을 나타냅니다.
                MessageBox.Show(jEx.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnGetTodoList.IsEnabled = true;
            }
        }

        /// <summary>
        /// GET 동기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetTodoList(object sender, RoutedEventArgs e)
        {
            btnGetTodoList.IsEnabled = false;

            client.GetAsync("api/todoitems").ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    MessageBox.Show(t.Exception.Message);
                    btnGetTodoList.IsEnabled = true;
                }
                else
                {
                    var response = t.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        response.Content.ReadAsAsync<IEnumerable<TodoItem>>().
                            ContinueWith(t2 =>
                            {
                                if (t2.IsFaulted)
                                {
                                    MessageBox.Show(t2.Exception.Message);
                                    btnGetTodoList.IsEnabled = true;
                                }
                                else
                                {
                                    var todoItems = t2.Result;
                                    _todoItems.CopyFrom(todoItems);
                                    btnGetTodoList.IsEnabled = true;
                                }
                            }, TaskScheduler.FromCurrentSynchronizationContext());
                    }

                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        #endregion


        #region POST
        /// <summary>
        /// POST 비동기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PostTodoListAsync(object sender, RoutedEventArgs e)
        {
            btnPostTodoList.IsEnabled = false;

            try
            {
                var todoItem = new TodoItem()
                {
                    Name = textName.Text
                };
                var response = await client.PostAsJsonAsync("api/todoitems", todoItem);
                response.EnsureSuccessStatusCode(); // 오류 코드를 던집니다.

                _todoItems.Add(todoItem);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //catch (System.FormatException)
            //{
            //    MessageBox.Show("Price must be a number");
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnPostTodoList.IsEnabled = true;
            }
        }
        #endregion
    }
}
