using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000838 RID: 2104
	public class CopyItemImageInfoByIndexOptions
	{
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004B55 RID: 19285 RVA: 0x000934D3 File Offset: 0x000916D3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x000934D6 File Offset: 0x000916D6
		// (set) Token: 0x06004B57 RID: 19287 RVA: 0x000934DE File Offset: 0x000916DE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06004B58 RID: 19288 RVA: 0x000934E7 File Offset: 0x000916E7
		// (set) Token: 0x06004B59 RID: 19289 RVA: 0x000934EF File Offset: 0x000916EF
		public string ItemId { get; set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06004B5A RID: 19290 RVA: 0x000934F8 File Offset: 0x000916F8
		// (set) Token: 0x06004B5B RID: 19291 RVA: 0x00093500 File Offset: 0x00091700
		public uint ImageInfoIndex { get; set; }
	}
}
