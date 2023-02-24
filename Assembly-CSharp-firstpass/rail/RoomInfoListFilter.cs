using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003D4 RID: 980
	public class RoomInfoListFilter
	{
		// Token: 0x04000F56 RID: 3926
		public EnumRailOptionalValue filter_friends_in_room;

		// Token: 0x04000F57 RID: 3927
		public string room_tag;

		// Token: 0x04000F58 RID: 3928
		public uint available_slot_at_least;

		// Token: 0x04000F59 RID: 3929
		public EnumRailOptionalValue filter_password;

		// Token: 0x04000F5A RID: 3930
		public string room_name_contained;

		// Token: 0x04000F5B RID: 3931
		public List<RoomInfoListFilterKey> key_filters = new List<RoomInfoListFilterKey>();

		// Token: 0x04000F5C RID: 3932
		public EnumRailOptionalValue filter_friends_owned;
	}
}
