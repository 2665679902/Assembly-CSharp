using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200057D RID: 1405
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x00082524 File Offset: 0x00080724
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x00082548 File Offset: 0x00080748
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x0008256A File Offset: 0x0008076A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x00082574 File Offset: 0x00080774
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001636 RID: 5686
		private Result m_ResultCode;

		// Token: 0x04001637 RID: 5687
		private IntPtr m_ClientData;

		// Token: 0x04001638 RID: 5688
		private IntPtr m_LocalUserId;
	}
}
