using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;
using BizCommon_Core.Models;

namespace WebApiService.Mssql
{
    public class DBManager
    {
        public DBManager()
        {

        }

        public bool DbConnection(ConnectionModel conItem)
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

    public class TblConnection
    {
        /// <summary>
        /// 연결결과 상태
        /// </summary>
        public ResultStatus resultStatus { get; set; }
    }

    public interface QueryResult
    {
        ResultStatus resultStatus { get; set; }
    }

    public enum ResultStatus
    {
        X = 0,
        O = 1
    }
}
