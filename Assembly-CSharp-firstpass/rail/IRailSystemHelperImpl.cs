using System;

namespace rail
{
	// Token: 0x020002C2 RID: 706
	public class IRailSystemHelperImpl : RailObject, IRailSystemHelper
	{
		// Token: 0x06002A3D RID: 10813 RVA: 0x0005520B File Offset: 0x0005340B
		internal IRailSystemHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0005521C File Offset: 0x0005341C
		~IRailSystemHelperImpl()
		{
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x00055244 File Offset: 0x00053444
		public virtual RailResult SetTerminationTimeoutOwnershipExpired(int timeout_seconds)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSystemHelper_SetTerminationTimeoutOwnershipExpired(this.swigCPtr_, timeout_seconds);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00055252 File Offset: 0x00053452
		public virtual RailSystemState GetPlatformSystemState()
		{
			return (RailSystemState)RAIL_API_PINVOKE.IRailSystemHelper_GetPlatformSystemState(this.swigCPtr_);
		}
	}
}
