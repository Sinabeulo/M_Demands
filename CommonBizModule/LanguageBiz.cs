using BizCommon_Std.Models;
using CommonDacModule;
using System.Data;

namespace CommonBizModule
{
    public class LanguageBiz
    {
        private static LanguageBiz _instance;
        public static LanguageBiz BizInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LanguageBiz();
                }

                return _instance;
            }
        }

        public DataTable SetLanguage(ConnectionModel con, LanguageControlModel langModel)
        {
            string execStr = $"EXEC [sp_LanguageManagement] @Flag = '{langModel.Flag}'," +
                $" @LanguageKey = '{langModel.LanguageKey}'," +
                $" @LanguageValue = '{langModel.LanguageValue}'";

            if(!(InsertDac.InsertQuery.SendQueary(con, execStr) is DataTable dt))
            {
                return null;
            }

            return dt;
        }
    }
}
