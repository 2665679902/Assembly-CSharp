using System;

namespace Geometry
{
	// Token: 0x0200050E RID: 1294
	public struct VerticalEvent
	{
		// Token: 0x0600374A RID: 14154 RVA: 0x0007CB27 File Offset: 0x0007AD27
		public VerticalEvent(float y, bool isStart, bool subtract)
		{
			this.y = y;
			this.isStart = isStart;
			this.subtract = subtract;
		}

		// Token: 0x040013FA RID: 5114
		public float y;

		// Token: 0x040013FB RID: 5115
		public bool isStart;

		// Token: 0x040013FC RID: 5116
		public bool subtract;
	}
}
