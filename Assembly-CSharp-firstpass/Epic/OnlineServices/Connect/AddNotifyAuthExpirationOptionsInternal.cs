using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000887 RID: 2183
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAuthExpirationOptionsInternal : IDisposable
	{
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004D7C RID: 19836 RVA: 0x000958C8 File Offset: 0x00093AC8
		// (set) Token: 0x06004D7D RID: 19837 RVA: 0x000958EA File Offset: 0x00093AEA
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

		// Token: 0x06004D7E RID: 19838 RVA: 0x000958F9 File Offset: 0x00093AF9
		public void Dispose()
		{
		}

		// Token: 0x04001E0A RID: 7690
		private int m_ApiVersion;
	}
}
