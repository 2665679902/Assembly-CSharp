using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C0 RID: 1728
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x00089BAC File Offset: 0x00087DAC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x00089BD0 File Offset: 0x00087DD0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x00089BF2 File Offset: 0x00087DF2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x00089BFC File Offset: 0x00087DFC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x00089C20 File Offset: 0x00087E20
		public uint FileCount
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_FileCount, out @default);
				return @default;
			}
		}

		// Token: 0x04001921 RID: 6433
		private Result m_ResultCode;

		// Token: 0x04001922 RID: 6434
		private IntPtr m_ClientData;

		// Token: 0x04001923 RID: 6435
		private IntPtr m_LocalUserId;

		// Token: 0x04001924 RID: 6436
		private uint m_FileCount;
	}
}
