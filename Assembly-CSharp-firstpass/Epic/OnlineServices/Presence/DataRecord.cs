using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200066C RID: 1644
	public class DataRecord
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x00087C6B File Offset: 0x00085E6B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x00087C6E File Offset: 0x00085E6E
		// (set) Token: 0x06003FCD RID: 16333 RVA: 0x00087C76 File Offset: 0x00085E76
		public string Key { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x00087C7F File Offset: 0x00085E7F
		// (set) Token: 0x06003FCF RID: 16335 RVA: 0x00087C87 File Offset: 0x00085E87
		public string Value { get; set; }
	}
}
