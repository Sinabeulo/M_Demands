using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WpfWebApiClient
{
    /// <summary>
    ///  데이터 바인딩을 위한 Observable 클래스입니다.
    /// </summary>
    class TodoItemCollection : ObservableCollection<TodoItem>
    {
        public void CopyFrom(IEnumerable<TodoItem> todoItems)
        {
            this.Items.Clear();
            foreach (var p in todoItems)
            {
                this.Items.Add(p);
            }

            this.OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
