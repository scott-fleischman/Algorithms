using System.Collections.Generic;

namespace Algorithms.Collections
{
	public interface IOrderByAlgorithm
	{
		IEnumerable<T> OrderByAlgorithm<T>(IEnumerable<T> items, IComparer<T> comparer);
	}
}
