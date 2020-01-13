using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MVVM
{
    public class FileManager
    {
        //static public string fileDir = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;
        //static private string folderDir = string.Empty;         // 경로
        static private StorageFolder storageFolder = null;      // 저장 폴더 경로
        
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

        public async Task OpenFolderPicker()
        {
            try
            {
                // 이거 참 그지 같구만
                Windows.Storage.Pickers.FolderPicker folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                folderPicker.FileTypeFilter.Add("*");

                StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                if (folder != null)
                {
                    storageFolder = folder;
                }
                else
                {
                    CommonFeature.Feature.ShowMessageAsync("you selected nothing");
                    storageFolder = null;
                }
            }
            catch(Exception e)
            {
                CommonFeature.Feature.ShowMessageAsync("Exception OpenFolderPicker : " + e.Message);
            }
            // 참고해 봐야 할 듯
            //https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/FileAccess/cs
        }

        #region FileWriter
        /// <summary>
        /// 파일 쓰기 메서드
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="way"></param>
        /// <returns></returns>
        public async Task<bool> FileWriteAsync(string fileName, List<string> data, ReadWriteWay way)
        {
            try
            {
                if (CheckStorageFolderSetting() == false) return false;

                await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                StorageFile storage = await storageFolder.GetFileAsync(fileName);
                if (storage == null)
                {
                    CommonFeature.Feature.ShowMessageAsync("Get File Failed ");
                    return false;
                }

                switch (way)
                {
                    case ReadWriteWay.Buffer:
                        await FileWrite_UsingBuffer(storage, data);
                        break;
                    case ReadWriteWay.Stream:
                        await FileWrite_UsingStream(storage, data);
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                CommonFeature.Feature.ShowMessageAsync($"Exception FileWrite {way} : " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 버퍼로 파일 작성하기
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        private async Task<bool> FileWrite_UsingBuffer(StorageFile storage, List<string> data)
        {
            if (CheckStorageFolderSetting() == false) return false;

            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(
                    data.ToStringForFileWrite()
                , Windows.Security.Cryptography.BinaryStringEncoding.Utf8);

            await Windows.Storage.FileIO.WriteBufferAsync(storage, buffer);

            return true;
        }

        /// <summary>
        /// 스트림으로 파일 작성하기
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> FileWrite_UsingStream(StorageFile storage, List<string> data)
        {
            var stream = await storage.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

            // outputStream 가져옴
            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    dataWriter.WriteString("StreamWriteTest");
                    await dataWriter.StoreAsync();
                }
                await outputStream.FlushAsync();
            }

            stream.Dispose();
            return true;
        }
        #endregion FileWriter

        #region FileReader
        /// <summary>
        /// 파일 읽기 메서드
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="way"></param>
        /// <returns></returns>
        public async Task<string> FileReadAsync(string fileName, ReadWriteWay way)
        {
            try
            {
                string retText = string.Empty;

                if (CheckStorageFolderSetting() == false) return null;

                await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                StorageFile storage = await storageFolder.GetFileAsync(fileName);
                if (storage == null)
                {
                    CommonFeature.Feature.ShowMessageAsync("Get File Failed ");
                    return null;
                }

                switch (way)
                {
                    case ReadWriteWay.Buffer:
                        retText = await FileRead_UsingBuffer(storage);
                        break;
                    case ReadWriteWay.Stream:
                        retText = await FileRead_UsingStream(storage);
                        break;
                }
                return retText;
            }
            catch (Exception e)
            {
                CommonFeature.Feature.ShowMessageAsync($"Exception FileRead {way} : " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 버퍼로 파일 읽기
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        private async Task<string> FileRead_UsingBuffer(StorageFile storage)
        {
            string text = string.Empty;

            var buffer = await Windows.Storage.FileIO.ReadBufferAsync(storage);

            using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
            {
                text = dataReader.ReadString(buffer.Length);
            }

            return text;
        }

        /// <summary>
        /// 스트림으로 파일 읽기
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        private async Task<string> FileRead_UsingStream(StorageFile storage)
        {          
            string text = string.Empty;

            var stream = await storage.OpenAsync(FileAccessMode.Read, StorageOpenOptions.AllowOnlyReaders);
            ulong size = stream.Size;

            using (var inputStream = stream.GetInputStreamAt(0))
            {
                using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                {
                    uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                    text = dataReader.ReadString(numBytesLoaded);
                }
            }

            return text;
        }     
        #endregion FileReader

        /// <summary>
        /// 저장경로 설정 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckStorageFolderSetting()
        {
            if (storageFolder == null)
            {
                CommonFeature.Feature.ShowMessage("Storage Folder has not Selected");

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 저장경로 가져오기
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetStorageDirectory()
        {
            if (storageFolder == null)  return null;

            return storageFolder.Path;
        }

        /// <summary>
        /// 파일 IO 방법
        /// </summary>
        public enum ReadWriteWay
        {
            Buffer = 0,
            Stream = 1
        }
    }
}
