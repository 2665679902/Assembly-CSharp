using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200060A RID: 1546
	public enum OnlineSessionState
	{
		// Token: 0x04001752 RID: 5970
		NoSession,
		// Token: 0x04001753 RID: 5971
		Creating,
		// Token: 0x04001754 RID: 5972
		Pending,
		// Token: 0x04001755 RID: 5973
		Starting,
		// Token: 0x04001756 RID: 5974
		InProgress,
		// Token: 0x04001757 RID: 5975
		Ending,
		// Token: 0x04001758 RID: 5976
		Ended,
		// Token: 0x04001759 RID: 5977
		Destroying
	}
}
