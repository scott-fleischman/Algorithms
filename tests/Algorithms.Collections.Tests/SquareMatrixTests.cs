using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;

namespace Algorithms.Collections.Tests
{
	[TestFixture]
	public class SquareMatrixTests
	{
		[TestCaseSource("GetTestCases")]
		public void Multiply(TestCase testCase)
		{
			int[,] result = SquareMatrix.Multiply(testCase.Left, testCase.Right, (x, y) => x + y, (x, y) => x * y);
			CollectionAssert.AreEqual(result, testCase.Product);
		}

		public IEnumerable<TestCase> GetTestCases()
		{
			return new[]
				{
					new TestCase
						{
							Left = new[,] {{1, 2}, {3, 4}},
							Right = new[,] {{4, 3}, {2, 1}},
							Product = new[,] {{8, 5}, {20, 13}},
						},
				};
		}

		public class TestCase
		{
			public int[,] Left { get; set; }
			public int[,] Right { get; set; }
			public int[,] Product { get; set; }

			public override string ToString()
			{
				return string.Format("{{Left={0}, Right={1}, Product={2}}}", RenderMatrix(Left), RenderMatrix(Right), RenderMatrix(Product));
			}

			private static string RenderMatrix(int[,] matrix)
			{
				var builder = new StringBuilder();
				builder.Append("{");
				for (int row = 0; row < matrix.GetLength(0); row++)
				{
					if (row != 0)
						builder.Append(",");
					builder.Append("{");
					for (int column = 0; column < matrix.GetLength(1); column++)
					{
						if (column != 0)
							builder.Append(",");
						builder.Append(matrix[row, column]);
					}
					builder.Append("}");
				}
				builder.Append("}");
				return builder.ToString();
			}

			private static string RenderRow(int row, int[,] matrix, int padding)
			{
				var builder = new StringBuilder();
				if (row < matrix.GetLength(row))
				{
					for (int column = 0; column < matrix.GetLength(1); column++)
						builder.Append(matrix[row, column].ToString(CultureInfo.InvariantCulture).PadLeft(padding));
				}
				return builder.ToString();
			}
		}
	}
}
