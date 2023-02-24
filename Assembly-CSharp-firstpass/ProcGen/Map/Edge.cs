using System;
using KSerialization;
using Satsuma;
using UnityEngine;

namespace ProcGen.Map
{
	// Token: 0x020004F8 RID: 1272
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Edge : Arc
	{
		// Token: 0x060036E1 RID: 14049 RVA: 0x00078ACC File Offset: 0x00076CCC
		public Edge()
			: base(WorldGenTags.Edge.Name)
		{
			this.tags = new TagSet();
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x00078AF8 File Offset: 0x00076CF8
		public Edge(Arc arc, Cell s0, Cell s1, Corner c0, Corner c1)
			: base(arc, WorldGenTags.Edge.Name)
		{
			this.corner0 = c0;
			this.corner1 = c1;
			this.tags = new TagSet();
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x00078B34 File Offset: 0x00076D34
		public void SetCorners(Corner corner0, Corner corner1)
		{
			this.corner0 = corner0;
			this.corner1 = corner1;
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x00078B44 File Offset: 0x00076D44
		public Vector2 MidPoint()
		{
			return (this.corner1.position - this.corner0.position) * 0.5f + this.corner0.position;
		}

		// Token: 0x040013A7 RID: 5031
		public Corner corner0;

		// Token: 0x040013A8 RID: 5032
		public Corner corner1;
	}
}
