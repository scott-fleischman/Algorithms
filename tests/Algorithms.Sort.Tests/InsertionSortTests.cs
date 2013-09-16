using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithms.Sort.Tests
{
	public class InsertionSortTests
	{
		[TestCase(new[] { 5, 2, 4, 6, 1, 3 }, new[] { 1, 2, 3, 4, 5, 6 })]
		[TestCase(new[] { 31, 41, 59, 26, 41, 58 }, new[] { 26, 31, 41, 41, 58, 59 })]
		public void TestSortInPlace(int[] elements, int[] expected)
		{
			var list = new List<int>(elements);
			InsertionSort.SortInPlace(list);
			Assert.That(list, Is.EqualTo(expected));
		}
	}
}
