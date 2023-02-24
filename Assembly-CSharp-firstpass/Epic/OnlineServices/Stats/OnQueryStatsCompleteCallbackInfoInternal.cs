using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005B4 RID: 1460
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryStatsCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000838C0 File Offset: 0x00081AC0
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000838E4 File Offset: 0x00081AE4
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x00083906 File Offset: 0x00081B06
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x00083910 File Offset: 0x00081B10
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x00083934 File Offset: 0x00081B34
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x040016BC RID: 5820
		private Result m_ResultCode;

		// Token: 0x040016BD RID: 5821
		private IntPtr m_ClientData;

		// Token: 0x040016BE RID: 5822
		private IntPtr m_LocalUserId;

		// Token: 0x040016BF RID: 5823
		private IntPtr m_TargetUserId;
	}
}
