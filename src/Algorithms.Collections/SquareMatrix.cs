using System;

namespace Algorithms.Collections
{
	public static class SquareMatrix
	{
		public static T[,] Multiply<T>(T[,] A, T[,] B, Func<T, T, T> add, Func<T, T, T> multiply)
		{
			ValidateAreEqualSquare(A, B);

			int length = A.GetLength(0);
			var C = new T[length,length];

			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length; j++)
				{
					C[i, j] = default(T);
					for (int k = 0; k < length; k++)
						C[i, j] = add(C[i, j], multiply(A[i, k], B[k, j]));
				}
			}

			return C;
		}

		public static T[,] Add<T>(T[,] left, T[,] right, Func<T, T, T> add)
		{
			ValidateAreEqualSquare(left, right);

			int length = left.GetLength(0);
			var result = new T[length,length];

			for (int row = 0; row < left.GetLength(0); row++)
			{
				for (int column = 0; column < left.GetLength(1); column++)
					result[row, column] = add(left[row, column], right[row, column]);
			}

			return result;
		}

		private static void ValidateAreEqualSquare<T>(T[,] A, T[,] B)
		{
			if (!IsSquare(A))
				throw new ArgumentException("A must be square", "A");
			if (!IsSquare(B))
				throw new ArgumentException("B must be square", "B");
			int aSize = A.GetLength(0);
			int bSize = B.GetLength(0);
			if (aSize != bSize)
				throw new ArgumentException("A and B must be the same size", "A");
		}

		private static bool IsSquare<T>(T[,] array)
		{
			int rowLength = array.GetLength(0);
			int columnLength = array.GetLength(1);
			return rowLength == columnLength;
		}
	}
}
