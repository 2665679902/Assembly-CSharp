using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200092F RID: 2351
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetUnlockedAchievementCountOptionsInternal : IDisposable
	{
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060051C2 RID: 20930 RVA: 0x00099D80 File Offset: 0x00097F80
		// (set) Token: 0x060051C3 RID: 20931 RVA: 0x00099DA2 File Offset: 0x00097FA2
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

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060051C4 RID: 20932 RVA: 0x00099DB4 File Offset: 0x00097FB4
		// (set) Token: 0x060051C5 RID: 20933 RVA: 0x00099DD6 File Offset: 0x00097FD6
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00099DE5 File Offset: 0x00097FE5
		public void Dispose()
		{
		}

		// Token: 0x04001FD2 RID: 8146
		private int m_ApiVersion;

		// Token: 0x04001FD3 RID: 8147
		private IntPtr m_UserId;
	}
}
