using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200064B RID: 1611
	public class SessionSearchRemoveParameterOptions
	{
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x0008699F File Offset: 0x00084B9F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x000869A2 File Offset: 0x00084BA2
		// (set) Token: 0x06003EEE RID: 16110 RVA: 0x000869AA File Offset: 0x00084BAA
		public string Key { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000869B3 File Offset: 0x00084BB3
		// (set) Token: 0x06003EF0 RID: 16112 RVA: 0x000869BB File Offset: 0x00084BBB
		public ComparisonOp ComparisonOp { get; set; }
	}
}
