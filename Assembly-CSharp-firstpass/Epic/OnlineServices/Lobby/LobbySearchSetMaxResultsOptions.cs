using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000792 RID: 1938
	public class LobbySearchSetMaxResultsOptions
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06004754 RID: 18260 RVA: 0x0008FD5F File Offset: 0x0008DF5F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06004755 RID: 18261 RVA: 0x0008FD62 File Offset: 0x0008DF62
		// (set) Token: 0x06004756 RID: 18262 RVA: 0x0008FD6A File Offset: 0x0008DF6A
		public uint MaxResults { get; set; }
	}
}
