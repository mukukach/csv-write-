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
            var path = "c://Users//mukukach//Documents//csvfiles//worldcities.csv";
            var doubleTypeConversion = new DoubleConversion();
            IList<CityModel> myList = ReadCsv.ReadCsvFile<CityModel, CityMap>(path, doubleTypeConversion);
            var Uscountry = from s in myList
                                      where s.Country.Equals("United States")
                                      orderby s.Country ascending
                                      select s;
            // SOme Update
            foreach (CityModel city in Uscountry)
            {
                Debug.Write(city.Country + ": " + city.City_name + Environment.NewLine);
            }
            var queryName = nameof(Uscountry);
            var writePath = "c://csvfiles//" + queryName + ".csv";
            using (var writer = new StreamWriter(writePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(Uscountry);
            }
            Assert.IsTrue(File.Exists(writePath));

            var QSCount = (from city in Uscountry
                           select city).Count();

            Debug.Write(QSCount);

            Assert.AreEqual(15493, myList.Count());

           
        }
    }
}