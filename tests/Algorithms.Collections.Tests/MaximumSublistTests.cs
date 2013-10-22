using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algorithms.Collections.Tests
{
	[TestFixture]
	public class MaximumSublistTests
	{
		[TestCaseSource("GetTestCases_Int32")]
		public void GetMaximumSublist_Int32(TestCase<int> testCase)
		{
			var result = MaximumSublist.GetMaximumSublist(testCase.Items, (x, y) => x + y);
			Assert.AreEqual(testCase.Expected.StartIndex, result.StartIndex);
			Assert.AreEqual(testCase.Expected.EndIndex, result.EndIndex);
			Assert.AreEqual(testCase.Expected.Sum, result.Sum);
		}

		public IEnumerable<TestCase<int>> GetTestCases_Int32()
		{
			return new[]
				{
					TestCase.Create(new[] { 1 }, SublistSum.Create(0, 0, 1)),
					TestCase.Create(new[] { 1, 2 }, SublistSum.Create(0, 1, 3)),
					TestCase.Create(new[] { 1, -2 }, SublistSum.Create(0, 0, 1)),
					TestCase.Create(new[] { 1, -4, 3, -4 }, SublistSum.Create(2, 2, 3)),
					TestCase.Create(new[] { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 },
						SublistSum.Create(7, 10, 18 + 20 + -7 + 12)),
				};
		}

		public static class TestCase
		{
			public static TestCase<T> Create<T>(IEnumerable<T> items, SublistSum<T> expected)
			{
				return new TestCase<T>(items, expected);
			}
		}

		public class TestCase<T>
		{
			public TestCase(IEnumerable<T> items, SublistSum<T> expected)
			{
				m_items = items.ToList();
				m_expected = expected;
			}

			public IList<T> Items
			{
				get { return m_items; }
			}

			public SublistSum<T> Expected
			{
				get { return m_expected; }
			}

			readonly IList<T> m_items;
			readonly SublistSum<T> m_expected;
		}
	}
}
