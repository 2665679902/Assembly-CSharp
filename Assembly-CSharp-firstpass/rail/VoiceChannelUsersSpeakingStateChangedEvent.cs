using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200046B RID: 1131
	public class VoiceChannelUsersSpeakingStateChangedEvent : EventBase
	{
		// Token: 0x040010E5 RID: 4325
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040010E6 RID: 4326
		public List<RailVoiceChannelUserSpeakingState> users_speaking_state = new List<RailVoiceChannelUserSpeakingState>();
	}
}
