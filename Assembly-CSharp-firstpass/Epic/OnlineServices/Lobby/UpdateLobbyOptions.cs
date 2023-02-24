using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007CE RID: 1998
	public class UpdateLobbyOptions
	{
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x00090753 File Offset: 0x0008E953
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x00090756 File Offset: 0x0008E956
		// (set) Token: 0x06004879 RID: 18553 RVA: 0x0009075E File Offset: 0x0008E95E
		public LobbyModification LobbyModificationHandle { get; set; }
	}
}
