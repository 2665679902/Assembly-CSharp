using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008E1 RID: 2273
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLoginStatusChangedOptionsInternal : IDisposable
	{
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x00097848 File Offset: 0x00095A48
		// (set) Token: 0x06004F90 RID: 20368 RVA: 0x0009786A File Offset: 0x00095A6A
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

		// Token: 0x06004F91 RID: 20369 RVA: 0x00097879 File Offset: 0x00095A79
		public void Dispose()
		{
		}

		// Token: 0x04001ED9 RID: 7897
		private int m_ApiVersion;
	}
}
