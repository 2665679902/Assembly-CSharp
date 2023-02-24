using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200053C RID: 1340
	public class CopyExternalUserInfoByIndexOptions
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x00080C3B File Offset: 0x0007EE3B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x00080C3E File Offset: 0x0007EE3E
		// (set) Token: 0x060038B4 RID: 14516 RVA: 0x00080C46 File Offset: 0x0007EE46
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x00080C4F File Offset: 0x0007EE4F
		// (set) Token: 0x060038B6 RID: 14518 RVA: 0x00080C57 File Offset: 0x0007EE57
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060038B7 RID: 14519 RVA: 0x00080C60 File Offset: 0x0007EE60
		// (set) Token: 0x060038B8 RID: 14520 RVA: 0x00080C68 File Offset: 0x0007EE68
		public uint Index { get; set; }
	}
}
