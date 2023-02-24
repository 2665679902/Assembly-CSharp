using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000796 RID: 1942
	public class LobbySearchSetTargetUserIdOptions
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x0008FEB7 File Offset: 0x0008E0B7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600476B RID: 18283 RVA: 0x0008FEBA File Offset: 0x0008E0BA
		// (set) Token: 0x0600476C RID: 18284 RVA: 0x0008FEC2 File Offset: 0x0008E0C2
		public ProductUserId TargetUserId { get; set; }
	}
}
