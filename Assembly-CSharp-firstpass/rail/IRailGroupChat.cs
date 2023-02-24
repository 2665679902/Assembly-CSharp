using System;

namespace rail
{
	// Token: 0x02000365 RID: 869
	public interface IRailGroupChat : IRailComponent
	{
		// Token: 0x06002EC2 RID: 11970
		RailResult GetGroupInfo(RailGroupInfo group_info);

		// Token: 0x06002EC3 RID: 11971
		RailResult OpenGroupWindow();
	}
}
