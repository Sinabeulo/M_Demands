namespace Main_Form.Models
{
    class ConnectionInfo
    {
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
        public string Password { get; set; }
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
