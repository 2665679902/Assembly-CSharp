using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000898 RID: 2200
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateDeviceIdOptionsInternal : IDisposable
	{
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x00096520 File Offset: 0x00094720
		// (set) Token: 0x06004E04 RID: 19972 RVA: 0x00096542 File Offset: 0x00094742
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

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x00096554 File Offset: 0x00094754
		// (set) Token: 0x06004E06 RID: 19974 RVA: 0x00096576 File Offset: 0x00094776
		public string DeviceModel
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DeviceModel, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DeviceModel, value);
			}
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x00096585 File Offset: 0x00094785
		public void Dispose()
		{
		}

		// Token: 0x04001E43 RID: 7747
		private int m_ApiVersion;

		// Token: 0x04001E44 RID: 7748
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DeviceModel;
	}
}
