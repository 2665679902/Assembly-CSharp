using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000555 RID: 1365
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoOptionsInternal : IDisposable
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x00081538 File Offset: 0x0007F738
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x0008155A File Offset: 0x0007F75A
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

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x0008156C File Offset: 0x0007F76C
		// (set) Token: 0x06003960 RID: 14688 RVA: 0x0008158E File Offset: 0x0007F78E
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

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000815A0 File Offset: 0x0007F7A0
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x000815C2 File Offset: 0x0007F7C2
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

		// Token: 0x06003963 RID: 14691 RVA: 0x000815D1 File Offset: 0x0007F7D1
		public void Dispose()
		{
		}

		// Token: 0x04001569 RID: 5481
		private int m_ApiVersion;

		// Token: 0x0400156A RID: 5482
		private IntPtr m_LocalUserId;

		// Token: 0x0400156B RID: 5483
		private IntPtr m_TargetUserId;
	}
}
