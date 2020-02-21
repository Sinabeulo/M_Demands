using BizCommon_Std.FileIO;
using BizCommon_Std.Models;
using BizCommon_Std.Extensions;
using CommonDacModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CommonBizModule.TestData
{
    public class TestDataBiz
    {
        private Dictionary<string, string> logics;
        private static TestDataBiz _instance;
        public static TestDataBiz BizInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestDataBiz();
                }

                return _instance;
            }
        }

        /// <summary>
        /// 유저코드 Insert문 가져오기
        /// </summary>
        /// <param name="fromCon"></param>
        /// <param name="headerCode"></param>
        /// <returns></returns>
        public List<string> GetUserCodeData(ConnectionModel fromCon, string headerCode)
        {
            List<string> retList = GetData(fromCon, logics["headquery"]).ToListString();
            retList.AddRange(GetData(fromCon, logics["linequery"]).ToListString());
            
            return retList;
        }

        private List<dynamic> GetData(ConnectionModel con, string query)
        {
            return SelectDac.SelectQuery.SendQuearyDapper(con, query);
        }

        private void GetLogics()
        {
            try
            {
                List<string> readLogics = FileManager.FileReadWriter.FileReaderToList("Logic.txt");
                logics = readLogics.Select(s => s.Split("#")).ToDictionaryOnly2Size();
            }
            catch
            {

            }
        }
    }
}
