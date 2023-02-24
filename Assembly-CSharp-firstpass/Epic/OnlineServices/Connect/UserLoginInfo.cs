using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008DC RID: 2268
	public class UserLoginInfo
	{
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004F7B RID: 20347 RVA: 0x00097733 File Offset: 0x00095933
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x00097736 File Offset: 0x00095936
		// (set) Token: 0x06004F7D RID: 20349 RVA: 0x0009773E File Offset: 0x0009593E
		public string DisplayName { get; set; }
	}
}
