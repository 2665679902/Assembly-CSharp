using System;

namespace rail
{
	// Token: 0x0200031D RID: 797
	public class DlcOwnershipChanged : EventBase
	{
		// Token: 0x04000B32 RID: 2866
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x04000B33 RID: 2867
		public bool is_active;
	}
}
