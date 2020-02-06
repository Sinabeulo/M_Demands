using BizCommon_Std.Enums;
using BizCommon_Std.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CommonDacModule
{
    public class SelectDac
    {
        private static SelectDac _instance;
        public static SelectDac SelectQuery
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SelectDac();
                }

                return _instance;
            }
        }

        public List<dynamic> SendQuearyDapper(ConnectionModel conItem, string query)
        {
            try
            {
                List<dynamic> results;
                using (SqlConnection connection = new SqlConnection(conItem.ToString()))
                {
                    connection.Open();

                    results = connection.Query(query).ToList();
                }
                return results;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable SendQueary(ConnectionModel conItem, string query)
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection connection = new SqlConnection(conItem.ToString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
