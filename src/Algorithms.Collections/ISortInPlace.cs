using System.Collections.Generic;

namespace Algorithms.Collections
{
	public interface ISortInPlace
	{
		void SortInPlace<T>(IList<T> items, IComparer<T> comparer);
	}
}
