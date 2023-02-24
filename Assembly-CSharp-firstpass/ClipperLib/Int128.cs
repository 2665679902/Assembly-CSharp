using System;

namespace ClipperLib
{
	// Token: 0x0200015A RID: 346
	internal struct Int128
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002E4FC File Offset: 0x0002C6FC
		public Int128(long _lo)
		{
			this.lo = (ulong)_lo;
			if (_lo < 0L)
			{
				this.hi = -1L;
				return;
			}
			this.hi = 0L;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002E51B File Offset: 0x0002C71B
		public Int128(long _hi, ulong _lo)
		{
			this.lo = _lo;
			this.hi = _hi;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002E52B File Offset: 0x0002C72B
		public Int128(Int128 val)
		{
			this.hi = val.hi;
			this.lo = val.lo;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002E545 File Offset: 0x0002C745
		public bool IsNegative()
		{
			return this.hi < 0L;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002E554 File Offset: 0x0002C754
		public static bool operator ==(Int128 val1, Int128 val2)
		{
			return val1 == val2 || (val1 != null && val2 != null && val1.hi == val2.hi && val1.lo == val2.lo);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002E5A1 File Offset: 0x0002C7A1
		public static bool operator !=(Int128 val1, Int128 val2)
		{
			return !(val1 == val2);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Int128))
			{
				return false;
			}
			Int128 @int = (Int128)obj;
			return @int.hi == this.hi && @int.lo == this.lo;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002E5EF File Offset: 0x0002C7EF
		public override int GetHashCode()
		{
			return this.hi.GetHashCode() ^ this.lo.GetHashCode();
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002E608 File Offset: 0x0002C808
		public static bool operator >(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi > val2.hi;
			}
			return val1.lo > val2.lo;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002E635 File Offset: 0x0002C835
		public static bool operator <(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi < val2.hi;
			}
			return val1.lo < val2.lo;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002E662 File Offset: 0x0002C862
		public static Int128 operator +(Int128 lhs, Int128 rhs)
		{
			lhs.hi += rhs.hi;
			lhs.lo += rhs.lo;
			if (lhs.lo < rhs.lo)
			{
				lhs.hi += 1L;
			}
			return lhs;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002E6A2 File Offset: 0x0002C8A2
		public static Int128 operator -(Int128 lhs, Int128 rhs)
		{
			return lhs + -rhs;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		public static Int128 operator -(Int128 val)
		{
			if (val.lo == 0UL)
			{
				return new Int128(-val.hi, 0UL);
			}
			return new Int128(~val.hi, ~val.lo + 1UL);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002E6E0 File Offset: 0x0002C8E0
		public static explicit operator double(Int128 val)
		{
			if (val.hi >= 0L)
			{
				return val.lo + (double)val.hi * 1.8446744073709552E+19;
			}
			if (val.lo == 0UL)
			{
				return (double)val.hi * 1.8446744073709552E+19;
			}
			return -(~val.lo + (double)(~(double)val.hi) * 1.8446744073709552E+19);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002E74C File Offset: 0x0002C94C
		public static Int128 Int128Mul(long lhs, long rhs)
		{
			bool flag = lhs < 0L != rhs < 0L;
			if (lhs < 0L)
			{
				lhs = -lhs;
			}
			if (rhs < 0L)
			{
				rhs = -rhs;
			}
			ulong num = (ulong)lhs >> 32;
			ulong num2 = (ulong)(lhs & (long)((ulong)(-1)));
			ulong num3 = (ulong)rhs >> 32;
			ulong num4 = (ulong)(rhs & (long)((ulong)(-1)));
			ulong num5 = num * num3;
			ulong num6 = num2 * num4;
			ulong num7 = num * num4 + num2 * num3;
			long num8 = (long)(num5 + (num7 >> 32));
			ulong num9 = (num7 << 32) + num6;
			if (num9 < num6)
			{
				num8 += 1L;
			}
			Int128 @int = new Int128(num8, num9);
			if (!flag)
			{
				return @int;
			}
			return -@int;
		}

		// Token: 0x04000756 RID: 1878
		private long hi;

		// Token: 0x04000757 RID: 1879
		private ulong lo;
	}
}
