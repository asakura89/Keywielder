using System;
using System.Text;

namespace KeyWielder
{
    public static class KeyBlaster
    {
        public enum SimpleKeyType { TEXT, NUMBER, ALPHANUMERIC }

        public static String BuildSimpleKey()
        {
            return BuildSimpleKey(8, SimpleKeyType.ALPHANUMERIC);
        }

        public static String BuildSimpleKey(Int32 keyLength, SimpleKeyType keyType)
        {
            const String TEXT_VALUE = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            const String NUMBER_VALUE = "0,1,2,3,4,5,6,7,8,9";
            const String ALPHANUMERIC_VALUE = TEXT_VALUE + "," + NUMBER_VALUE;

            var keyBuilder = new StringBuilder();
            Int32 seed = Guid.NewGuid().GetHashCode() % 50001;
            var rnd = new Random(seed);
            var tokenString = new String[keyLength];
            String[] combinationChar = null;
            switch (keyType)
            {
                case SimpleKeyType.TEXT:
                    combinationChar = TEXT_VALUE.Split(',');
                    break;
                case SimpleKeyType.NUMBER:
                    combinationChar = NUMBER_VALUE.Split(',');
                    break;
                case SimpleKeyType.ALPHANUMERIC:
                    combinationChar = ALPHANUMERIC_VALUE.Split(',');
                    break;
            }

            for (Int32 i = 0; i < tokenString.Length; i++)
            {
                Int32 temp = rnd.Next(0, combinationChar.Length - 1);
                tokenString[i] = combinationChar[temp];
                keyBuilder.Append(tokenString[i]);
            }

            return keyBuilder.ToString();
        }

        public static String BuildComplexKey()
        {
            return Guid.NewGuid().ToString("N");
        } 
    }
}