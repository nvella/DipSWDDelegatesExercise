using System;
using System.Collections.Generic;
using System.Linq;
using FileParserNetStandard;
using NUnit.Framework;
using ObjectLibrary;

namespace PersonHandler_Tests {
    [TestFixture]
    public class Tests {
        private FileHandler _fh;
        private DataParser _dp;

        private string csvPath = "C:\\tmp\\data.csv"; // change to appropriate path
        private List<List<string>> data;

        private List<Person> people;

        [SetUp]
        public void Init() {
            _fh = new FileHandler();
            _dp = new DataParser();

            var lines = _fh.ReadFile(csvPath);
            data = _fh.ParseCsv(lines);
        }
        
        [Test]
        public void TestConstructor() {
            PersonHandler ph = new PersonHandler(data);
            
            Assert.AreEqual(500, ph.People.Count);
            Assert.AreEqual("Arlinda", ph.People.ElementAt(462).FirstName);
        }

        [Test]
        public void TestOldest() {
            PersonHandler ph = new PersonHandler(data);
            var oldest = ph.GetOldest();
            
            Assert.AreEqual(2, oldest.Count);
            var result = oldest.Where(person => person.Id == 205 || person.Id == 402).ToList();
            if (result.Count == 2) {
                Assert.True(true);
            } else {
                Assert.True(false);
            }
        }
        
        [Test]
        public void TestGetString() {
            PersonHandler ph = new PersonHandler(data);
            
            string expected = "Thornton Mynett 2/05/2017 12:00:00 AM/5/2017";
            string result = ph.GetString(368);            
            Assert.AreEqual(expected, result);
            
            expected = "Elianore Wyley 10/11/2017 12:00:00 AM/11/2017";
            result = ph.GetString(85);            
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestGetOrderBySurname() {
            PersonHandler ph = new PersonHandler(data);

            var ordered = ph.GetOrderBySurname();
            
            Assert.AreEqual(500, ordered.Count);
            Assert.AreEqual("Aarons", ordered[0].Surname);
            Assert.AreEqual("Zarb", ordered[ordered.Count-1].Surname);
            
        }

        [Test]
        public void TestGetNumSurnameBegins() {
            PersonHandler ph = new PersonHandler(data);

            Assert.AreEqual(24, ph.GetNumSurnameBegins("A", true));
            Assert.AreEqual(24, ph.GetNumSurnameBegins("a", false));
            Assert.AreEqual(0, ph.GetNumSurnameBegins("a", true));
            
            Assert.AreEqual(0, ph.GetNumSurnameBegins("tA", true));
            Assert.AreEqual(1, ph.GetNumSurnameBegins("tA", false));            
        }
        
        [Test]
        public void TestGetAmountBornOnEachDate() {
            PersonHandler ph = new PersonHandler(data);

            var result = ph.GetAmountBornOnEachDate();
            Assert.AreEqual("11/04/2017 12:00:00 AM\t2", result.First());
            Assert.AreEqual("8/05/2017 12:00:00 AM	1", result[23]);
        }
    }
}