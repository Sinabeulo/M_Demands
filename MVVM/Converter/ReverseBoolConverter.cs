using System;
using Windows.UI.Xaml.Data;

namespace MVVM.Converter
{
    public class ReverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (!(value is bool bValue)) return null;

                return !bValue;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (!(value is bool bValue)) return null;

                return !bValue;
            }
            catch
            {
                return null;
            }
        }
    }
}
