using System;
using System.Globalization;

namespace Satsuma.Drawing
{
	// Token: 0x02000287 RID: 647
	public struct PointD : IEquatable<PointD>
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0004C8F1 File Offset: 0x0004AAF1
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x0004C8F9 File Offset: 0x0004AAF9
		public double X { readonly get; private set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0004C902 File Offset: 0x0004AB02
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x0004C90A File Offset: 0x0004AB0A
		public double Y { readonly get; private set; }

		// Token: 0x060013EE RID: 5102 RVA: 0x0004C913 File Offset: 0x0004AB13
		public PointD(double x, double y)
		{
			this = default(PointD);
			this.X = x;
			this.Y = y;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004C92A File Offset: 0x0004AB2A
		public bool Equals(PointD other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004C94C File Offset: 0x0004AB4C
		public override bool Equals(object obj)
		{
			return obj is PointD && this.Equals((PointD)obj);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004C964 File Offset: 0x0004AB64
		public static bool operator ==(PointD a, PointD b)
		{
			return a.Equals(b);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004C96E File Offset: 0x0004AB6E
		public static bool operator !=(PointD a, PointD b)
		{
			return !(a == b);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004C97C File Offset: 0x0004AB7C
		public override int GetHashCode()
		{
			return this.X.GetHashCode() * 17 + this.Y.GetHashCode();
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0004C9A9 File Offset: 0x0004ABA9
		public string ToString(IFormatProvider provider)
		{
			return string.Format(provider, "({0} {1})", this.X, this.Y);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004C9CC File Offset: 0x0004ABCC
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0004C9D9 File Offset: 0x0004ABD9
		public static PointD operator +(PointD a, PointD b)
		{
			return new PointD(a.X + b.X, a.Y + b.Y);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004C9FE File Offset: 0x0004ABFE
		public static PointD Add(PointD a, PointD b)
		{
			return a + b;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004CA08 File Offset: 0x0004AC08
		public double Distance(PointD other)
		{
			return Math.Sqrt((this.X - other.X) * (this.X - other.X) + (this.Y - other.Y) * (this.Y - other.Y));
		}
	}
}
