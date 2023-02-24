using System;
using Epic.OnlineServices.Connect;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008E7 RID: 2279
	public class Credentials
	{
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004FB5 RID: 20405 RVA: 0x00097C87 File Offset: 0x00095E87
		public int ApiVersion
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x00097C8A File Offset: 0x00095E8A
		// (set) Token: 0x06004FB7 RID: 20407 RVA: 0x00097C92 File Offset: 0x00095E92
		public string Id { get; set; }

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x00097C9B File Offset: 0x00095E9B
		// (set) Token: 0x06004FB9 RID: 20409 RVA: 0x00097CA3 File Offset: 0x00095EA3
		public string Token { get; set; }

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x00097CAC File Offset: 0x00095EAC
		// (set) Token: 0x06004FBB RID: 20411 RVA: 0x00097CB4 File Offset: 0x00095EB4
		public LoginCredentialType Type { get; set; }

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x00097CBD File Offset: 0x00095EBD
		// (set) Token: 0x06004FBD RID: 20413 RVA: 0x00097CC5 File Offset: 0x00095EC5
		public IntPtr SystemAuthCredentialsOptions { get; set; }

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x00097CCE File Offset: 0x00095ECE
		// (set) Token: 0x06004FBF RID: 20415 RVA: 0x00097CD6 File Offset: 0x00095ED6
		public ExternalCredentialType ExternalType { get; set; }
	}
}
