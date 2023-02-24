using System;

namespace rail
{
	// Token: 0x020002A7 RID: 679
	public class IRailGlobalStatsImpl : RailObject, IRailGlobalStats, IRailComponent
	{
		// Token: 0x060028F0 RID: 10480 RVA: 0x00051BAA File Offset: 0x0004FDAA
		internal IRailGlobalStatsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x00051BBC File Offset: 0x0004FDBC
		~IRailGlobalStatsImpl()
		{
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x00051BE4 File Offset: 0x0004FDE4
		public virtual RailResult AsyncRequestGlobalStats(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_AsyncRequestGlobalStats(this.swigCPtr_, user_data);
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x00051BF2 File Offset: 0x0004FDF2
		public virtual RailResult GetGlobalStatValue(string name, out long data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValue__SWIG_0(this.swigCPtr_, name, out data);
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x00051C01 File Offset: 0x0004FE01
		public virtual RailResult GetGlobalStatValue(string name, out double data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValue__SWIG_1(this.swigCPtr_, name, out data);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00051C10 File Offset: 0x0004FE10
		public virtual RailResult GetGlobalStatValueHistory(string name, long[] global_stats_data, uint data_size, out int num_global_stats)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValueHistory__SWIG_0(this.swigCPtr_, name, global_stats_data, data_size, out num_global_stats);
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00051C22 File Offset: 0x0004FE22
		public virtual RailResult GetGlobalStatValueHistory(string name, double[] global_stats_data, uint data_size, out int num_global_stats)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValueHistory__SWIG_1(this.swigCPtr_, name, global_stats_data, data_size, out num_global_stats);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x00051C34 File Offset: 0x0004FE34
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x00051C41 File Offset: 0x0004FE41
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
