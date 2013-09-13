using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithms.Sort.Tests
{
	public class InsertionSortTests
	{
		[Test]
		public void TestSortInPlace()
		{
			var expected = new[] { 1, 2, 3, 4, 5, 6 };
			var list = new List<int> { 5, 2, 4, 6, 1, 3 };
			InsertionSort.SortInPlace(list);
			Assert.That(list, Is.EqualTo(expected));
		}
	}
}
