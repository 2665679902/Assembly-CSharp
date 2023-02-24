using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200069E RID: 1694
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteFileOptionsInternal : IDisposable
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x0008906C File Offset: 0x0008726C
		// (set) Token: 0x0600410F RID: 16655 RVA: 0x0008908E File Offset: 0x0008728E
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

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x000890A0 File Offset: 0x000872A0
		// (set) Token: 0x06004111 RID: 16657 RVA: 0x000890C2 File Offset: 0x000872C2
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

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x000890D4 File Offset: 0x000872D4
		// (set) Token: 0x06004113 RID: 16659 RVA: 0x000890F6 File Offset: 0x000872F6
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

		// Token: 0x06004114 RID: 16660 RVA: 0x00089105 File Offset: 0x00087305
		public void Dispose()
		{
		}

		// Token: 0x040018E7 RID: 6375
		private int m_ApiVersion;

		// Token: 0x040018E8 RID: 6376
		private IntPtr m_LocalUserId;

		// Token: 0x040018E9 RID: 6377
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
