using BizCommon_Std.FileIO;
using BizCommon_Std.Extension;
using CommonBizModule.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using BizCommon_Std.Models;
using CommonDacModule;
using System.Linq;

namespace CommonBizModule
{
    public class BLTestDataMakerBiz : TestDataBiz
    {
        private static BLTestDataMakerBiz _instance;
        public static BLTestDataMakerBiz BizInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BLTestDataMakerBiz();
                }

                return _instance;
            }
        }

        private readonly string filePath = Environment.CurrentDirectory + "\\BLTestDatalogics.txt";
        private static Dictionary<string, string> logics;

        public override void ReadLogicFile()
        {
            var readList = FileManager.FileReadWriter.FileReaderToList(filePath);
            logics = readList.ToDictionary();
        }

        public List<string> GetMakerQuery(ConnectionModel con, string param)
        {
            if (string.IsNullOrEmpty(param))
                return null;

            if (logics == null)
                ReadLogicFile();

            string paramQuery = logics["BLQuery"].Replace("{paramCode}", param);


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
