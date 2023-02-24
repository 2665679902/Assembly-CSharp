using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003B9 RID: 953
	public interface IRailRoom : IRailComponent
	{
		// Token: 0x06002F53 RID: 12115
		RailResult AsyncJoinRoom(string password, string user_data);

		// Token: 0x06002F54 RID: 12116
		ulong GetRoomID();

		// Token: 0x06002F55 RID: 12117
		RailResult GetRoomName(out string name);

		// Token: 0x06002F56 RID: 12118
		RailID GetOwnerID();

		// Token: 0x06002F57 RID: 12119
		bool HasPassword();

		// Token: 0x06002F58 RID: 12120
		EnumRoomType GetRoomType();

		// Token: 0x06002F59 RID: 12121
		uint GetMembers();

		// Token: 0x06002F5A RID: 12122
		RailID GetMemberByIndex(uint index);

		// Token: 0x06002F5B RID: 12123
		RailResult GetMemberNameByIndex(uint index, out string name);

		// Token: 0x06002F5C RID: 12124
		uint GetMaxMembers();

		// Token: 0x06002F5D RID: 12125
		void Leave();

		// Token: 0x06002F5E RID: 12126
		RailResult AsyncSetNewRoomOwner(RailID new_owner_id, string user_data);

		// Token: 0x06002F5F RID: 12127
		RailResult AsyncGetRoomMembers(string user_data);

		// Token: 0x06002F60 RID: 12128
		RailResult AsyncGetAllRoomData(string user_data);

		// Token: 0x06002F61 RID: 12129
		RailResult AsyncKickOffMember(RailID member_id, string user_data);

		// Token: 0x06002F62 RID: 12130
		RailResult AsyncSetRoomTag(string room_tag, string user_data);

		// Token: 0x06002F63 RID: 12131
		RailResult AsyncGetRoomTag(string user_data);

		// Token: 0x06002F64 RID: 12132
		RailResult AsyncSetRoomMetadata(List<RailKeyValue> key_values, string user_data);

		// Token: 0x06002F65 RID: 12133
		RailResult AsyncGetRoomMetadata(List<string> keys, string user_data);

		// Token: 0x06002F66 RID: 12134
		RailResult AsyncClearRoomMetadata(List<string> keys, string user_data);

		// Token: 0x06002F67 RID: 12135
		RailResult AsyncSetMemberMetadata(RailID member_id, List<RailKeyValue> key_values, string user_data);

		// Token: 0x06002F68 RID: 12136
		RailResult AsyncGetMemberMetadata(RailID member_id, List<string> keys, string user_data);

		// Token: 0x06002F69 RID: 12137
		RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06002F6A RID: 12138
		RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x06002F6B RID: 12139
		RailResult SetGameServerID(RailID game_server_rail_id);

		// Token: 0x06002F6C RID: 12140
		RailID GetGameServerID();

		// Token: 0x06002F6D RID: 12141
		RailResult SetRoomJoinable(bool is_joinable);

		// Token: 0x06002F6E RID: 12142
		bool IsRoomJoinable();

		// Token: 0x06002F6F RID: 12143
		RailResult GetFriendsInRoom(List<RailID> friend_ids);

		// Token: 0x06002F70 RID: 12144
		bool IsUserInRoom(RailID user_rail_id);

		// Token: 0x06002F71 RID: 12145
		RailResult EnableTeamVoice(bool enable);

		// Token: 0x06002F72 RID: 12146
		RailResult AsyncSetRoomType(EnumRoomType room_type, string user_data);

		// Token: 0x06002F73 RID: 12147
		RailResult AsyncSetRoomMaxMember(uint max_member, string user_data);
	}
}
