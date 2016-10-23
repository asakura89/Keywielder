using System;
using System.Collections;

namespace Keywielder.Test
{
    public static class IEnumerableExtensions
    {
        public static void CheckDuplicate(this IList suspectedList)
        {
            if (suspectedList.ContainsDuplicate())
                throw new Exception("Duplicate!!!");
        }

        public static Boolean ContainsDuplicate(this IList suspectedList)
        {
            return suspectedList.GetDuplicate().Count() > 0;
        }

        public static IEnumerable GetDuplicate(this IList suspectedList)
        {
            Int32 suspectedListCount = suspectedList.Count();

            for (int i = 0; i < suspectedListCount - 1; i++)
                for (int j = i + 1; j < suspectedListCount; j++)
                    if (suspectedList[i].ToString() == suspectedList[j].ToString())
                        yield return suspectedList[i].ToString();
        }

        public static int Count(this IEnumerable source)
        {
            int res = 0;
            foreach (var item in source)
                res++;

            return res;
        }
    }
}