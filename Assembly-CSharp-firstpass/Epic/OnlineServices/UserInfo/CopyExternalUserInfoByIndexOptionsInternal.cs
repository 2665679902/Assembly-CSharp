using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200053D RID: 1341
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x00080C7C File Offset: 0x0007EE7C
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x00080C9E File Offset: 0x0007EE9E
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

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x00080CB0 File Offset: 0x0007EEB0
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x00080CD2 File Offset: 0x0007EED2
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x00080D06 File Offset: 0x0007EF06
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x00080D18 File Offset: 0x0007EF18
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x00080D3A File Offset: 0x0007EF3A
		public uint Index
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_Index, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_Index, value);
			}
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x00080D49 File Offset: 0x0007EF49
		public void Dispose()
		{
		}

		// Token: 0x04001528 RID: 5416
		private int m_ApiVersion;

		// Token: 0x04001529 RID: 5417
		private IntPtr m_LocalUserId;

		// Token: 0x0400152A RID: 5418
		private IntPtr m_TargetUserId;

		// Token: 0x0400152B RID: 5419
		private uint m_Index;
	}
}
