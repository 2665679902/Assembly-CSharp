using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000755 RID: 1877
	public class LobbyDetailsCopyAttributeByIndexOptions
	{
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x0008DEC3 File Offset: 0x0008C0C3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x0008DEC6 File Offset: 0x0008C0C6
		// (set) Token: 0x060045C8 RID: 17864 RVA: 0x0008DECE File Offset: 0x0008C0CE
		public uint AttrIndex { get; set; }
	}
}
