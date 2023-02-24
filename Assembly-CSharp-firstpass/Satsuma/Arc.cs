using System;

namespace Satsuma
{
	// Token: 0x0200025D RID: 605
	public struct Arc : IEquatable<Arc>
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00047BC7 File Offset: 0x00045DC7
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00047BCF File Offset: 0x00045DCF
		public long Id { readonly get; private set; }

		// Token: 0x06001267 RID: 4711 RVA: 0x00047BD8 File Offset: 0x00045DD8
		public Arc(long id)
		{
			this = default(Arc);
			this.Id = id;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00047BE8 File Offset: 0x00045DE8
		public static Arc Invalid
		{
			get
			{
				return new Arc(0L);
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00047BF1 File Offset: 0x00045DF1
		public bool Equals(Arc other)
		{
			return this.Id == other.Id;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00047C02 File Offset: 0x00045E02
		public override bool Equals(object obj)
		{
			return obj is Arc && this.Equals((Arc)obj);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00047C1C File Offset: 0x00045E1C
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00047C38 File Offset: 0x00045E38
		public override string ToString()
		{
			return "|" + this.Id.ToString();
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00047C5D File Offset: 0x00045E5D
		public static bool operator ==(Arc a, Arc b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00047C67 File Offset: 0x00045E67
		public static bool operator !=(Arc a, Arc b)
		{
			return !(a == b);
		}
	}
}
