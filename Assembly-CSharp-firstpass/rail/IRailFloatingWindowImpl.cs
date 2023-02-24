using System;

namespace rail
{
	// Token: 0x020002A1 RID: 673
	public class IRailFloatingWindowImpl : RailObject, IRailFloatingWindow
	{
		// Token: 0x06002878 RID: 10360 RVA: 0x000505EB File Offset: 0x0004E7EB
		internal IRailFloatingWindowImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000505FC File Offset: 0x0004E7FC
		~IRailFloatingWindowImpl()
		{
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x00050624 File Offset: 0x0004E824
		public virtual RailResult AsyncShowRailFloatingWindow(EnumRailWindowType window_type, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowRailFloatingWindow(this.swigCPtr_, (int)window_type, user_data);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x00050633 File Offset: 0x0004E833
		public virtual RailResult AsyncCloseRailFloatingWindow(EnumRailWindowType window_type, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncCloseRailFloatingWindow(this.swigCPtr_, (int)window_type, user_data);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00050644 File Offset: 0x0004E844
		public virtual RailResult SetNotifyWindowPosition(EnumRailNotifyWindowType window_type, RailWindowLayout layout)
		{
			IntPtr intPtr = ((layout == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailWindowLayout__SWIG_0());
			if (layout != null)
			{
				RailConverter.Csharp2Cpp(layout, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_SetNotifyWindowPosition(this.swigCPtr_, (int)window_type, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailWindowLayout(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x00050694 File Offset: 0x0004E894
		public virtual RailResult AsyncShowStoreWindow(ulong id, RailStoreOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStoreOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowStoreWindow(this.swigCPtr_, id, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailStoreOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000506E8 File Offset: 0x0004E8E8
		public virtual bool IsFloatingWindowAvailable()
		{
			return RAIL_API_PINVOKE.IRailFloatingWindow_IsFloatingWindowAvailable(this.swigCPtr_);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000506F5 File Offset: 0x0004E8F5
		public virtual RailResult AsyncShowDefaultGameStoreWindow(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowDefaultGameStoreWindow(this.swigCPtr_, user_data);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x00050703 File Offset: 0x0004E903
		public virtual RailResult SetNotifyWindowEnable(EnumRailNotifyWindowType window_type, bool enable)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFloatingWindow_SetNotifyWindowEnable(this.swigCPtr_, (int)window_type, enable);
		}
	}
}
