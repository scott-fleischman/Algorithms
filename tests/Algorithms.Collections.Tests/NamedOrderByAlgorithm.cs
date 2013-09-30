using System;
using System.Collections.Generic;

namespace Algorithms.Collections.Tests
{
	public class NamedOrderByAlgorithm<T>
	{
		public NamedOrderByAlgorithm(string name, Func<IEnumerable<T>, IComparer<T>, IEnumerable<T>> algorithm)
		{
			m_name = name;
			m_algorithm = algorithm;
		}

		public string Name
		{
			get { return m_name; }
		}

		public Func<IEnumerable<T>, IComparer<T>, IEnumerable<T>> Algorithm
		{
			get { return m_algorithm; }
		}

		public override string ToString()
		{
			return Name;
		}

		readonly string m_name;
		readonly Func<IEnumerable<T>, IComparer<T>, IEnumerable<T>> m_algorithm;
	}
}