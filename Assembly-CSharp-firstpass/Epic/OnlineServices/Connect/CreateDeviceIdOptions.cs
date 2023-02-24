using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000897 RID: 2199
	public class CreateDeviceIdOptions
	{
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004DFF RID: 19967 RVA: 0x00096502 File Offset: 0x00094702
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x00096505 File Offset: 0x00094705
		// (set) Token: 0x06004E01 RID: 19969 RVA: 0x0009650D File Offset: 0x0009470D
		public string DeviceModel { get; set; }
	}
}
