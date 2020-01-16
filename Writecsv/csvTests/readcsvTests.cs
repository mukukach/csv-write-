using Cities;
using Csv;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace csv.Tests
{
    [TestClass()]
    public class ReadcsvTests
    {
        [TestMethod()]
        public void ReadInCSVTest()
        {
            var path = "c://csvfiles//worldcities.csv";
            var doubleTypeConversion = new DoubleConversion();
            IList<CityModel> myList = ReadCsv.ReadCsvFile<CityModel, CityMap>(path, doubleTypeConversion);
            var countryCapitalQuery = from s in myList
                           where s.Capital.Equals("primary")
                           orderby s.Country ascending
                           select s;
            // SOme Update
            foreach (CityModel city in countryCapitalQuery)
            {
                Debug.Write(city.Country + ": " + city.City_name + Environment.NewLine);
            }
            var queryName = nameof(countryCapitalQuery);
            var writePath = "c://csvfiles//" +  queryName  + ".csv";
            using (var writer = new StreamWriter(writePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(countryCapitalQuery);
            }
            Assert.IsTrue(File.Exists(writePath));

            var QSCount = (from city in countryCapitalQuery
                           select city).Count();

            Debug.Write(QSCount);

            Assert.AreEqual(15493, myList.Count());

            // countryQuery = records.Where(city => city.Country.Equals("United States"));
            /*
            foreach (CityModel city in countryQuery)
            {
                var name = city.City_name.ToString();
            }
            */
        }
    }
}