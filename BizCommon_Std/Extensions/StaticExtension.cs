using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizCommon_Std.Extensions
{
    public static class StaticExtension
    {
        /// <summary>
        /// Convert 2 dimension string array to Dictionanry
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<string,string> ToDictionaryOnly2Size(this IEnumerable<string[]> source)
        {
            // check source data
            if (source?.FirstOrDefault()?.Length != 2)
                return null;

            Dictionary<string, string> pairs = new Dictionary<string, string>();
            foreach(string[] dItem in source)
            {
                pairs.Add(dItem[0], dItem[1]);
            }
            return pairs;
        }

        /// <summary>
        /// Convert dynamic List to string List
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static List<string> ToListString(this IEnumerable<dynamic> src)
        {
            // reference
            // https://stackoverflow.com/questions/7827407/c-sharp-best-way-to-convert-dynamic-to-string
            //List<string> Hello = src.Select(s => Convert.ToString(s)).ToList<string>();
            // https://stackoverflow.com/questions/49109225/convert-listdynamic-to-liststring/49109269
            return src.OfType<string>().ToList();
        }
    }
}
