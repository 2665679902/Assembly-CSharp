using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200067D RID: 1661
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceChangedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x00088298 File Offset: 0x00086498
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x000882BA File Offset: 0x000864BA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x000882C4 File Offset: 0x000864C4
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x000882E8 File Offset: 0x000864E8
		public EpicAccountId PresenceUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_PresenceUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001887 RID: 6279
		private IntPtr m_ClientData;

		// Token: 0x04001888 RID: 6280
		private IntPtr m_LocalUserId;

		// Token: 0x04001889 RID: 6281
		private IntPtr m_PresenceUserId;
	}
}
