using System;

namespace Klei
{
	// Token: 0x02000D57 RID: 3415
	public class SimSaveFileStructure
	{
		// Token: 0x06006857 RID: 26711 RVA: 0x0028A648 File Offset: 0x00288848
		public SimSaveFileStructure()
		{
			this.worldDetail = new WorldDetailSave();
		}

		// Token: 0x04004E97 RID: 20119
		public int WidthInCells;

		// Token: 0x04004E98 RID: 20120
		public int HeightInCells;

		// Token: 0x04004E99 RID: 20121
		public int x;

		// Token: 0x04004E9A RID: 20122
		public int y;

		// Token: 0x04004E9B RID: 20123
		public byte[] Sim;

		// Token: 0x04004E9C RID: 20124
		public WorldDetailSave worldDetail;
	}
}
