using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D0 RID: 1488
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleByInviteIdOptionsInternal : IDisposable
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x00084688 File Offset: 0x00082888
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x000846AA File Offset: 0x000828AA
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x000846BC File Offset: 0x000828BC
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x000846DE File Offset: 0x000828DE
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_InviteId, value);
			}
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x000846ED File Offset: 0x000828ED
		public void Dispose()
		{
		}

		// Token: 0x04001704 RID: 5892
		private int m_ApiVersion;

		// Token: 0x04001705 RID: 5893
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;
	}
}
