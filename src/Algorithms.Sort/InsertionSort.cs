using System.Collections.Generic;

namespace Algorithms.Sort
{
	public static class InsertionSort
	{
		public static void SortInPlace<T>(IList<T> list)
		{
			SortInPlace(list, Comparer<T>.Default);
		}

		public static void SortInPlace<T>(IList<T> list, IComparer<T> comparer)
		{
			if (list.Count < 2)
				return;

			for (int currentIndex = 1; currentIndex < list.Count; currentIndex++)
			{
				T currentItem = list[currentIndex];

				int insertIndex = currentIndex - 1;
				while (insertIndex >= 0 && comparer.Compare(list[insertIndex], currentItem) > 0)
				{
					list[insertIndex + 1] = list[insertIndex];
					insertIndex--;
				}
				list[insertIndex + 1] = currentItem;
			}
		}
	}
}
