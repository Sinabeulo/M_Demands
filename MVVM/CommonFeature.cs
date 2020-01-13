using System;

namespace MVVM
{
    public class CommonFeature
    {
        #region Singleton
        static private CommonFeature _pvtFeature;
        static public CommonFeature Feature
        {
            get
            {
                if (_pvtFeature == null)
                {
                    _pvtFeature = new CommonFeature();
                }
                return _pvtFeature;
            }
        }

        private CommonFeature() { }
        #endregion

        public async void ShowMessageAsync(string message)
        {
            Windows.UI.Popups.MessageDialog dialog =
                new Windows.UI.Popups.MessageDialog(message);

            await dialog.ShowAsync();
        }

        public async void ShowMessage(string message)
        {
            Windows.UI.Popups.MessageDialog dialog =
                new Windows.UI.Popups.MessageDialog(message);

            dialog.ShowAsync();
        }
    }
}
