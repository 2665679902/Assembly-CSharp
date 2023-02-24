using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200046A RID: 1130
	public class VoiceChannelSpeakingUsersChangedEvent : EventBase
	{
		// Token: 0x040010E2 RID: 4322
		public List<RailID> speaking_users = new List<RailID>();

		// Token: 0x040010E3 RID: 4323
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040010E4 RID: 4324
		public List<RailID> not_speaking_users = new List<RailID>();
	}
}
