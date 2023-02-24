using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200073C RID: 1852
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbySearchOptionsInternal : IDisposable
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x0008D314 File Offset: 0x0008B514
		// (set) Token: 0x06004520 RID: 17696 RVA: 0x0008D336 File Offset: 0x0008B536
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

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x0008D348 File Offset: 0x0008B548
		// (set) Token: 0x06004522 RID: 17698 RVA: 0x0008D36A File Offset: 0x0008B56A
		public uint MaxResults
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxResults, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxResults, value);
			}
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x0008D379 File Offset: 0x0008B579
		public void Dispose()
		{
		}

		// Token: 0x04001AB3 RID: 6835
		private int m_ApiVersion;

		// Token: 0x04001AB4 RID: 6836
		private uint m_MaxResults;
	}
}
