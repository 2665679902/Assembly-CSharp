using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000876 RID: 2166
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x000951CC File Offset: 0x000933CC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004D16 RID: 19734 RVA: 0x000951F0 File Offset: 0x000933F0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x00095212 File Offset: 0x00093412
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x0009521C File Offset: 0x0009341C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x00095240 File Offset: 0x00093440
		public ItemOwnershipInternal[] ItemOwnership
		{
			get
			{
				ItemOwnershipInternal[] @default = Helper.GetDefault<ItemOwnershipInternal[]>();
				Helper.TryMarshalGet<ItemOwnershipInternal>(this.m_ItemOwnership, out @default, this.m_ItemOwnershipCount);
				return @default;
			}
		}

		// Token: 0x04001DDD RID: 7645
		private Result m_ResultCode;

		// Token: 0x04001DDE RID: 7646
		private IntPtr m_ClientData;

		// Token: 0x04001DDF RID: 7647
		private IntPtr m_LocalUserId;

		// Token: 0x04001DE0 RID: 7648
		private IntPtr m_ItemOwnership;

		// Token: 0x04001DE1 RID: 7649
		private uint m_ItemOwnershipCount;
	}
}
