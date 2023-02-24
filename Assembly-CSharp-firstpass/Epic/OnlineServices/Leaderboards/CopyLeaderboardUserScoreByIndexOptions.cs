using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D8 RID: 2008
	public class CopyLeaderboardUserScoreByIndexOptions
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060048A4 RID: 18596 RVA: 0x000909E7 File Offset: 0x0008EBE7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060048A5 RID: 18597 RVA: 0x000909EA File Offset: 0x0008EBEA
		// (set) Token: 0x060048A6 RID: 18598 RVA: 0x000909F2 File Offset: 0x0008EBF2
		public uint LeaderboardUserScoreIndex { get; set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x000909FB File Offset: 0x0008EBFB
		// (set) Token: 0x060048A8 RID: 18600 RVA: 0x00090A03 File Offset: 0x0008EC03
		public string StatName { get; set; }
	}
}
