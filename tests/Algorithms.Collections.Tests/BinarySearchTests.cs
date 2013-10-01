using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Algorithms.Collections.Tests
{
	[TestFixture]
	public class BinarySearchTests
	{
		[TestCase("", 1, null)]
		[TestCase("1", 1, 0)]
		[TestCase("1", 2, null)]
		[TestCase("1, 2", 1, 0)]
		[TestCase("1, 2", 2, 1)]
		[TestCase("1, 2, 3", 3, 2)]
		[TestCase("1, 10, 100, 1000", 100, 2)]
		[TestCase("-1, 0, 1", 0, 1)]
		[TestCase("-2147483648, 0, 2147483647", -2147483648, 0)]
		public void Test(string valuesString, int value, int? expectedIndex)
		{
			List<int> values = valuesString.Split(',')
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.Select(x => int.Parse(x.Trim(), CultureInfo.InvariantCulture))
				.ToList();
			int? result = BinarySearch.FindIndex(values, value, Comparer<int>.Default);
			Assert.That(result, Is.EqualTo(expectedIndex));
		}
	}
}
