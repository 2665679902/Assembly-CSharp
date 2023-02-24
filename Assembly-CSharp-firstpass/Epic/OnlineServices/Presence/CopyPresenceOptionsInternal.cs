using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000669 RID: 1641
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPresenceOptionsInternal : IDisposable
	{
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x00087B4C File Offset: 0x00085D4C
		// (set) Token: 0x06003FBC RID: 16316 RVA: 0x00087B6E File Offset: 0x00085D6E
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

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x00087B80 File Offset: 0x00085D80
		// (set) Token: 0x06003FBE RID: 16318 RVA: 0x00087BA2 File Offset: 0x00085DA2
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

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06003FBF RID: 16319 RVA: 0x00087BB4 File Offset: 0x00085DB4
		// (set) Token: 0x06003FC0 RID: 16320 RVA: 0x00087BD6 File Offset: 0x00085DD6
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

		// Token: 0x06003FC1 RID: 16321 RVA: 0x00087BE5 File Offset: 0x00085DE5
		public void Dispose()
		{
		}

		// Token: 0x04001853 RID: 6227
		private int m_ApiVersion;

		// Token: 0x04001854 RID: 6228
		private IntPtr m_LocalUserId;

		// Token: 0x04001855 RID: 6229
		private IntPtr m_TargetUserId;
	}
}
