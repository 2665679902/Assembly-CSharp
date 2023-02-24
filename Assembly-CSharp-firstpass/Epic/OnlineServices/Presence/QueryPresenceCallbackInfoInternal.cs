using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200068D RID: 1677
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPresenceCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x00088B28 File Offset: 0x00086D28
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x00088B4C File Offset: 0x00086D4C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x00088B6E File Offset: 0x00086D6E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x00088B78 File Offset: 0x00086D78
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x00088B9C File Offset: 0x00086D9C
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x040018BB RID: 6331
		private Result m_ResultCode;

		// Token: 0x040018BC RID: 6332
		private IntPtr m_ClientData;

		// Token: 0x040018BD RID: 6333
		private IntPtr m_LocalUserId;

		// Token: 0x040018BE RID: 6334
		private IntPtr m_TargetUserId;
	}
}
