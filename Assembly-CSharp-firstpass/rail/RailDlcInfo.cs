using System;

namespace rail
{
	// Token: 0x02000321 RID: 801
	public class RailDlcInfo
	{
		// Token: 0x04000B39 RID: 2873
		public double original_price;

		// Token: 0x04000B3A RID: 2874
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x04000B3B RID: 2875
		public string description;

		// Token: 0x04000B3C RID: 2876
		public double discount_price;

		// Token: 0x04000B3D RID: 2877
		public string version;

		// Token: 0x04000B3E RID: 2878
		public RailGameID game_id = new RailGameID();

		// Token: 0x04000B3F RID: 2879
		public string name;
	}
}
