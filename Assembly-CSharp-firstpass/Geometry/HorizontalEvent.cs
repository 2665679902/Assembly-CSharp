using System;

namespace Geometry
{
	// Token: 0x0200050D RID: 1293
	public struct HorizontalEvent
	{
		// Token: 0x06003749 RID: 14153 RVA: 0x0007CB10 File Offset: 0x0007AD10
		public HorizontalEvent(float x, Strip strip, bool isStart)
		{
			this.x = x;
			this.strip = strip;
			this.isStart = isStart;
		}

		// Token: 0x040013F7 RID: 5111
		public float x;

		// Token: 0x040013F8 RID: 5112
		public Strip strip;

		// Token: 0x040013F9 RID: 5113
		public bool isStart;
	}
}
