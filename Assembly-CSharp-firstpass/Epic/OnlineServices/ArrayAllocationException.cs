using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000525 RID: 1317
	internal class ArrayAllocationException : AllocationException
	{
		// Token: 0x06003811 RID: 14353 RVA: 0x0007F575 File Offset: 0x0007D775
		public ArrayAllocationException(IntPtr address, int foundLength, int expectedLength)
			: base(string.Format("Found array allocation with length {0} but expected {1} at {2:X}", foundLength, expectedLength, address.ToString("X")))
		{
		}
	}
}
