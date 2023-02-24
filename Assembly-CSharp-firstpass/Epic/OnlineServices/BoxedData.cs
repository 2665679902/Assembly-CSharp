using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices
{
	// Token: 0x0200051F RID: 1311
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class BoxedData
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x0007F469 File Offset: 0x0007D669
		// (set) Token: 0x06003804 RID: 14340 RVA: 0x0007F471 File Offset: 0x0007D671
		public object Data { get; private set; }

		// Token: 0x06003805 RID: 14341 RVA: 0x0007F47A File Offset: 0x0007D67A
		public BoxedData(object data)
		{
			this.Data = data;
		}
	}
}
