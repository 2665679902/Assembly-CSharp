using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D2 RID: 1490
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleByUiEventIdOptionsInternal : IDisposable
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x0008470C File Offset: 0x0008290C
		// (set) Token: 0x06003C67 RID: 15463 RVA: 0x0008472E File Offset: 0x0008292E
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

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x00084740 File Offset: 0x00082940
		// (set) Token: 0x06003C69 RID: 15465 RVA: 0x00084762 File Offset: 0x00082962
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

		// Token: 0x06003C6A RID: 15466 RVA: 0x00084771 File Offset: 0x00082971
		public void Dispose()
		{
		}

		// Token: 0x04001707 RID: 5895
		private int m_ApiVersion;

		// Token: 0x04001708 RID: 5896
		private ulong m_UiEventId;
	}
}
