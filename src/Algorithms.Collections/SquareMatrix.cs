using System;

namespace Algorithms.Collections
{
	public static class SquareMatrix
	{
		public static T[,] MultiplyStrassen<T>(T[,] A, T[,] B, Func<T, T, T> add, Func<T, T, T> subtract, Func<T, T, T> multiply)
		{
			ValidateAreEqualSquare(A, B);
			int length = A.GetLength(0);

			if (length == 1)
				return new[,] { { multiply(A[0, 0], B[0, 0]) } };

			var partitionedA = Partition(A);
			var partitionedB = Partition(B);

			var S1 = Accumulate(partitionedB.TopRight, partitionedB.BottomRight, subtract);
			var S2 = Accumulate(partitionedA.TopLeft, partitionedA.TopRight, add);
			var S3 = Accumulate(partitionedA.BottomLeft, partitionedA.BottomRight, add);
			var S4 = Accumulate(partitionedB.BottomLeft, partitionedB.TopLeft, subtract);
			var S5 = Accumulate(partitionedA.TopLeft, partitionedA.BottomRight, add);
			var S6 = Accumulate(partitionedB.TopLeft, partitionedB.BottomRight, add);
			var S7 = Accumulate(partitionedA.TopRight, partitionedA.BottomRight, subtract);
			var S8 = Accumulate(partitionedB.BottomLeft, partitionedB.BottomRight, add);
			var S9 = Accumulate(partitionedA.TopLeft, partitionedA.BottomLeft, subtract);
			var S10 = Accumulate(partitionedB.TopLeft, partitionedB.TopRight, add);

			var P1 = MultiplyStrassen(partitionedA.TopLeft, S1, add, subtract, multiply);
			var P2 = MultiplyStrassen(S2, partitionedB.BottomRight, add, subtract, multiply);
			var P3 = MultiplyStrassen(S3, partitionedB.TopLeft, add, subtract, multiply);
			var P4 = MultiplyStrassen(partitionedA.BottomRight, S4, add, subtract, multiply);
			var P5 = MultiplyStrassen(S5, S6, add, subtract, multiply);
			var P6 = MultiplyStrassen(S7, S8, add, subtract, multiply);
			var P7 = MultiplyStrassen(S9, S10, add, subtract, multiply);

			var C11 = 
				Accumulate(
				Accumulate(
				Accumulate(P5, P4, add),
					P2, subtract),
					P6, add);
			var C12 = Accumulate(P1, P2, add);
			var C21 = Accumulate(P3, P4, add);
			var C22 =
				Accumulate(
				Accumulate(
				Accumulate(P5, P1, add),
					P3, subtract),
					P7, subtract);

			return Combine(new PartitionedMatrix<T>(C11, C12, C21, C22));
		}

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

		public static T[,] Scale<T>(T[,] matrix, T scale, Func<T, T, T> multiply)
		{
			int rowLength = matrix.GetLength(0);
			int columnLength = matrix.GetLength(1);

			var result = new T[rowLength, columnLength];
			for (int row = 0; row < rowLength; row++)
			{
				for (int column = 0; column < columnLength; column++)
					result[row, column] = multiply(scale, matrix[row, column]);
			}
			return result;
		}

		public static T[,] MultiplyRecursive<T>(T[,] A, T[,] B, Func<T, T, T> add, Func<T, T, T> multiply)
		{
			ValidateAreEqualSquare(A, B);
			int length = A.GetLength(0);

			if (length == 1)
				return new[,] { { multiply(A[0, 0], B[0, 0]) } };

			var partitionedA = Partition(A);
			var partitionedB = Partition(B);

			var resultTopLeft = Accumulate(
				MultiplyRecursive(partitionedA.TopLeft, partitionedB.TopLeft, add, multiply),
				MultiplyRecursive(partitionedA.TopRight, partitionedB.BottomLeft, add, multiply),
				add);
			var resultTopRight = Accumulate(
				MultiplyRecursive(partitionedA.TopLeft, partitionedB.TopRight, add, multiply),
				MultiplyRecursive(partitionedA.TopRight, partitionedB.BottomRight, add, multiply),
				add);
			var resultBottomLeft = Accumulate(
				MultiplyRecursive(partitionedA.BottomLeft, partitionedB.TopLeft, add, multiply),
				MultiplyRecursive(partitionedA.BottomRight, partitionedB.BottomLeft, add, multiply),
				add);
			var resultBottomRight = Accumulate(
				MultiplyRecursive(partitionedA.BottomLeft, partitionedB.TopRight, add, multiply),
				MultiplyRecursive(partitionedA.BottomRight, partitionedB.BottomRight, add, multiply),
				add);

			return Combine(new PartitionedMatrix<T>(resultTopLeft, resultTopRight, resultBottomLeft, resultBottomRight));
		}

		public static T[,] Accumulate<T>(T[,] left, T[,] right, Func<T, T, T> accumulator)
		{
			ValidateAreEqualSquare(left, right);

			int length = left.GetLength(0);
			var result = new T[length, length];

			for (int row = 0; row < left.GetLength(0); row++)
			{
				for (int column = 0; column < left.GetLength(1); column++)
					result[row, column] = accumulator(left[row, column], right[row, column]);
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
