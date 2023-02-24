using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002B9 RID: 697
	public class IRailRoomImpl : RailObject, IRailRoom, IRailComponent
	{
		// Token: 0x06002999 RID: 10649 RVA: 0x000536E7 File Offset: 0x000518E7
		internal IRailRoomImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000536F8 File Offset: 0x000518F8
		~IRailRoomImpl()
		{
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00053720 File Offset: 0x00051920
		public virtual RailResult AsyncJoinRoom(string password, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncJoinRoom(this.swigCPtr_, password, user_data);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x0005372F File Offset: 0x0005192F
		public virtual ulong GetRoomID()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetRoomID(this.swigCPtr_);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0005373C File Offset: 0x0005193C
		public virtual RailResult GetRoomName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_GetRoomName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00053784 File Offset: 0x00051984
		public virtual RailID GetOwnerID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoom_GetOwnerID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000537A9 File Offset: 0x000519A9
		public virtual bool HasPassword()
		{
			return RAIL_API_PINVOKE.IRailRoom_HasPassword(this.swigCPtr_);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000537B6 File Offset: 0x000519B6
		public virtual EnumRoomType GetRoomType()
		{
			return (EnumRoomType)RAIL_API_PINVOKE.IRailRoom_GetRoomType(this.swigCPtr_);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000537C3 File Offset: 0x000519C3
		public virtual uint GetMembers()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetMembers(this.swigCPtr_);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000537D0 File Offset: 0x000519D0
		public virtual RailID GetMemberByIndex(uint index)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoom_GetMemberByIndex(this.swigCPtr_, index);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000537F8 File Offset: 0x000519F8
		public virtual RailResult GetMemberNameByIndex(uint index, out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_GetMemberNameByIndex(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00053840 File Offset: 0x00051A40
		public virtual uint GetMaxMembers()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetMaxMembers(this.swigCPtr_);
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x0005384D File Offset: 0x00051A4D
		public virtual void Leave()
		{
			RAIL_API_PINVOKE.IRailRoom_Leave(this.swigCPtr_);
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x0005385C File Offset: 0x00051A5C
		public virtual RailResult AsyncSetNewRoomOwner(RailID new_owner_id, string user_data)
		{
			IntPtr intPtr = ((new_owner_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (new_owner_id != null)
			{
				RailConverter.Csharp2Cpp(new_owner_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetNewRoomOwner(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000538B8 File Offset: 0x00051AB8
		public virtual RailResult AsyncGetRoomMembers(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetRoomMembers(this.swigCPtr_, user_data);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000538C6 File Offset: 0x00051AC6
		public virtual RailResult AsyncGetAllRoomData(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetAllRoomData(this.swigCPtr_, user_data);
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000538D4 File Offset: 0x00051AD4
		public virtual RailResult AsyncKickOffMember(RailID member_id, string user_data)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncKickOffMember(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00053930 File Offset: 0x00051B30
		public virtual RailResult AsyncSetRoomTag(string room_tag, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetRoomTag(this.swigCPtr_, room_tag, user_data);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x0005393F File Offset: 0x00051B3F
		public virtual RailResult AsyncGetRoomTag(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetRoomTag(this.swigCPtr_, user_data);
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x00053950 File Offset: 0x00051B50
		public virtual RailResult AsyncSetRoomMetadata(List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetRoomMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000539A0 File Offset: 0x00051BA0
		public virtual RailResult AsyncGetRoomMetadata(List<string> keys, string user_data)
		{
			IntPtr intPtr = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetRoomMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000539F0 File Offset: 0x00051BF0
		public virtual RailResult AsyncClearRoomMetadata(List<string> keys, string user_data)
		{
			IntPtr intPtr = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncClearRoomMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00053A40 File Offset: 0x00051C40
		public virtual RailResult AsyncSetMemberMetadata(RailID member_id, List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			IntPtr intPtr2 = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetMemberMetadata(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x00053AC0 File Offset: 0x00051CC0
		public virtual RailResult AsyncGetMemberMetadata(RailID member_id, List<string> keys, string user_data)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			IntPtr intPtr2 = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetMemberMetadata(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x00053B40 File Offset: 0x00051D40
		public virtual RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_SendDataToMember__SWIG_0(this.swigCPtr_, intPtr, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x00053BA0 File Offset: 0x00051DA0
		public virtual RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_SendDataToMember__SWIG_1(this.swigCPtr_, intPtr, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x00053C00 File Offset: 0x00051E00
		public virtual RailResult SetGameServerID(RailID game_server_rail_id)
		{
			IntPtr intPtr = ((game_server_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_rail_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_SetGameServerID(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00053C5C File Offset: 0x00051E5C
		public virtual RailID GetGameServerID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoom_GetGameServerID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x00053C81 File Offset: 0x00051E81
		public virtual RailResult SetRoomJoinable(bool is_joinable)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_SetRoomJoinable(this.swigCPtr_, is_joinable);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x00053C8F File Offset: 0x00051E8F
		public virtual bool IsRoomJoinable()
		{
			return RAIL_API_PINVOKE.IRailRoom_IsRoomJoinable(this.swigCPtr_);
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x00053C9C File Offset: 0x00051E9C
		public virtual RailResult GetFriendsInRoom(List<RailID> friend_ids)
		{
			IntPtr intPtr = ((friend_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_GetFriendsInRoom(this.swigCPtr_, intPtr);
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

		// Token: 0x060029B8 RID: 10680 RVA: 0x00053CEC File Offset: 0x00051EEC
		public virtual bool IsUserInRoom(RailID user_rail_id)
		{
			IntPtr intPtr = ((user_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (user_rail_id != null)
			{
				RailConverter.Csharp2Cpp(user_rail_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailRoom_IsUserInRoom(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00053D48 File Offset: 0x00051F48
		public virtual RailResult EnableTeamVoice(bool enable)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_EnableTeamVoice(this.swigCPtr_, enable);
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00053D56 File Offset: 0x00051F56
		public virtual RailResult AsyncSetRoomType(EnumRoomType room_type, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetRoomType(this.swigCPtr_, (int)room_type, user_data);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x00053D65 File Offset: 0x00051F65
		public virtual RailResult AsyncSetRoomMaxMember(uint max_member, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetRoomMaxMember(this.swigCPtr_, max_member, user_data);
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x00053D74 File Offset: 0x00051F74
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00053D81 File Offset: 0x00051F81
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
