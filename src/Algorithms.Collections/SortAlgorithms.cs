using System;
using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class SortAlgorithms
	{
		public static void SortByAlgorithm<T>(
			this IList<T> source,
			Action<IList<T>, IComparer<T>> algorithm)
		{
			SortByAlgorithm(source, Comparer<T>.Default, algorithm);
		}

		public static void SortByAlgorithm<T>(
			this IList<T> source,
			IComparer<T> comparer,
			Action<IList<T>, IComparer<T>> algorithm)
		{
			algorithm(source, comparer);
		}

		public static IEnumerable<T> OrderByAlgorithm<T>(
			this IEnumerable<T> source,
			Func<IEnumerable<T>, IComparer<T>, IEnumerable<T>> algorithm)
		{
			return OrderByAlgorithm(source, Comparer<T>.Default, algorithm);
		}

		public static IEnumerable<T> OrderByAlgorithm<T>(
			this IEnumerable<T> source,
			IComparer<T> comparer,
			Func<IEnumerable<T>, IComparer<T>, IEnumerable<T>> algorithm)
		{
			return algorithm(source, comparer);
		}
	}
}
