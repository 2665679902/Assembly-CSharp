using System;

namespace rail
{
	// Token: 0x02000334 RID: 820
	public class RailFriendInfo
	{
		// Token: 0x04000C64 RID: 3172
		public RailID friend_rail_id = new RailID();

		// Token: 0x04000C65 RID: 3173
		public EnumRailFriendType friend_type;

		// Token: 0x04000C66 RID: 3174
		public RailFriendOnLineState online_state = new RailFriendOnLineState();
	}
}
