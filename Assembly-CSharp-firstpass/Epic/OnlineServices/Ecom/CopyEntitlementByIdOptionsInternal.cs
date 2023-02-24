using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000831 RID: 2097
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByIdOptionsInternal : IDisposable
	{
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06004B23 RID: 19235 RVA: 0x00093198 File Offset: 0x00091398
		// (set) Token: 0x06004B24 RID: 19236 RVA: 0x000931BA File Offset: 0x000913BA
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

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x000931CC File Offset: 0x000913CC
		// (set) Token: 0x06004B26 RID: 19238 RVA: 0x000931EE File Offset: 0x000913EE
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

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x00093200 File Offset: 0x00091400
		// (set) Token: 0x06004B28 RID: 19240 RVA: 0x00093222 File Offset: 0x00091422
		public string EntitlementId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_EntitlementId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementId, value);
			}
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x00093231 File Offset: 0x00091431
		public void Dispose()
		{
		}

		// Token: 0x04001D0C RID: 7436
		private int m_ApiVersion;

		// Token: 0x04001D0D RID: 7437
		private IntPtr m_LocalUserId;

		// Token: 0x04001D0E RID: 7438
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_EntitlementId;
	}
}
