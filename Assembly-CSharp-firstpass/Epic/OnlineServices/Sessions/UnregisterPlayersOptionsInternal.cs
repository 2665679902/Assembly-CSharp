using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200065D RID: 1629
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlayersOptionsInternal : IDisposable
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x00087800 File Offset: 0x00085A00
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x00087822 File Offset: 0x00085A22
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

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x00087834 File Offset: 0x00085A34
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x00087856 File Offset: 0x00085A56
		public string SessionName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionName, value);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x00087868 File Offset: 0x00085A68
		// (set) Token: 0x06003F89 RID: 16265 RVA: 0x00087890 File Offset: 0x00085A90
		public ProductUserId[] PlayersToUnregister
		{
			get
			{
				ProductUserId[] @default = Helper.GetDefault<ProductUserId[]>();
				Helper.TryMarshalGet<ProductUserId>(this.m_PlayersToUnregister, out @default, this.m_PlayersToUnregisterCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_PlayersToUnregister, value, out this.m_PlayersToUnregisterCount);
			}
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000878A5 File Offset: 0x00085AA5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlayersToUnregister);
		}

		// Token: 0x0400183D RID: 6205
		private int m_ApiVersion;

		// Token: 0x0400183E RID: 6206
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x0400183F RID: 6207
		private IntPtr m_PlayersToUnregister;

		// Token: 0x04001840 RID: 6208
		private uint m_PlayersToUnregisterCount;
	}
}
