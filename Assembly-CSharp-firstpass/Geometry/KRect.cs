using System;
using UnityEngine;

namespace Geometry
{
	// Token: 0x0200050F RID: 1295
	public struct KRect
	{
		// Token: 0x0600374B RID: 14155 RVA: 0x0007CB3E File Offset: 0x0007AD3E
		public KRect(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x0007CB4E File Offset: 0x0007AD4E
		public KRect(float x0, float y0, float x1, float y1)
		{
			this.min = new Vector2(x0, y0);
			this.max = new Vector2(x1, y1);
		}

		// Token: 0x040013FD RID: 5117
		public Vector2 min;

		// Token: 0x040013FE RID: 5118
		public Vector2 max;
	}
}
