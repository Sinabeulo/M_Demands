//using BizLayer_DF;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DACLayer_DF.Querys
{
    public class QueryManager
    {
        //private string dataSource;
        //private string initialCatalog;
        //private string userID;
        //private string password;

        private string connectionString;
        private bool connectionStringOK = false;
        public QueryManager()
        {

        }

        public QueryManager(string dataSource, string initialCatalog, string userID, string password)
        {
            //this.dataSource = dataSource;
            //this.initialCatalog = initialCatalog;
            //this.userID = userID;
            //this.password = password;
            if (string.IsNullOrEmpty(dataSource) || string.IsNullOrEmpty(initialCatalog) 
                || string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                connectionStringOK = false;
                return;
            }

            connectionString = $@"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userID};Password={password};";
            connectionStringOK = true;
        }

        public Tuple<bool, IEnumerable<dynamic>> EmptyQuery(string query)
        {
            if (!connectionStringOK) return new Tuple<bool, IEnumerable<dynamic>>(false, null);

            IEnumerable<dynamic> results = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        results = connection.Query(query);
                        //var ret = connection.Query(query).Select(obj => new TestClass
                        //{
                        //    Text = (string)obj.Text
                        //}).ToList();

                        //results = ret.Select(s => s.Text).ToList(); 

                    }

                    // The Complete method commits the transaction. If an exception has been thrown,
                    // Complete is not  called and the transaction is rolled back.
                    scope.Complete();
                }
                return new Tuple<bool, IEnumerable<dynamic>>(false, results);
            }
            catch(TransactionAbortedException ex)
            {
                // 오류 메시지 리턴
                return new Tuple<bool, IEnumerable<dynamic>>(false, new List<dynamic>() { ex.Message });
            }
        }

        public Tuple<bool, IEnumerable<dynamic>> EmptyQuery_GetTable(string query)
        {
            if (!connectionStringOK) return new Tuple<bool, IEnumerable<dynamic>>(false, null);

            IEnumerable<dynamic> results = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        //results = connection.Query(query);
                        results = connection.Query(query).Select(obj => new 
                        {


                        }).ToList();

                        //results = ret.Select(s => s.Text).ToList(); 

                    }

                    // The Complete method commits the transaction. If an exception has been thrown,
                    // Complete is not  called and the transaction is rolled back.
                    scope.Complete();
                }
                return new Tuple<bool, IEnumerable<dynamic>>(false, results);
            }
            catch (TransactionAbortedException ex)
            {
                // 오류 메시지 리턴
                return new Tuple<bool, IEnumerable<dynamic>>(false, new List<dynamic>() { ex.Message });
            }
        }

        //public Tuple<bool, IEnumerable<string>> EmptyQueryToStringList(string query)
        //{
        //    if (!connectionStringOK) return new Tuple<bool, IEnumerable<string>>(false, null);

        //    IEnumerable<string> results = null;
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();

        //                //results = connection.Query(query);


        //                var temp = connection.Query(query);
        //                if (temp == null) return new Tuple<bool, IEnumerable<string>>(false, null);
        //                //results = connection.Query(query).Select(obj => new TestClass
        //                //{
        //                //    Text = (string)obj.Text
        //                //}).ToList();

        //                var ret = temp.Select(obj => new TestClass
        //                {
        //                    Text = (string)obj.Text
        //                }).ToList();
        //                results = ret.Select(s => s.Text).ToList();

        //            }

        //            // The Complete method commits the transaction. If an exception has been thrown,
        //            // Complete is not  called and the transaction is rolled back.
        //            scope.Complete();
        //        }
        //        return new Tuple<bool, IEnumerable<string>>(true, results);
        //    }
        //    catch (TransactionAbortedException ex)
        //    {
        //        // 오류 메시지 리턴
        //        return new Tuple<bool, IEnumerable<string>>(false, new List<string>() { ex.Message });
        //    }
        //}
    }

    
}
