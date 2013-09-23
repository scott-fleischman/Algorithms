using System.Collections.Generic;

namespace Algorithms.Sort
{
	public interface ISortInPlace
	{
		void SortInPlace<T>(IList<T> items, IComparer<T> comparer);
	}
}
