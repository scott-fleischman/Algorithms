using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
	public class MergeSort : ISortInPlace
	{
		// 2.3.1, pp. 31-34
		public void SortInPlace<T>(IList<T> items, IComparer<T> comparer)
		{
			MergeSortInPlace(items, 0, items.Count - 1, new ItemSentinelComparer<T>(comparer));
		}

		private void MergeSortInPlace<T>(IList<T> items, int left, int right, ItemSentinelComparer<T> comparer)
		{
			if (left < right)
			{
				int middle = (left + right) / 2;
				MergeSortInPlace(items, left, middle, comparer);
				MergeSortInPlace(items, middle + 1, right, comparer);
				Merge(items, left, middle, right, comparer);
			}
		}

		private void Merge<T>(IList<T> items, int leftEnd, int middle, int rightEnd, ItemSentinelComparer<T> comparer)
		{
			int leftSize = middle - leftEnd + 1;
			int rightSize = rightEnd - middle;

			var leftItems = new ItemSentinel<T>[leftSize + 1];
			var rightItems = new ItemSentinel<T>[rightSize + 1];
			int left;
			int right;
			for (left = 0; left < leftSize; left++)
				leftItems[left] = new ItemSentinel<T>(items[leftEnd + left]);
			for (right = 0; right < rightSize; right++)
				rightItems[right] = new ItemSentinel<T>(items[middle + right + 1]);

			leftItems[leftSize] = new ItemSentinel<T>();
			rightItems[rightSize] = new ItemSentinel<T>();

			left = 0;
			right = 0;
			for (int current = leftEnd; current <= rightEnd; current++)
			{
				if (comparer.Compare(leftItems[left], rightItems[right]) <= 0)
				{
					items[current] = leftItems[left].Item;
					left++;
				}
				else
				{
					items[current] = rightItems[right].Item;
					right++;
				}
			}
		}

		private class ItemSentinelComparer<T> : IComparer<ItemSentinel<T>>
		{
			public ItemSentinelComparer(IComparer<T> comparer)
			{
				m_comparer = comparer;
			}

			public int Compare(ItemSentinel<T> x, ItemSentinel<T> y)
			{
				if (x.IsSentinel && !y.IsSentinel)
					return 1;
				if (!x.IsSentinel && y.IsSentinel)
					return -1;
				if (x.IsSentinel && y.IsSentinel)
					return 0;
				return m_comparer.Compare(x.Item, y.Item);
			}

			readonly IComparer<T> m_comparer;
		}

		private class ItemSentinel<T>
		{
			public ItemSentinel()
			{
				m_isSentinel = true;
			}

			public ItemSentinel(T item)
			{
				m_item = item;
			}

			public bool IsSentinel
			{
				get { return m_isSentinel; }
			}

			public T Item
			{
				get
				{
					if (m_isSentinel)
						throw new InvalidOperationException("Must not get Item for a sentinel value");

					return m_item;
				}
			}

			readonly bool m_isSentinel;
			readonly T m_item;
		}
	}
}
