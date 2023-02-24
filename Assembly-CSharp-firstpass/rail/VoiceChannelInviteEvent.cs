using System;

namespace rail
{
	// Token: 0x02000466 RID: 1126
	public class VoiceChannelInviteEvent : EventBase
	{
		// Token: 0x040010D9 RID: 4313
		public string channel_name;

		// Token: 0x040010DA RID: 4314
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040010DB RID: 4315
		public string inviter_name;
	}
}
