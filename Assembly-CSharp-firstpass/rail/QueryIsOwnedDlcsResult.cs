using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000320 RID: 800
	public class QueryIsOwnedDlcsResult : EventBase
	{
		// Token: 0x04000B38 RID: 2872
		public List<RailDlcOwned> dlc_owned_list = new List<RailDlcOwned>();
	}
}
