using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algorithms.Collections.Tests
{
	[TestFixture]
	public class SortAlgorithmTests
	{
		[Test]
		public void SortAlgorithmInt32(
			[ValueSource("GetInt32SortAlgorithms")] NamedSortAlgorithm<int> namedSortAlgorithm,
			[ValueSource(typeof(SortTestCases), "Int32")] SortTestCase<int> testCase)
		{
			var list = new List<int>(testCase.Items);
			list.SortByAlgorithm(namedSortAlgorithm.Algorithm);
			Assert.That(list, Is.EqualTo(testCase.ExpectedOrder));
		}

		[Test]
		public void OrderByAlgorithmInt32(
			[ValueSource("GetInt32OrderByAlgorithms")] NamedOrderByAlgorithm<int> namedOrderByAlgorithm,
			[ValueSource(typeof(SortTestCases), "Int32")] SortTestCase<int> testCase)
		{
			Assert.That(
				testCase.Items.OrderByAlgorithm(namedOrderByAlgorithm.Algorithm).ToList(),
				Is.EqualTo(testCase.ExpectedOrder));
		}

		private static IEnumerable<NamedSortAlgorithm<int>> GetInt32SortAlgorithms()
		{
			return GetGenericSortAlgorithms<int>();
		}

		private static IEnumerable<NamedOrderByAlgorithm<int>> GetInt32OrderByAlgorithms()
		{
			return GetGenericOrderByAlgorithms<int>();
		}

		private static IEnumerable<NamedSortAlgorithm<T>> GetGenericSortAlgorithms<T>()
		{
			return new[]
				{
					new NamedSortAlgorithm<T>("InsertionSort.Sort", InsertionSort.Sort),
					new NamedSortAlgorithm<T>("InsertionSort.SortRecursive", InsertionSort.SortRecursive),
					new NamedSortAlgorithm<T>("MergeSort.Sort", MergeSort.Sort),
					new NamedSortAlgorithm<T>("SelectionSort.Sort", SelectionSort.Sort),
				};
		}

		private static IEnumerable<NamedOrderByAlgorithm<T>> GetGenericOrderByAlgorithms<T>()
		{
			return new[]
				{
					new NamedOrderByAlgorithm<T>("MergeSort.OrderBy", MergeSort.OrderBy),
				};
		}
	}
}
