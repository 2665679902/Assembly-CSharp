using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200055C RID: 1372
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyDisplaySettingsUpdatedOptionsInternal : IDisposable
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x00081B40 File Offset: 0x0007FD40
		// (set) Token: 0x060039A3 RID: 14755 RVA: 0x00081B62 File Offset: 0x0007FD62
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

		// Token: 0x060039A4 RID: 14756 RVA: 0x00081B71 File Offset: 0x0007FD71
		public void Dispose()
		{
		}

		// Token: 0x04001587 RID: 5511
		private int m_ApiVersion;
	}
}
