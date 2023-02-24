using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000794 RID: 1940
	public class LobbySearchSetParameterOptions
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x0008FDE3 File Offset: 0x0008DFE3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x0008FDE6 File Offset: 0x0008DFE6
		// (set) Token: 0x0600475F RID: 18271 RVA: 0x0008FDEE File Offset: 0x0008DFEE
		public AttributeData Parameter { get; set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x0008FDF7 File Offset: 0x0008DFF7
		// (set) Token: 0x06004761 RID: 18273 RVA: 0x0008FDFF File Offset: 0x0008DFFF
		public ComparisonOp ComparisonOp { get; set; }
	}
}
