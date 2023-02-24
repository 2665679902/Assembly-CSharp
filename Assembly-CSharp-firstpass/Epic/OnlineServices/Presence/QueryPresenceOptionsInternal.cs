using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200068F RID: 1679
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPresenceOptionsInternal : IDisposable
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x00088BEC File Offset: 0x00086DEC
		// (set) Token: 0x060040BD RID: 16573 RVA: 0x00088C0E File Offset: 0x00086E0E
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

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x00088C20 File Offset: 0x00086E20
		// (set) Token: 0x060040BF RID: 16575 RVA: 0x00088C42 File Offset: 0x00086E42
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

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x00088C54 File Offset: 0x00086E54
		// (set) Token: 0x060040C1 RID: 16577 RVA: 0x00088C76 File Offset: 0x00086E76
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

		// Token: 0x060040C2 RID: 16578 RVA: 0x00088C85 File Offset: 0x00086E85
		public void Dispose()
		{
		}

		// Token: 0x040018C1 RID: 6337
		private int m_ApiVersion;

		// Token: 0x040018C2 RID: 6338
		private IntPtr m_LocalUserId;

		// Token: 0x040018C3 RID: 6339
		private IntPtr m_TargetUserId;
	}
}
