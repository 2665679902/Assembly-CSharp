using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008FC RID: 2300
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogoutOptionsInternal : IDisposable
	{
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x0600503C RID: 20540 RVA: 0x000984E4 File Offset: 0x000966E4
		// (set) Token: 0x0600503D RID: 20541 RVA: 0x00098506 File Offset: 0x00096706
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

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x00098518 File Offset: 0x00096718
		// (set) Token: 0x0600503F RID: 20543 RVA: 0x0009853A File Offset: 0x0009673A
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

		// Token: 0x06005040 RID: 20544 RVA: 0x00098549 File Offset: 0x00096749
		public void Dispose()
		{
		}

		// Token: 0x04001F3C RID: 7996
		private int m_ApiVersion;

		// Token: 0x04001F3D RID: 7997
		private IntPtr m_LocalUserId;
	}
}
