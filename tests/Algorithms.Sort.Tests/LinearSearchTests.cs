using NUnit.Framework;

namespace Algorithms.Sort.Tests
{
	[TestFixture]
	public class LinearSearchTests
	{
		[TestCase(4, new[] { 1, 2, 3, 4 }, 3)]
		[TestCase(5, new[] { 1, 2, 3, 4 }, null)]
		[TestCase(0, new[] { 1, 2, 3, 4 }, null)]
		[TestCase(1, new[] { 1, 2, 3, 4, 1 }, 0)]
		public void TestNull(int value, int[] values, int? expectedIndex)
		{
			int? result = LinearSearch.FindIndex(value, values);
			Assert.That(result, Is.EqualTo(expectedIndex));
		}
	}
}
