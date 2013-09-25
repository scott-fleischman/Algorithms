using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algorithms.Collections.Tests
{
	[TestFixture]
	public class DigitAdditionTests
	{
		[TestCase("0", "0", "0")]
		[TestCase("0", "1", "1")]
		[TestCase("1", "0", "1")]
		[TestCase("1", "1", "10")]
		[TestCase("10", "1", "11")]
		[TestCase("10", "10", "100")]
		[TestCase("11", "1", "100")]
		[TestCase("11", "10", "101")]
		[TestCase("11", "11", "110")]
		[TestCase("1111", "1", "10000")]
		public static void Base2(string left, string right, string expected)
		{
			Func<string, IEnumerable<int>> toValues = x => x.AsEnumerable().Reverse().Select(c => c - '0');
			Assert.That(DigitAddition.Add(toValues(left), toValues(right)), Is.EqualTo(toValues(expected)));
		}

		[TestCase("0", "0", "0")]
		[TestCase("0", "1", "1")]
		[TestCase("1", "1", "2")]
		[TestCase("2", "1", "3")]
		[TestCase("2", "2", "4")]
		[TestCase("8", "1", "9")]
		[TestCase("8", "2", "10")]
		[TestCase("999", "1", "1000")]
		public static void Base10(string left, string right, string expected)
		{
			Func<string, IEnumerable<int>> toValues = x => x.AsEnumerable().Reverse().Select(c => c - '0');
			Assert.That(DigitAddition.Add(toValues(left), toValues(right), 10), Is.EqualTo(toValues(expected)));
		}
	}
}
