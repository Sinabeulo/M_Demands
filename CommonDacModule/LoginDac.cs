using BizCommon_Std.Enums;
using BizCommon_Std.Models;
using CommonDacModule.Tables;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace CommonDacModule
{
    public class LoginDac
    {
        private static LoginDac _instance;
        public static LoginDac LoginQuery
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoginDac();
                }

                return _instance;
            }
        }

        public bool LoginToDatabase(ConnectionModel conItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conItem.ToString()))
                {
                    connection.Open();

                    var results = connection.Query<TblConnection>("SELECT 'O' AS resultStatus").Select(obj => new TblConnection()
                    {
                        resultStatus = obj.resultStatus
                    }).FirstOrDefault();

                    if (results.resultStatus != ResultStatus.O)
                        return false;

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
