using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000739 RID: 1849
	public class CreateLobbyOptions
	{
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x0008D1A2 File Offset: 0x0008B3A2
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x0008D1A5 File Offset: 0x0008B3A5
		// (set) Token: 0x06004508 RID: 17672 RVA: 0x0008D1AD File Offset: 0x0008B3AD
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x0008D1B6 File Offset: 0x0008B3B6
		// (set) Token: 0x0600450A RID: 17674 RVA: 0x0008D1BE File Offset: 0x0008B3BE
		public uint MaxLobbyMembers { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x0008D1C7 File Offset: 0x0008B3C7
		// (set) Token: 0x0600450C RID: 17676 RVA: 0x0008D1CF File Offset: 0x0008B3CF
		public LobbyPermissionLevel PermissionLevel { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x0008D1D8 File Offset: 0x0008B3D8
		// (set) Token: 0x0600450E RID: 17678 RVA: 0x0008D1E0 File Offset: 0x0008B3E0
		public bool PresenceEnabled { get; set; }
	}
}
