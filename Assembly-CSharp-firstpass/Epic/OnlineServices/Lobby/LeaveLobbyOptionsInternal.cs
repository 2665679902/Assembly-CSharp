using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000752 RID: 1874
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveLobbyOptionsInternal : IDisposable
	{
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x0008DB44 File Offset: 0x0008BD44
		// (set) Token: 0x060045A7 RID: 17831 RVA: 0x0008DB66 File Offset: 0x0008BD66
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

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060045A8 RID: 17832 RVA: 0x0008DB78 File Offset: 0x0008BD78
		// (set) Token: 0x060045A9 RID: 17833 RVA: 0x0008DB9A File Offset: 0x0008BD9A
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

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x0008DBAC File Offset: 0x0008BDAC
		// (set) Token: 0x060045AB RID: 17835 RVA: 0x0008DBCE File Offset: 0x0008BDCE
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

		// Token: 0x060045AC RID: 17836 RVA: 0x0008DBDD File Offset: 0x0008BDDD
		public void Dispose()
		{
		}

		// Token: 0x04001AF0 RID: 6896
		private int m_ApiVersion;

		// Token: 0x04001AF1 RID: 6897
		private IntPtr m_LocalUserId;

		// Token: 0x04001AF2 RID: 6898
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
