using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000789 RID: 1929
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchFindOptionsInternal : IDisposable
	{
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x0008FB6C File Offset: 0x0008DD6C
		// (set) Token: 0x0600472D RID: 18221 RVA: 0x0008FB8E File Offset: 0x0008DD8E
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

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x0008FBA0 File Offset: 0x0008DDA0
		// (set) Token: 0x0600472F RID: 18223 RVA: 0x0008FBC2 File Offset: 0x0008DDC2
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

		// Token: 0x06004730 RID: 18224 RVA: 0x0008FBD1 File Offset: 0x0008DDD1
		public void Dispose()
		{
		}

		// Token: 0x04001B9E RID: 7070
		private int m_ApiVersion;

		// Token: 0x04001B9F RID: 7071
		private IntPtr m_LocalUserId;
	}
}
