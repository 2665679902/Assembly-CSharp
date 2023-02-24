using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200075E RID: 1886
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyMemberAttributeByKeyOptionsInternal : IDisposable
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x0008E100 File Offset: 0x0008C300
		// (set) Token: 0x060045F1 RID: 17905 RVA: 0x0008E122 File Offset: 0x0008C322
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

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x0008E134 File Offset: 0x0008C334
		// (set) Token: 0x060045F3 RID: 17907 RVA: 0x0008E156 File Offset: 0x0008C356
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

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x0008E168 File Offset: 0x0008C368
		// (set) Token: 0x060045F5 RID: 17909 RVA: 0x0008E18A File Offset: 0x0008C38A
		public string AttrKey
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AttrKey, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AttrKey, value);
			}
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x0008E199 File Offset: 0x0008C399
		public void Dispose()
		{
		}

		// Token: 0x04001B04 RID: 6916
		private int m_ApiVersion;

		// Token: 0x04001B05 RID: 6917
		private IntPtr m_TargetUserId;

		// Token: 0x04001B06 RID: 6918
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AttrKey;
	}
}
