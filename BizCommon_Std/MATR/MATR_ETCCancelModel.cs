using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon_Std.MATR
{
    public class MATR_ETCCancelModel
    {
        #region 전송할 파라미터
        /// <summary>
        /// [입력] LotNo Param
        /// </summary>
        public string pmLotNo { get; set; }

        /// <summary>
        /// [입력] LotQty Param
        /// </summary>
        public decimal pmLotQty { get; set; }

        /// <summary>
        /// [입력] RevisionMemo Param
        /// </summary>
        public string pmRevisionMemo { get; set; }
        #endregion
    }
}
