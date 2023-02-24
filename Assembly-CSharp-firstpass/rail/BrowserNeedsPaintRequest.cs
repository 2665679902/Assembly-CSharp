using System;

namespace rail
{
	// Token: 0x020002FA RID: 762
	public class BrowserNeedsPaintRequest : EventBase
	{
		// Token: 0x04000ACA RID: 2762
		public uint bgra_width;

		// Token: 0x04000ACB RID: 2763
		public uint scroll_y_pos;

		// Token: 0x04000ACC RID: 2764
		public string bgra_data;

		// Token: 0x04000ACD RID: 2765
		public float page_scale_factor;

		// Token: 0x04000ACE RID: 2766
		public int offset_x;

		// Token: 0x04000ACF RID: 2767
		public uint scroll_x_pos;

		// Token: 0x04000AD0 RID: 2768
		public uint bgra_height;

		// Token: 0x04000AD1 RID: 2769
		public int offset_y;
	}
}
