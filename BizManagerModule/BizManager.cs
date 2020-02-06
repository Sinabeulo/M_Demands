using System;
using System.Collections.Generic;
using System.Text;

namespace BizManagerModule
{
    public class BizManager
    {
        private static BizManager _instance;
        public static BizManager Manager
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new BizManager();
                    _matrLogics = LogicFileControl.Logics.FileReader("MATR.txt");
                }
                return _instance;
            }
        }

        private static Dictionary<string, string> _matrLogics;

        public void GetLogics()
        {
            //_matrLogics = LogicFileControl.Logics.FileReader("MATR.txt");
        }
    }
}
