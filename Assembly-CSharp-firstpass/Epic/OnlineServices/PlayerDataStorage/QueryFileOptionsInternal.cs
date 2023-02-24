using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C4 RID: 1732
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileOptionsInternal : IDisposable
	{
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060041F2 RID: 16882 RVA: 0x00089CF4 File Offset: 0x00087EF4
		// (set) Token: 0x060041F3 RID: 16883 RVA: 0x00089D16 File Offset: 0x00087F16
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

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x00089D28 File Offset: 0x00087F28
		// (set) Token: 0x060041F5 RID: 16885 RVA: 0x00089D4A File Offset: 0x00087F4A
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x00089D5C File Offset: 0x00087F5C
		// (set) Token: 0x060041F7 RID: 16887 RVA: 0x00089D7E File Offset: 0x00087F7E
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Filename, value);
			}
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x00089D8D File Offset: 0x00087F8D
		public void Dispose()
		{
		}

		// Token: 0x0400192A RID: 6442
		private int m_ApiVersion;

		// Token: 0x0400192B RID: 6443
		private IntPtr m_LocalUserId;

		// Token: 0x0400192C RID: 6444
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
