using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200066B RID: 1643
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreatePresenceModificationOptionsInternal : IDisposable
	{
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x00087C04 File Offset: 0x00085E04
		// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x00087C26 File Offset: 0x00085E26
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

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x00087C38 File Offset: 0x00085E38
		// (set) Token: 0x06003FC9 RID: 16329 RVA: 0x00087C5A File Offset: 0x00085E5A
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

		// Token: 0x06003FCA RID: 16330 RVA: 0x00087C69 File Offset: 0x00085E69
		public void Dispose()
		{
		}

		// Token: 0x04001857 RID: 6231
		private int m_ApiVersion;

		// Token: 0x04001858 RID: 6232
		private IntPtr m_LocalUserId;
	}
}
