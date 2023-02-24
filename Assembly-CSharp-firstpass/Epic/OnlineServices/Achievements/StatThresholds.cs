using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200094C RID: 2380
	public class StatThresholds
	{
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06005279 RID: 21113 RVA: 0x0009A6C7 File Offset: 0x000988C7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600527A RID: 21114 RVA: 0x0009A6CA File Offset: 0x000988CA
		// (set) Token: 0x0600527B RID: 21115 RVA: 0x0009A6D2 File Offset: 0x000988D2
		public string Name { get; set; }

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x0009A6DB File Offset: 0x000988DB
		// (set) Token: 0x0600527D RID: 21117 RVA: 0x0009A6E3 File Offset: 0x000988E3
		public int Threshold { get; set; }
	}
}
