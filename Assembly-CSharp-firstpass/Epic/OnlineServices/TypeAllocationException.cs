using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000524 RID: 1316
	internal class TypeAllocationException : AllocationException
	{
		// Token: 0x06003810 RID: 14352 RVA: 0x0007F555 File Offset: 0x0007D755
		public TypeAllocationException(IntPtr address, Type foundType, Type expectedType)
			: base(string.Format("Found allocation of {0} but expected {1} at {2}", foundType, expectedType, address.ToString("X")))
		{
		}
	}
}
