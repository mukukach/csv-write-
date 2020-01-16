using Cities;
using Csv;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Tests
{
    [TestClass()]
    public class WritecvsfilesTests
    {
        [TestMethod()]
        public void WritecvsTest()
        {
            var path = "c://csvfiles//worldcities.csv";
            var doubleTypeConversion = new DoubleConversion();
            IList<CityModel> myList = ReadCsv.ReadCsvFile<CityModel, CityMap>(path, doubleTypeConversion);
            var variablename = from s in myList
                                      where s.Country.Equals("United States")
                                      orderby s.Country ascending
                                      select s;
            // SOme Update
            foreach (CityModel city in variablename)
            {
                Debug.Write(city.Country + ": " + city.City_name + Environment.NewLine);
            }
            var queryName = nameof(variablename);
            var writePath = "c://csvfiles//" + queryName + ".csv";
            using (var writer = new StreamWriter(writePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(variablename);
            }
            Assert.IsTrue(File.Exists(writePath));

            var QSCount = (from city in variablename
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