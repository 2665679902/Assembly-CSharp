using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200062D RID: 1581
	public class SessionInviteReceivedCallbackInfo
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x00085E86 File Offset: 0x00084086
		// (set) Token: 0x06003E45 RID: 15941 RVA: 0x00085E8E File Offset: 0x0008408E
		public object ClientData { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x00085E97 File Offset: 0x00084097
		// (set) Token: 0x06003E47 RID: 15943 RVA: 0x00085E9F File Offset: 0x0008409F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x00085EA8 File Offset: 0x000840A8
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x00085EB0 File Offset: 0x000840B0
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x00085EB9 File Offset: 0x000840B9
		// (set) Token: 0x06003E4B RID: 15947 RVA: 0x00085EC1 File Offset: 0x000840C1
		public string InviteId { get; set; }
	}
}
