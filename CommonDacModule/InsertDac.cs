﻿using BizCommon_Std.Enums;
using BizCommon_Std.Models;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        public bool SendQuearyDapper(ConnectionModel conItem, string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conItem.ToString()))
                {
                    connection.Open();

                    var results = connection.Query<LanguageControlModel>(query).Select(obj => new LanguageControlModel
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
