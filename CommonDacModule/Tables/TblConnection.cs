using BizCommon_Core.Enums;
using BizCommon_Core.Interface;

namespace CommonDacModule.Tables
{
    public class TblConnection : QueryResult
    {
        /// <summary>
        /// 연결결과 상태
        /// </summary>
        public ResultStatus resultStatus { get; set; }

    }
}
