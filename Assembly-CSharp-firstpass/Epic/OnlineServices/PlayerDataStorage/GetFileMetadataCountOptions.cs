using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A7 RID: 1703
	public class GetFileMetadataCountOptions
	{
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x000894EA File Offset: 0x000876EA
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x000894ED File Offset: 0x000876ED
		// (set) Token: 0x06004155 RID: 16725 RVA: 0x000894F5 File Offset: 0x000876F5
		public ProductUserId LocalUserId { get; set; }
	}
}
