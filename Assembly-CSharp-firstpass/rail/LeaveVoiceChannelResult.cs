using System;

namespace rail
{
	// Token: 0x02000461 RID: 1121
	public class LeaveVoiceChannelResult : EventBase
	{
		// Token: 0x040010CD RID: 4301
		public RailVoiceChannelID voice_channel_id = new RailVoiceChannelID();

		// Token: 0x040010CE RID: 4302
		public EnumRailVoiceLeaveChannelReason reason;
	}
}
