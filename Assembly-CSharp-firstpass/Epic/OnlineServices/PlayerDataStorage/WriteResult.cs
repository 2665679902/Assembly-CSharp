using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006D2 RID: 1746
	public enum WriteResult
	{
		// Token: 0x0400196D RID: 6509
		ContinueWriting = 1,
		// Token: 0x0400196E RID: 6510
		CompleteRequest,
		// Token: 0x0400196F RID: 6511
		FailRequest,
		// Token: 0x04001970 RID: 6512
		CancelRequest
	}
}
