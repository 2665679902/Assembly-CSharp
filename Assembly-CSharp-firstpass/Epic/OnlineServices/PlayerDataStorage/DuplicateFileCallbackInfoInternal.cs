using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A0 RID: 1696
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DuplicateFileCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x00089144 File Offset: 0x00087344
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x00089168 File Offset: 0x00087368
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x0008918A File Offset: 0x0008738A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x00089194 File Offset: 0x00087394
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x040018ED RID: 6381
		private Result m_ResultCode;

		// Token: 0x040018EE RID: 6382
		private IntPtr m_ClientData;

		// Token: 0x040018EF RID: 6383
		private IntPtr m_LocalUserId;
	}
}
