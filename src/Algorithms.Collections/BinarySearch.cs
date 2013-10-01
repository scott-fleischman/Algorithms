using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class BinarySearch
	{
		// Ex 2.3-5
		public static int? FindIndex<T>(IList<T> list, T value, IComparer<T> comparer)
		{
			int start = 0;
			int end = list.Count - 1;

			while (start <= end)
			{
				int middle = start + (end - start) / 2;
				int result = comparer.Compare(list[middle], value);

				if (result < 0)
					start = middle + 1;
				else if (result > 0)
					end = middle - 1;
				else
					return middle;
			}

			return null;
		}
	}
}
