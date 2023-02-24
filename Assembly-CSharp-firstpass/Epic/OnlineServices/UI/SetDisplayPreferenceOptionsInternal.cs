using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000570 RID: 1392
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetDisplayPreferenceOptionsInternal : IDisposable
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x00081E38 File Offset: 0x00080038
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x00081E5A File Offset: 0x0008005A
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

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x00081E6C File Offset: 0x0008006C
		// (set) Token: 0x060039F1 RID: 14833 RVA: 0x00081E8E File Offset: 0x0008008E
		public NotificationLocation NotificationLocation
		{
			get
			{
				NotificationLocation @default = Helper.GetDefault<NotificationLocation>();
				Helper.TryMarshalGet<NotificationLocation>(this.m_NotificationLocation, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<NotificationLocation>(ref this.m_NotificationLocation, value);
			}
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x00081E9D File Offset: 0x0008009D
		public void Dispose()
		{
		}

		// Token: 0x04001611 RID: 5649
		private int m_ApiVersion;

		// Token: 0x04001612 RID: 5650
		private NotificationLocation m_NotificationLocation;
	}
}
