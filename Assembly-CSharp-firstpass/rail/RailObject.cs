using System;

namespace rail
{
	// Token: 0x020002CE RID: 718
	public class RailObject
	{
		// Token: 0x06002AC5 RID: 10949 RVA: 0x00056A52 File Offset: 0x00054C52
		internal RailObject()
		{
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x00056A65 File Offset: 0x00054C65
		internal static IntPtr getCPtr(RailObject obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr_;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x00056A78 File Offset: 0x00054C78
		~RailObject()
		{
		}

		// Token: 0x04000A6E RID: 2670
		protected IntPtr swigCPtr_ = IntPtr.Zero;
	}
}
