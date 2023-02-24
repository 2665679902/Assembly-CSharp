using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007BC RID: 1980
	public class PromoteMemberOptions
	{
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x00090066 File Offset: 0x0008E266
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x00090069 File Offset: 0x0008E269
		// (set) Token: 0x06004808 RID: 18440 RVA: 0x00090071 File Offset: 0x0008E271
		public string LobbyId { get; set; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x0009007A File Offset: 0x0008E27A
		// (set) Token: 0x0600480A RID: 18442 RVA: 0x00090082 File Offset: 0x0008E282
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x0009008B File Offset: 0x0008E28B
		// (set) Token: 0x0600480C RID: 18444 RVA: 0x00090093 File Offset: 0x0008E293
		public ProductUserId TargetUserId { get; set; }
	}
}
