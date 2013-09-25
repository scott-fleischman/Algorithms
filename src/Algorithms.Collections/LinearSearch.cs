using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class LinearSearch
	{
		// Ex 2.1-3, p.22
		public static int? FindIndex(int value, IEnumerable<int> values)
		{
			int index = 0;
			foreach (var currentValue in values)
			{
				if (currentValue == value)
					return index;
				index++;
			}
			return null;
		}
	}
}
