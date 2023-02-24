using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000595 RID: 1429
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x000829C4 File Offset: 0x00080BC4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000829E8 File Offset: 0x00080BE8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x00082A0A File Offset: 0x00080C0A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x00082A14 File Offset: 0x00080C14
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x00082A38 File Offset: 0x00080C38
		public uint FileCount
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_FileCount, out @default);
				return @default;
			}
		}

		// Token: 0x0400165A RID: 5722
		private Result m_ResultCode;

		// Token: 0x0400165B RID: 5723
		private IntPtr m_ClientData;

		// Token: 0x0400165C RID: 5724
		private IntPtr m_LocalUserId;

		// Token: 0x0400165D RID: 5725
		private uint m_FileCount;
	}
}
