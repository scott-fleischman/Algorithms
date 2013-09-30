using System.Collections.Generic;

namespace Algorithms.Collections.Tests
{
	public class SortTestCases
	{
		public static IEnumerable<SortTestCase<int>> Int32
		{
			get
			{
				return new[]
					{
						new SortTestCase<int>(new int[0], new int[0]),
						new SortTestCase<int>(new[] { 0 }, new[] { 0 }),
						new SortTestCase<int>(new[] { 0, 1 }, new[] { 0, 1 }),
						new SortTestCase<int>(new[] { 1, 0 }, new[] { 0, 1 }),
						new SortTestCase<int>(new[] { -1, 0 }, new[] { -1, 0 }),
						new SortTestCase<int>(new[] { 5, 2, 4, 6, 1, 3 }, new[] { 1, 2, 3, 4, 5, 6 }),
						new SortTestCase<int>(new[] { 31, 41, 59, 26, 41, 58 }, new[] { 26, 31, 41, 41, 58, 59 }),
						new SortTestCase<int>(new[] { int.MaxValue, int.MinValue, 0 }, new[] { int.MinValue, 0, int.MaxValue }), 
					};
			}
		}
	}
}
