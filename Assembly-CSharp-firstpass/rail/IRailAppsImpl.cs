using System;

namespace rail
{
	// Token: 0x02000298 RID: 664
	public class IRailAppsImpl : RailObject, IRailApps
	{
		// Token: 0x060027F4 RID: 10228 RVA: 0x0004F0DA File Offset: 0x0004D2DA
		internal IRailAppsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0004F0EC File Offset: 0x0004D2EC
		~IRailAppsImpl()
		{
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0004F114 File Offset: 0x0004D314
		public virtual bool IsGameInstalled(RailGameID game_id)
		{
			IntPtr intPtr = ((game_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGameID__SWIG_0());
			if (game_id != null)
			{
				RailConverter.Csharp2Cpp(game_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailApps_IsGameInstalled(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
			return flag;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0004F170 File Offset: 0x0004D370
		public virtual RailResult AsyncQuerySubscribeWishPlayState(RailGameID game_id, string user_data)
		{
			IntPtr intPtr = ((game_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGameID__SWIG_0());
			if (game_id != null)
			{
				RailConverter.Csharp2Cpp(game_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailApps_AsyncQuerySubscribeWishPlayState(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
			return railResult;
		}
	}
}
