using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ObjectLibrary;
using Delegate_Exercise;


namespace FileParserNetStandard {    
    public class PersonHandler {
        public List<Person> People;

        /// <summary>
        /// Converts List of list of strings into Person objects for People attribute.
        /// </summary>
        /// <param name="people"></param>
        public PersonHandler(List<List<string>> people) {
            var a = CsvHandler.ParserImpl(people).Skip(1);
            People = a.Select(row => new Person(
                int.Parse(row[0]),
                row[1],
                row[2],
                new DateTime(Int64.Parse(row[3]))
                )).ToList();
        }

        /// <summary>
        /// Gets oldest people
        /// </summary>
        /// <returns></returns>
        public List<Person> GetOldest() {
            return People.GroupBy(p => p.Dob).OrderBy(g => g.Key).First().ToList();
        }

        /// <summary>
        /// Gets string (from ToString) of Person from given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetString(int id) {
            return People.Find(p => p.Id == id).ToString();
        }

        public List<Person> GetOrderBySurname() {
            return People.OrderBy(p => p.Surname).ToList();
        }

        /// <summary>
        /// Returns number of people with surname starting with a given string.  Allows case-sensitive and case-insensitive searches
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public int GetNumSurnameBegins(string searchTerm, bool caseSensitive) {
            return People.Where(p =>
                p.Surname.StartsWith(searchTerm, !caseSensitive, null)).Count();
        }
        
        /// <summary>
        /// Returns a string with date and number of people with that date of birth.  Two values seperated by a tab.  Results ordered by date.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAmountBornOnEachDate() {
            return People
                .GroupBy(p => p.Dob)
                .OrderBy(g => g.Key)
                .Select(g => $"{g.Key.ToString()}\t{g.Count()}")
                .ToList();
        }
    }
}