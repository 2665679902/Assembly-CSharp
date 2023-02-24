using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005B7 RID: 1463
	public class Stat
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x00083B07 File Offset: 0x00081D07
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x00083B0A File Offset: 0x00081D0A
		// (set) Token: 0x06003BC7 RID: 15303 RVA: 0x00083B12 File Offset: 0x00081D12
		public string Name { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x00083B1B File Offset: 0x00081D1B
		// (set) Token: 0x06003BC9 RID: 15305 RVA: 0x00083B23 File Offset: 0x00081D23
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x00083B2C File Offset: 0x00081D2C
		// (set) Token: 0x06003BCB RID: 15307 RVA: 0x00083B34 File Offset: 0x00081D34
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x00083B3D File Offset: 0x00081D3D
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x00083B45 File Offset: 0x00081D45
		public int Value { get; set; }
	}
}
