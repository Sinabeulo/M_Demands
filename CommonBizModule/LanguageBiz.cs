using BizCommon_Core.Models;
using CommonDacModule;
using System;
using System.Collections.Generic;
using System.Text;

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

        public bool SetLanguage(LanguageControlModel lanModel)
        {
            if (InsertDac.InsertQuery.SendQueary())
            {
                return true;
            }

            return false;
        }
    }
}
