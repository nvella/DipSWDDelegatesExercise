using System;
using System.Collections.Generic;
using NUnit.Framework;
using FileParserNetStandard;

namespace FileParserNetStandard_Tests {
    [TestFixture]
    public class Tests {
        private FileHandler _fh;
        private DataParser _dp;
        private string _csvPath = "C:\\tmp\\data.csv";  // change to appropriate path
        private string _tsvPath = "C:\\tmp\\data.tsv";  // change to appropriate path

        private string _writeFile = "C:\\tmp\\dataWrite.txt";  // change to appropriate path
       
        private List<List<string>> _data;

        [SetUp]
        public void Init() {
            _fh = new FileHandler();
            _dp = new DataParser();
            
            _data = new List<List<string>>();
            
            _data.Add(new List<string>() {"1", "John", "Smith"});
            _data.Add(new List<string>() {"2", "Jane", "Jones"});
            _data.Add(new List<string>() {"3", "Jill", "Rhodes"});
            _data.Add(new List<string>() {"4", "Bill", "Holmes"});
            _data.Add(new List<string>() {"5", "Peter", "Watson"});
            _data.Add(new List<string>() {"6", "Ophelia", "Turing"});
            _data.Add(new List<string>() {"7", "Catherine", "Clark"});
            _data.Add(new List<string>() {" 8", "    Wilfred     ", "Sutherland "});
            _data.Add(new List<string>() {"9", "Rickgard", "Arthurs"});
        }

        [Test]
        public void ReadFileTest() {
            var list = _fh.ReadFile(_csvPath);

            Assert.AreEqual(501, list.Count);
            Assert.AreEqual("\"#419\",\"#Augy\",\"#Dedrick\",\"#636399072000000000\"", list[419]);
            Assert.AreEqual("\"#231\",\"#Silvester\",\"#O'Crowley\",\"#636340320000000000\"", list[231]);
            
            list = _fh.ReadFile(_tsvPath);

            Assert.AreEqual(1001, list.Count);
            Assert.AreEqual("YLU-423	Kia	Sephia	2001", list[735]);
            Assert.AreEqual("YTU-308	Lotus	Elise	2005", list[918]);
        }
        
        [Test]
        public void ParseDataTest() {
            var list = _fh.ReadFile(_csvPath);
            var data = _fh.ParseData(list, ',');
            
            Assert.AreEqual(501, data.Count);
            Assert.AreEqual("\"#Dedrick\"", data[419][2]);
            Assert.AreEqual("\"#Silvester\"", data[231][1]);
            
            list = _fh.ReadFile(_tsvPath);
            data = _fh.ParseData(list, '\t');

            Assert.AreEqual(1001, data.Count);
            Assert.AreEqual("YLU-423", data[735][0]);
            Assert.AreEqual("Elise", data[918][2]);
        }
        
        [Test]
        public void ParseCsvTest() {
            var list = _fh.ReadFile(_csvPath);
            var data = _fh.ParseCsv(list);
            
            Assert.AreEqual(501, data.Count);
            Assert.AreEqual("\"#Dedrick\"", data[419][2]);
            Assert.AreEqual("\"#Silvester\"", data[231][1]);
        }

        [Test]
        public void WriteFileTest() {
            _fh.WriteFile(_writeFile, '*', _data);

            var data = _fh.ReadFile(_writeFile);
            
            Assert.AreEqual("7*Catherine*Clark", data[6]);

        }

        [Test]
        public void StripWhiteSpaceTest() {
            var list = _dp.StripWhiteSpace(_data);

            Assert.AreEqual("8", list[7][0]); 
            Assert.AreEqual("Wilfred", list[7][1]);
            Assert.AreEqual("Sutherland", list[7][2]);

            
        }
        
        [Test]
        public void StripQuotesTest() {
            var list = _dp.StripQuotes(_data);

            Assert.AreEqual("6", list[5][0]); 
            Assert.AreEqual("Ophelia", list[5][1]);
            Assert.AreEqual("Turing", list[5][2]);   
        }
    }
}