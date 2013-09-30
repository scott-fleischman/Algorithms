using System;
using System.Collections.Generic;

namespace Algorithms.Collections.Tests
{
	public class NamedSortAlgorithm<T>
	{
		public NamedSortAlgorithm(string name, Action<IList<T>, IComparer<T>> algorithm)
		{
			m_name = name;
			m_algorithm = algorithm;
		}

		public string Name
		{
			get { return m_name; }
		}

		public Action<IList<T>, IComparer<T>> Algorithm
		{
			get { return m_algorithm; }
		}

		public override string ToString()
		{
			return Name;
		}

		readonly string m_name;
		readonly Action<IList<T>, IComparer<T>> m_algorithm;
	}
}