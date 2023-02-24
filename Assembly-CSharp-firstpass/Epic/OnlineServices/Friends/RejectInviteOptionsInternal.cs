using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200081F RID: 2079
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : IDisposable
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x000923D4 File Offset: 0x000905D4
		// (set) Token: 0x06004A51 RID: 19025 RVA: 0x000923F6 File Offset: 0x000905F6
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

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x00092408 File Offset: 0x00090608
		// (set) Token: 0x06004A53 RID: 19027 RVA: 0x0009242A File Offset: 0x0009062A
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

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x0009243C File Offset: 0x0009063C
		// (set) Token: 0x06004A55 RID: 19029 RVA: 0x0009245E File Offset: 0x0009065E
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

		// Token: 0x06004A56 RID: 19030 RVA: 0x0009246D File Offset: 0x0009066D
		public void Dispose()
		{
		}

		// Token: 0x04001CA8 RID: 7336
		private int m_ApiVersion;

		// Token: 0x04001CA9 RID: 7337
		private IntPtr m_LocalUserId;

		// Token: 0x04001CAA RID: 7338
		private IntPtr m_TargetUserId;
	}
}
