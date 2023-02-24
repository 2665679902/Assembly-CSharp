using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200073B RID: 1851
	public class CreateLobbySearchOptions
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x0008D2F7 File Offset: 0x0008B4F7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x0008D2FA File Offset: 0x0008B4FA
		// (set) Token: 0x0600451D RID: 17693 RVA: 0x0008D302 File Offset: 0x0008B502
		public uint MaxResults { get; set; }
	}
}
