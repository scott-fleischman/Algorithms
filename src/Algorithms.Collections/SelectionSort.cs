using System;
using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class SelectionSort
	{
		// Ex 2.2-2, p. 29
		public static void Sort<T>(IList<T> list, IComparer<T> comparer)
		{
			if (list.Count <= 1)
				return;

			for (int index = 0; index < list.Count - 1; index++)
			{
				int minIndex = IndexOfMinimum(list, index, comparer);
				Utility.Swap(list, index, minIndex);
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
	}
}
