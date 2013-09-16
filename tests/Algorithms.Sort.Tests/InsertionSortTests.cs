using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithms.Sort.Tests
{
	public class InsertionSortTests
	{
		[Test, TestCaseSource(typeof(SortTestCases), "Integer")]
		public void TestSortInPlace(SortTestCase<int> testCase)
		{
			var list = new List<int>(testCase.Items);
			InsertionSort.SortInPlace(list);
			Assert.That(list, Is.EqualTo(testCase.ExpectedOrder));
		}
	}
}
