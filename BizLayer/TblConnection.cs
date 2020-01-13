using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer
{
    public class TblConnection : QueryResult
    {
        /// <summary>
        /// 연결결과 상태
        /// </summary>
        public ResultStatus resultStatus { get; set; }
    }
}
