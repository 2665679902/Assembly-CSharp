using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000469 RID: 1129
	public class VoiceChannelRemoveUsersResult : EventBase
	{
		// Token: 0x040010DF RID: 4319
		public List<RailID> success_ids = new List<RailID>();

		// Token: 0x040010E0 RID: 4320
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040010E1 RID: 4321
		public List<RailID> failed_ids = new List<RailID>();
	}
}
