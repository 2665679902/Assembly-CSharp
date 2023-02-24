using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002A2 RID: 674
	public class IRailFriendsImpl : RailObject, IRailFriends
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x00050712 File Offset: 0x0004E912
		internal IRailFriendsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x00050724 File Offset: 0x0004E924
		~IRailFriendsImpl()
		{
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0005074C File Offset: 0x0004E94C
		public virtual RailResult AsyncGetPersonalInfo(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncGetPersonalInfo(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0005079C File Offset: 0x0004E99C
		public virtual RailResult AsyncGetFriendMetadata(RailID rail_id, List<string> keys, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			IntPtr intPtr2 = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncGetFriendMetadata(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0005081C File Offset: 0x0004EA1C
		public virtual RailResult AsyncSetMyMetadata(List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncSetMyMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0005086C File Offset: 0x0004EA6C
		public virtual RailResult AsyncClearAllMyMetadata(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncClearAllMyMetadata(this.swigCPtr_, user_data);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0005087A File Offset: 0x0004EA7A
		public virtual RailResult AsyncSetInviteCommandLine(string command_line, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncSetInviteCommandLine(this.swigCPtr_, command_line, user_data);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0005088C File Offset: 0x0004EA8C
		public virtual RailResult AsyncGetInviteCommandLine(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncGetInviteCommandLine(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000508E8 File Offset: 0x0004EAE8
		public virtual RailResult AsyncReportPlayedWithUserList(List<RailUserPlayedWith> player_list, string user_data)
		{
			IntPtr intPtr = ((player_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailUserPlayedWith__SWIG_0());
			if (player_list != null)
			{
				RailConverter.Csharp2Cpp(player_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncReportPlayedWithUserList(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailUserPlayedWith(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00050938 File Offset: 0x0004EB38
		public virtual RailResult GetFriendsList(List<RailFriendInfo> friends_list)
		{
			IntPtr intPtr = ((friends_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailFriendInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_GetFriendsList(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (friends_list != null)
				{
					RailConverter.Cpp2Csharp(intPtr, friends_list);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailFriendInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00050988 File Offset: 0x0004EB88
		public virtual RailResult AsyncQueryFriendPlayedGamesInfo(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryFriendPlayedGamesInfo(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000509E4 File Offset: 0x0004EBE4
		public virtual RailResult AsyncQueryPlayedWithFriendsList(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryPlayedWithFriendsList(this.swigCPtr_, user_data);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000509F4 File Offset: 0x0004EBF4
		public virtual RailResult AsyncQueryPlayedWithFriendsTime(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryPlayedWithFriendsTime(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x00050A44 File Offset: 0x0004EC44
		public virtual RailResult AsyncQueryPlayedWithFriendsGames(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryPlayedWithFriendsGames(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x00050A94 File Offset: 0x0004EC94
		public virtual RailResult AsyncAddFriend(RailFriendsAddFriendRequest request, string user_data)
		{
			IntPtr intPtr = ((request == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailFriendsAddFriendRequest__SWIG_0());
			if (request != null)
			{
				RailConverter.Csharp2Cpp(request, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncAddFriend(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailFriendsAddFriendRequest(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00050AE4 File Offset: 0x0004ECE4
		public virtual RailResult AsyncUpdateFriendsData(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncUpdateFriendsData(this.swigCPtr_, user_data);
		}
	}
}
