using System;

namespace ClipperLib
{
	// Token: 0x02000157 RID: 343
	public struct DoublePoint
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		public DoublePoint(double x = 0.0, double y = 0.0)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002E2B0 File Offset: 0x0002C4B0
		public DoublePoint(DoublePoint dp)
		{
			this.X = dp.X;
			this.Y = dp.Y;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002E2CA File Offset: 0x0002C4CA
		public DoublePoint(IntPoint ip)
		{
			this.X = (double)ip.X;
			this.Y = (double)ip.Y;
		}

		// Token: 0x0400074C RID: 1868
		public double X;

		// Token: 0x0400074D RID: 1869
		public double Y;
	}
}
