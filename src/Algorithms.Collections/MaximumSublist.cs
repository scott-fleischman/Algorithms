using System;
using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class MaximumSublist
	{
		// Ch 4.1, p. 72
		public static SublistResult<T> GetMaximumSublist<T>(IList<T> list, Func<T, T, T> add, IComparer<T> comparer)
		{
			if (list.Count == 0)
				throw new ArgumentException("list has no elements", "list");
			return GetMaximumSublist(list, 0, list.Count - 1, add, comparer);
		}

		public static SublistResult<T> GetMaximumSublist<T>(IList<T> list, int startIndex, int endIndex, Func<T, T, T> add, IComparer<T> comparer)
		{
			if (startIndex == endIndex)
				return new SublistResult<T>(startIndex, endIndex, list[startIndex]);

			int middleIndex = (startIndex + endIndex) / 2;
			SublistResult<T> left = GetMaximumSublist(list, startIndex, middleIndex, add, comparer);
			SublistResult<T> right = GetMaximumSublist(list, middleIndex + 1, endIndex, add, comparer);
			SublistResult<T> cross = GetMaximumCrossingSublist(list, startIndex, middleIndex, endIndex, add, comparer);

			if (comparer.Compare(left.Sum, right.Sum) >= 0 && comparer.Compare(left.Sum, cross.Sum) >= 0)
				return left;
			else if (comparer.Compare(right.Sum, left.Sum) >= 0 && comparer.Compare(right.Sum, cross.Sum) >= 0)
				return right;
			else
				return cross;
		}

		private static SublistResult<T> GetMaximumCrossingSublist<T>(IList<T> list, int startIndex, int middleIndex, int endIndex, Func<T, T, T> add, IComparer<T> comparer)
		{
			throw new NotImplementedException();
		}
	}

	public class SublistResult<T>
	{
		public SublistResult(int startIndex, int endIndex, T sum)
		{
			m_startIndex = startIndex;
			m_endIndex = endIndex;
			m_sum = sum;
		}

		public int StartIndex
		{
			get { return m_startIndex; }
		}

		public int EndIndex
		{
			get { return m_endIndex; }
		}

		public T Sum
		{
			get { return m_sum; }
		}

		readonly int m_startIndex;
		readonly int m_endIndex;
		readonly T m_sum;
	}
}
