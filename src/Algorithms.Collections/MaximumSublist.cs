﻿using System;
using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class MaximumSublist
	{
		public static SublistSum<T> GetMaximumSublist<T>(IList<T> list, Func<T, T, T> add)
		{
			return GetMaximumSublist(list, add, Comparer<T>.Default);
		}

		// Ch 4.1, p. 72
		public static SublistSum<T> GetMaximumSublist<T>(IList<T> list, Func<T, T, T> add, IComparer<T> comparer)
		{
			if (list.Count == 0)
				throw new ArgumentException("list has no elements", "list");
			return GetMaximumSublist(list, 0, list.Count - 1, add, comparer);
		}

		public static SublistSum<T> GetMaximumSublist<T>(IList<T> list, int startIndex, int endIndex, Func<T, T, T> add, IComparer<T> comparer)
		{
			if (startIndex == endIndex)
				return SublistSum.Create(startIndex, endIndex, list[startIndex]);

			int middleIndex = (startIndex + endIndex) / 2;
			SublistSum<T> left = GetMaximumSublist(list, startIndex, middleIndex, add, comparer);
			SublistSum<T> right = GetMaximumSublist(list, middleIndex + 1, endIndex, add, comparer);
			SublistSum<T> cross = GetMaximumCrossingSublist(list, startIndex, middleIndex, endIndex, add, comparer);

			if (comparer.Compare(left.Sum, right.Sum) >= 0 && comparer.Compare(left.Sum, cross.Sum) >= 0)
				return left;
			else if (comparer.Compare(right.Sum, left.Sum) >= 0 && comparer.Compare(right.Sum, cross.Sum) >= 0)
				return right;
			else
				return cross;
		}

		// Ex 4.1-2
		public static SublistSum<T> GetMaximumSublistBruteForce<T>(IList<T> list, Func<T, T, T> add, IComparer<T> comparer)
		{
			if (list.Count == 0)
				throw new ArgumentException("list has no elements", "list");

			SublistSum<T>? maxSum = null;
			for (int leftIndex = 0; leftIndex < list.Count; leftIndex++)
			{
				T sum = default(T);
				for (int rightIndex = leftIndex; rightIndex < list.Count; rightIndex++)
				{
					sum = add(sum, list[rightIndex]);
					if (maxSum == null || comparer.Compare(sum, maxSum.Value.Sum) > 0)
						maxSum = new SublistSum<T>(leftIndex, rightIndex, sum);
				}
			}

			return maxSum.Value;
		}

		// Ex 4.1-5
		public static SublistSum<T> GetMaximumSublistLinear<T>(IList<T> list, Func<T, T, T> add, IComparer<T> comparer)
		{
			if (list.Count == 0)
				throw new ArgumentException("list has no elements", "list");

			var currentMax = new SublistSum<T>(0, 0, list[0]);
			var endMax = currentMax;

			for (int index = 1; index < list.Count; index++)
			{
				T value = list[index];
				T endSum = add(endMax.Sum, value);
				endMax = comparer.Compare(endSum, value) < 0 ?
					new SublistSum<T>(index, index, value) :
					new SublistSum<T>(endMax.StartIndex, index, endSum);

				currentMax = comparer.Compare(currentMax.Sum, endMax.Sum) < 0 ? endMax : currentMax;
			}

			return currentMax;
		}

		private static SublistSum<T> GetMaximumCrossingSublist<T>(IList<T> list, int startIndex, int middleIndex, int endIndex, Func<T, T, T> add, IComparer<T> comparer)
		{
			int leftIndex = middleIndex;
			T leftSum = list[middleIndex];

			T sum = leftSum;
			for (int index = leftIndex - 1; index >= startIndex; index--)
			{
				sum = add(sum, list[index]);
				if (comparer.Compare(sum, leftSum) > 0)
				{
					leftIndex = index;
					leftSum = sum;
				}
			}

			int rightIndex = middleIndex + 1;
			T rightSum = list[rightIndex];

			sum = rightSum;
			for (int index = rightIndex + 1; index <= endIndex; index++)
			{
				sum = add(sum, list[index]);
				if (comparer.Compare(sum, rightSum) > 0)
				{
					rightIndex = index;
					rightSum = sum;
				}
			}

			return SublistSum.Create(leftIndex, rightIndex, add(leftSum, rightSum));
		}
	}

	public static class SublistSum
	{
		public static SublistSum<T> Create<T>(int startIndex, int endIndex, T sum)
		{
			return new SublistSum<T>(startIndex, endIndex, sum);
		}
	}

	public struct SublistSum<T>
	{
		public SublistSum(int startIndex, int endIndex, T sum)
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

		public override string ToString()
		{
			return string.Format("{{Start:{0}, End:{1}, Sum:{2}}}", StartIndex, EndIndex, Sum);
		}

		readonly int m_startIndex;
		readonly int m_endIndex;
		readonly T m_sum;
	}
}
