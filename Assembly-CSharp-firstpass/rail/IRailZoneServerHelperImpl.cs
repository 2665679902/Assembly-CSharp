using System;

namespace rail
{
	// Token: 0x020002CB RID: 715
	public class IRailZoneServerHelperImpl : RailObject, IRailZoneServerHelper
	{
		// Token: 0x06002AB3 RID: 10931 RVA: 0x00056866 File Offset: 0x00054A66
		internal IRailZoneServerHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x00056878 File Offset: 0x00054A78
		~IRailZoneServerHelperImpl()
		{
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000568A0 File Offset: 0x00054AA0
		public virtual RailZoneID GetPlayerSelectedZoneID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailZoneServerHelper_GetPlayerSelectedZoneID(this.swigCPtr_);
			RailZoneID railZoneID = new RailZoneID();
			RailConverter.Cpp2Csharp(intPtr, railZoneID);
			return railZoneID;
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000568C8 File Offset: 0x00054AC8
		public virtual RailZoneID GetRootZoneID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailZoneServerHelper_GetRootZoneID(this.swigCPtr_);
			RailZoneID railZoneID = new RailZoneID();
			RailConverter.Cpp2Csharp(intPtr, railZoneID);
			return railZoneID;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000568F0 File Offset: 0x00054AF0
		public virtual IRailZoneServer OpenZoneServer(RailZoneID zone_id, out RailResult result)
		{
			IntPtr intPtr = ((zone_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailZoneID__SWIG_0());
			if (zone_id != null)
			{
				RailConverter.Csharp2Cpp(zone_id, intPtr);
			}
			IRailZoneServer railZoneServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailZoneServerHelper_OpenZoneServer(this.swigCPtr_, intPtr, out result);
				railZoneServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailZoneServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailZoneID(intPtr);
			}
			return railZoneServer;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x00056964 File Offset: 0x00054B64
		public virtual RailResult AsyncSwitchPlayerSelectedZone(RailZoneID zone_id)
		{
			IntPtr intPtr = ((zone_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailZoneID__SWIG_0());
			if (zone_id != null)
			{
				RailConverter.Csharp2Cpp(zone_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServerHelper_AsyncSwitchPlayerSelectedZone(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailZoneID(intPtr);
			}
			return railResult;
		}
	}
}
