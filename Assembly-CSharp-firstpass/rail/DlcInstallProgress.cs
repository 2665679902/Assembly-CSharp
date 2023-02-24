using System;

namespace rail
{
	// Token: 0x0200031A RID: 794
	public class DlcInstallProgress : EventBase
	{
		// Token: 0x04000B2D RID: 2861
		public RailDlcInstallProgress progress = new RailDlcInstallProgress();

		// Token: 0x04000B2E RID: 2862
		public RailDlcID dlc_id = new RailDlcID();
	}
}
