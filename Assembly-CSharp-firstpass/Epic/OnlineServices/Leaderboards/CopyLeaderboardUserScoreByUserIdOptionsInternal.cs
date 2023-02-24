using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007DB RID: 2011
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardUserScoreByUserIdOptionsInternal : IDisposable
	{
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060048B7 RID: 18615 RVA: 0x00090ADC File Offset: 0x0008ECDC
		// (set) Token: 0x060048B8 RID: 18616 RVA: 0x00090AFE File Offset: 0x0008ECFE
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

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x00090B10 File Offset: 0x0008ED10
		// (set) Token: 0x060048BA RID: 18618 RVA: 0x00090B32 File Offset: 0x0008ED32
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

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060048BB RID: 18619 RVA: 0x00090B44 File Offset: 0x0008ED44
		// (set) Token: 0x060048BC RID: 18620 RVA: 0x00090B66 File Offset: 0x0008ED66
		public string StatName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_StatName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_StatName, value);
			}
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x00090B75 File Offset: 0x0008ED75
		public void Dispose()
		{
		}

		// Token: 0x04001C07 RID: 7175
		private int m_ApiVersion;

		// Token: 0x04001C08 RID: 7176
		private IntPtr m_UserId;

		// Token: 0x04001C09 RID: 7177
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_StatName;
	}
}
