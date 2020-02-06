using BizCommon_Std.Interface;
using BizCommon_Std.MATR;
using BizCommon_Std.Models;
using CommonDacModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CommonBizModule.MATRBizModule
{
    /// <summary>
    /// 기타부서 출고 취소
    /// </summary>
    public class MATR_ETCCancel : IFeatureExecute
    {
        private MATR_ETCCancelModel _model;
        public ConnectionModel Target { get; set; }
        public MATR_ETCCancel(MATR_ETCCancelModel model)
        {
            _model = model;
        }

        public object Execute()
        {
            if (_model == null)
                return null;

            using (TransactionScope transaction = new TransactionScope())
            {
                //if (!CheckQty()) return null;


                transaction.Complete();
            }

            return null;
        }

        #region Logic
        /*
         * 1. 취소수량 체크
         * 2. 수정 쿼리
         */

        //private bool CheckQty()
        //{
        //    string query = ""
        //    SelectDac.SelectQuery.SendQuearyDapper(Target, )
        //}


        #endregion
    }
}
