using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000351 RID: 849
	public interface IRailGameServer : IRailComponent
	{
		// Token: 0x06002E74 RID: 11892
		RailID GetGameServerRailID();

		// Token: 0x06002E75 RID: 11893
		RailResult GetGameServerName(out string name);

		// Token: 0x06002E76 RID: 11894
		RailResult GetGameServerFullName(out string full_name);

		// Token: 0x06002E77 RID: 11895
		RailID GetOwnerRailID();

		// Token: 0x06002E78 RID: 11896
		bool SetHost(string game_server_host);

		// Token: 0x06002E79 RID: 11897
		bool GetHost(out string game_server_host);

		// Token: 0x06002E7A RID: 11898
		bool SetMapName(string game_server_map);

		// Token: 0x06002E7B RID: 11899
		bool GetMapName(out string game_server_map);

		// Token: 0x06002E7C RID: 11900
		bool SetPasswordProtect(bool has_password);

		// Token: 0x06002E7D RID: 11901
		bool GetPasswordProtect();

		// Token: 0x06002E7E RID: 11902
		bool SetMaxPlayers(uint max_player_count);

		// Token: 0x06002E7F RID: 11903
		uint GetMaxPlayers();

		// Token: 0x06002E80 RID: 11904
		bool SetBotPlayers(uint bot_player_count);

		// Token: 0x06002E81 RID: 11905
		uint GetBotPlayers();

		// Token: 0x06002E82 RID: 11906
		bool SetGameServerDescription(string game_server_description);

		// Token: 0x06002E83 RID: 11907
		bool GetGameServerDescription(out string game_server_description);

		// Token: 0x06002E84 RID: 11908
		bool SetGameServerTags(string game_server_tags);

		// Token: 0x06002E85 RID: 11909
		bool GetGameServerTags(out string game_server_tags);

		// Token: 0x06002E86 RID: 11910
		bool SetMods(List<string> server_mods);

		// Token: 0x06002E87 RID: 11911
		bool GetMods(List<string> server_mods);

		// Token: 0x06002E88 RID: 11912
		bool SetSpectatorHost(string spectator_host);

		// Token: 0x06002E89 RID: 11913
		bool GetSpectatorHost(out string spectator_host);

		// Token: 0x06002E8A RID: 11914
		bool SetGameServerVersion(string version);

		// Token: 0x06002E8B RID: 11915
		bool GetGameServerVersion(out string version);

		// Token: 0x06002E8C RID: 11916
		bool SetIsFriendOnly(bool is_friend_only);

		// Token: 0x06002E8D RID: 11917
		bool GetIsFriendOnly();

		// Token: 0x06002E8E RID: 11918
		bool ClearAllMetadata();

		// Token: 0x06002E8F RID: 11919
		RailResult GetMetadata(string key, out string value);

		// Token: 0x06002E90 RID: 11920
		RailResult SetMetadata(string key, string value);

		// Token: 0x06002E91 RID: 11921
		RailResult AsyncSetMetadata(List<RailKeyValue> key_values, string user_data);

		// Token: 0x06002E92 RID: 11922
		RailResult AsyncGetMetadata(List<string> keys, string user_data);

		// Token: 0x06002E93 RID: 11923
		RailResult AsyncGetAllMetadata(string user_data);

		// Token: 0x06002E94 RID: 11924
		RailResult AsyncAcquireGameServerSessionTicket(string user_data);

		// Token: 0x06002E95 RID: 11925
		RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data);

		// Token: 0x06002E96 RID: 11926
		void TerminateSessionOfPlayer(RailID player_rail_id);

		// Token: 0x06002E97 RID: 11927
		void AbandonGameServerSessionTicket(RailSessionTicket session_ticket);

		// Token: 0x06002E98 RID: 11928
		RailResult ReportPlayerJoinGameServer(List<GameServerPlayerInfo> player_infos);

		// Token: 0x06002E99 RID: 11929
		RailResult ReportPlayerQuitGameServer(List<GameServerPlayerInfo> player_infos);

		// Token: 0x06002E9A RID: 11930
		RailResult UpdateGameServerPlayerList(List<GameServerPlayerInfo> player_infos);

		// Token: 0x06002E9B RID: 11931
		uint GetCurrentPlayers();

		// Token: 0x06002E9C RID: 11932
		void RemoveAllPlayers();

		// Token: 0x06002E9D RID: 11933
		RailResult RegisterToGameServerList();

		// Token: 0x06002E9E RID: 11934
		RailResult UnregisterFromGameServerList();

		// Token: 0x06002E9F RID: 11935
		RailResult CloseGameServer();

		// Token: 0x06002EA0 RID: 11936
		RailResult GetFriendsInGameServer(List<RailID> friend_ids);

		// Token: 0x06002EA1 RID: 11937
		bool IsUserInGameServer(RailID user_rail_id);

		// Token: 0x06002EA2 RID: 11938
		bool SetServerInfo(string server_info);

		// Token: 0x06002EA3 RID: 11939
		bool GetServerInfo(out string server_info);

		// Token: 0x06002EA4 RID: 11940
		RailResult EnableTeamVoice(bool enable);
	}
}
