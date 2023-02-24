using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200072D RID: 1837
	public class AttributeData
	{
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x0008CB73 File Offset: 0x0008AD73
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x0008CB76 File Offset: 0x0008AD76
		// (set) Token: 0x060044B7 RID: 17591 RVA: 0x0008CB7E File Offset: 0x0008AD7E
		public string Key { get; set; }

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x0008CB87 File Offset: 0x0008AD87
		// (set) Token: 0x060044B9 RID: 17593 RVA: 0x0008CB8F File Offset: 0x0008AD8F
		public AttributeDataValue Value { get; set; }
	}
}
