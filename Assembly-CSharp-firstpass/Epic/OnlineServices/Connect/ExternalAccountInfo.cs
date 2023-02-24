using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A3 RID: 2211
	public class ExternalAccountInfo
	{
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004E36 RID: 20022 RVA: 0x0009683F File Offset: 0x00094A3F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004E37 RID: 20023 RVA: 0x00096842 File Offset: 0x00094A42
		// (set) Token: 0x06004E38 RID: 20024 RVA: 0x0009684A File Offset: 0x00094A4A
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x00096853 File Offset: 0x00094A53
		// (set) Token: 0x06004E3A RID: 20026 RVA: 0x0009685B File Offset: 0x00094A5B
		public string DisplayName { get; set; }

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x00096864 File Offset: 0x00094A64
		// (set) Token: 0x06004E3C RID: 20028 RVA: 0x0009686C File Offset: 0x00094A6C
		public string AccountId { get; set; }

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x00096875 File Offset: 0x00094A75
		// (set) Token: 0x06004E3E RID: 20030 RVA: 0x0009687D File Offset: 0x00094A7D
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004E3F RID: 20031 RVA: 0x00096886 File Offset: 0x00094A86
		// (set) Token: 0x06004E40 RID: 20032 RVA: 0x0009688E File Offset: 0x00094A8E
		public DateTimeOffset? LastLoginTime { get; set; }
	}
}
