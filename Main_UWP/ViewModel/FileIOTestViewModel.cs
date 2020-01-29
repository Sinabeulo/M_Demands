using MVVM;
using MVVM.Base;
using System;
using System.Threading.Tasks;

namespace Main_UWP.ViewModel
{
    public class FileIOTestViewModel: ViewModelBase
    {
        public FileIOTestViewModel()
        {
            AddCommand = new RelayCommand(ExecuteAddCommand);
        }

        private async void ExecuteAddCommand()
        {
            Windows.Storage.StorageFolder folder = await OpenFolderPicker();

            if (folder == null) return;

            await CreateFile("DefaultWrite.txt",folder);
            await CreateFile("BufferWrite.txt", folder); 
            await CreateFile("StreamWrite.txt",folder);

            FileWrite_UsingBuffer(folder);
            FileWrite_Default(folder);
            FileWrite_UsingStream(folder);
        }

        private async Task CreateFile(string fileName)
        {
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.Storage.StorageFolder.GetFolderFromPathAsync();

            // 앱 설치 경로
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.ApplicationModel.Package.Current.InstalledLocation;

            // UWP 파일 / 폴더 접근에 관한 기본설정....
            // 구데기.. 기본적으로 UWP 앱은 직접 만든 사용자의 다운로드 폴더에 있는 파일과 폴더에만 액세스할 수 있습니다.
            //https://docs.microsoft.com/ko-kr/windows/uwp/files/file-access-permissions

            //Windows.Storage.StorageFile storageFile =
            //    await Windows.Storage.DownloadsFolder.CreateFileAsync(fileName, 
            //    Windows.Storage.CreationCollisionOption.ReplaceExisting);
            Windows.Storage.StorageFile storageFile =
                await Windows.Storage.DownloadsFolder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            // storageFolder 경로에 파일생성
            //Windows.Storage.StorageFile sampleFile =
            //    await storageFolder.CreateFileAsync(fileName,
            //        Windows.Storage.CreationCollisionOption.ReplaceExisting);
        }

        private async Task CreateFile(string fileName, Windows.Storage.StorageFolder folder)
        {
            Windows.Storage.StorageFile storageFile =
                await folder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
        }

        private async Task<Windows.Storage.StorageFolder> OpenFolderPicker()
        {
            // 이거 참 그지 같구만
            Windows.Storage.Pickers.FolderPicker folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if(folder != null)
            {

            }
            else
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("you selected nothing");
                await dialog.ShowAsync();
            }
            return folder;

            // 참고해 봐야 할 듯
            //https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/FileAccess/cs
        }

        private async void FileWrite_Default(Windows.Storage.StorageFolder storageFolder)
        {
            // Un Recommanded
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.ApplicationModel.Package.Current.InstalledLocation;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("DefaultWrite.txt");

            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "DefaultWrite");
        }

        private async void FileWrite_UsingBuffer(Windows.Storage.StorageFolder storageFolder)
        {
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.ApplicationModel.Package.Current.InstalledLocation;

            Windows.Storage.StorageFile storage = await storageFolder.GetFileAsync("BufferWrite.txt");


            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(
                "HelloThere!\nHello~", Windows.Security.Cryptography.BinaryStringEncoding.Utf8);

            await Windows.Storage.FileIO.WriteBufferAsync(storage, buffer);
        }

        private async void FileWrite_UsingStream(Windows.Storage.StorageFolder storageFolder)
        {
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder storageFolder =
            //    Windows.ApplicationModel.Package.Current.InstalledLocation;
            try
            {
                Windows.Storage.StorageFile storage = await storageFolder.GetFileAsync("StreamWrite.txt");

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
            }
            catch
            {

            }
        }


    }
}
