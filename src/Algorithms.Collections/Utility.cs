using System.Collections.Generic;

namespace Algorithms.Collections
{
	internal static class Utility
	{
		internal static void Swap<T>(IList<T> items, int first, int second)
		{
			if (first == second)
				return;

			T temp = items[first];
			items[first] = items[second];
			items[second] = temp;
		}
	}
}
