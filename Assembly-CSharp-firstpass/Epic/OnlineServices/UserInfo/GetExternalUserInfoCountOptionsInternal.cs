using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000543 RID: 1347
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetExternalUserInfoCountOptionsInternal : IDisposable
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x00080F50 File Offset: 0x0007F150
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x00080F72 File Offset: 0x0007F172
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

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x00080F84 File Offset: 0x0007F184
		// (set) Token: 0x060038EA RID: 14570 RVA: 0x00080FA6 File Offset: 0x0007F1A6
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

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060038EB RID: 14571 RVA: 0x00080FB8 File Offset: 0x0007F1B8
		// (set) Token: 0x060038EC RID: 14572 RVA: 0x00080FDA File Offset: 0x0007F1DA
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

		// Token: 0x060038ED RID: 14573 RVA: 0x00080FE9 File Offset: 0x0007F1E9
		public void Dispose()
		{
		}

		// Token: 0x0400153A RID: 5434
		private int m_ApiVersion;

		// Token: 0x0400153B RID: 5435
		private IntPtr m_LocalUserId;

		// Token: 0x0400153C RID: 5436
		private IntPtr m_TargetUserId;
	}
}
