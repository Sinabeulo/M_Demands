using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACLayer_DF.Connection
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
    }
}
