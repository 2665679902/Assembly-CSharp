using System;

namespace ClipperLib
{
	// Token: 0x0200015B RID: 347
	public struct IntPoint
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002E7D5 File Offset: 0x0002C9D5
		public IntPoint(long X, long Y)
		{
			this.X = X;
			this.Y = Y;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002E7E5 File Offset: 0x0002C9E5
		public IntPoint(double x, double y)
		{
			this.X = (long)x;
			this.Y = (long)y;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002E7F7 File Offset: 0x0002C9F7
		public IntPoint(IntPoint pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002E811 File Offset: 0x0002CA11
		public static bool operator ==(IntPoint a, IntPoint b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002E831 File Offset: 0x0002CA31
		public static bool operator !=(IntPoint a, IntPoint b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002E854 File Offset: 0x0002CA54
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is IntPoint)
			{
				IntPoint intPoint = (IntPoint)obj;
				return this.X == intPoint.X && this.Y == intPoint.Y;
			}
			return false;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002E895 File Offset: 0x0002CA95
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000758 RID: 1880
		public long X;

		// Token: 0x04000759 RID: 1881
		public long Y;
	}
}
