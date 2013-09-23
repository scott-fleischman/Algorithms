using System.Collections.Generic;

namespace Algorithms.Sort
{
	public class MergeSort : ISortInPlace
	{
		// 2.3.1, pp. 31-34
		public void SortInPlace<T>(IList<T> items, IComparer<T> comparer)
		{
			MergeSortInPlace(items, 0, items.Count - 1, comparer);
		}

		private void MergeSortInPlace<T>(IList<T> items, int left, int right, IComparer<T> comparer)
		{
			if (left < right)
			{
				int middle = (left + right) / 2;
				MergeSortInPlace(items, left, middle, comparer);
				MergeSortInPlace(items, middle + 1, right, comparer);
				Merge(items, left, middle, right, comparer);
			}
		}

		private void Merge<T>(IList<T> items, int leftEnd, int middle, int rightEnd, IComparer<T> comparer)
		{
			T[] leftItems = Slice(items, leftEnd, middle);
			T[] rightItems = Slice(items, middle + 1, rightEnd);

			int left = 0;
			int right = 0;
			for (int index = leftEnd; index <= rightEnd; index++)
			{
				bool getFromLeft = left < leftItems.Length &&
					((right < rightItems.Length && comparer.Compare(leftItems[left], rightItems[right]) <= 0) ||
					right >= rightItems.Length);

				if (getFromLeft)
				{
					items[index] = leftItems[left];
					left++;
				}
				else
				{
					items[index] = rightItems[right];
					right++;
				}
			}
		}

		private static T[] Slice<T>(IList<T> items, int start, int end)
		{
			var slice = new T[end - start + 1];
			for (int index = 0; index < slice.Length; index++)
				slice[index] = items[start + index];
			return slice;
		}
	}
}
