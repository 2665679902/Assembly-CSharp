using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000695 RID: 1685
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPresenceOptionsInternal : IDisposable
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x00088D64 File Offset: 0x00086F64
		// (set) Token: 0x060040DD RID: 16605 RVA: 0x00088D86 File Offset: 0x00086F86
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

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x00088D98 File Offset: 0x00086F98
		// (set) Token: 0x060040DF RID: 16607 RVA: 0x00088DBA File Offset: 0x00086FBA
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

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x00088DCC File Offset: 0x00086FCC
		// (set) Token: 0x060040E1 RID: 16609 RVA: 0x00088DEE File Offset: 0x00086FEE
		public PresenceModification PresenceModificationHandle
		{
			get
			{
				PresenceModification @default = Helper.GetDefault<PresenceModification>();
				Helper.TryMarshalGet<PresenceModification>(this.m_PresenceModificationHandle, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceModificationHandle, value);
			}
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x00088DFD File Offset: 0x00086FFD
		public void Dispose()
		{
		}

		// Token: 0x040018CC RID: 6348
		private int m_ApiVersion;

		// Token: 0x040018CD RID: 6349
		private IntPtr m_LocalUserId;

		// Token: 0x040018CE RID: 6350
		private IntPtr m_PresenceModificationHandle;
	}
}
