using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002B7 RID: 695
	public class IRailPlayerAchievementImpl : RailObject, IRailPlayerAchievement, IRailComponent
	{
		// Token: 0x0600297B RID: 10619 RVA: 0x00053414 File Offset: 0x00051614
		internal IRailPlayerAchievementImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00053424 File Offset: 0x00051624
		~IRailPlayerAchievementImpl()
		{
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x0005344C File Offset: 0x0005164C
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailPlayerAchievement_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x00053471 File Offset: 0x00051671
		public virtual RailResult AsyncRequestAchievement(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncRequestAchievement(this.swigCPtr_, user_data);
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0005347F File Offset: 0x0005167F
		public virtual RailResult HasAchieved(string name, out bool achieved)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_HasAchieved(this.swigCPtr_, name, out achieved);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00053490 File Offset: 0x00051690
		public virtual RailResult GetAchievementInfo(string name, out string achievement_info)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_GetAchievementInfo__SWIG_0(this.swigCPtr_, name, intPtr);
			}
			finally
			{
				achievement_info = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000534D8 File Offset: 0x000516D8
		public virtual RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_0(this.swigCPtr_, name, current_value, max_value, user_data);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000534EA File Offset: 0x000516EA
		public virtual RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_1(this.swigCPtr_, name, current_value, max_value);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000534FA File Offset: 0x000516FA
		public virtual RailResult AsyncTriggerAchievementProgress(string name, uint current_value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_2(this.swigCPtr_, name, current_value);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x00053509 File Offset: 0x00051709
		public virtual RailResult MakeAchievement(string name)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_MakeAchievement(this.swigCPtr_, name);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00053517 File Offset: 0x00051717
		public virtual RailResult CancelAchievement(string name)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_CancelAchievement(this.swigCPtr_, name);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x00053525 File Offset: 0x00051725
		public virtual RailResult AsyncStoreAchievement(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncStoreAchievement(this.swigCPtr_, user_data);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x00053533 File Offset: 0x00051733
		public virtual RailResult ResetAllAchievements()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_ResetAllAchievements(this.swigCPtr_);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x00053540 File Offset: 0x00051740
		public virtual RailResult GetAllAchievementsName(List<string> names)
		{
			IntPtr intPtr = ((names == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_GetAllAchievementsName(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (names != null)
				{
					RailConverter.Cpp2Csharp(intPtr, names);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x00053590 File Offset: 0x00051790
		public virtual RailResult GetAchievementInfo(string name, RailPlayerAchievementInfo achievement_info)
		{
			IntPtr intPtr = ((achievement_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailPlayerAchievementInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayerAchievement_GetAchievementInfo__SWIG_1(this.swigCPtr_, name, intPtr);
			}
			finally
			{
				if (achievement_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, achievement_info);
				}
				RAIL_API_PINVOKE.delete_RailPlayerAchievementInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000535E0 File Offset: 0x000517E0
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000535ED File Offset: 0x000517ED
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
