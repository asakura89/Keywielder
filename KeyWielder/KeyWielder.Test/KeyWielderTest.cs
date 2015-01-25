using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ayumi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyWielder.Test
{
    [TestClass]
    public class KeyWielderTest
    {
        private String firstKey;
        private String secondKey;
        private String thirdKey;

        [TestInitialize]
        public void Initialize()
        {
            firstKey = KeyWielder
                .New()
                .AddString("GRE", 3)
                .AddLongYear()
                .AddNumericMonth()
                .AddDate()
                .AddCounter(12, 4)
                .BuildKey();

            Debug.WriteLine(firstKey);

            secondKey = KeyWielder
                .New()
                .AddString("GRE", 3, "/")
                .AddLongYear("-")
                .AddNumericMonth("-")
                .AddDate("/")
                .AddCounter(237, 5)
                .BuildKey();

            Debug.WriteLine(secondKey);

            thirdKey = KeyWielder
                .New()
                .AddString("ASAKURA", 7)
                .AddCounter(77, 12, 2)
                .BuildKey();

            Debug.WriteLine(thirdKey);
        }

        [TestMethod]
        public void BuildKeyTest()
        {
            DateTime currentDate = DateTime.Now;
            String firstExpectation = currentDate.ToString("yyyyMMdd");
            String secondExpectation = currentDate.ToString("yyyy-MM-dd");

            Assert.AreEqual("GRE" + firstExpectation + "0013", firstKey); // change expectation to today's date
            Assert.AreEqual("GRE/" + secondExpectation + "/00238", secondKey); // change expectation to today's date
            Assert.AreEqual("ASAKURA89", thirdKey);
        }

        [TestMethod]
        public void AllOutKeyTest()
        {
            var defaultMonthArray = new[] { String.Empty, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var indonesianMonthArray = new[] { String.Empty, "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
            var indonesianMonthList = new List<String>(ExtendedCollection.Select(indonesianMonthArray, month => month.ToUpperInvariant()));
            var indonesianDayArray = new[] { String.Empty, "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu" };
            var indonesianDayList = new List<String>(ExtendedCollection.Select(indonesianDayArray, day => day.ToUpperInvariant()));

            DateTime currentDate = DateTime.Now;
            Int32 currentDayOfWeek = Convert.ToInt32(currentDate.DayOfWeek) + 1;
            Int32 currentMonth = currentDate.Month;
            String currentMonthString = indonesianMonthArray[currentMonth];
            String currentYearString = currentDate.Year.ToString();
            String currentDayString = indonesianDayArray[currentDayOfWeek];
            String separator = "•";
            String fourthKey = KeyWielder
                .New()
                .AddString("DOC", 3)
                .AddString("UMENTATION", 5, "/")
                .AddShortYear("/")
                .AddLongYear("/")
                .AddShortMonth(separator)
                .AddShortMonth(indonesianMonthArray, separator)
                .AddLongMonth(separator)
                .AddLongMonth(indonesianMonthList, separator)
                .AddNumericMonth("/")
                .AddDate("/")
                .AddShortDay(separator)
                .AddLongDay(separator)
                .AddShortDay(indonesianDayArray, separator)
                .AddLongDay(indonesianDayList, separator)
                .AddNumericDay("/")
                .AddCounter(123, 400, 5)
                .BuildKey();

            Debug.WriteLine(fourthKey);

            Assert.AreEqual(
                "DOCUMENT/" + currentYearString.Substring(2, 2) + "/" + currentYearString + "/" + currentMonthString.ToUpper().Substring(0, 3) +
                separator + currentMonthString.Substring(0, 3) + separator +  defaultMonthArray[currentMonth].ToUpper() + separator +
                currentMonthString.ToUpper() + separator + currentMonth.ToString().PadLeft(2, '0') + "/" + currentDate.Day.ToString().PadLeft(2, '0') + "/" +
                currentDate.DayOfWeek.ToString().ToUpper().Substring(0, 3) + separator + currentDate.DayOfWeek.ToString().ToUpper() + separator +
                currentDayString.Substring(0, 3) + separator + currentDayString.ToUpper() + separator + currentDayOfWeek.ToString().PadLeft(2, '0') +
                "/00523",
                fourthKey);
        }

        [TestMethod]
        public void KeyUniquenessTest()
        {
            Assert.AreNotEqual(firstKey, secondKey);
            Assert.AreNotEqual(secondKey, thirdKey);
            Assert.AreNotEqual(firstKey, thirdKey);
        }
    }
}
