using System.ComponentModel;

namespace DACLayer.Connection
{
    public class ConnectionInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProperty<T>(ref T prop, string propName, T value)
        {
            prop = value;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        /*

        private string _dataSource;
        private string _initialCatalog;
        private string _userID;
        private string _password;
        private string _title;
        /// <summary>
        /// DB 주소
        /// </summary>
        public string DataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource, nameof(DataSource), value);
        }
        /// <summary>
        /// DB 명
        /// </summary>
        public string InitialCatalog
        {
            get => _initialCatalog;
            set => SetProperty(ref _initialCatalog, nameof(InitialCatalog), value);
        }
        /// <summary>
        /// 로그인ID
        /// </summary>
        public string UserID
        {
            get => _userID;
            set => SetProperty(ref _userID, nameof(UserID), value);
        }
        /// <summary>
        /// 비밀번호
        /// </summary>
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, nameof(Password), value);
        }
        /// <summary>
        /// 타이틀 명
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, nameof(Title), value);
        }
        */
        private string _password;

        /// <summary>
        /// DB 주소
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// DB 명
        /// </summary>
        public string InitialCatalog { get; set; }
        /// <summary>
        /// 로그인ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 비밀번호
        /// </summary>
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, nameof(Password), value);
        }
        /// <summary>
        /// 타이틀 명
        /// </summary>
        public string Title { get; set; }

        public override string ToString()
        {
            return $"{DataSource}#{InitialCatalog}#{UserID}#{Title}";
        }
    }
}
