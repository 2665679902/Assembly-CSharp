using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002A4 RID: 676
	public class IRailGameServerImpl : RailObject, IRailGameServer, IRailComponent
	{
		// Token: 0x060028A6 RID: 10406 RVA: 0x00050E10 File Offset: 0x0004F010
		internal IRailGameServerImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x00050E20 File Offset: 0x0004F020
		~IRailGameServerImpl()
		{
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x00050E48 File Offset: 0x0004F048
		public virtual RailID GetGameServerRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGameServer_GetGameServerRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x00050E70 File Offset: 0x0004F070
		public virtual RailResult GetGameServerName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetGameServerName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x00050EB8 File Offset: 0x0004F0B8
		public virtual RailResult GetGameServerFullName(out string full_name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetGameServerFullName(this.swigCPtr_, intPtr);
			}
			finally
			{
				full_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x00050F00 File Offset: 0x0004F100
		public virtual RailID GetOwnerRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGameServer_GetOwnerRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x00050F25 File Offset: 0x0004F125
		public virtual bool SetHost(string game_server_host)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetHost(this.swigCPtr_, game_server_host);
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00050F34 File Offset: 0x0004F134
		public virtual bool GetHost(out string game_server_host)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetHost(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_host = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00050F7C File Offset: 0x0004F17C
		public virtual bool SetMapName(string game_server_map)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetMapName(this.swigCPtr_, game_server_map);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x00050F8C File Offset: 0x0004F18C
		public virtual bool GetMapName(out string game_server_map)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetMapName(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_map = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x00050FD4 File Offset: 0x0004F1D4
		public virtual bool SetPasswordProtect(bool has_password)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetPasswordProtect(this.swigCPtr_, has_password);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x00050FE2 File Offset: 0x0004F1E2
		public virtual bool GetPasswordProtect()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetPasswordProtect(this.swigCPtr_);
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00050FEF File Offset: 0x0004F1EF
		public virtual bool SetMaxPlayers(uint max_player_count)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetMaxPlayers(this.swigCPtr_, max_player_count);
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x00050FFD File Offset: 0x0004F1FD
		public virtual uint GetMaxPlayers()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetMaxPlayers(this.swigCPtr_);
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0005100A File Offset: 0x0004F20A
		public virtual bool SetBotPlayers(uint bot_player_count)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetBotPlayers(this.swigCPtr_, bot_player_count);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x00051018 File Offset: 0x0004F218
		public virtual uint GetBotPlayers()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetBotPlayers(this.swigCPtr_);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x00051025 File Offset: 0x0004F225
		public virtual bool SetGameServerDescription(string game_server_description)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetGameServerDescription(this.swigCPtr_, game_server_description);
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x00051034 File Offset: 0x0004F234
		public virtual bool GetGameServerDescription(out string game_server_description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetGameServerDescription(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x0005107C File Offset: 0x0004F27C
		public virtual bool SetGameServerTags(string game_server_tags)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetGameServerTags(this.swigCPtr_, game_server_tags);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0005108C File Offset: 0x0004F28C
		public virtual bool GetGameServerTags(out string game_server_tags)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetGameServerTags(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_tags = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000510D4 File Offset: 0x0004F2D4
		public virtual bool SetMods(List<string> server_mods)
		{
			IntPtr intPtr = ((server_mods == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (server_mods != null)
			{
				RailConverter.Csharp2Cpp(server_mods, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_SetMods(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x00051124 File Offset: 0x0004F324
		public virtual bool GetMods(List<string> server_mods)
		{
			IntPtr intPtr = ((server_mods == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetMods(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (server_mods != null)
				{
					RailConverter.Cpp2Csharp(intPtr, server_mods);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x00051174 File Offset: 0x0004F374
		public virtual bool SetSpectatorHost(string spectator_host)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetSpectatorHost(this.swigCPtr_, spectator_host);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x00051184 File Offset: 0x0004F384
		public virtual bool GetSpectatorHost(out string spectator_host)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetSpectatorHost(this.swigCPtr_, intPtr);
			}
			finally
			{
				spectator_host = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000511CC File Offset: 0x0004F3CC
		public virtual bool SetGameServerVersion(string version)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetGameServerVersion(this.swigCPtr_, version);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000511DC File Offset: 0x0004F3DC
		public virtual bool GetGameServerVersion(out string version)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetGameServerVersion(this.swigCPtr_, intPtr);
			}
			finally
			{
				version = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x00051224 File Offset: 0x0004F424
		public virtual bool SetIsFriendOnly(bool is_friend_only)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetIsFriendOnly(this.swigCPtr_, is_friend_only);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x00051232 File Offset: 0x0004F432
		public virtual bool GetIsFriendOnly()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetIsFriendOnly(this.swigCPtr_);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0005123F File Offset: 0x0004F43F
		public virtual bool ClearAllMetadata()
		{
			return RAIL_API_PINVOKE.IRailGameServer_ClearAllMetadata(this.swigCPtr_);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x0005124C File Offset: 0x0004F44C
		public virtual RailResult GetMetadata(string key, out string value)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetMetadata(this.swigCPtr_, key, intPtr);
			}
			finally
			{
				value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x00051294 File Offset: 0x0004F494
		public virtual RailResult SetMetadata(string key, string value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_SetMetadata(this.swigCPtr_, key, value);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000512A4 File Offset: 0x0004F4A4
		public virtual RailResult AsyncSetMetadata(List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncSetMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000512F4 File Offset: 0x0004F4F4
		public virtual RailResult AsyncGetMetadata(List<string> keys, string user_data)
		{
			IntPtr intPtr = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncGetMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x00051344 File Offset: 0x0004F544
		public virtual RailResult AsyncGetAllMetadata(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncGetAllMetadata(this.swigCPtr_, user_data);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x00051352 File Offset: 0x0004F552
		public virtual RailResult AsyncAcquireGameServerSessionTicket(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncAcquireGameServerSessionTicket(this.swigCPtr_, user_data);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x00051360 File Offset: 0x0004F560
		public virtual RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data)
		{
			IntPtr intPtr = ((player_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (player_ticket != null)
			{
				RailConverter.Csharp2Cpp(player_ticket, intPtr);
			}
			IntPtr intPtr2 = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncStartSessionWithPlayer(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000513E0 File Offset: 0x0004F5E0
		public virtual void TerminateSessionOfPlayer(RailID player_rail_id)
		{
			IntPtr intPtr = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailGameServer_TerminateSessionOfPlayer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0005143C File Offset: 0x0004F63C
		public virtual void AbandonGameServerSessionTicket(RailSessionTicket session_ticket)
		{
			IntPtr intPtr = ((session_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (session_ticket != null)
			{
				RailConverter.Csharp2Cpp(session_ticket, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailGameServer_AbandonGameServerSessionTicket(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0005148C File Offset: 0x0004F68C
		public virtual RailResult ReportPlayerJoinGameServer(List<GameServerPlayerInfo> player_infos)
		{
			IntPtr intPtr = ((player_infos == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0());
			if (player_infos != null)
			{
				RailConverter.Csharp2Cpp(player_infos, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_ReportPlayerJoinGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000514DC File Offset: 0x0004F6DC
		public virtual RailResult ReportPlayerQuitGameServer(List<GameServerPlayerInfo> player_infos)
		{
			IntPtr intPtr = ((player_infos == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0());
			if (player_infos != null)
			{
				RailConverter.Csharp2Cpp(player_infos, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_ReportPlayerQuitGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0005152C File Offset: 0x0004F72C
		public virtual RailResult UpdateGameServerPlayerList(List<GameServerPlayerInfo> player_infos)
		{
			IntPtr intPtr = ((player_infos == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0());
			if (player_infos != null)
			{
				RailConverter.Csharp2Cpp(player_infos, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_UpdateGameServerPlayerList(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0005157C File Offset: 0x0004F77C
		public virtual uint GetCurrentPlayers()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetCurrentPlayers(this.swigCPtr_);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x00051589 File Offset: 0x0004F789
		public virtual void RemoveAllPlayers()
		{
			RAIL_API_PINVOKE.IRailGameServer_RemoveAllPlayers(this.swigCPtr_);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x00051596 File Offset: 0x0004F796
		public virtual RailResult RegisterToGameServerList()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_RegisterToGameServerList(this.swigCPtr_);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000515A3 File Offset: 0x0004F7A3
		public virtual RailResult UnregisterFromGameServerList()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_UnregisterFromGameServerList(this.swigCPtr_);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000515B0 File Offset: 0x0004F7B0
		public virtual RailResult CloseGameServer()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_CloseGameServer(this.swigCPtr_);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000515C0 File Offset: 0x0004F7C0
		public virtual RailResult GetFriendsInGameServer(List<RailID> friend_ids)
		{
			IntPtr intPtr = ((friend_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetFriendsInGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (friend_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, friend_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x00051610 File Offset: 0x0004F810
		public virtual bool IsUserInGameServer(RailID user_rail_id)
		{
			IntPtr intPtr = ((user_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (user_rail_id != null)
			{
				RailConverter.Csharp2Cpp(user_rail_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_IsUserInGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0005166C File Offset: 0x0004F86C
		public virtual bool SetServerInfo(string server_info)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetServerInfo(this.swigCPtr_, server_info);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0005167C File Offset: 0x0004F87C
		public virtual bool GetServerInfo(out string server_info)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetServerInfo(this.swigCPtr_, intPtr);
			}
			finally
			{
				server_info = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000516C4 File Offset: 0x0004F8C4
		public virtual RailResult EnableTeamVoice(bool enable)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_EnableTeamVoice(this.swigCPtr_, enable);
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000516D2 File Offset: 0x0004F8D2
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000516DF File Offset: 0x0004F8DF
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
