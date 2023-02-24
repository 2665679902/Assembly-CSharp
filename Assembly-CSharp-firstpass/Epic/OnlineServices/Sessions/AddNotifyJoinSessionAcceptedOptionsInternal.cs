using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005C4 RID: 1476
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinSessionAcceptedOptionsInternal : IDisposable
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x00084184 File Offset: 0x00082384
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x000841A6 File Offset: 0x000823A6
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x000841B5 File Offset: 0x000823B5
		public void Dispose()
		{
		}

		// Token: 0x040016EE RID: 5870
		private int m_ApiVersion;
	}
}
