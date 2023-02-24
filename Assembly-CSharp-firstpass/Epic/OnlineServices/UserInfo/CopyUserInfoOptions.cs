using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200053E RID: 1342
	public class CopyUserInfoOptions
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060038C3 RID: 14531 RVA: 0x00080D4B File Offset: 0x0007EF4B
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x00080D4E File Offset: 0x0007EF4E
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x00080D56 File Offset: 0x0007EF56
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x00080D5F File Offset: 0x0007EF5F
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x00080D67 File Offset: 0x0007EF67
		public EpicAccountId TargetUserId { get; set; }
	}
}
