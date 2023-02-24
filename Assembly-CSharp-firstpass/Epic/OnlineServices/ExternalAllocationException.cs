using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000523 RID: 1315
	internal class ExternalAllocationException : AllocationException
	{
		// Token: 0x0600380F RID: 14351 RVA: 0x0007F536 File Offset: 0x0007D736
		public ExternalAllocationException(IntPtr address, Type type)
			: base(string.Format("Attempting to allocate {0} over externally allocated memory at {1:X}", type, address.ToString("X")))
		{
		}
	}
}
