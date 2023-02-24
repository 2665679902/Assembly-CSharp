using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000797 RID: 1943
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetTargetUserIdOptionsInternal : IDisposable
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x0008FED4 File Offset: 0x0008E0D4
		// (set) Token: 0x0600476F RID: 18287 RVA: 0x0008FEF6 File Offset: 0x0008E0F6
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

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x0008FF08 File Offset: 0x0008E108
		// (set) Token: 0x06004771 RID: 18289 RVA: 0x0008FF2A File Offset: 0x0008E12A
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

		// Token: 0x06004772 RID: 18290 RVA: 0x0008FF39 File Offset: 0x0008E139
		public void Dispose()
		{
		}

		// Token: 0x04001BB2 RID: 7090
		private int m_ApiVersion;

		// Token: 0x04001BB3 RID: 7091
		private IntPtr m_TargetUserId;
	}
}
