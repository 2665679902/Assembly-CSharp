using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200025B RID: 603
	public sealed class DisjointSet<T> : IDisjointSet<T>, IReadOnlyDisjointSet<T>, IClearable
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x0004799B File Offset: 0x00045B9B
		public DisjointSet()
		{
			this.parent = new Dictionary<T, T>();
			this.next = new Dictionary<T, T>();
			this.last = new Dictionary<T, T>();
			this.tmpList = new List<T>();
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000479CF File Offset: 0x00045BCF
		public void Clear()
		{
			this.parent.Clear();
			this.next.Clear();
			this.last.Clear();
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000479F4 File Offset: 0x00045BF4
		public DisjointSetSet<T> WhereIs(T element)
		{
			T t;
			while (this.parent.TryGetValue(element, out t))
			{
				this.tmpList.Add(element);
				element = t;
			}
			foreach (T t2 in this.tmpList)
			{
				this.parent[t2] = element;
			}
			this.tmpList.Clear();
			return new DisjointSetSet<T>(element);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00047A80 File Offset: 0x00045C80
		private T GetLast(T x)
		{
			T t;
			if (this.last.TryGetValue(x, out t))
			{
				return t;
			}
			return x;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00047AA0 File Offset: 0x00045CA0
		public DisjointSetSet<T> Union(DisjointSetSet<T> a, DisjointSetSet<T> b)
		{
			T representative = a.Representative;
			T representative2 = b.Representative;
			if (!representative.Equals(representative2))
			{
				this.parent[representative] = representative2;
				this.next[this.GetLast(representative2)] = representative;
				this.last[representative2] = this.GetLast(representative);
			}
			return b;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00047B06 File Offset: 0x00045D06
		public IEnumerable<T> Elements(DisjointSetSet<T> aSet)
		{
			T element = aSet.Representative;
			do
			{
				yield return element;
			}
			while (this.next.TryGetValue(element, out element));
			yield break;
		}

		// Token: 0x0400099F RID: 2463
		private readonly Dictionary<T, T> parent;

		// Token: 0x040009A0 RID: 2464
		private readonly Dictionary<T, T> next;

		// Token: 0x040009A1 RID: 2465
		private readonly Dictionary<T, T> last;

		// Token: 0x040009A2 RID: 2466
		private readonly List<T> tmpList;
	}
}
