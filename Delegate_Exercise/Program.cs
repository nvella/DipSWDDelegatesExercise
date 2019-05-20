using System;
using System.Linq;
using System.Collections.Generic;
using FileParserNetStandard;

public delegate List<List<string>> Parser(List<List<string>> data);

namespace Delegate_Exercise {
    public class Delegate_Exercise {

        public static void Main(string[] args) {
            var fileHandler = new FileHandler();
            var csvHandler = new CsvHandler();
            csvHandler.ProcessCsv("data.csv", "processed_data.csv", data => CsvHandler.ParserImpl(data));
        }
    }
}