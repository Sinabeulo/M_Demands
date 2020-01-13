using System;
using System.Collections.Generic;
using System.IO;

namespace DF_Common
{
    public class FileManager
    {
        static public string fileDir = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;
        
        #region Singleton
        static private FileManager PvtFileManager;
        static public FileManager SingleFileManager
        {
            get
            {
                if (PvtFileManager == null)
                    PvtFileManager = new FileManager();
                return PvtFileManager;
            }
        }
        #endregion

        private FileManager()
        {

        }

        public bool FileWriter(string path, List<string> data, bool encry = false)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs);

                if (encry)
                    foreach (var line in data)
                    {
                        //writer.WriteLine(EncryptionManager.EncryManager.Encry(line));
                    }
                else
                    foreach (var line in data)
                        writer.WriteLine(line);

                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> FileReader(string path, bool encry = false)
        {
            try
            {
                List<string> retData = new List<string>();
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);

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

                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
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

        //public List<string> OpenFile(string path = null)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    //fileDialog.file
        //    fileDialog.InitialDirectory = fileDir;
        //    //fileDialog.FileName = path;
        //    if (fileDialog.ShowDialog() == DialogResult.OK)//== true)
        //    {
        //        return FileReader(fileDialog.FileName);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
