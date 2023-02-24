using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200080B RID: 2059
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetStatusOptionsInternal : IDisposable
	{
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060049E8 RID: 18920 RVA: 0x00091FDC File Offset: 0x000901DC
		// (set) Token: 0x060049E9 RID: 18921 RVA: 0x00091FFE File Offset: 0x000901FE
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

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060049EA RID: 18922 RVA: 0x00092010 File Offset: 0x00090210
		// (set) Token: 0x060049EB RID: 18923 RVA: 0x00092032 File Offset: 0x00090232
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

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060049EC RID: 18924 RVA: 0x00092044 File Offset: 0x00090244
		// (set) Token: 0x060049ED RID: 18925 RVA: 0x00092066 File Offset: 0x00090266
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

		// Token: 0x060049EE RID: 18926 RVA: 0x00092075 File Offset: 0x00090275
		public void Dispose()
		{
		}

		// Token: 0x04001C88 RID: 7304
		private int m_ApiVersion;

		// Token: 0x04001C89 RID: 7305
		private IntPtr m_LocalUserId;

		// Token: 0x04001C8A RID: 7306
		private IntPtr m_TargetUserId;
	}
}
