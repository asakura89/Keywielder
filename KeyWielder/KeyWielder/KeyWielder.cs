using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KeyWielder
{
    public class KeyWielder
    {
        private readonly StringBuilder keyBuilder = new StringBuilder();

        private KeyWielder() { }
        public static KeyWielder New() { return new KeyWielder(); }

        public KeyWielder AddRandomString(Int32 valueLength)
        {
            return AddRandomString(valueLength, String.Empty);
        }

        public KeyWielder AddRandomString(Int32 valueLength, String backSeparator)
        {
            const String alphabet = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            return AddRandom(valueLength, alphabet.Split(','), backSeparator);
        }

        public KeyWielder AddRandomNumber(Int32 valueLength)
        {
            return AddRandomNumber(valueLength, String.Empty);
        }

        public KeyWielder AddRandomNumber(Int32 valueLength, String backSeparator)
        {
            const String numeric = "0,1,2,3,4,5,6,7,8,9";
            return AddRandom(valueLength, numeric.Split(','), backSeparator);
        }

        public KeyWielder AddRandomAlphaNumeric(Int32 valueLength)
        {
            return AddRandomAlphaNumeric(valueLength, String.Empty);
        }

        public KeyWielder AddRandomAlphaNumeric(Int32 valueLength, String backSeparator)
        {
            const String alphaNumeric = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
            return AddRandom(valueLength, alphaNumeric.Split(','), backSeparator);
        }

        private KeyWielder AddRandom(Int32 valueLength, String[] charCombination, String backSeparator)
        {
            Int32 seed = Guid.NewGuid().GetHashCode() % 50001;
            var rnd = new Random(seed);
            var randomString = new StringBuilder();
            for (Int32 i = 0; i < valueLength; i++)
            {
                Int32 randomIdx = rnd.Next(0, charCombination.Length - 1);
                randomString.Append(charCombination[randomIdx]);
            }

            keyBuilder.Append(randomString + backSeparator);
            return this;
        }

        public KeyWielder AddGUIDString()
        {
            return AddGUIDString(String.Empty);
        }

        public KeyWielder AddGUIDString(String backSeparator)
        {
            keyBuilder.Append(Guid.NewGuid().ToString("N") + backSeparator);
            return this;
        }

        public KeyWielder AddString(String value, Int32 valueLength)
        {
            return AddString(value, valueLength, String.Empty);
        }

        public KeyWielder AddString(String value, Int32 valueLength, String backSeparator)
        {
            String strWithLength = value.Substring(0, valueLength).ToUpper();
            keyBuilder.Append(strWithLength + backSeparator);
            return this;
        }

        public KeyWielder AddShortYear()
        {
            return AddYear(2, String.Empty);
        }

        public KeyWielder AddShortYear(String backSeparator)
        {
            return AddYear(2, backSeparator);
        }

        public KeyWielder AddLongYear()
        {
            return AddYear(4, String.Empty);
        }

        public KeyWielder AddLongYear(String backSeparator)
        {
            return AddYear(4, backSeparator);
        }

        private KeyWielder AddYear(Int32 valueLength, String backSeparator)
        {
            Int32 currentYear = DateTime.Now.Year;
            String yearWithLength = valueLength == 4 ? currentYear.ToString(CultureInfo.InvariantCulture) : currentYear.ToString(CultureInfo.InvariantCulture).Substring(2, 2);
            keyBuilder.Append(yearWithLength + backSeparator);
            return this;
        }

        public KeyWielder AddShortMonth()
        {
            return AddMonth(3, String.Empty);
        }

        public KeyWielder AddShortMonth(String backSeparator)
        {
            return AddMonth(3, backSeparator);
        }

        public KeyWielder AddShortMonth(IList<String> customMonthList)
        {
            return AddMonth(3, customMonthList, String.Empty);
        }

        public KeyWielder AddShortMonth(IList<String> customMonthList, String backSeparator)
        {
            return AddMonth(3, customMonthList, backSeparator);
        }

        public KeyWielder AddLongMonth()
        {
            return AddMonth(4, String.Empty);
        }

        public KeyWielder AddLongMonth(String backSeparator)
        {
            return AddMonth(4, backSeparator);
        }

        public KeyWielder AddLongMonth(IList<String> customMonthList)
        {
            return AddMonth(4, customMonthList, String.Empty);
        }

        public KeyWielder AddLongMonth(IList<String> customMonthList, String backSeparator)
        {
            return AddMonth(4, customMonthList, backSeparator);
        }

        public KeyWielder AddNumericMonth()
        {
            return AddMonth(2, String.Empty);
        }

        public KeyWielder AddNumericMonth(String backSeparator)
        {
            return AddMonth(2, backSeparator);
        }

        private KeyWielder AddMonth(Int32 valueLength, String backSeparator)
        {
            String[] defaultMonthList = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            return AddMonth(valueLength, defaultMonthList, backSeparator);
        }

        private KeyWielder AddMonth(Int32 valueLength, IList<String> monthList, String backSeparator)
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

        public KeyWielder AddDate()
        {
            return AddDate(String.Empty);
        }

        public KeyWielder AddDate(String backSeparator)
        {
            keyBuilder.Append(DateTime.Now.Day + backSeparator);
            return this;
        }

        public KeyWielder AddShortDay()
        {
            return AddDay(3, String.Empty);
        }

        public KeyWielder AddShortDay(String backSeparator)
        {
            return AddDay(3, backSeparator);
        }

        public KeyWielder AddShortDay(IList<String> customDayList)
        {
            return AddDay(3, customDayList, String.Empty);
        }

        public KeyWielder AddShortDay(IList<String> customDayList, String backSeparator)
        {
            return AddDay(3, customDayList, backSeparator);
        }

        public KeyWielder AddLongDay()
        {
            return AddDay(4, String.Empty);
        }

        public KeyWielder AddLongDay(String backSeparator)
        {
            return AddDay(4, backSeparator);
        }

        public KeyWielder AddLongDay(IList<String> customDayList)
        {
            return AddDay(4, customDayList, String.Empty);
        }

        public KeyWielder AddLongDay(IList<String> customDayList, String backSeparator)
        {
            return AddDay(4, customDayList, backSeparator);
        }

        public KeyWielder AddNumericDay()
        {
            return AddDay(2, String.Empty);
        }

        public KeyWielder AddNumericDay(String backSeparator)
        {
            return AddDay(2, backSeparator);
        }

        private KeyWielder AddDay(Int32 valueLength, String backSeparator)
        {
            String[] defaultDayList = { "", "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };
            return AddDay(valueLength, defaultDayList, backSeparator);
        }

        private KeyWielder AddDay(Int32 valueLength, IList<String> dayList, String backSeparator)
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

        public KeyWielder AddCounter(Int32 currentCounter, Int32 valueLength)
        {
            return AddCounter(currentCounter, 1, valueLength, String.Empty);
        }

        public KeyWielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength)
        {
            return AddCounter(currentCounter, increment, valueLength, String.Empty);
        }

        public KeyWielder AddCounter(Int32 currentCounter, Int32 valueLength, String backSeparator)
        {
            return AddCounter(currentCounter, 1, valueLength, backSeparator);
        }

        public KeyWielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength, String backSeparator)
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