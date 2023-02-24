using System;
using System.Collections.Generic;

namespace Klei
{
	// Token: 0x02000D55 RID: 3413
	public class WorldGenSave
	{
		// Token: 0x06006855 RID: 26709 RVA: 0x0028A617 File Offset: 0x00288817
		public WorldGenSave()
		{
			this.data = new Data();
			this.stats = new Dictionary<string, object>();
		}

		// Token: 0x04004E8C RID: 20108
		public Vector2I version;

		// Token: 0x04004E8D RID: 20109
		public Dictionary<string, object> stats;

		// Token: 0x04004E8E RID: 20110
		public Data data;

		// Token: 0x04004E8F RID: 20111
		public string worldID;

		// Token: 0x04004E90 RID: 20112
		public List<string> traitIDs;

		// Token: 0x04004E91 RID: 20113
		public List<string> storyTraitIDs;
	}
}
