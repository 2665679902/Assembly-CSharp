using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000639 RID: 1593
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetInvitesAllowedOptionsInternal : IDisposable
	{
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x000863C0 File Offset: 0x000845C0
		// (set) Token: 0x06003E92 RID: 16018 RVA: 0x000863E2 File Offset: 0x000845E2
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

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000863F4 File Offset: 0x000845F4
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x00086416 File Offset: 0x00084616
		public bool InvitesAllowed
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_InvitesAllowed, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_InvitesAllowed, value);
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x00086425 File Offset: 0x00084625
		public void Dispose()
		{
		}

		// Token: 0x040017C6 RID: 6086
		private int m_ApiVersion;

		// Token: 0x040017C7 RID: 6087
		private int m_InvitesAllowed;
	}
}
