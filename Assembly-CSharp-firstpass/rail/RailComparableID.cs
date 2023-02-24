using System;

namespace rail
{
	// Token: 0x02000295 RID: 661
	public class RailComparableID : IEquatable<RailComparableID>, IComparable<RailComparableID>
	{
		// Token: 0x060027E0 RID: 10208 RVA: 0x0004EEE4 File Offset: 0x0004D0E4
		public RailComparableID()
		{
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x0004EEEC File Offset: 0x0004D0EC
		public RailComparableID(ulong id)
		{
			this.id_ = id;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0004EEFB File Offset: 0x0004D0FB
		public bool IsValid()
		{
			return this.id_ > 0UL;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0004EF07 File Offset: 0x0004D107
		public override bool Equals(object other)
		{
			return other is RailComparableID && this == (RailComparableID)other;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0004EF1F File Offset: 0x0004D11F
		public override int GetHashCode()
		{
			return this.id_.GetHashCode();
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0004EF2C File Offset: 0x0004D12C
		public static bool operator ==(RailComparableID x, RailComparableID y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0004EF3D File Offset: 0x0004D13D
		public static bool operator !=(RailComparableID x, RailComparableID y)
		{
			return !(x == y);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0004EF49 File Offset: 0x0004D149
		public static explicit operator RailComparableID(ulong value)
		{
			return new RailComparableID(value);
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0004EF51 File Offset: 0x0004D151
		public static explicit operator ulong(RailComparableID that)
		{
			return that.id_;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0004EF59 File Offset: 0x0004D159
		public bool Equals(RailComparableID other)
		{
			return other != null && (this == other || (!(base.GetType() != other.GetType()) && other.id_ == this.id_));
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0004EF89 File Offset: 0x0004D189
		public int CompareTo(RailComparableID other)
		{
			return this.id_.CompareTo(other.id_);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0004EF9C File Offset: 0x0004D19C
		public override string ToString()
		{
			return this.id_.ToString();
		}

		// Token: 0x04000A6C RID: 2668
		public static readonly RailComparableID Nil = new RailComparableID(0UL);

		// Token: 0x04000A6D RID: 2669
		public ulong id_;
	}
}
