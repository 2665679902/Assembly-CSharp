using System;

namespace rail
{
	// Token: 0x02000324 RID: 804
	public class EventBase
	{
		// Token: 0x04000B46 RID: 2886
		public RailResult result;

		// Token: 0x04000B47 RID: 2887
		public RailGameID game_id = new RailGameID();

		// Token: 0x04000B48 RID: 2888
		public string user_data;

		// Token: 0x04000B49 RID: 2889
		public RailID rail_id = new RailID();
	}
}
