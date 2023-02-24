using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200063F RID: 1599
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetPermissionLevelOptionsInternal : IDisposable
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06003EAC RID: 16044 RVA: 0x0008654C File Offset: 0x0008474C
		// (set) Token: 0x06003EAD RID: 16045 RVA: 0x0008656E File Offset: 0x0008476E
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

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x00086580 File Offset: 0x00084780
		// (set) Token: 0x06003EAF RID: 16047 RVA: 0x000865A2 File Offset: 0x000847A2
		public OnlineSessionPermissionLevel PermissionLevel
		{
			get
			{
				OnlineSessionPermissionLevel @default = Helper.GetDefault<OnlineSessionPermissionLevel>();
				Helper.TryMarshalGet<OnlineSessionPermissionLevel>(this.m_PermissionLevel, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<OnlineSessionPermissionLevel>(ref this.m_PermissionLevel, value);
			}
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x000865B1 File Offset: 0x000847B1
		public void Dispose()
		{
		}

		// Token: 0x040017CF RID: 6095
		private int m_ApiVersion;

		// Token: 0x040017D0 RID: 6096
		private OnlineSessionPermissionLevel m_PermissionLevel;
	}
}
