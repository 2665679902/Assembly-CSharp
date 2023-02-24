using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200070A RID: 1802
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetRelayControlOptionsInternal : IDisposable
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x0008C130 File Offset: 0x0008A330
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x0008C152 File Offset: 0x0008A352
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

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x0008C164 File Offset: 0x0008A364
		// (set) Token: 0x06004420 RID: 17440 RVA: 0x0008C186 File Offset: 0x0008A386
		public RelayControl RelayControl
		{
			get
			{
				RelayControl @default = Helper.GetDefault<RelayControl>();
				Helper.TryMarshalGet<RelayControl>(this.m_RelayControl, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<RelayControl>(ref this.m_RelayControl, value);
			}
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x0008C195 File Offset: 0x0008A395
		public void Dispose()
		{
		}

		// Token: 0x04001A2C RID: 6700
		private int m_ApiVersion;

		// Token: 0x04001A2D RID: 6701
		private RelayControl m_RelayControl;
	}
}
