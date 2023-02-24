using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000465 RID: 1125
	public class VoiceChannelAddUsersResult : EventBase
	{
		// Token: 0x040010D6 RID: 4310
		public List<RailID> success_ids = new List<RailID>();

		// Token: 0x040010D7 RID: 4311
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040010D8 RID: 4312
		public List<RailID> failed_ids = new List<RailID>();
	}
}
