using BizCommon_Std.Enums;
using BizCommon_Std.Interface;

namespace BizCommon_Std.Models
{
    public class TestDataModel : IQueryRequirement, IQueryResult
    {
        /// <summary>
        /// 결과
        /// </summary>
        public ResultStatus resultStatus { get; set; }
        /// <summary>
        /// 쿼리를 전송할 연결 타이틀
        /// </summary>
        public string TargetTitle { get; set; }
        /// <summary>
        /// 실행하고자하는 기능
        /// </summary>
        public string TargetFeature { get; set; }
        /// <summary>
        /// 전송할 데이터
        /// </summary>
        public object DataElement { get; set; }
    }
}
