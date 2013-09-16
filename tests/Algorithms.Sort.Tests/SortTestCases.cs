using System.Collections.Generic;

namespace Algorithms.Sort.Tests
{
	public class SortTestCases
	{
		public static IEnumerable<SortTestCase<int>> Integer
		{
			get
			{
				return new[]
					{
						new SortTestCase<int>(new[] { 5, 2, 4, 6, 1, 3 }, new[] { 1, 2, 3, 4, 5, 6 }),
						new SortTestCase<int>(new[] { 31, 41, 59, 26, 41, 58 }, new[] { 26, 31, 41, 41, 58, 59 })
					};
			}
		}
	}
}