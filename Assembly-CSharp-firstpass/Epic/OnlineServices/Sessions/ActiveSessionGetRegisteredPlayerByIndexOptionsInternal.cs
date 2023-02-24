using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005BE RID: 1470
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionGetRegisteredPlayerByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x00083F70 File Offset: 0x00082170
		// (set) Token: 0x06003BFC RID: 15356 RVA: 0x00083F92 File Offset: 0x00082192
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

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x00083FA4 File Offset: 0x000821A4
		// (set) Token: 0x06003BFE RID: 15358 RVA: 0x00083FC6 File Offset: 0x000821C6
		public uint PlayerIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_PlayerIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_PlayerIndex, value);
			}
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x00083FD5 File Offset: 0x000821D5
		public void Dispose()
		{
		}

		// Token: 0x040016E2 RID: 5858
		private int m_ApiVersion;

		// Token: 0x040016E3 RID: 5859
		private uint m_PlayerIndex;
	}
}
