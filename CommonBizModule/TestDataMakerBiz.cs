using BizCommon_Std.FileIO;
using BizCommon_Std.Models;
using BizCommon_Std.Extension;
using CommonDacModule;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonBizModule.Abstract;

namespace CommonBizModule
{
    public class TestDataMakerBiz : TestDataBiz
    {
        private static TestDataMakerBiz _instance;
        public static TestDataMakerBiz BizInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestDataMakerBiz();
                }

                return _instance;
            }
        }

        private readonly string filePath = Environment.CurrentDirectory + "\\TestDatalogics.txt";
        private static Dictionary<string, string> logics;

        public override void ReadLogicFile()
        {
            var readList = FileManager.FileReadWriter.FileReaderToList(filePath);
            logics = readList.ToDictionary();
        }

        public List<string> GetMakerQuery(ConnectionModel con, string headerCode)
        {
            if (string.IsNullOrEmpty(headerCode))
                return null;

            if (logics == null)
                ReadLogicFile();

            //List<string> retList = new List<string>();
            // 받은 headercode로 로직에 치환하여 쿼리 반환
            string headerQuery = logics["headquery"].Replace("{headerCode}", headerCode);
            string lineQuery = logics["lineQuery"].Replace("{headerCode}", headerCode);


            List<string> retList = SendTestDataMakeQuery(con, headerQuery);
            var retList2 = SendTestDataMakeQuery(con, lineQuery);

            retList.AddRange(retList2);
            //var aa = SelectDac.SelectQuery.SendQuearyDapper(con, lineQuery);

            //var dd = aa.Select(s => ((IDictionary<string, string>)s).ToDictionary(ks => ks.Key)).ToList();

            //var dd = aa.Select(s => (IDictionary<string, object>)s).ToList();
            //var aaa = cc["query"];


            //List<string> retHeaderQuery = SelectDac.SelectQuery.SendQuearyDapper(con, headerQuery).ToListString();
            //List<string> retlineQuery = SelectDac.SelectQuery.SendQuearyDapper(con, lineQuery).ToListString();

            //retList.AddRange(retHeaderQuery);
            //retList.AddRange(retlineQuery);

            return retList;
        }


        private List<string> SendTestDataMakeQuery(ConnectionModel con, string query)
        {
            //var retQuery = SelectDac.SelectQuery.SendQuearyDapper(con, query).Select(s => (IDictionary<string, object>)s).ToDictionary(s1 => s1.Keys, s1 => s1.Values);
            //var retQuery = SelectDac.SelectQuery.SendQuearyDapper(con, query).OfType<KeyValuePair<string, object>>().Select(s => (string)s.Value).ToList();
            var retData = SelectDac.SelectQuery.SendQuearyDapper(con, query);

            List<string> list = new List<string>();
            foreach (dynamic item in retData)
            {
                var subItem = ((IDictionary<string, object>)item).Select(s => s.Value.ToString()).ToList();
                foreach (string str in subItem)
                    list.Add(str);
                //var subItem = ((IDictionary<string, object>)item).Select(s => s.Value.ToString());
            }
            return list;
            //var retDictionary = retData
            //return retDictionary;
        }
    }
}
