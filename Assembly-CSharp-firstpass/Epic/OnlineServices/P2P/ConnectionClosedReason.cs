using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E8 RID: 1768
	public enum ConnectionClosedReason
	{
		// Token: 0x040019C5 RID: 6597
		Unknown,
		// Token: 0x040019C6 RID: 6598
		ClosedByLocalUser,
		// Token: 0x040019C7 RID: 6599
		ClosedByPeer,
		// Token: 0x040019C8 RID: 6600
		TimedOut,
		// Token: 0x040019C9 RID: 6601
		TooManyConnections,
		// Token: 0x040019CA RID: 6602
		InvalidMessage,
		// Token: 0x040019CB RID: 6603
		InvalidData,
		// Token: 0x040019CC RID: 6604
		ConnectionFailed,
		// Token: 0x040019CD RID: 6605
		ConnectionClosed,
		// Token: 0x040019CE RID: 6606
		NegotiationFailed,
		// Token: 0x040019CF RID: 6607
		UnexpectedError
	}
}
