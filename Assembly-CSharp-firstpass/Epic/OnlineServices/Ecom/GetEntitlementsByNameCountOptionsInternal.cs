using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200084D RID: 2125
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetEntitlementsByNameCountOptionsInternal : IDisposable
	{
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x000946D0 File Offset: 0x000928D0
		// (set) Token: 0x06004C31 RID: 19505 RVA: 0x000946F2 File Offset: 0x000928F2
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

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x00094704 File Offset: 0x00092904
		// (set) Token: 0x06004C33 RID: 19507 RVA: 0x00094726 File Offset: 0x00092926
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

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x00094738 File Offset: 0x00092938
		// (set) Token: 0x06004C35 RID: 19509 RVA: 0x0009475A File Offset: 0x0009295A
		public string EntitlementName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_EntitlementName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementName, value);
			}
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x00094769 File Offset: 0x00092969
		public void Dispose()
		{
		}

		// Token: 0x04001D8F RID: 7567
		private int m_ApiVersion;

		// Token: 0x04001D90 RID: 7568
		private IntPtr m_LocalUserId;

		// Token: 0x04001D91 RID: 7569
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_EntitlementName;
	}
}
