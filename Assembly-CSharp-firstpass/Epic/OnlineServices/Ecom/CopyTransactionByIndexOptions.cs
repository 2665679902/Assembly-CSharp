using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000846 RID: 2118
	public class CopyTransactionByIndexOptions
	{
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x00093B6B File Offset: 0x00091D6B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x00093B6E File Offset: 0x00091D6E
		// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x00093B76 File Offset: 0x00091D76
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x00093B7F File Offset: 0x00091D7F
		// (set) Token: 0x06004BC4 RID: 19396 RVA: 0x00093B87 File Offset: 0x00091D87
		public uint TransactionIndex { get; set; }
	}
}
