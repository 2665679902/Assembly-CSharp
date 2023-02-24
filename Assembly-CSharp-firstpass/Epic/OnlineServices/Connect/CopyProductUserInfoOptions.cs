using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000893 RID: 2195
	public class CopyProductUserInfoOptions
	{
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004DEE RID: 19950 RVA: 0x00096403 File Offset: 0x00094603
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004DEF RID: 19951 RVA: 0x00096406 File Offset: 0x00094606
		// (set) Token: 0x06004DF0 RID: 19952 RVA: 0x0009640E File Offset: 0x0009460E
		public ProductUserId TargetUserId { get; set; }
	}
}
