using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithms.Sort.Tests
{
	[TestFixture]
	public class SortAlgorithmTests
	{
		[Test]
		public void SortInPlaceInteger(
			[ValueSource("GetSortAlgorithms")] ISortInPlace algorithm,
			[ValueSource(typeof(SortTestCases), "Integer")] SortTestCase<int> testCase)
		{
			var list = new List<int>(testCase.Items);
			list.SortByAlgorithm(algorithm);
			Assert.That(list, Is.EqualTo(testCase.ExpectedOrder));
		}

		private static IEnumerable<ISortInPlace> GetSortAlgorithms()
		{
			return new ISortInPlace[]
				{
					SortAlgorithms.InsertionSort,
					SortAlgorithms.SelectionSort,
				};
		}
	}
}
