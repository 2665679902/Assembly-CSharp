using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008E6 RID: 2278
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserAuthTokenOptionsInternal : IDisposable
	{
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x00097C54 File Offset: 0x00095E54
		// (set) Token: 0x06004FB3 RID: 20403 RVA: 0x00097C76 File Offset: 0x00095E76
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

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00097C85 File Offset: 0x00095E85
		public void Dispose()
		{
		}

		// Token: 0x04001EEE RID: 7918
		private int m_ApiVersion;
	}
}
