using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200061D RID: 1565
	public class SessionDetailsAttribute
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x0008581B File Offset: 0x00083A1B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x0008581E File Offset: 0x00083A1E
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x00085826 File Offset: 0x00083A26
		public AttributeData Data { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x0008582F File Offset: 0x00083A2F
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x00085837 File Offset: 0x00083A37
		public SessionAttributeAdvertisementType AdvertisementType { get; set; }
	}
}
