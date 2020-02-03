using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CommonDacModule
{
    public class InsertDac
    {
        private static InsertDac _instance;
        public static InsertDac InsertQuery
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InsertDac();
                }

                return _instance;
            }
        }

        public bool SendQueary()
        {
            try
            {
                //using (SqlConnection connection = new SqlConnection(conItem.ToString()))
                //{
                //    connection.Open();
                //
                //    var results = connection.Query<TblConnection>("SELECT 'O' AS resultStatus").Select(obj => new TblConnection()
                //    {
                //        resultStatus = obj.resultStatus
                //    }).FirstOrDefault();
                //
                //    if (results.resultStatus != ResultStatus.O)
                //        return false;
                //
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
