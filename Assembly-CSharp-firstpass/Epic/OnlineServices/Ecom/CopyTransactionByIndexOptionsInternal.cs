using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000847 RID: 2119
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyTransactionByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x00093B98 File Offset: 0x00091D98
		// (set) Token: 0x06004BC7 RID: 19399 RVA: 0x00093BBA File Offset: 0x00091DBA
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

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x00093BCC File Offset: 0x00091DCC
		// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x00093BEE File Offset: 0x00091DEE
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

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06004BCA RID: 19402 RVA: 0x00093C00 File Offset: 0x00091E00
		// (set) Token: 0x06004BCB RID: 19403 RVA: 0x00093C22 File Offset: 0x00091E22
		public uint TransactionIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_TransactionIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_TransactionIndex, value);
			}
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x00093C31 File Offset: 0x00091E31
		public void Dispose()
		{
		}

		// Token: 0x04001D4D RID: 7501
		private int m_ApiVersion;

		// Token: 0x04001D4E RID: 7502
		private IntPtr m_LocalUserId;

		// Token: 0x04001D4F RID: 7503
		private uint m_TransactionIndex;
	}
}
