using System;

namespace rail
{
	// Token: 0x0200031C RID: 796
	public class DlcInstallStartResult : EventBase
	{
		// Token: 0x04000B30 RID: 2864
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x04000B31 RID: 2865
		public new RailResult result;
	}
}
