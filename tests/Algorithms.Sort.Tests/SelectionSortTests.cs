using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithms.Sort.Tests
{
	[TestFixture]
	public class SelectionSortTests
	{
		[Test, TestCaseSource(typeof(SortTestCases), "Integer")]
		public void TestSortInPlace(SortTestCase<int> testCase)
		{
			var list = new List<int>(testCase.Items);
			SelectionSort.SortInPlace(list);
			Assert.That(list, Is.EqualTo(testCase.ExpectedOrder));
		}
	}
}
