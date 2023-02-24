using System;
using System.Collections.Generic;
using KMod;

namespace Klei
{
	// Token: 0x02000D53 RID: 3411
	internal class SaveFileRoot
	{
		// Token: 0x06006853 RID: 26707 RVA: 0x0028A599 File Offset: 0x00288799
		public SaveFileRoot()
		{
			this.streamed = new Dictionary<string, byte[]>();
		}

		// Token: 0x04004E79 RID: 20089
		public int WidthInCells;

		// Token: 0x04004E7A RID: 20090
		public int HeightInCells;

		// Token: 0x04004E7B RID: 20091
		public Dictionary<string, byte[]> streamed;

		// Token: 0x04004E7C RID: 20092
		public string clusterID;

		// Token: 0x04004E7D RID: 20093
		public List<ModInfo> requiredMods;

		// Token: 0x04004E7E RID: 20094
		public List<Label> active_mods;
	}
}
