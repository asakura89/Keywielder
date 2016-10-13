using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Keywielder
{
    public class Wielder
    {
        private readonly StringBuilder keyBuilder = new StringBuilder();

        private Wielder() { }
        public static Wielder New() { return new Wielder(); }

        private Int32 GetRandomNumber(Int32 lowerBound, Int32 upperBound)
        {
            Int32 seed = Guid.NewGuid().GetHashCode() % 50001;
            var rnd = new Random(seed);
            return rnd.Next(lowerBound, upperBound);
        }

        public Wielder AddRandomString(Int32 valueLength)
        {
            return AddRandomString(valueLength, String.Empty);
        }

        public Wielder AddRandomString(Int32 valueLength, String backSeparator)
        {
            const String alphabet = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            return AddRandom(valueLength, alphabet.Split(','), backSeparator);
        }

        public Wielder AddRandomNumber(Int32 valueLength)
        {
            return AddRandomNumber(valueLength, String.Empty);
        }

        public Wielder AddRandomNumber(Int32 valueLength, String backSeparator)
        {
            const String numeric = "0,1,2,3,4,5,6,7,8,9";
            return AddRandom(valueLength, numeric.Split(','), backSeparator);
        }

        public Wielder AddRandomAlphaNumeric(Int32 valueLength)
        {
            return AddRandomAlphaNumeric(valueLength, String.Empty);
        }

        public Wielder AddRandomAlphaNumeric(Int32 valueLength, String backSeparator)
        {
            const String alphaNumeric = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
            return AddRandom(valueLength, alphaNumeric.Split(','), backSeparator);
        }

        private Wielder AddRandom(Int32 valueLength, String[] charCombination, String backSeparator)
        {
            var randomString = new StringBuilder();
            for (Int32 i = 0; i < valueLength; i++)
            {
                Int32 randomIdx = GetRandomNumber(0, charCombination.Length - 1);
                randomString.Append(charCombination[randomIdx]);
            }

            keyBuilder.Append(randomString + backSeparator);
            return this;
        }

        public Wielder AddGUIDString()
        {
            return AddGUIDString(String.Empty);
        }

        public Wielder AddGUIDString(String backSeparator)
        {
            keyBuilder.Append(Guid.NewGuid().ToString("N") + backSeparator);
            return this;
        }

        public Wielder AddString(String value, Int32 valueLength)
        {
            return AddString(value, valueLength, String.Empty);
        }

        public Wielder AddString(String value, Int32 valueLength, String backSeparator)
        {
            String strWithLength = value.Substring(0, valueLength).ToUpper();
            keyBuilder.Append(strWithLength + backSeparator);
            return this;
        }

        public Wielder AddShortYear()
        {
            return AddYear(2, String.Empty);
        }

        public Wielder AddShortYear(String backSeparator)
        {
            return AddYear(2, backSeparator);
        }

        public Wielder AddLongYear()
        {
            return AddYear(4, String.Empty);
        }

        public Wielder AddLongYear(String backSeparator)
        {
            return AddYear(4, backSeparator);
        }

        private Wielder AddYear(Int32 valueLength, String backSeparator)
        {
            Int32 currentYear = DateTime.Now.Year;
            String yearWithLength = valueLength == 4 ? currentYear.ToString(CultureInfo.InvariantCulture) : currentYear.ToString(CultureInfo.InvariantCulture).Substring(2, 2);
            keyBuilder.Append(yearWithLength + backSeparator);
            return this;
        }

        public Wielder AddShortMonth()
        {
            return AddMonth(3, String.Empty);
        }

        public Wielder AddShortMonth(String backSeparator)
        {
            return AddMonth(3, backSeparator);
        }

        public Wielder AddShortMonth(IList<String> customMonthList)
        {
            return AddMonth(3, customMonthList, String.Empty);
        }

        public Wielder AddShortMonth(IList<String> customMonthList, String backSeparator)
        {
            return AddMonth(3, customMonthList, backSeparator);
        }

        public Wielder AddLongMonth()
        {
            return AddMonth(4, String.Empty);
        }

        public Wielder AddLongMonth(String backSeparator)
        {
            return AddMonth(4, backSeparator);
        }

        public Wielder AddLongMonth(IList<String> customMonthList)
        {
            return AddMonth(4, customMonthList, String.Empty);
        }

        public Wielder AddLongMonth(IList<String> customMonthList, String backSeparator)
        {
            return AddMonth(4, customMonthList, backSeparator);
        }

        public Wielder AddNumericMonth()
        {
            return AddMonth(2, String.Empty);
        }

        public Wielder AddNumericMonth(String backSeparator)
        {
            return AddMonth(2, backSeparator);
        }

        private Wielder AddMonth(Int32 valueLength, String backSeparator)
        {
            String[] defaultMonthList = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            return AddMonth(valueLength, defaultMonthList, backSeparator);
        }

        private Wielder AddMonth(Int32 valueLength, IList<String> monthList, String backSeparator)
        {
            String month = String.Empty;
            Int32 currentMonth = DateTime.Now.Month;
            switch (valueLength)
            {
                case 4:
                    month = monthList[currentMonth];
                    break;
                case 3:
                    month = monthList[currentMonth].Substring(0, 3);
                    break;
                case 2:
                    month = currentMonth.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    break;
            }

            keyBuilder.Append(month + backSeparator);
            return this;
        }

        public Wielder AddDate()
        {
            return AddDate(String.Empty);
        }

        public Wielder AddDate(String backSeparator)
        {
            return AddDate(0, backSeparator);
        }

        public Wielder AddDate(Int32 valueLength, String backSeparator)
        {
            keyBuilder.Append(DateTime.Now.Day.ToString().PadLeft(valueLength, '0') + backSeparator);
            return this;
        }

        public Wielder AddShortDay()
        {
            return AddDay(3, String.Empty);
        }

        public Wielder AddShortDay(String backSeparator)
        {
            return AddDay(3, backSeparator);
        }

        public Wielder AddShortDay(IList<String> customDayList)
        {
            return AddDay(3, customDayList, String.Empty);
        }

        public Wielder AddShortDay(IList<String> customDayList, String backSeparator)
        {
            return AddDay(3, customDayList, backSeparator);
        }

        public Wielder AddLongDay()
        {
            return AddDay(4, String.Empty);
        }

        public Wielder AddLongDay(String backSeparator)
        {
            return AddDay(4, backSeparator);
        }

        public Wielder AddLongDay(IList<String> customDayList)
        {
            return AddDay(4, customDayList, String.Empty);
        }

        public Wielder AddLongDay(IList<String> customDayList, String backSeparator)
        {
            return AddDay(4, customDayList, backSeparator);
        }

        public Wielder AddNumericDay()
        {
            return AddDay(2, String.Empty);
        }

        public Wielder AddNumericDay(String backSeparator)
        {
            return AddDay(2, backSeparator);
        }

        private Wielder AddDay(Int32 valueLength, String backSeparator)
        {
            String[] defaultDayList = { "", "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };
            return AddDay(valueLength, defaultDayList, backSeparator);
        }

        private Wielder AddDay(Int32 valueLength, IList<String> dayList, String backSeparator)
        {
            String day = String.Empty;
            Int32 currentDayOfWeek = Convert.ToInt32(DateTime.Now.DayOfWeek) + 1;
            switch (valueLength)
            {
                case 4:
                    day = dayList[currentDayOfWeek];
                    break;
                case 3:
                    day = dayList[currentDayOfWeek].Substring(0, 3);
                    break;
                case 2:
                    day = currentDayOfWeek.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    break;
            }

            keyBuilder.Append(day + backSeparator);
            return this;
        }

        public Wielder AddCounter(Int32 currentCounter, Int32 valueLength)
        {
            return AddCounter(currentCounter, 1, valueLength, String.Empty);
        }

        public Wielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength)
        {
            return AddCounter(currentCounter, increment, valueLength, String.Empty);
        }

        public Wielder AddCounter(Int32 currentCounter, Int32 valueLength, String backSeparator)
        {
            return AddCounter(currentCounter, 1, valueLength, backSeparator);
        }

        public Wielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength, String backSeparator)
        {
            String counter = (currentCounter + increment).ToString(CultureInfo.InvariantCulture).PadLeft(valueLength, '0');
            keyBuilder.Append(counter + backSeparator);
            return this;
        }

        public String BuildKey()
        {
            return keyBuilder.ToString();
        }
    }
}