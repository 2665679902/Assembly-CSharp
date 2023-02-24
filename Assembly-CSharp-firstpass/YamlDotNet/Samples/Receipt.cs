using System;

namespace YamlDotNet.Samples
{
	// Token: 0x020001F4 RID: 500
	public class Receipt
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003D7A9 File Offset: 0x0003B9A9
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0003D7B1 File Offset: 0x0003B9B1
		public string receipt { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0003D7BA File Offset: 0x0003B9BA
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x0003D7C2 File Offset: 0x0003B9C2
		public DateTime date { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0003D7CB File Offset: 0x0003B9CB
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0003D7D3 File Offset: 0x0003B9D3
		public Customer customer { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003D7DC File Offset: 0x0003B9DC
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		public Item[] items { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003D7ED File Offset: 0x0003B9ED
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0003D7F5 File Offset: 0x0003B9F5
		public Address bill_to { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003D7FE File Offset: 0x0003B9FE
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0003D806 File Offset: 0x0003BA06
		public Address ship_to { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003D80F File Offset: 0x0003BA0F
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0003D817 File Offset: 0x0003BA17
		public string specialDelivery { get; set; }
	}
}
