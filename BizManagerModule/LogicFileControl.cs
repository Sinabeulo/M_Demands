using System;
using System.Collections.Generic;
using System.IO;

namespace BizManagerModule
{
    public class LogicFileControl
    {
        private static LogicFileControl _instance;

        public static LogicFileControl Logics
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new LogicFileControl();
                }
                return _instance;
            }
        }

        //private Dictionary<string, string> _matrLogics = new Dictionary<string, string>();

        public Dictionary<string, string> FileReader(string path)
        {
            Dictionary<string, string> logics = new Dictionary<string, string>();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        while (!reader.EndOfStream)
                        {
                            //retData.Add(reader.ReadLine());
                            // [key]#[data]
                            var pair = reader.ReadLine().Split("#");
                            logics.Add(pair[0], pair[1]);
                        }
                    }
                }
                return logics;

            }
            catch 
            {
                return null;
            }
        }
        
        
    }
}
