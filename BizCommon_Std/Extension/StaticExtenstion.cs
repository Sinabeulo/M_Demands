using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BizCommon_Std.Extension
{
    public static class StaticExtenstion
    {
        public static Dictionary<string, string> ToDictionary(this List<string> srcList)
        {
            //srcList.Select(s=>s.Split('#'))
            IEnumerable<KeyValuePair<string, string>> dtnSource
                = from item in srcList
                  let split = item.Split('#')
                  where split.Length == 2
                  let dtn = new KeyValuePair<string, string>(split[0], split[1])
                  select dtn;

            return dtnSource.ToDictionary(x => x.Key, x => x.Value);
        }

        public static List<string> ToStringList(this IEnumerable<dynamic> srcList)
        {
            //https://stackoverflow.com/questions/49109225/convert-listdynamic-to-liststring/49109269
            return srcList.Cast<string>().ToList();

        }
        public static List<string> JsonToListString(this string srcString)
        {
            Regex regex = new Regex("\",\"");

            if (string.IsNullOrEmpty(srcString)) return null;

            string replaced = Regex.Replace(srcString, "[[]\"|\"[]]", "");

            var items = regex.Split(replaced);
            return items.ToList();
        }
    }
}
