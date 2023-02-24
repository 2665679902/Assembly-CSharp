using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000671 RID: 1649
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HasPresenceOptionsInternal : IDisposable
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x00087E28 File Offset: 0x00086028
		// (set) Token: 0x06003FEC RID: 16364 RVA: 0x00087E4A File Offset: 0x0008604A
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

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x00087E5C File Offset: 0x0008605C
		// (set) Token: 0x06003FEE RID: 16366 RVA: 0x00087E7E File Offset: 0x0008607E
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

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x00087E90 File Offset: 0x00086090
		// (set) Token: 0x06003FF0 RID: 16368 RVA: 0x00087EB2 File Offset: 0x000860B2
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

		// Token: 0x06003FF1 RID: 16369 RVA: 0x00087EC1 File Offset: 0x000860C1
		public void Dispose()
		{
		}

		// Token: 0x04001865 RID: 6245
		private int m_ApiVersion;

		// Token: 0x04001866 RID: 6246
		private IntPtr m_LocalUserId;

		// Token: 0x04001867 RID: 6247
		private IntPtr m_TargetUserId;
	}
}
