﻿namespace BizCommon_Core.Models
{
    public class ConnectionModel
    {
        public int Id { get; set; }
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
        /// 제목
        /// </summary>
        public string Title { get; set; }

        public override string ToString()
        {
            return $@"Data Source={DataSource};Initial Catalog={InitialCatalog}; User Id={UserID}; Password={Password}";
        }

        //public string ToJsonString()
        //{
        //    return "[{\"DataSource\":\"" + DataSource + "\",\"InitialCatalog\":\"" + InitialCatalog + "\",\"UserID\":\"" + UserID + "\",\"Password\":\"" + Password + "\",\"Title\":\"" + Title + "\"}]";
        //}
    }
}
