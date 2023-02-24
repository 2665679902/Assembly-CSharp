using System;

namespace rail
{
	// Token: 0x0200031F RID: 799
	public class DlcUninstallFinished : EventBase
	{
		// Token: 0x04000B36 RID: 2870
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x04000B37 RID: 2871
		public new RailResult result;
	}
}
