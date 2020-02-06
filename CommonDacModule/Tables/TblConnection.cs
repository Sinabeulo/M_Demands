using BizCommon_Std.Enums;
using BizCommon_Std.Interface;

namespace CommonDacModule.Tables
{
    public class TblConnection : IQueryResult
    {
        /// <summary>
        /// 연결결과 상태
        /// </summary>
        public ResultStatus resultStatus { get; set; }

    }
}
