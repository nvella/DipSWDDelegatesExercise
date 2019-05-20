using System.Collections.Generic;
using System.Linq;

namespace FileParserNetStandard {
    public class DataParser {
        public static List<List<string>> S(List<List<string>> oldData, List<List<string>> newData)
        {
            for (var i = 0; i < oldData.Count; i++) oldData[i] = newData[i];
            return oldData;
        }

        /// <summary>
        /// Strips any whitespace before and after a data value.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> StripWhiteSpace(List<List<string>> data)
            => S(data, data.Select(row => row.Select(val => val.Trim()).ToList()).ToList());

        /// <summary>
        /// Strips quotes from beginning and end of each data value
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> StripQuotes(List<List<string>> data)
            => S(data, data.Select(row => row.Select(val => val.Trim('"')).ToList()).ToList());

    }
}