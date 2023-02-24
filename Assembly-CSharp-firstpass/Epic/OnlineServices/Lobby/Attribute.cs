using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200072B RID: 1835
	public class Attribute
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x0008CA9F File Offset: 0x0008AC9F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x0008CAA2 File Offset: 0x0008ACA2
		// (set) Token: 0x060044AA RID: 17578 RVA: 0x0008CAAA File Offset: 0x0008ACAA
		public AttributeData Data { get; set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x0008CAB3 File Offset: 0x0008ACB3
		// (set) Token: 0x060044AC RID: 17580 RVA: 0x0008CABB File Offset: 0x0008ACBB
		public LobbyAttributeVisibility Visibility { get; set; }
	}
}
