using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200093E RID: 2366
	public class OnQueryPlayerAchievementsCompleteCallbackInfo
	{
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x00099FF6 File Offset: 0x000981F6
		// (set) Token: 0x06005209 RID: 21001 RVA: 0x00099FFE File Offset: 0x000981FE
		public Result ResultCode { get; set; }

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x0009A007 File Offset: 0x00098207
		// (set) Token: 0x0600520B RID: 21003 RVA: 0x0009A00F File Offset: 0x0009820F
		public object ClientData { get; set; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x0600520C RID: 21004 RVA: 0x0009A018 File Offset: 0x00098218
		// (set) Token: 0x0600520D RID: 21005 RVA: 0x0009A020 File Offset: 0x00098220
		public ProductUserId UserId { get; set; }
	}
}
