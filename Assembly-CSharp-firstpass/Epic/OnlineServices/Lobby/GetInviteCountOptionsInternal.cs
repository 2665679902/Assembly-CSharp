using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000742 RID: 1858
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteCountOptionsInternal : IDisposable
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x0008D510 File Offset: 0x0008B710
		// (set) Token: 0x06004541 RID: 17729 RVA: 0x0008D532 File Offset: 0x0008B732
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

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x0008D544 File Offset: 0x0008B744
		// (set) Token: 0x06004543 RID: 17731 RVA: 0x0008D566 File Offset: 0x0008B766
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

		// Token: 0x06004544 RID: 17732 RVA: 0x0008D575 File Offset: 0x0008B775
		public void Dispose()
		{
		}

		// Token: 0x04001AC1 RID: 6849
		private int m_ApiVersion;

		// Token: 0x04001AC2 RID: 6850
		private IntPtr m_LocalUserId;
	}
}
