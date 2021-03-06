﻿using BizCommon_Core.Enums;
using BizCommon_Core.Interface;

namespace BizCommon_Core.Models
{
    public class LanguageControlModel : QueryRequirement , QueryResult
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
    }
}
