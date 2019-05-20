using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FileParserNetStandard;

namespace Delegate_Exercise {
    public delegate List<List<string>> Parser(List<List<string>> data);

    public class CsvHandler {
        public static Parser ParserImpl =
            new Parser(new DataParser().StripQuotes) +
            new Parser(new DataParser().StripWhiteSpace) +
            new Parser(RemoveHashtag);

        /// <summary>
        /// Takes a list of list of strings applies datahandling via dataHandler delegate and writes result as csv to writeFile.
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="writeFile"></param>
        /// <param name="dataHandler"></param>
        public void ProcessCsv(string readFile, string writeFile, Func<List<List<string>>, List<List<string>>> dataHandler) {
            var fileHandler = new FileHandler();
            fileHandler.WriteFile(writeFile, ',', dataHandler(fileHandler.ParseCsv(fileHandler.ReadFile(readFile))));
        }
        
        public void ProcessCsvParsed(string readFile, string writeFile, Parser parser)
        {
            ProcessCsv(readFile, writeFile, data => (parser + new Parser(Capitalise))(data));
        }

        public static List<List<string>> RemoveHashtag(List<List<string>> data)
            => DataParser.S(data, data.Select(row => row.Select(val => val.TrimStart('#')).ToList()).ToList());

        public static List<List<string>> Capitalise(List<List<string>> data)
            => DataParser.S(data, data.Select(row => row.Select(val => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(val.ToLower())).ToList()).ToList());
    }
}