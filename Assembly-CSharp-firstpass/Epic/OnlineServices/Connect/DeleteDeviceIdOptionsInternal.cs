using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A2 RID: 2210
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteDeviceIdOptionsInternal : IDisposable
	{
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004E33 RID: 20019 RVA: 0x0009680C File Offset: 0x00094A0C
		// (set) Token: 0x06004E34 RID: 20020 RVA: 0x0009682E File Offset: 0x00094A2E
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

		// Token: 0x06004E35 RID: 20021 RVA: 0x0009683D File Offset: 0x00094A3D
		public void Dispose()
		{
		}

		// Token: 0x04001E57 RID: 7767
		private int m_ApiVersion;
	}
}
