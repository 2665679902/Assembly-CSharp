using System;
using KSerialization;

namespace ProcGenGame
{
	// Token: 0x02000C3F RID: 3135
	[SerializationConfig(MemberSerialization.OptOut)]
	public struct Neighbors
	{
		// Token: 0x06006342 RID: 25410 RVA: 0x0024DFD2 File Offset: 0x0024C1D2
		public Neighbors(TerrainCell a, TerrainCell b)
		{
			Debug.Assert(a != null && b != null, "NULL Neighbor");
			this.n0 = a;
			this.n1 = b;
		}

		// Token: 0x040044E1 RID: 17633
		public TerrainCell n0;

		// Token: 0x040044E2 RID: 17634
		public TerrainCell n1;
	}
}
