using System;

namespace rail
{
	// Token: 0x0200031E RID: 798
	public class DlcRefundChanged : EventBase
	{
		// Token: 0x04000B34 RID: 2868
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x04000B35 RID: 2869
		public EnumRailGameRefundState refund_state;
	}
}
