using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200088D RID: 2189
	public class CopyProductUserExternalAccountByAccountIdOptions
	{
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x000961A8 File Offset: 0x000943A8
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x000961AB File Offset: 0x000943AB
		// (set) Token: 0x06004DC9 RID: 19913 RVA: 0x000961B3 File Offset: 0x000943B3
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x000961BC File Offset: 0x000943BC
		// (set) Token: 0x06004DCB RID: 19915 RVA: 0x000961C4 File Offset: 0x000943C4
		public string AccountId { get; set; }
	}
}
