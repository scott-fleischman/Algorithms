using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algorithms.Collections.Tests
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

		[Test]
		public void OrderByAlgorithmInteger(
			[ValueSource("GetOrderByAlgorithms")] IOrderByAlgorithm algorithm,
			[ValueSource(typeof(SortTestCases), "Integer")] SortTestCase<int> testCase)
		{
			Assert.That(
				testCase.Items.OrderByAlgorithm(algorithm).ToList(),
				Is.EqualTo(testCase.ExpectedOrder));
		}

		private static IEnumerable<ISortInPlace> GetSortAlgorithms()
		{
			return new ISortInPlace[]
				{
					SortAlgorithms.InsertionSort,
					SortAlgorithms.MergeSort,
					SortAlgorithms.SelectionSort,
				};
		}

		private static IEnumerable<IOrderByAlgorithm> GetOrderByAlgorithms()
		{
			return new IOrderByAlgorithm[]
				{
					SortAlgorithms.MergeSort,
				};
		}
	}
}
