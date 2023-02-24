using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000757 RID: 1879
	public class LobbyDetailsCopyAttributeByKeyOptions
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x0008DF47 File Offset: 0x0008C147
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x0008DF4A File Offset: 0x0008C14A
		// (set) Token: 0x060045D1 RID: 17873 RVA: 0x0008DF52 File Offset: 0x0008C152
		public string AttrKey { get; set; }
	}
}
