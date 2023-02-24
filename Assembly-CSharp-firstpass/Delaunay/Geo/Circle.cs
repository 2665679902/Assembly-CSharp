using System;
using UnityEngine;

namespace Delaunay.Geo
{
	// Token: 0x02000151 RID: 337
	public sealed class Circle
	{
		// Token: 0x06000B5B RID: 2907 RVA: 0x0002C6F6 File Offset: 0x0002A8F6
		public Circle(float centerX, float centerY, float radius)
		{
			this.center = new Vector2(centerX, centerY);
			this.radius = radius;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002C714 File Offset: 0x0002A914
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Circle (center: ",
				this.center.ToString(),
				"; radius: ",
				this.radius.ToString(),
				")"
			});
		}

		// Token: 0x0400073B RID: 1851
		public Vector2 center;

		// Token: 0x0400073C RID: 1852
		public float radius;
	}
}
