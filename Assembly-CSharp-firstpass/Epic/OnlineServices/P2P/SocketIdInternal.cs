using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200070C RID: 1804
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SocketIdInternal : IDisposable
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x0008C1B4 File Offset: 0x0008A3B4
		// (set) Token: 0x06004427 RID: 17447 RVA: 0x0008C1D6 File Offset: 0x0008A3D6
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

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x0008C1E8 File Offset: 0x0008A3E8
		// (set) Token: 0x06004429 RID: 17449 RVA: 0x0008C20A File Offset: 0x0008A40A
		public string SocketName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet(this.m_SocketName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_SocketName, value, 33);
			}
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x0008C21B File Offset: 0x0008A41B
		public void Dispose()
		{
		}

		// Token: 0x04001A2F RID: 6703
		private int m_ApiVersion;

		// Token: 0x04001A30 RID: 6704
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
		private byte[] m_SocketName;
	}
}
