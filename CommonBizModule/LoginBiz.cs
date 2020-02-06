using BizCommon_Std.Models;
using CommonDacModule;

namespace CommonBizModule
{
    public class LoginBiz
    {
        private static LoginBiz _instance;
        public static LoginBiz Login
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoginBiz();
                }

                return _instance;
            }
        }

        public bool LoginToDatabase(ConnectionModel connection)
        {
            if (LoginDac.LoginQuery.LoginToDatabase(connection))
            {
                return true;
            }

            return false;
        }
    }
}
