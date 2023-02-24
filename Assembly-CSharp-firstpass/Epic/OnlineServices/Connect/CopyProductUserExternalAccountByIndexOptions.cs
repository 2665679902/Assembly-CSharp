using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000891 RID: 2193
	public class CopyProductUserExternalAccountByIndexOptions
	{
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004DE1 RID: 19937 RVA: 0x0009633B File Offset: 0x0009453B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004DE2 RID: 19938 RVA: 0x0009633E File Offset: 0x0009453E
		// (set) Token: 0x06004DE3 RID: 19939 RVA: 0x00096346 File Offset: 0x00094546
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x0009634F File Offset: 0x0009454F
		// (set) Token: 0x06004DE5 RID: 19941 RVA: 0x00096357 File Offset: 0x00094557
		public uint ExternalAccountInfoIndex { get; set; }
	}
}
