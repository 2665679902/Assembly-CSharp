using System;

namespace rail
{
	// Token: 0x020003CB RID: 971
	public class NotifyMetadataChange : EventBase
	{
		// Token: 0x04000F33 RID: 3891
		public RailID changer_id = new RailID();

		// Token: 0x04000F34 RID: 3892
		public ulong room_id;
	}
}
