namespace BizCommon_Std.Interface
{
    /// <summary>
    /// 쿼리 필수요소
    /// </summary>
    public interface IQueryRequirement
    {
        /// <summary>
        /// 전송대상 타이틀명
        /// </summary>
        string TargetTitle { get; set; }
        /// <summary>
        /// 실행시키고자 하는 기능
        /// </summary>
        string TargetFeature { get; set; }
    }
}
