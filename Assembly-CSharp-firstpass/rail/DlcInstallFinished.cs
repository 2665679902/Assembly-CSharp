using System;

namespace rail
{
	// Token: 0x02000319 RID: 793
	public class DlcInstallFinished : EventBase
	{
		// Token: 0x04000B2B RID: 2859
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x04000B2C RID: 2860
		public new RailResult result;
	}
}
