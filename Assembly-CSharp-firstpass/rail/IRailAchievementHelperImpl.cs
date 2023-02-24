using System;

namespace rail
{
	// Token: 0x02000296 RID: 662
	public class IRailAchievementHelperImpl : RailObject, IRailAchievementHelper
	{
		// Token: 0x060027ED RID: 10221 RVA: 0x0004EFB7 File Offset: 0x0004D1B7
		internal IRailAchievementHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0004EFC8 File Offset: 0x0004D1C8
		~IRailAchievementHelperImpl()
		{
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0004EFF0 File Offset: 0x0004D1F0
		public virtual IRailPlayerAchievement CreatePlayerAchievement(RailID player)
		{
			IntPtr intPtr = ((player == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player != null)
			{
				RailConverter.Csharp2Cpp(player, intPtr);
			}
			IRailPlayerAchievement railPlayerAchievement;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailAchievementHelper_CreatePlayerAchievement(this.swigCPtr_, intPtr);
				railPlayerAchievement = ((intPtr2 == IntPtr.Zero) ? null : new IRailPlayerAchievementImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railPlayerAchievement;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0004F064 File Offset: 0x0004D264
		public virtual IRailGlobalAchievement GetGlobalAchievement()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailAchievementHelper_GetGlobalAchievement(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGlobalAchievementImpl(intPtr);
			}
			return null;
		}
	}
}
