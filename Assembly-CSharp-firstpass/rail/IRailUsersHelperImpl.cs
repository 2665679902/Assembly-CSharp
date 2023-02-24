using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002C5 RID: 709
	public class IRailUsersHelperImpl : RailObject, IRailUsersHelper
	{
		// Token: 0x06002A4B RID: 10827 RVA: 0x00055424 File Offset: 0x00053624
		internal IRailUsersHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x00055434 File Offset: 0x00053634
		~IRailUsersHelperImpl()
		{
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0005545C File Offset: 0x0005365C
		public virtual RailResult AsyncGetUsersInfo(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetUsersInfo(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000554AC File Offset: 0x000536AC
		public virtual RailResult AsyncInviteUsers(string command_line, List<RailID> users, RailInviteOptions options, string user_data)
		{
			IntPtr intPtr = ((users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (users != null)
			{
				RailConverter.Csharp2Cpp(users, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailInviteOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncInviteUsers(this.swigCPtr_, command_line, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
				RAIL_API_PINVOKE.delete_RailInviteOptions(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00055520 File Offset: 0x00053720
		public virtual RailResult AsyncGetInviteDetail(RailID inviter, EnumRailUsersInviteType invite_type, string user_data)
		{
			IntPtr intPtr = ((inviter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (inviter != null)
			{
				RailConverter.Csharp2Cpp(inviter, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetInviteDetail(this.swigCPtr_, intPtr, (int)invite_type, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00055580 File Offset: 0x00053780
		public virtual RailResult AsyncCancelInvite(EnumRailUsersInviteType invite_type, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncCancelInvite(this.swigCPtr_, (int)invite_type, user_data);
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x0005558F File Offset: 0x0005378F
		public virtual RailResult AsyncCancelAllInvites(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncCancelAllInvites(this.swigCPtr_, user_data);
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000555A0 File Offset: 0x000537A0
		public virtual RailResult AsyncGetUserLimits(RailID user_id, string user_data)
		{
			IntPtr intPtr = ((user_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (user_id != null)
			{
				RailConverter.Csharp2Cpp(user_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetUserLimits(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000555FC File Offset: 0x000537FC
		public virtual RailResult AsyncShowChatWindowWithFriend(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncShowChatWindowWithFriend(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00055658 File Offset: 0x00053858
		public virtual RailResult AsyncShowUserHomepageWindow(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncShowUserHomepageWindow(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}
	}
}
