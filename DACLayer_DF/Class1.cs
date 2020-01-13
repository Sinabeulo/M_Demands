using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACLayer_DF
{
    public class Class1
    {
        public Class1()
        {

        }

        /// <summary>
        /// https://riptutorial.com/ko/dapper/topic/2/dapper-net-%EC%8B%9C%EC%9E%91%ED%95%98%EA%B8%B0
        /// Dapper - https://dapper-tutorial.net/result-multi-mapping
        /// 트랜젝션 스코프 사용 - https://docs.microsoft.com/ko-kr/dotnet/framework/data/transactions/implementing-an-implicit-transaction-using-transaction-scope
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string TEST_Dapper(string id, string password)
        {
            string result = string.Empty;

            string Param = "A";

            
            var query = "Select TOP 100 * from TableA where Param = @Param";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Param", Param);

            using (IDbConnection connection = new SqlConnection($@"Data Source=;Initial Catalog=;User ID={id};Password={password}"))
            {
                connection.Open();
                //IEnumerable<dynamic> results = connection.Query(query, dynamicParameters);
                connection.Execute(query, dynamicParameters
                    //,null,null, CommandType.StoredProcedure // 프로시저
                    );
            }
            return result;
        }

    }
}

