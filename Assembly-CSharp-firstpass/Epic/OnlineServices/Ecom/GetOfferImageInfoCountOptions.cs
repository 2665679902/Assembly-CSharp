using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000856 RID: 2134
	public class GetOfferImageInfoCountOptions
	{
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004C63 RID: 19555 RVA: 0x00094A03 File Offset: 0x00092C03
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x00094A06 File Offset: 0x00092C06
		// (set) Token: 0x06004C65 RID: 19557 RVA: 0x00094A0E File Offset: 0x00092C0E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004C66 RID: 19558 RVA: 0x00094A17 File Offset: 0x00092C17
		// (set) Token: 0x06004C67 RID: 19559 RVA: 0x00094A1F File Offset: 0x00092C1F
		public string OfferId { get; set; }
	}
}
