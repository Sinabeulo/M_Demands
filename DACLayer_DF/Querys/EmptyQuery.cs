using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACLayer_DF.Querys
{
    public class EmptyQuery
    {
        
        //public bool SendQuery(string query)
        //{
        //    //string result = string.Empty;

        //    ////var query = "Select TOP 100 * from tbl_LOT where LocationCode = @LocationCode";
        //    ////var dynamicParameters = new DynamicParameters();
        //    ////dynamicParameters.Add("@LocationCode", LocationCode);

        //    //using (IDbConnection connection = new SqlConnection($@"Data Source=192.16.1.104\MES_TEST, 10004;Initial Catalog=CAMMSYS_MES_TEST;User ID={id};Password={password}"))
        //    //{
        //    //    connection.Open();
        //    //    //IEnumerable<dynamic> results = connection.Query(query, dynamicParameters);
        //    //    connection.Execute(query, dynamicParameters
        //    //        //,null,null, CommandType.StoredProcedure // 프로시저
        //    //        );
        //    //}
        //    //return result;
        //}
    }
}
