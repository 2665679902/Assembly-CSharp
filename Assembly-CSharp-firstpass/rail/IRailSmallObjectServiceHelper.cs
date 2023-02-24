using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003E4 RID: 996
	public interface IRailSmallObjectServiceHelper
	{
		// Token: 0x06002FA7 RID: 12199
		RailResult AsyncDownloadObjects(List<uint> indexes, string user_data);

		// Token: 0x06002FA8 RID: 12200
		RailResult GetObjectContent(uint index, out string content);

		// Token: 0x06002FA9 RID: 12201
		RailResult AsyncQueryObjectState(string user_data);
	}
}
