using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200055E RID: 1374
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendsVisibleOptionsInternal : IDisposable
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x00081B90 File Offset: 0x0007FD90
		// (set) Token: 0x060039AA RID: 14762 RVA: 0x00081BB2 File Offset: 0x0007FDB2
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

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x00081BC4 File Offset: 0x0007FDC4
		// (set) Token: 0x060039AC RID: 14764 RVA: 0x00081BE6 File Offset: 0x0007FDE6
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

		// Token: 0x060039AD RID: 14765 RVA: 0x00081BF5 File Offset: 0x0007FDF5
		public void Dispose()
		{
		}

		// Token: 0x04001589 RID: 5513
		private int m_ApiVersion;

		// Token: 0x0400158A RID: 5514
		private IntPtr m_LocalUserId;
	}
}
