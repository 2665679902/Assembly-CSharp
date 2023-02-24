using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000845 RID: 2117
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyTransactionByIdOptionsInternal : IDisposable
	{
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x00093AD0 File Offset: 0x00091CD0
		// (set) Token: 0x06004BBA RID: 19386 RVA: 0x00093AF2 File Offset: 0x00091CF2
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

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06004BBB RID: 19387 RVA: 0x00093B04 File Offset: 0x00091D04
		// (set) Token: 0x06004BBC RID: 19388 RVA: 0x00093B26 File Offset: 0x00091D26
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

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06004BBD RID: 19389 RVA: 0x00093B38 File Offset: 0x00091D38
		// (set) Token: 0x06004BBE RID: 19390 RVA: 0x00093B5A File Offset: 0x00091D5A
		public string TransactionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_TransactionId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_TransactionId, value);
			}
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x00093B69 File Offset: 0x00091D69
		public void Dispose()
		{
		}

		// Token: 0x04001D48 RID: 7496
		private int m_ApiVersion;

		// Token: 0x04001D49 RID: 7497
		private IntPtr m_LocalUserId;

		// Token: 0x04001D4A RID: 7498
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_TransactionId;
	}
}
