using System;

namespace rail
{
	// Token: 0x02000309 RID: 777
	public enum EnumRailGameRefundState
	{
		// Token: 0x04000B0A RID: 2826
		kRailGameRefundStateUnknown,
		// Token: 0x04000B0B RID: 2827
		kRailGameRefundStateApplyReceived = 1000,
		// Token: 0x04000B0C RID: 2828
		kRailGameRefundStateUserCancelApply = 1100,
		// Token: 0x04000B0D RID: 2829
		kRailGameRefundStateAdminCancelApply,
		// Token: 0x04000B0E RID: 2830
		kRailGameRefundStateRefundApproved = 1150,
		// Token: 0x04000B0F RID: 2831
		kRailGameRefundStateRefundSuccess = 1200,
		// Token: 0x04000B10 RID: 2832
		kRailGameRefundStateRefundFailed
	}
}
