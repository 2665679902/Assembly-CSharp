using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000769 RID: 1897
	public class LobbyDetailsInfo
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x0008E363 File Offset: 0x0008C563
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x0008E366 File Offset: 0x0008C566
		// (set) Token: 0x0600461A RID: 17946 RVA: 0x0008E36E File Offset: 0x0008C56E
		public string LobbyId { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600461B RID: 17947 RVA: 0x0008E377 File Offset: 0x0008C577
		// (set) Token: 0x0600461C RID: 17948 RVA: 0x0008E37F File Offset: 0x0008C57F
		public ProductUserId LobbyOwnerUserId { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x0008E388 File Offset: 0x0008C588
		// (set) Token: 0x0600461E RID: 17950 RVA: 0x0008E390 File Offset: 0x0008C590
		public LobbyPermissionLevel PermissionLevel { get; set; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600461F RID: 17951 RVA: 0x0008E399 File Offset: 0x0008C599
		// (set) Token: 0x06004620 RID: 17952 RVA: 0x0008E3A1 File Offset: 0x0008C5A1
		public uint AvailableSlots { get; set; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x0008E3AA File Offset: 0x0008C5AA
		// (set) Token: 0x06004622 RID: 17954 RVA: 0x0008E3B2 File Offset: 0x0008C5B2
		public uint MaxMembers { get; set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06004623 RID: 17955 RVA: 0x0008E3BB File Offset: 0x0008C5BB
		// (set) Token: 0x06004624 RID: 17956 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
		public bool AllowInvites { get; set; }
	}
}
