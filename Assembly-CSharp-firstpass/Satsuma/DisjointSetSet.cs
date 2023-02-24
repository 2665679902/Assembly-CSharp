using System;

namespace Satsuma
{
	// Token: 0x02000258 RID: 600
	public struct DisjointSetSet<T> : IEquatable<DisjointSetSet<T>>
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x000478BD File Offset: 0x00045ABD
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x000478C5 File Offset: 0x00045AC5
		public T Representative { readonly get; private set; }

		// Token: 0x0600124B RID: 4683 RVA: 0x000478CE File Offset: 0x00045ACE
		public DisjointSetSet(T representative)
		{
			this = default(DisjointSetSet<T>);
			this.Representative = representative;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x000478E0 File Offset: 0x00045AE0
		public bool Equals(DisjointSetSet<T> other)
		{
			T representative = this.Representative;
			return representative.Equals(other.Representative);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004790D File Offset: 0x00045B0D
		public override bool Equals(object obj)
		{
			return obj is DisjointSetSet<T> && this.Equals((DisjointSetSet<T>)obj);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00047925 File Offset: 0x00045B25
		public static bool operator ==(DisjointSetSet<T> a, DisjointSetSet<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004792F File Offset: 0x00045B2F
		public static bool operator !=(DisjointSetSet<T> a, DisjointSetSet<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004793C File Offset: 0x00045B3C
		public override int GetHashCode()
		{
			T representative = this.Representative;
			return representative.GetHashCode();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00047960 File Offset: 0x00045B60
		public override string ToString()
		{
			string text = "[DisjointSetSet:";
			T representative = this.Representative;
			return text + ((representative != null) ? representative.ToString() : null) + "]";
		}
	}
}
