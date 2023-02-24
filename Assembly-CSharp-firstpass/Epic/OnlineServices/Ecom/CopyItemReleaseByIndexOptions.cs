using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200083A RID: 2106
	public class CopyItemReleaseByIndexOptions
	{
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06004B66 RID: 19302 RVA: 0x000935E3 File Offset: 0x000917E3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x000935E6 File Offset: 0x000917E6
		// (set) Token: 0x06004B68 RID: 19304 RVA: 0x000935EE File Offset: 0x000917EE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06004B69 RID: 19305 RVA: 0x000935F7 File Offset: 0x000917F7
		// (set) Token: 0x06004B6A RID: 19306 RVA: 0x000935FF File Offset: 0x000917FF
		public string ItemId { get; set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06004B6B RID: 19307 RVA: 0x00093608 File Offset: 0x00091808
		// (set) Token: 0x06004B6C RID: 19308 RVA: 0x00093610 File Offset: 0x00091810
		public uint ReleaseIndex { get; set; }
	}
}
