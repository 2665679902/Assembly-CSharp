using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002C8 RID: 712
	public class IRailVoiceChannelImpl : RailObject, IRailVoiceChannel, IRailComponent
	{
		// Token: 0x06002A79 RID: 10873 RVA: 0x00055FC4 File Offset: 0x000541C4
		internal IRailVoiceChannelImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00055FD4 File Offset: 0x000541D4
		~IRailVoiceChannelImpl()
		{
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00055FFC File Offset: 0x000541FC
		public virtual RailVoiceChannelID GetVoiceChannelID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailVoiceChannel_GetVoiceChannelID(this.swigCPtr_);
			RailVoiceChannelID railVoiceChannelID = new RailVoiceChannelID();
			RailConverter.Cpp2Csharp(intPtr, railVoiceChannelID);
			return railVoiceChannelID;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00056021 File Offset: 0x00054221
		public virtual string GetVoiceChannelName()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailVoiceChannel_GetVoiceChannelName(this.swigCPtr_));
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00056033 File Offset: 0x00054233
		public virtual EnumRailVoiceChannelJoinState GetJoinState()
		{
			return (EnumRailVoiceChannelJoinState)RAIL_API_PINVOKE.IRailVoiceChannel_GetJoinState(this.swigCPtr_);
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00056040 File Offset: 0x00054240
		public virtual RailResult AsyncJoinVoiceChannel(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncJoinVoiceChannel(this.swigCPtr_, user_data);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x0005604E File Offset: 0x0005424E
		public virtual RailResult AsyncLeaveVoiceChannel(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncLeaveVoiceChannel(this.swigCPtr_, user_data);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x0005605C File Offset: 0x0005425C
		public virtual RailResult GetUsers(List<RailID> user_list)
		{
			IntPtr intPtr = ((user_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_GetUsers(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (user_list != null)
				{
					RailConverter.Cpp2Csharp(intPtr, user_list);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000560AC File Offset: 0x000542AC
		public virtual RailResult AsyncAddUsers(List<RailID> user_list, string user_data)
		{
			IntPtr intPtr = ((user_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (user_list != null)
			{
				RailConverter.Csharp2Cpp(user_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncAddUsers(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000560FC File Offset: 0x000542FC
		public virtual RailResult AsyncRemoveUsers(List<RailID> user_list, string user_data)
		{
			IntPtr intPtr = ((user_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (user_list != null)
			{
				RailConverter.Csharp2Cpp(user_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncRemoveUsers(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x0005614C File Offset: 0x0005434C
		public virtual RailResult CloseChannel()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_CloseChannel(this.swigCPtr_);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00056159 File Offset: 0x00054359
		public virtual RailResult SetSelfSpeaking(bool speaking)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_SetSelfSpeaking(this.swigCPtr_, speaking);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x00056167 File Offset: 0x00054367
		public virtual bool IsSelfSpeaking()
		{
			return RAIL_API_PINVOKE.IRailVoiceChannel_IsSelfSpeaking(this.swigCPtr_);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00056174 File Offset: 0x00054374
		public virtual RailResult AsyncSetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state, string user_data)
		{
			IntPtr intPtr = ((users_speaking_state == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_0());
			if (users_speaking_state != null)
			{
				RailConverter.Csharp2Cpp(users_speaking_state, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncSetUsersSpeakingState(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailVoiceChannelUserSpeakingState(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000561C4 File Offset: 0x000543C4
		public virtual RailResult GetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state)
		{
			IntPtr intPtr = ((users_speaking_state == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_GetUsersSpeakingState(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (users_speaking_state != null)
				{
					RailConverter.Cpp2Csharp(intPtr, users_speaking_state);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailVoiceChannelUserSpeakingState(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00056214 File Offset: 0x00054414
		public virtual RailResult GetSpeakingUsers(List<RailID> speaking_users, List<RailID> not_speaking_users)
		{
			IntPtr intPtr = ((speaking_users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			IntPtr intPtr2 = ((not_speaking_users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_GetSpeakingUsers(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				if (speaking_users != null)
				{
					RailConverter.Cpp2Csharp(intPtr, speaking_users);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
				if (not_speaking_users != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, not_speaking_users);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00056284 File Offset: 0x00054484
		public virtual bool IsOwner()
		{
			return RAIL_API_PINVOKE.IRailVoiceChannel_IsOwner(this.swigCPtr_);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x00056291 File Offset: 0x00054491
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0005629E File Offset: 0x0005449E
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
