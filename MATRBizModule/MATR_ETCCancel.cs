using BizCommon_Std.FileIO;
using BizCommon_Std.Interface;
using BizCommon_Std.MATR;
using BizCommon_Std.Models;
using CommonDacModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MATRBizModule
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

        public object Execute(string category, string feature)
        {
            if (_model == null)
                return null;

            using (TransactionScope transaction = new TransactionScope())
            {
                //var check = CheckQty().Result;
                if (!CheckQty().Result) return null;


                transaction.Complete();
            }

            return null;
        }

        #region Logic
        /*
         * 1. 취소수량 체크
         * 2. 수정 쿼리
         */

        private async Task<bool> CheckQty()
        {
            string query = await FileManager.FileReadWriter.FileReaderToString("MATR_ETCCancel_CheckQty.txt");
            var result = SelectDac.SelectQuery.SendQuearyDapper(Target, query);

            if (result == null)
                return false;
            else
                return true;
        }


        #endregion
    }
}
