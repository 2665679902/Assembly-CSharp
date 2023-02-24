using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000630 RID: 1584
	public class SessionModificationAddAttributeOptions
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06003E65 RID: 15973 RVA: 0x00086141 File Offset: 0x00084341
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x00086144 File Offset: 0x00084344
		// (set) Token: 0x06003E67 RID: 15975 RVA: 0x0008614C File Offset: 0x0008434C
		public AttributeData SessionAttribute { get; set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x00086155 File Offset: 0x00084355
		// (set) Token: 0x06003E69 RID: 15977 RVA: 0x0008615D File Offset: 0x0008435D
		public SessionAttributeAdvertisementType AdvertisementType { get; set; }
	}
}
