using System.Collections.Generic;

namespace Algorithms.Sort
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

		public static InsertionSort InsertionSort
		{
			get { return s_insertionSort; }
		}

		public static SelectionSort SelectionSort
		{
			get { return s_selectionSort; }
		}

		static readonly InsertionSort s_insertionSort = new InsertionSort();
		static readonly SelectionSort s_selectionSort = new SelectionSort();
	}
}
