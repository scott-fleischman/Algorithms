using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
	public static class DigitAddition
	{
		// Sequences start with least significant digits
		public static IEnumerable<int> Add(IEnumerable<int> leftDigits, IEnumerable<int> rightDigits, int radix = 2)
		{
			using (var leftEnumerator = leftDigits.GetEnumerator())
			using (var rightEnumerator = rightDigits.GetEnumerator())
			{
				int carry = 0;

				while (true)
				{
					bool leftHasNext = leftEnumerator.MoveNext();
					bool rightHasNext = rightEnumerator.MoveNext();

					if (!leftHasNext && !rightHasNext)
						break;

					int leftDigit = leftHasNext ? leftEnumerator.Current : 0;
					int rightDigit = rightHasNext ? rightEnumerator.Current : 0;

					if (leftDigit < 0 || leftDigit >= radix)
						throw new ArgumentException(string.Format("Invalid left digit: {0}", leftDigit));
					if (rightDigit < 0 || rightDigit >= radix)
						throw new ArgumentException(string.Format("Invalid right digit: {0}", rightDigit));

					int newDigit = leftDigit + rightDigit + carry;
					if (newDigit >= radix)
					{
						int total = newDigit;
						carry = total / radix;
						newDigit = total % radix;
					}
					else
					{
						carry = 0;
					}

					yield return newDigit;
				}

				if (carry != 0)
					yield return carry;
			}
		}
	}
}
