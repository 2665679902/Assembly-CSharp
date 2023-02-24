using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000734 RID: 1844
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleByUiEventIdOptionsInternal : IDisposable
	{
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x0008CFC4 File Offset: 0x0008B1C4
		// (set) Token: 0x060044EA RID: 17642 RVA: 0x0008CFE6 File Offset: 0x0008B1E6
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

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x0008CFF8 File Offset: 0x0008B1F8
		// (set) Token: 0x060044EC RID: 17644 RVA: 0x0008D01A File Offset: 0x0008B21A
		public ulong UiEventId
		{
			get
			{
				ulong @default = Helper.GetDefault<ulong>();
				Helper.TryMarshalGet<ulong>(this.m_UiEventId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ulong>(ref this.m_UiEventId, value);
			}
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x0008D029 File Offset: 0x0008B229
		public void Dispose()
		{
		}

		// Token: 0x04001A9C RID: 6812
		private int m_ApiVersion;

		// Token: 0x04001A9D RID: 6813
		private ulong m_UiEventId;
	}
}
