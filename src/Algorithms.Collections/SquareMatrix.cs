using System;

namespace Algorithms.Collections
{
	public static class SquareMatrix
	{
		public static T[,] Multiply<T>(T[,] A, T[,] B, Func<T, T, T> add, Func<T, T, T> multiply)
		{
			ValidateAreEqualSquare(A, B);

			int length = A.GetLength(0);
			var C = new T[length, length];

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

		public static T[,] MultiplyRecursive<T>(T[,] A, T[,] B, Func<T, T, T> add, Func<T, T, T> multiply)
		{
			ValidateAreEqualSquare(A, B);
			int length = A.GetLength(0);

			if (length == 1)
				return new[,] { { multiply(A[0, 0], B[0, 0]) } };

			var partitionedA = Partition(A);
			var partitionedB = Partition(B);

			var resultTopLeft = Add(
				MultiplyRecursive(partitionedA.TopLeft, partitionedB.TopLeft, add, multiply),
				MultiplyRecursive(partitionedA.TopRight, partitionedB.BottomLeft, add, multiply),
				add);
			var resultTopRight = Add(
				MultiplyRecursive(partitionedA.TopLeft, partitionedB.TopRight, add, multiply),
				MultiplyRecursive(partitionedA.TopRight, partitionedB.BottomRight, add, multiply),
				add);
			var resultBottomLeft = Add(
				MultiplyRecursive(partitionedA.BottomLeft, partitionedB.TopLeft, add, multiply),
				MultiplyRecursive(partitionedA.BottomRight, partitionedB.BottomLeft, add, multiply),
				add);
			var resultBottomRight = Add(
				MultiplyRecursive(partitionedA.BottomLeft, partitionedB.TopRight, add, multiply),
				MultiplyRecursive(partitionedA.BottomRight, partitionedB.BottomRight, add, multiply),
				add);

			return Combine(new PartitionedMatrix<T>(resultTopLeft, resultTopRight, resultBottomLeft, resultBottomRight));
		}

		public static T[,] Add<T>(T[,] left, T[,] right, Func<T, T, T> add)
		{
			ValidateAreEqualSquare(left, right);

			int length = left.GetLength(0);
			var result = new T[length, length];

			for (int row = 0; row < left.GetLength(0); row++)
			{
				for (int column = 0; column < left.GetLength(1); column++)
					result[row, column] = add(left[row, column], right[row, column]);
			}

			return result;
		}

		private static PartitionedMatrix<T> Partition<T>(T[,] matrix)
		{
			int rowLength = matrix.GetLength(0);
			int columnLength = matrix.GetLength(1);

			int topRowLength = rowLength / 2;
			int topRowIndex = 0;

			int bottomRowLength = rowLength - topRowLength;
			int bottomRowIndex = topRowLength;

			int leftColumnLength = columnLength / 2;
			int leftColumnIndex = 0;

			int rightColumnLength = columnLength - leftColumnLength;
			int rightColumnIndex = leftColumnLength;

			return new PartitionedMatrix<T>(
				topLeft: Extract(matrix, topRowIndex, topRowLength, leftColumnIndex, leftColumnLength),
				topRight: Extract(matrix, topRowIndex, topRowLength, rightColumnIndex, rightColumnLength),
				bottomLeft: Extract(matrix, bottomRowIndex, bottomRowLength, leftColumnIndex, leftColumnLength),
				bottomRight: Extract(matrix, bottomRowIndex, bottomRowLength, rightColumnIndex, rightColumnLength));
		}

		private static T[,] Combine<T>(PartitionedMatrix<T> matrix)
		{
			int topRowLength = matrix.TopLeft.GetLength(0);
			int topRowIndex = 0;

			int bottomRowLength = matrix.BottomLeft.GetLength(0);
			int bottomRowIndex = topRowLength;

			int leftColumnLength = matrix.TopLeft.GetLength(1);
			int leftColumnIndex = 0;

			int rightColumnLength = matrix.TopRight.GetLength(1);
			int rightColumnIndex = leftColumnLength;

			var result = new T[topRowLength + bottomRowLength, leftColumnLength + rightColumnLength];
			Copy(matrix.TopLeft, 0, 0, result, topRowIndex, leftColumnIndex, topRowLength, leftColumnLength);
			Copy(matrix.TopRight, 0, 0, result, topRowIndex, rightColumnIndex, topRowLength, rightColumnLength);
			Copy(matrix.BottomLeft, 0, 0, result, bottomRowIndex, leftColumnIndex, bottomRowLength, leftColumnLength);
			Copy(matrix.BottomRight, 0, 0, result, bottomRowIndex, rightColumnIndex, bottomRowLength, rightColumnLength);
			return result;
		}

		private static T[,] Extract<T>(T[,] matrix, int row, int rowLength, int column, int columnLength)
		{
			var result = new T[rowLength, columnLength];
			Copy(matrix, row, column, result, 0, 0, rowLength, columnLength);
			return result;
		}

		private static void Copy<T>(T[,] source, int sourceRow, int sourceColumn, T[,] target, int targetRow, int targetColumn, int rowLength, int columnLength)
		{
			for (int rowOffset = 0; rowOffset < rowLength; rowOffset++)
			{
				for (int columnOffset = 0; columnOffset < columnLength; columnOffset++)
				{
					target[targetRow + rowOffset, targetColumn + columnOffset] =
						source[sourceRow + rowOffset, sourceColumn + columnOffset];
				}
			}
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

		private class PartitionedMatrix<T>
		{
			public PartitionedMatrix(T[,] topLeft, T[,] topRight, T[,] bottomLeft, T[,] bottomRight)
			{
				m_topLeft = topLeft;
				m_topRight = topRight;
				m_bottomLeft = bottomLeft;
				m_bottomRight = bottomRight;
			}

			public T[,] TopLeft
			{
				get { return m_topLeft; }
			}

			public T[,] TopRight
			{
				get { return m_topRight; }
			}

			public T[,] BottomLeft
			{
				get { return m_bottomLeft; }
			}

			public T[,] BottomRight
			{
				get { return m_bottomRight; }
			}

			readonly T[,] m_topLeft;
			readonly T[,] m_topRight;
			readonly T[,] m_bottomLeft;
			readonly T[,] m_bottomRight;
		}
	}
}
