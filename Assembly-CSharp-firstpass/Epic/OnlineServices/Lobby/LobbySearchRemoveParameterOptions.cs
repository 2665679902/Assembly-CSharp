using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200078E RID: 1934
	public class LobbySearchRemoveParameterOptions
	{
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600473E RID: 18238 RVA: 0x0008FC13 File Offset: 0x0008DE13
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x0008FC16 File Offset: 0x0008DE16
		// (set) Token: 0x06004740 RID: 18240 RVA: 0x0008FC1E File Offset: 0x0008DE1E
		public string Key { get; set; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06004741 RID: 18241 RVA: 0x0008FC27 File Offset: 0x0008DE27
		// (set) Token: 0x06004742 RID: 18242 RVA: 0x0008FC2F File Offset: 0x0008DE2F
		public ComparisonOp ComparisonOp { get; set; }
	}
}
