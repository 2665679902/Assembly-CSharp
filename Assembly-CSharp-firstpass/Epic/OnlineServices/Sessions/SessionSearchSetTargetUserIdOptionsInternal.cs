using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000654 RID: 1620
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetTargetUserIdOptionsInternal : IDisposable
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x00086C60 File Offset: 0x00084E60
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x00086C82 File Offset: 0x00084E82
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

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x00086C94 File Offset: 0x00084E94
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x00086CB6 File Offset: 0x00084EB6
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x00086CC5 File Offset: 0x00084EC5
		public void Dispose()
		{
		}

		// Token: 0x040017ED RID: 6125
		private int m_ApiVersion;

		// Token: 0x040017EE RID: 6126
		private IntPtr m_TargetUserId;
	}
}
