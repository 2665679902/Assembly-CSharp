using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005C6 RID: 1478
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifySessionInviteAcceptedOptionsInternal : IDisposable
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000841C4 File Offset: 0x000823C4
		// (set) Token: 0x06003C22 RID: 15394 RVA: 0x000841E6 File Offset: 0x000823E6
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

		// Token: 0x06003C23 RID: 15395 RVA: 0x000841F5 File Offset: 0x000823F5
		public void Dispose()
		{
		}

		// Token: 0x040016EF RID: 5871
		private int m_ApiVersion;
	}
}
