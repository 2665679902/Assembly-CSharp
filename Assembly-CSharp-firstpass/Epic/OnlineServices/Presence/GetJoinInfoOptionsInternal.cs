using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200066F RID: 1647
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetJoinInfoOptionsInternal : IDisposable
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x00087D60 File Offset: 0x00085F60
		// (set) Token: 0x06003FDF RID: 16351 RVA: 0x00087D82 File Offset: 0x00085F82
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

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x00087D94 File Offset: 0x00085F94
		// (set) Token: 0x06003FE1 RID: 16353 RVA: 0x00087DB6 File Offset: 0x00085FB6
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x00087DC8 File Offset: 0x00085FC8
		// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x00087DEA File Offset: 0x00085FEA
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x00087DF9 File Offset: 0x00085FF9
		public void Dispose()
		{
		}

		// Token: 0x04001860 RID: 6240
		private int m_ApiVersion;

		// Token: 0x04001861 RID: 6241
		private IntPtr m_LocalUserId;

		// Token: 0x04001862 RID: 6242
		private IntPtr m_TargetUserId;
	}
}
