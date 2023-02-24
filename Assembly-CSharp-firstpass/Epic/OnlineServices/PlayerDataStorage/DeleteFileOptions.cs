using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200069D RID: 1693
	public class DeleteFileOptions
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x0008903E File Offset: 0x0008723E
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x00089041 File Offset: 0x00087241
		// (set) Token: 0x0600410A RID: 16650 RVA: 0x00089049 File Offset: 0x00087249
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x00089052 File Offset: 0x00087252
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x0008905A File Offset: 0x0008725A
		public string Filename { get; set; }
	}
}
