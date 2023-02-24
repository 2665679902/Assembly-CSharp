using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200077B RID: 1915
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationRemoveAttributeOptionsInternal : IDisposable
	{
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x0008F634 File Offset: 0x0008D834
		// (set) Token: 0x060046E4 RID: 18148 RVA: 0x0008F656 File Offset: 0x0008D856
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

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x0008F668 File Offset: 0x0008D868
		// (set) Token: 0x060046E6 RID: 18150 RVA: 0x0008F68A File Offset: 0x0008D88A
		public string Key
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Key, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Key, value);
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x0008F699 File Offset: 0x0008D899
		public void Dispose()
		{
		}

		// Token: 0x04001B87 RID: 7047
		private int m_ApiVersion;

		// Token: 0x04001B88 RID: 7048
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;
	}
}
