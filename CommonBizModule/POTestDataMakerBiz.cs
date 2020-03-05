using BizCommon_Std.FileIO;
using BizCommon_Std.Models;
using BizCommon_Std.Extension;
using CommonDacModule;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonBizModule.Abstract;
using System.Text;

namespace CommonBizModule
{
    public class POTestDataMakerBiz : TestDataBiz
    {
        private static POTestDataMakerBiz _instance;
        public static POTestDataMakerBiz BizInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new POTestDataMakerBiz();
                }

                return _instance;
            }
        }

        private readonly string filePath = Environment.CurrentDirectory + "\\POTestDatalogics.txt";
        private static Dictionary<string, string> logics;

        public override void ReadLogicFile()
        {
            StringBuilder sb = new StringBuilder();
            var readList = FileManager.FileReadWriter.FileReaderToList(filePath);

            List<string> readLogics = new List<string>();

            foreach(string str in readList)
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    sb.Append(str);
                }
                else
                {
                    readLogics.Add(sb.ToString());
                    sb.Clear();
                }
            }

            logics = readLogics.ToDictionary();
        }

        public List<string> GetMakerQuery(ConnectionModel con, string param)
        {
            if (string.IsNullOrEmpty(param))
                return null;

            if (logics == null)
                ReadLogicFile();

            string paramQuery = logics["POQuery"].Replace("{paramCode}", param);


            List<string> retList = SendTestDataMakeQuery(con, paramQuery);
            return retList;
        }

        private List<string> SendTestDataMakeQuery(ConnectionModel con, string query)
        {
            var retData = SelectDac.SelectQuery.SendQuearyDapper(con, query);

            List<string> list = new List<string>();
            foreach (dynamic item in retData)
            {
                var subItem = ((IDictionary<string, object>)item).Select(s => s.Value.ToString()).ToList();
                foreach (string str in subItem)
                    list.Add(str);
            }
            return list;
        }
    }
}