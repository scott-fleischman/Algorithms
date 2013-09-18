using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Algorithms.Sort.Tests
{
	public class SortTestCase<T>
	{
		public static SortTestCase<T> Create(IEnumerable<T> items, IEnumerable<T> expectedOrder)
		{
			return new SortTestCase<T>(items, expectedOrder);
		}

		public SortTestCase(IEnumerable<T> items, IEnumerable<T> expectedOrder)
		{
			m_items = items.ToList().AsReadOnly();
			m_expectedOrder = expectedOrder.ToList().AsReadOnly();
		}

		public ReadOnlyCollection<T> Items
		{
			get { return m_items; }
		}

		public ReadOnlyCollection<T> ExpectedOrder
		{
			get { return m_expectedOrder; }
		}

		public override string ToString()
		{
			return string.Format("[{0}]", string.Join(", ", m_items.Select(x => x.ToString())));
		}

		readonly ReadOnlyCollection<T> m_items;
		readonly ReadOnlyCollection<T> m_expectedOrder;
	}
}
