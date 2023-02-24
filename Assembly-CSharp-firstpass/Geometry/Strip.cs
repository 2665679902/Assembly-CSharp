using System;

namespace Geometry
{
	// Token: 0x02000510 RID: 1296
	public class Strip
	{
		// Token: 0x0600374D RID: 14157 RVA: 0x0007CB6B File Offset: 0x0007AD6B
		public Strip(float yMin, float yMax, bool subtract)
		{
			this.yMin = yMin;
			this.yMax = yMax;
			this.subtract = subtract;
		}

		// Token: 0x040013FF RID: 5119
		public float yMin;

		// Token: 0x04001400 RID: 5120
		public float yMax;

		// Token: 0x04001401 RID: 5121
		public bool subtract;
	}
}
