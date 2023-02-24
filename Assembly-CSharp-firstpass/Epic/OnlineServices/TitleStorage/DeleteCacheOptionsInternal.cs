using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200057F RID: 1407
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheOptionsInternal : IDisposable
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06003A53 RID: 14931 RVA: 0x000825B4 File Offset: 0x000807B4
		// (set) Token: 0x06003A54 RID: 14932 RVA: 0x000825D6 File Offset: 0x000807D6
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

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000825E8 File Offset: 0x000807E8
		// (set) Token: 0x06003A56 RID: 14934 RVA: 0x0008260A File Offset: 0x0008080A
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

		// Token: 0x06003A57 RID: 14935 RVA: 0x00082619 File Offset: 0x00080819
		public void Dispose()
		{
		}

		// Token: 0x0400163A RID: 5690
		private int m_ApiVersion;

		// Token: 0x0400163B RID: 5691
		private IntPtr m_LocalUserId;
	}
}
