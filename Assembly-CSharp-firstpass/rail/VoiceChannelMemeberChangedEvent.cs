using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000467 RID: 1127
	public class VoiceChannelMemeberChangedEvent : EventBase
	{
		// Token: 0x040010DC RID: 4316
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040010DD RID: 4317
		public List<RailID> member_ids = new List<RailID>();
	}
}
