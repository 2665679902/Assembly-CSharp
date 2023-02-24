using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000564 RID: 1380
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HideFriendsOptionsInternal : IDisposable
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060039C2 RID: 14786 RVA: 0x00081D04 File Offset: 0x0007FF04
		// (set) Token: 0x060039C3 RID: 14787 RVA: 0x00081D26 File Offset: 0x0007FF26
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

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x00081D38 File Offset: 0x0007FF38
		// (set) Token: 0x060039C5 RID: 14789 RVA: 0x00081D5A File Offset: 0x0007FF5A
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

		// Token: 0x060039C6 RID: 14790 RVA: 0x00081D69 File Offset: 0x0007FF69
		public void Dispose()
		{
		}

		// Token: 0x04001593 RID: 5523
		private int m_ApiVersion;

		// Token: 0x04001594 RID: 5524
		private IntPtr m_LocalUserId;
	}
}
