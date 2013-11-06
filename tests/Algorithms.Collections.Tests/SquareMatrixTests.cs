using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;

namespace Algorithms.Collections.Tests
{
	[TestFixture]
	public class SquareMatrixTests
	{
		[TestCaseSource("GetProductTestCases")]
		public void Multiply(TestCase testCase)
		{
			int[,] result = SquareMatrix.Multiply(testCase.Left, testCase.Right, (x, y) => x + y, (x, y) => x * y);
			CollectionAssert.AreEqual(result, testCase.Result);
		}

		[TestCaseSource("GetProductTestCases")]
		public void MultiplyRecursive(TestCase testCase)
		{
			int[,] result = SquareMatrix.MultiplyRecursive(testCase.Left, testCase.Right, (x, y) => x + y, (x, y) => x * y);
			CollectionAssert.AreEqual(result, testCase.Result);
		}

		[TestCaseSource("GetAdditionTestCases")]
		public void Add(TestCase testCase)
		{
			int[,] result = SquareMatrix.Add(testCase.Left, testCase.Right, (x, y) => x + y);
			CollectionAssert.AreEqual(result, testCase.Result);
		}

		public IEnumerable<TestCase> GetAdditionTestCases()
		{
			return new[]
				{
					new TestCase
						{
							Left = new[,] {{2}},
							Right = new[,] {{3}},
							Result = new[,] {{5}},
						},
					new TestCase
						{
							Left = new[,] {{1, 2}, {3, 4}},
							Right = new[,] {{4, 3}, {2, 1}},
							Result = new[,] {{5, 5}, {5, 5}},
						},
					new TestCase
					{
						Left = new[,] {{1, 2}, {7, 5}},
						Right = new[,] {{6, 8}, {4, 2}},
						Result = new[,] {{7, 10}, {11, 7}}
					}
				};
		}

		public IEnumerable<TestCase> GetProductTestCases()
		{
			return new[]
				{
					new TestCase
						{
							Left = new[,] {{2}},
							Right = new[,] {{3}},
							Result = new[,] {{6}},
						},
					new TestCase
						{
							Left = new[,] {{1, 2}, {3, 4}},
							Right = new[,] {{4, 3}, {2, 1}},
							Result = new[,] {{8, 5}, {20, 13}},
						},
					new TestCase
					{
						Left = new[,] {{1, 2}, {7, 5}},
						Right = new[,] {{6, 8}, {4, 2}},
						Result = new[,] {{2 * 7, 2 * 6}, {2 * 31, 2 * 33}}
					}
				};
		}

		public class TestCase
		{
			public int[,] Left { get; set; }
			public int[,] Right { get; set; }
			public int[,] Result { get; set; }

			public override string ToString()
			{
				return string.Format("{{Left={0}, Right={1}, Result={2}}}", RenderMatrix(Left), RenderMatrix(Right), RenderMatrix(Result));
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
