using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Collections
{
	public static class MergeSort
	{
		// 2.3.1, pp. 31-34
		public static void Sort<T>(IList<T> items, IComparer<T> comparer)
		{
			MergeSortInPlace(items, 0, items.Count - 1, comparer);
		}

		public static IEnumerable<T> OrderBy<T>(IEnumerable<T> items, IComparer<T> comparer)
		{
			int count = items.Count();
			if (count <= 1)
				return items;

			int leftCount = count / 2;
			return Merge(
				OrderBy(items.Take(leftCount), comparer),
				OrderBy(items.Skip(leftCount), comparer),
				comparer);
		}

		private static IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer)
		{
			using (IEnumerator<T> leftState = left.GetEnumerator())
			using (IEnumerator<T> rightState = right.GetEnumerator())
			{
				bool hasLeft = leftState.MoveNext();
				bool hasRight = rightState.MoveNext();

				while (hasLeft || hasRight)
				{
					bool getFromLeft = hasLeft &&
						((hasRight && comparer.Compare(leftState.Current, rightState.Current) <= 0) ||
						!hasRight);

					if (getFromLeft)
					{
						yield return leftState.Current;
						hasLeft = leftState.MoveNext();
					}
					else
					{
						yield return rightState.Current;
						hasRight = rightState.MoveNext();
					}
				}
			}
		}

		private static void MergeSortInPlace<T>(IList<T> items, int left, int right, IComparer<T> comparer)
		{
			if (left < right)
			{
				int middle = (left + right) / 2;
				MergeSortInPlace(items, left, middle, comparer);
				MergeSortInPlace(items, middle + 1, right, comparer);
				Merge(items, left, middle, right, comparer);
			}
		}

		private static void Merge<T>(IList<T> items, int leftEnd, int middle, int rightEnd, IComparer<T> comparer)
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
