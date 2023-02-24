using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007CD RID: 1997
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyModificationOptionsInternal : IDisposable
	{
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x000906B8 File Offset: 0x0008E8B8
		// (set) Token: 0x06004871 RID: 18545 RVA: 0x000906DA File Offset: 0x0008E8DA
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06004872 RID: 18546 RVA: 0x000906EC File Offset: 0x0008E8EC
		// (set) Token: 0x06004873 RID: 18547 RVA: 0x0009070E File Offset: 0x0008E90E
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x00090720 File Offset: 0x0008E920
		// (set) Token: 0x06004875 RID: 18549 RVA: 0x00090742 File Offset: 0x0008E942
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x00090751 File Offset: 0x0008E951
		public void Dispose()
		{
		}

		// Token: 0x04001BEE RID: 7150
		private int m_ApiVersion;

		// Token: 0x04001BEF RID: 7151
		private IntPtr m_LocalUserId;

		// Token: 0x04001BF0 RID: 7152
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
