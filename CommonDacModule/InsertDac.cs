using BizCommon_Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        public bool SendQueary(ConnectionModel conItem, string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conItem.ToString()))
                {
                    connection.Open();

                    var results = connection.Query<LanguageControlModel>(query).Select(obj => new LanguageControlModel
                    {
                        
                    }).FirstOrDefault();

                    //if (results.resultStatus != ResultStatus.O)
                    //    return false;

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
