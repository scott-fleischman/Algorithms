﻿using System.Collections.Generic;

namespace Algorithms.Collections
{
	public static class BubbleSort
	{
		public static void Sort<T>(IList<T> list, IComparer<T> comparer)
		{
			for (int i = 0; i < list.Count - 1; i++)
			{
				for (int j = list.Count - 1; j >= i + 1; j--)
				{
					if (comparer.Compare(list[j], list[j - 1]) < 0)
						Utility.Swap(list, j, j - 1);
				}
			}
		}
	}
}
