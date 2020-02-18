using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BizCommon_Std.FileIO
{
    public class FileManager
    {
        public static FileManager FileReadWriter
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FileManager();
                }
                return _instance;
            }
        }

        private static FileManager _instance;

        static public string fileDir = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;

        public bool FileWriter(string path, List<string> data, bool encry = false)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        if (encry)
                        {
                            foreach (var line in data)
                            {
                                //writer.WriteLine(EncryptionManager.EncryManager.Encry(line));
                            }
                        }
                        else
                        {
                            foreach (var line in data)
                            {
                                writer.WriteLine(line);
                            }
                        }
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool FileWriter(string path, string data, bool encry = false)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        if (encry)
                        {
                        }
                        else
                        {
                            writer.WriteLine(data);
                        }
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> FileReaderToList(string path, bool encry = false)
        {
            try
            {
                List<string> retData = new List<string>();
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        if (encry)
                        {
                            while (!reader.EndOfStream)
                            {
                                //retData.Add(EncryptionManager.EncryManager.Decry(reader.ReadLine()));
                            }
                        }
                        else
                        {
                            while (!reader.EndOfStream)
                            {
                                retData.Add(reader.ReadLine());
                            }
                        }

                    }
                }
                return retData;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> FileReaderToString(string path, bool encry = false)
        {
            try
            {
                string retData = string.Empty;

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    retData = await reader.ReadToEndAsync();
                }
                
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CheckOrCreateDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
