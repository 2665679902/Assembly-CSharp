using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000801 RID: 2049
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptInviteOptionsInternal : IDisposable
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x00091A60 File Offset: 0x0008FC60
		// (set) Token: 0x060049A9 RID: 18857 RVA: 0x00091A82 File Offset: 0x0008FC82
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

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x00091A94 File Offset: 0x0008FC94
		// (set) Token: 0x060049AB RID: 18859 RVA: 0x00091AB6 File Offset: 0x0008FCB6
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

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x00091AC8 File Offset: 0x0008FCC8
		// (set) Token: 0x060049AD RID: 18861 RVA: 0x00091AEA File Offset: 0x0008FCEA
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

		// Token: 0x060049AE RID: 18862 RVA: 0x00091AF9 File Offset: 0x0008FCF9
		public void Dispose()
		{
		}

		// Token: 0x04001C6C RID: 7276
		private int m_ApiVersion;

		// Token: 0x04001C6D RID: 7277
		private IntPtr m_LocalUserId;

		// Token: 0x04001C6E RID: 7278
		private IntPtr m_TargetUserId;
	}
}
