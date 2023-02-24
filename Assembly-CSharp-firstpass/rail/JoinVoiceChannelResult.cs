using System;

namespace rail
{
	// Token: 0x02000460 RID: 1120
	public class JoinVoiceChannelResult : EventBase
	{
		// Token: 0x040010CB RID: 4299
		public RailVoiceChannelID already_joined_channel_id = new RailVoiceChannelID();

		// Token: 0x040010CC RID: 4300
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();
	}
}
