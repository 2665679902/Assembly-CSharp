using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A8 RID: 2216
	public class GetProductUserExternalAccountCountOptions
	{
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x00096AE7 File Offset: 0x00094CE7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004E61 RID: 20065 RVA: 0x00096AEA File Offset: 0x00094CEA
		// (set) Token: 0x06004E62 RID: 20066 RVA: 0x00096AF2 File Offset: 0x00094CF2
		public ProductUserId TargetUserId { get; set; }
	}
}
