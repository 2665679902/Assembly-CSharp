using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000456 RID: 1110
	public interface IRailVoiceChannel : IRailComponent
	{
		// Token: 0x06003099 RID: 12441
		RailVoiceChannelID GetVoiceChannelID();

		// Token: 0x0600309A RID: 12442
		string GetVoiceChannelName();

		// Token: 0x0600309B RID: 12443
		EnumRailVoiceChannelJoinState GetJoinState();

		// Token: 0x0600309C RID: 12444
		RailResult AsyncJoinVoiceChannel(string user_data);

		// Token: 0x0600309D RID: 12445
		RailResult AsyncLeaveVoiceChannel(string user_data);

		// Token: 0x0600309E RID: 12446
		RailResult GetUsers(List<RailID> user_list);

		// Token: 0x0600309F RID: 12447
		RailResult AsyncAddUsers(List<RailID> user_list, string user_data);

		// Token: 0x060030A0 RID: 12448
		RailResult AsyncRemoveUsers(List<RailID> user_list, string user_data);

		// Token: 0x060030A1 RID: 12449
		RailResult CloseChannel();

		// Token: 0x060030A2 RID: 12450
		RailResult SetSelfSpeaking(bool speaking);

		// Token: 0x060030A3 RID: 12451
		bool IsSelfSpeaking();

		// Token: 0x060030A4 RID: 12452
		RailResult AsyncSetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state, string user_data);

		// Token: 0x060030A5 RID: 12453
		RailResult GetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state);

		// Token: 0x060030A6 RID: 12454
		RailResult GetSpeakingUsers(List<RailID> speaking_users, List<RailID> not_speaking_users);

		// Token: 0x060030A7 RID: 12455
		bool IsOwner();
	}
}
