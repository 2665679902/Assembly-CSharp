using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000823 RID: 2083
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : IDisposable
	{
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06004A6B RID: 19051 RVA: 0x00092580 File Offset: 0x00090780
		// (set) Token: 0x06004A6C RID: 19052 RVA: 0x000925A2 File Offset: 0x000907A2
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

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x000925B4 File Offset: 0x000907B4
		// (set) Token: 0x06004A6E RID: 19054 RVA: 0x000925D6 File Offset: 0x000907D6
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

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x000925E8 File Offset: 0x000907E8
		// (set) Token: 0x06004A70 RID: 19056 RVA: 0x0009260A File Offset: 0x0009080A
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

		// Token: 0x06004A71 RID: 19057 RVA: 0x00092619 File Offset: 0x00090819
		public void Dispose()
		{
		}

		// Token: 0x04001CB5 RID: 7349
		private int m_ApiVersion;

		// Token: 0x04001CB6 RID: 7350
		private IntPtr m_LocalUserId;

		// Token: 0x04001CB7 RID: 7351
		private IntPtr m_TargetUserId;
	}
}
