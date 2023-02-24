using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200074E RID: 1870
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickMemberOptionsInternal : IDisposable
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x0008D998 File Offset: 0x0008BB98
		// (set) Token: 0x0600458D RID: 17805 RVA: 0x0008D9BA File Offset: 0x0008BBBA
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

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x0008D9CC File Offset: 0x0008BBCC
		// (set) Token: 0x0600458F RID: 17807 RVA: 0x0008D9EE File Offset: 0x0008BBEE
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

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06004590 RID: 17808 RVA: 0x0008DA00 File Offset: 0x0008BC00
		// (set) Token: 0x06004591 RID: 17809 RVA: 0x0008DA22 File Offset: 0x0008BC22
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

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x0008DA34 File Offset: 0x0008BC34
		// (set) Token: 0x06004593 RID: 17811 RVA: 0x0008DA56 File Offset: 0x0008BC56
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x0008DA65 File Offset: 0x0008BC65
		public void Dispose()
		{
		}

		// Token: 0x04001AE4 RID: 6884
		private int m_ApiVersion;

		// Token: 0x04001AE5 RID: 6885
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;

		// Token: 0x04001AE6 RID: 6886
		private IntPtr m_LocalUserId;

		// Token: 0x04001AE7 RID: 6887
		private IntPtr m_TargetUserId;
	}
}
