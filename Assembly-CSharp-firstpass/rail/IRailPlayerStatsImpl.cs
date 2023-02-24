using System;

namespace rail
{
	// Token: 0x020002B8 RID: 696
	public class IRailPlayerStatsImpl : RailObject, IRailPlayerStats, IRailComponent
	{
		// Token: 0x0600298C RID: 10636 RVA: 0x000535FA File Offset: 0x000517FA
		internal IRailPlayerStatsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0005360C File Offset: 0x0005180C
		~IRailPlayerStatsImpl()
		{
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00053634 File Offset: 0x00051834
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailPlayerStats_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00053659 File Offset: 0x00051859
		public virtual RailResult AsyncRequestStats(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_AsyncRequestStats(this.swigCPtr_, user_data);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00053667 File Offset: 0x00051867
		public virtual RailResult GetStatValue(string name, out int data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_GetStatValue__SWIG_0(this.swigCPtr_, name, out data);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00053676 File Offset: 0x00051876
		public virtual RailResult GetStatValue(string name, out double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_GetStatValue__SWIG_1(this.swigCPtr_, name, out data);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00053685 File Offset: 0x00051885
		public virtual RailResult SetStatValue(string name, int data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_SetStatValue__SWIG_0(this.swigCPtr_, name, data);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00053694 File Offset: 0x00051894
		public virtual RailResult SetStatValue(string name, double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_SetStatValue__SWIG_1(this.swigCPtr_, name, data);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000536A3 File Offset: 0x000518A3
		public virtual RailResult UpdateAverageStatValue(string name, double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_UpdateAverageStatValue(this.swigCPtr_, name, data);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000536B2 File Offset: 0x000518B2
		public virtual RailResult AsyncStoreStats(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_AsyncStoreStats(this.swigCPtr_, user_data);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000536C0 File Offset: 0x000518C0
		public virtual RailResult ResetAllStats()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayerStats_ResetAllStats(this.swigCPtr_);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000536CD File Offset: 0x000518CD
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000536DA File Offset: 0x000518DA
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
