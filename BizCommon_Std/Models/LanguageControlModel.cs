using BizCommon_Std.Enums;
using BizCommon_Std.Interface;

namespace BizCommon_Std.Models
{
    public class LanguageControlModel : IQueryRequirement , IQueryResult
    {  
        /// <summary>
        /// 언어키
        /// </summary>
        public string LanguageKey { get; set; }
        /// <summary>
        /// 언어값
        /// </summary>
        public string LanguageValue { get; set; }
        /// <summary>
        /// Insert/Update/Delete
        /// </summary>
        public string Flag { get; set; }
        /// <summary>
        /// 결과
        /// </summary>
        public ResultStatus resultStatus { get ; set ; }
        /// <summary>
        /// 쿼리를 전송할 연결 타이틀
        /// </summary>
        public string TargetTitle { get; set; }
        /// <summary>
        /// 실행하고자하는 기능
        /// </summary>
        public Features TargetFeature { get; set; }
    }
}
