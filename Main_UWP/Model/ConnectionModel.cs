using MVVM.Base;
using MVVM.ItemType;
using Newtonsoft.Json;

namespace Main_UWP.Model
{
    public class ConnectionModel : ModelBase
    {
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

        [JsonIgnore]
        public EditType EditType { get; set; }

        public override string ToString()
        {
            return $"{DataSource}#{InitialCatalog}#{UserID}#{Title}";
        }
    }
}
