using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200074A RID: 1866
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyOptionsInternal : IDisposable
	{
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x0008D7DC File Offset: 0x0008B9DC
		// (set) Token: 0x06004571 RID: 17777 RVA: 0x0008D7FE File Offset: 0x0008B9FE
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

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x0008D810 File Offset: 0x0008BA10
		// (set) Token: 0x06004573 RID: 17779 RVA: 0x0008D832 File Offset: 0x0008BA32
		public LobbyDetails LobbyDetailsHandle
		{
			get
			{
				LobbyDetails @default = Helper.GetDefault<LobbyDetails>();
				Helper.TryMarshalGet<LobbyDetails>(this.m_LobbyDetailsHandle, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyDetailsHandle, value);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x0008D844 File Offset: 0x0008BA44
		// (set) Token: 0x06004575 RID: 17781 RVA: 0x0008D866 File Offset: 0x0008BA66
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

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x0008D878 File Offset: 0x0008BA78
		// (set) Token: 0x06004577 RID: 17783 RVA: 0x0008D89A File Offset: 0x0008BA9A
		public bool PresenceEnabled
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_PresenceEnabled, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceEnabled, value);
			}
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x0008D8A9 File Offset: 0x0008BAA9
		public void Dispose()
		{
		}

		// Token: 0x04001AD7 RID: 6871
		private int m_ApiVersion;

		// Token: 0x04001AD8 RID: 6872
		private IntPtr m_LobbyDetailsHandle;

		// Token: 0x04001AD9 RID: 6873
		private IntPtr m_LocalUserId;

		// Token: 0x04001ADA RID: 6874
		private int m_PresenceEnabled;
	}
}
