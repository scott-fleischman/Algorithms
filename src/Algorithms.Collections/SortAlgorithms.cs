using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class SortAlgorithms
	{
		public static void SortByAlgorithm<T>(this IList<T> source, ISortInPlace algorithm)
		{
			SortByAlgorithm(source, Comparer<T>.Default, algorithm);
		}

		public static void SortByAlgorithm<T>(this IList<T> source, IComparer<T> comparer, ISortInPlace algorithm)
		{
			algorithm.SortInPlace(source, comparer);
		}

		public static IEnumerable<T> OrderByAlgorithm<T>(this IEnumerable<T> source, IOrderByAlgorithm algorithm)
		{
			return OrderByAlgorithm(source, Comparer<T>.Default, algorithm);
		}

		public static IEnumerable<T> OrderByAlgorithm<T>(this IEnumerable<T> source, IComparer<T> comparer, IOrderByAlgorithm algorithm)
		{
			return algorithm.OrderByAlgorithm(source, comparer);
		}

		public static InsertionSort InsertionSort
		{
			get { return s_insertionSort; }
		}

		public static MergeSort MergeSort
		{
			get { return s_mergeSort; }
		}

		public static SelectionSort SelectionSort
		{
			get { return s_selectionSort; }
		}

		static readonly InsertionSort s_insertionSort = new InsertionSort();
		static readonly MergeSort s_mergeSort = new MergeSort();
		static readonly SelectionSort s_selectionSort = new SelectionSort();
	}
}
