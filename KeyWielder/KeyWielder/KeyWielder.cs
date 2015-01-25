using System;
using System.Collections.Generic;
using System.Text;

namespace KeyWielder
{
    public static class KeyWielder
    {
        public enum KeyType { Year, Month, Date, String, Counter }

        public class KeyWielderConfig
        {
            public KeyType keyFormat = KeyType.Counter;
            public String value = String.Empty;
            public Int32 valueLength = 4;
            public String replacementChar = "0";
            public String counterKey;
            public Int32 currentCounterValue = 0;
            public Int32 counterIncrement = 1;
            public String backSeparator = String.Empty;
        }

        public static String BuildKey(List<KeyWielderConfig> keyWielderConfigList)
        {
            var keyBuilder = new StringBuilder();
            var monthList = new[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var daysList = new[] { "", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            foreach (KeyWielderConfig keyConfigFormat in keyWielderConfigList)
            {
                switch (keyConfigFormat.keyFormat)
                {
                    case KeyType.Year:
                        AppendYearPart(keyConfigFormat, keyBuilder);
                        break;
                    case KeyType.Month:
                        AppendMonthPart(keyConfigFormat, monthList, keyBuilder);
                        break;
                    case KeyType.Date:
                        AppendDatePart(keyConfigFormat, daysList, keyBuilder);
                        break;
                    case KeyType.String:
                        AppendStringPart(keyConfigFormat, keyBuilder);
                        break;
                    case KeyType.Counter:
                        AppendCounterPart(keyConfigFormat, keyBuilder);
                        break;
                }
            }

            return keyBuilder.ToString();
        }

        private static void AppendCounterPart(KeyWielderConfig keyWielderConfig, StringBuilder keyBuilder)
        {
            Int32 currentCounterValue = keyWielderConfig.currentCounterValue + keyWielderConfig.counterIncrement;
            Char replacement = keyWielderConfig.replacementChar[0];
            String counterWithLength = currentCounterValue.ToString().PadLeft(keyWielderConfig.valueLength, replacement);
            keyBuilder.Append(counterWithLength);
        }

        private static void AppendStringPart(KeyWielderConfig keyConfigFormat, StringBuilder keyBuilder)
        {
            String strWithLength = keyConfigFormat.value.Substring(0, keyConfigFormat.valueLength).ToUpper();
            keyBuilder.Append(strWithLength + keyConfigFormat.backSeparator);
        }

        private static void AppendDatePart(KeyWielderConfig keyConfigFormat, String[] daysList, StringBuilder keyBuilder)
        {
            String datWithLength = String.Empty;
            switch (keyConfigFormat.valueLength)
            {
                case 4:
                    datWithLength = daysList[Convert.ToInt32(DateTime.Now.Date)];
                    break;
                case 3:
                    {
                        Int32 currentDayOfWeek = 0;
                        for (Int32 i = 0; i < daysList.Length; i++)
                            if (daysList[i] == Convert.ToString(DateTime.Now.DayOfWeek))
                                currentDayOfWeek = i;
                        datWithLength = currentDayOfWeek.ToString();
                    }
                    break;
                case 2:
                    datWithLength = Convert.ToString(DateTime.Now.Day).PadLeft(2, '0');
                    break;
                case 1:
                    datWithLength = Convert.ToString(DateTime.Now.DayOfWeek).Substring(0, 3).ToUpper();
                    break;
            }

            keyBuilder.Append(datWithLength + keyConfigFormat.backSeparator);
        }

        private static void AppendMonthPart(KeyWielderConfig keyConfigFormat, String[] monthList, StringBuilder keyBuilder)
        {
            String monWithLength = String.Empty;
            switch (keyConfigFormat.valueLength)
            {
                case 4:
                    monWithLength = monthList[DateTime.Now.Month];
                    break;
                case 3:
                    monWithLength = monthList[DateTime.Now.Month].Substring(0, 3).ToUpper();
                    break;
                case 2:
                    monWithLength = DateTime.Now.Month.ToString().PadLeft(2, '0');
                    break;
            }
            keyBuilder.Append(monWithLength + keyConfigFormat.backSeparator);
        }

        private static void AppendYearPart(KeyWielderConfig keyConfigFormat, StringBuilder keyBuilder)
        {
            String yearWithLength = String.Empty;
            yearWithLength = keyConfigFormat.valueLength == 4 ? DateTime.Now.Year.ToString() : DateTime.Now.Year.ToString().Substring(2, 2);
            keyBuilder.Append(yearWithLength + keyConfigFormat.backSeparator);
        } 
    }
}