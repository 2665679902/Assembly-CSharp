using System;

namespace rail
{
	// Token: 0x0200029A RID: 666
	public class IRailAssetsHelperImpl : RailObject, IRailAssetsHelper
	{
		// Token: 0x0600280C RID: 10252 RVA: 0x0004F62A File Offset: 0x0004D82A
		internal IRailAssetsHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0004F63C File Offset: 0x0004D83C
		~IRailAssetsHelperImpl()
		{
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0004F664 File Offset: 0x0004D864
		public virtual IRailAssets OpenAssets()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailAssetsHelper_OpenAssets(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAssetsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0004F694 File Offset: 0x0004D894
		public virtual IRailAssets OpenGameServerAssets()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailAssetsHelper_OpenGameServerAssets(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAssetsImpl(intPtr);
			}
			return null;
		}
	}
}
