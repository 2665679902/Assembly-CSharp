using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008DD RID: 2269
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserLoginInfoInternal : IDisposable
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x00097750 File Offset: 0x00095950
		// (set) Token: 0x06004F80 RID: 20352 RVA: 0x00097772 File Offset: 0x00095972
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

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x00097784 File Offset: 0x00095984
		// (set) Token: 0x06004F82 RID: 20354 RVA: 0x000977A6 File Offset: 0x000959A6
		public string DisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x000977B5 File Offset: 0x000959B5
		public void Dispose()
		{
		}

		// Token: 0x04001ED4 RID: 7892
		private int m_ApiVersion;

		// Token: 0x04001ED5 RID: 7893
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;
	}
}
