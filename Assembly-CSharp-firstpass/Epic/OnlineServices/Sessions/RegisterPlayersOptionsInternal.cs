using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000612 RID: 1554
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlayersOptionsInternal : IDisposable
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x00085348 File Offset: 0x00083548
		// (set) Token: 0x06003D9B RID: 15771 RVA: 0x0008536A File Offset: 0x0008356A
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

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x0008537C File Offset: 0x0008357C
		// (set) Token: 0x06003D9D RID: 15773 RVA: 0x0008539E File Offset: 0x0008359E
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

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x000853B0 File Offset: 0x000835B0
		// (set) Token: 0x06003D9F RID: 15775 RVA: 0x000853D8 File Offset: 0x000835D8
		public ProductUserId[] PlayersToRegister
		{
			get
			{
				ProductUserId[] @default = Helper.GetDefault<ProductUserId[]>();
				Helper.TryMarshalGet<ProductUserId>(this.m_PlayersToRegister, out @default, this.m_PlayersToRegisterCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_PlayersToRegister, value, out this.m_PlayersToRegisterCount);
			}
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000853ED File Offset: 0x000835ED
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlayersToRegister);
		}

		// Token: 0x04001769 RID: 5993
		private int m_ApiVersion;

		// Token: 0x0400176A RID: 5994
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x0400176B RID: 5995
		private IntPtr m_PlayersToRegister;

		// Token: 0x0400176C RID: 5996
		private uint m_PlayersToRegisterCount;
	}
}
