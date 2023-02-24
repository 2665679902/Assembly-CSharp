using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D5 RID: 1493
	public class CreateSessionModificationOptions
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x000847F7 File Offset: 0x000829F7
		public int ApiVersion
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000847FA File Offset: 0x000829FA
		// (set) Token: 0x06003C76 RID: 15478 RVA: 0x00084802 File Offset: 0x00082A02
		public string SessionName { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x0008480B File Offset: 0x00082A0B
		// (set) Token: 0x06003C78 RID: 15480 RVA: 0x00084813 File Offset: 0x00082A13
		public string BucketId { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x0008481C File Offset: 0x00082A1C
		// (set) Token: 0x06003C7A RID: 15482 RVA: 0x00084824 File Offset: 0x00082A24
		public uint MaxPlayers { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x0008482D File Offset: 0x00082A2D
		// (set) Token: 0x06003C7C RID: 15484 RVA: 0x00084835 File Offset: 0x00082A35
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x0008483E File Offset: 0x00082A3E
		// (set) Token: 0x06003C7E RID: 15486 RVA: 0x00084846 File Offset: 0x00082A46
		public bool PresenceEnabled { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x0008484F File Offset: 0x00082A4F
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x00084857 File Offset: 0x00082A57
		public string SessionId { get; set; }
	}
}
