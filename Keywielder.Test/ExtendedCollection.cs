using System;
using System.Collections;
using System.Collections.Generic;

namespace Ayumi
{
    public class ExtendedCollection
    {
        public delegate TResult Func<T, TResult>(T t);

        public static Boolean Any<TSource>(IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            using (IEnumerator<TSource> e = source.GetEnumerator())
                return e.MoveNext();
        }

        public static Boolean Any<TSource>(IEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");

            return Any(Where(source, predicate));
        }

        public static Int32 Count<TSource>(IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            Int32 counter = 0;
            using (IEnumerator<TSource> e = source.GetEnumerator())
                while (e.MoveNext())
                    counter++;

            return counter;
        }

        public static Int32 Count<TSource>(IEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");

            IEnumerable<TSource> predicateResult = Where(source, predicate);
            return Count(predicateResult);
        }

        public static TSource FirstOrDefault<TSource>(IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            using (IEnumerator<TSource> e = source.GetEnumerator())
                return e.MoveNext() ? e.Current : default(TSource);
        }

        public static TSource FirstOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");

            return FirstOrDefault(Where(source, predicate));
        }

        public static IEnumerable<TSource> Where<TSource>(IEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");

            using (IEnumerator<TSource> e = source.GetEnumerator())
                while (e.MoveNext())
                    if (predicate(e.Current) == true)
                        yield return e.Current;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");

            using (IEnumerator<TSource> e = source.GetEnumerator())
                while (e.MoveNext())
                    yield return predicate(e.Current);
        }

        public static Boolean Contains<TSource>(IEnumerable<TSource> source, TSource value)
        {
            if (source == null) throw new ArgumentNullException("source");

            IEqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;
            return Any(source, delegate(TSource item) { return comparer.Equals(item, value); });
        }

        public static IEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return OrderBy(source, keySelector, false);
        }

        public static IEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Boolean isDescending)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");

            TSource[] sourceList = ToArray(source);
            TKey[] keyList = new TKey[sourceList.Length];
            for (int i = 0; i < sourceList.Length; i++)
                keyList[i] = keySelector(sourceList[i]);

            Int32 orderDirection = isDescending ? -1 : 1;
            Comparer<TKey> keyComparer = Comparer<TKey>.Default;

            for (int i = 0; i < sourceList.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Int32 result = keyComparer.Compare(keyList[i], keyList[j]) * orderDirection;
                    if (result < 0)
                    {
                        TSource temp = sourceList[i];
                        sourceList[i] = sourceList[j];
                        sourceList[j] = temp;

                        TKey tempK = keyList[i];
                        keyList[i] = keyList[j];
                        keyList[j] = tempK;
                    }
                }
            }

            // to force array to IEnumerable
            foreach (TSource s in sourceList)
                yield return s;
        }

        public static List<TSource> ToList<TSource>(IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return new List<TSource>(source);
        }

        public static TSource[] ToArray<TSource>(IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            TSource[] arrayResult = new TSource[Count(source)];
            Int32 idx = 0;
            foreach (TSource item in source)
            {
                arrayResult[idx] = item;
                idx++;
            }

            return arrayResult;
        }

        public static IEnumerable<TResult> Cast<TResult>(IEnumerable source)
        {
            if (source == null) throw new ArgumentNullException("source");

            foreach (Object current in source)
                yield return (TResult)current;
        }
    }
}