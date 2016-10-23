using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Keywielder
{
    public class Wielder
    {
        private const Char Space = ' ';
        private const Char Zero = '0';
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
            const String alphabet = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            return AddRandom(valueLength, alphabet.Split(','));
        }

        public Wielder AddRandomNumber(Int32 valueLength)
        {
            const String numeric = "0,1,2,3,4,5,6,7,8,9";
            return AddRandom(valueLength, numeric.Split(','));
        }

        public Wielder AddRandomAlphaNumeric(Int32 valueLength)
        {
            const String alphaNumeric = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
            return AddRandom(valueLength, alphaNumeric.Split(','));
        }

        private Wielder AddRandom(Int32 valueLength, String[] charCombination)
        {
            var randomString = new StringBuilder();
            for (Int32 i = 0; i < valueLength; i++)
            {
                Int32 randomIdx = GetRandomNumber(0, charCombination.Length - 1);
                randomString.Append(charCombination[randomIdx]);
            }

            keyBuilder.Append(randomString);
            return this;
        }

        public Wielder AddGUIDString()
        {
            keyBuilder.Append(Guid.NewGuid().ToString("N"));
            return this;
        }

        public Wielder AddString(String value)
        {
            return AddString(value, value.Length);
        }

        public Wielder AddString(String value, Int32 valueLength)
        {
            String strWithLength = value.Substring(0, valueLength);
            keyBuilder.Append(strWithLength);
            return this;
        }

        public Wielder AddRightPadded(Func<Wielder, Wielder> toBeRightPadded, Int32 valueLength)
        {
            return AddRightPadded(toBeRightPadded(New()).BuildKey(), valueLength, Space);
        }

        public Wielder AddRightPadded(Func<Wielder, Wielder> toBeRightPadded, Int32 valueLength, Char paddedBy)
        {
            return AddRightPadded(toBeRightPadded(New()).BuildKey(), valueLength, paddedBy);
        }

        public Wielder AddRightPadded(String toBeRightPadded, Int32 valueLength)
        {
            return AddRightPadded(toBeRightPadded, valueLength, Space);
        }

        public Wielder AddRightPadded(String toBeRightPadded, Int32 valueLength, Char paddedBy)
        {
            String resultString = toBeRightPadded
                .PadRight(valueLength, paddedBy)
                .Substring(0, valueLength);
            keyBuilder.Append(resultString);
            return this;
        }

        public Wielder AddLeftPadded(Func<Wielder, Wielder> tobeLeftPadded, Int32 valueLength)
        {
            return AddLeftPadded(tobeLeftPadded(New()).BuildKey(), valueLength, Space);
        }

        public Wielder AddLeftPadded(Func<Wielder, Wielder> tobeLeftPadded, Int32 valueLength, Char paddedBy)
        {
            return AddLeftPadded(tobeLeftPadded(New()).BuildKey(), valueLength, paddedBy);
        }

        public Wielder AddLeftPadded(String tobeLeftPadded, Int32 valueLength)
        {
            return AddLeftPadded(tobeLeftPadded, valueLength, Space);
        }

        public Wielder AddLeftPadded(String tobeLeftPadded, Int32 valueLength, Char paddedBy)
        {
            String resultString = tobeLeftPadded
                .PadLeft(valueLength, paddedBy)
                .Substring(0, valueLength);
            keyBuilder.Append(resultString);
            return this;
        }

        public Wielder AddShortYear()
        {
            return AddYear(2);
        }

        public Wielder AddLongYear()
        {
            return AddYear(4);
        }

        private Wielder AddYear(Int32 valueLength)
        {
            Int32 currentYear = DateTime.Now.Year;
            String yearWithLength = valueLength == 4 ? currentYear.ToString(CultureInfo.InvariantCulture) : currentYear.ToString(CultureInfo.InvariantCulture).Substring(2, 2);
            keyBuilder.Append(yearWithLength);
            return this;
        }

        public Wielder AddShortMonth()
        {
            return AddMonth(3);
        }

        public Wielder AddShortMonth(IList<String> customMonthList)
        {
            return AddMonth(3, customMonthList);
        }

        public Wielder AddLongMonth()
        {
            return AddMonth(4);
        }

        public Wielder AddLongMonth(IList<String> customMonthList)
        {
            return AddMonth(4, customMonthList);
        }

        public Wielder AddNumericMonth()
        {
            return AddMonth(2);
        }

        private Wielder AddMonth(Int32 valueLength)
        {
            String[] defaultMonthList = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            return AddMonth(valueLength, defaultMonthList);
        }

        private Wielder AddMonth(Int32 valueLength, IList<String> monthList)
        {
            String month = String.Empty;
            Int32 currentMonth = DateTime.Now.Month;
            switch (valueLength)
            {
                case 2:
                    month = currentMonth.ToString(CultureInfo.InvariantCulture).PadLeft(2, Zero);
                    break;
                case 3:
                    month = monthList[currentMonth].Substring(0, 3);
                    break;
                case 4:
                default:
                    month = monthList[currentMonth];
                    break;
            }

            keyBuilder.Append(month);
            return this;
        }

        public Wielder AddDate()
        {
            keyBuilder.Append(DateTime.Now.Day.ToString().PadLeft(2, Zero));
            return this;
        }

        public Wielder AddShortDay()
        {
            return AddDay(3);
        }

        public Wielder AddShortDay(IList<String> customDayList)
        {
            return AddDay(3, customDayList);
        }

        public Wielder AddLongDay()
        {
            return AddDay(4);
        }

        public Wielder AddLongDay(IList<String> customDayList)
        {
            return AddDay(4, customDayList);
        }

        public Wielder AddNumericDay()
        {
            return AddDay(2);
        }

        private Wielder AddDay(Int32 valueLength)
        {
            String[] defaultDayList = { "", "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };
            return AddDay(valueLength, defaultDayList);
        }

        private Wielder AddDay(Int32 valueLength, IList<String> dayList)
        {
            String day = String.Empty;
            Int32 currentDayOfWeek = Convert.ToInt32(DateTime.Now.DayOfWeek) + 1;
            switch (valueLength)
            {
                case 2:
                    day = currentDayOfWeek.ToString(CultureInfo.InvariantCulture).PadLeft(2, Zero);
                    break;
                case 3:
                    day = dayList[currentDayOfWeek].Substring(0, 3);
                    break;
                case 4:
                default:
                    day = dayList[currentDayOfWeek];
                    break;
            }

            keyBuilder.Append(day);
            return this;
        }

        public Wielder AddCounter(Int32 currentCounter)
        {
            return AddCounter(currentCounter, 1);
        }

        public Wielder AddCounter(Int32 currentCounter, Int32 increment)
        {
            String counter = (currentCounter + increment).ToString(CultureInfo.InvariantCulture);
            keyBuilder.Append(counter);
            return this;
        }

        public String BuildKey()
        {
            return keyBuilder.ToString();
        }
    }
}