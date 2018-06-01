using System;
using System.Collections.Generic;

namespace Bartok
{
    public static class ListExtension
    {
        private static Random _random = new Random();

        public static void MoveToEnd<T>(this List<T> list, T element)
        {
            if (list.Contains(element))
            {
                int index = list.IndexOf(element);
                list.RemoveAt(index);
                list.Insert(list.Count, element);
            }
        }

        public static List<T> PickRandom<T>(this List<T> list, int count)
        {
            if (list.Count <= count)
            {
                return list;
            }
            List<T> list2 = new List<T>(list);
            list2.Shuffle<T>(null);
            return list2.GetRange(0, count);
        }

        public static void Shuffle<T>(this List<T> list, Random existingRandom = null)
        {
            Random random = (existingRandom == null) ? _random : existingRandom;
            for (int i = list.Count; i > 1; i--)
            {
                int num2 = random.Next(i);
                T local = list[num2];
                list[num2] = list[i - 1];
                list[i - 1] = local;
            }
        }
    }
}

