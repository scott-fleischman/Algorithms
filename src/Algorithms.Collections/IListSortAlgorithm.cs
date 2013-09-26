using System.Collections.Generic;

namespace Algorithms.Collections
{
	public interface IListSortAlgorithm
	{
		void SortByAlgorithm<T>(IList<T> items, IComparer<T> comparer);
	}
}
