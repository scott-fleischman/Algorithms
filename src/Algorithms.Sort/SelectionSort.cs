using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
	public class SelectionSort
	{
		public static void SortInPlace<T>(IList<T> list)
		{
			SortInPlace(list, Comparer<T>.Default);
		}

		// Ex 2.2-2, p. 29
		public static void SortInPlace<T>(IList<T> list, IComparer<T> comparer)
		{
			if (list.Count <= 1)
				return;

			for (int index = 0; index < list.Count - 1; index++)
			{
				int minIndex = IndexOfMinimum(list, index, comparer);
				Swap(list, index, minIndex);
			}
		}

		private static int IndexOfMinimum<T>(IList<T> items, int startIndex, IComparer<T> comparer)
		{
			if (items.Count - startIndex == 0)
				throw new ArgumentException("items must not be empty", "items");
			if (items.Count - startIndex == 1)
				return startIndex;

			int minIndex = startIndex;
			for (int index = startIndex + 1; index < items.Count; index++)
			{
				T item = items[index];
				if (comparer.Compare(items[minIndex], item) > 0)
					minIndex = index;
			}
			return minIndex;
		}

		private static void Swap<T>(IList<T> items, int first, int second)
		{
			if (first == second)
				return;

			T temp = items[first];
			items[first] = items[second];
			items[second] = temp;
		}
	}
}
