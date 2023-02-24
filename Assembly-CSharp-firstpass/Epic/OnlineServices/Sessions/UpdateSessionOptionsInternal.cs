using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000663 RID: 1635
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionOptionsInternal : IDisposable
	{
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x00087A38 File Offset: 0x00085C38
		// (set) Token: 0x06003FA7 RID: 16295 RVA: 0x00087A5A File Offset: 0x00085C5A
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

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x00087A6C File Offset: 0x00085C6C
		// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x00087A8E File Offset: 0x00085C8E
		public SessionModification SessionModificationHandle
		{
			get
			{
				SessionModification @default = Helper.GetDefault<SessionModification>();
				Helper.TryMarshalGet<SessionModification>(this.m_SessionModificationHandle, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionModificationHandle, value);
			}
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00087A9D File Offset: 0x00085C9D
		public void Dispose()
		{
		}

		// Token: 0x0400184D RID: 6221
		private int m_ApiVersion;

		// Token: 0x0400184E RID: 6222
		private IntPtr m_SessionModificationHandle;
	}
}
