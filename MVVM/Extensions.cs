using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public static class Extensions
    { 
        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> items)
        {
            try
            {
                return new ObservableCollection<T>(items);
            }
            catch
            {
                return null;
            }
        }

        public static string ToStringForFileWrite(this IList<string> lines)
        {
            StringBuilder sb = new StringBuilder(lines.Count * 100);

            lines.Select(s => sb.Append(s.ToString() + "\n")).Count();

            return sb.ToString();
        }

        public static List<string> SplitToList(this string strData, char splitIdx)
        {
            try
            {
                return strData.Split(splitIdx).ToList();
            }
            catch
            {
                return null;
            }
        }
        
    }
}
