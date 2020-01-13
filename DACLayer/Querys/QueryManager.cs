using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
//using System.Transactions;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using MVVM;
using BizLayer;

namespace DACLayer.Querys
{
    public class QueryManager
    {      
        private string connectionString;
        private bool connectionStringOK = false;    // 커낵션 문자열 입력 상태
        private bool connecionOK = false;           // 연결 가능여부
        public QueryManager()
        {

        }

        public QueryManager(string dataSource, string initialCatalog, string userID, string password)
        {
            if (string.IsNullOrEmpty(dataSource) || string.IsNullOrEmpty(initialCatalog)
                || string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                connectionStringOK = false;
                return;
            }

            connectionString = $@"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userID};Password={password}";
            connectionStringOK = true;
        }

        public async Task<bool> ConnectionTest()
        {
            try
            {
                if (connectionStringOK == false)
                {
                    CommonFeature.Feature.ShowMessageAsync("Server Info is not correct");
                    return false;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var results = connection.Query<TblConnection>("SELECT 'O' AS ConStatus").Select(obj => new TblConnection()
                    {
                        resultStatus = obj.resultStatus
                    }).FirstOrDefault();

                    if (results.resultStatus != ResultStatus.O)
                        return false;

                    connecionOK = true;
                    return true;
                }
            }
            catch(Exception e)
            {
                CommonFeature.Feature.ShowMessageAsync("Exeception ConnectionTest : " + e.Message);
                return false;
            }
        }

        public Tuple<bool, IEnumerable<dynamic>> EmptyQuery(string query)
        {
            if (!connecionOK || !connectionStringOK) return new Tuple<bool, IEnumerable<dynamic>>(false, null);

            IEnumerable<dynamic> results = null;
            try
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    //using (SqlConnection connection = new SqlConnection(connectionString))
                //    using (SqlConnection connection = new SqlConnection(connectionString))
                //    {
                //        connection.Open();
                //
                //        //results = connection.Query(query);                   
                //    }
                //
                //    // The Complete method commits the transaction. If an exception has been thrown,
                //    // Complete is not  called and the transaction is rolled back.
                //    scope.Complete();
                //}

                //using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    results = connection.Query(query);                   
                }
                return new Tuple<bool, IEnumerable<dynamic>>(false, results);
            }
            catch (Exception ex)
            {
                // 오류 메시지 리턴
                return new Tuple<bool, IEnumerable<dynamic>>(false, new List<dynamic>() { ex.Message });
            }
        }

    }
}
