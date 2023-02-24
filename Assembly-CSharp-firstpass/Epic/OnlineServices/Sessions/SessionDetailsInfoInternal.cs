using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000628 RID: 1576
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsInfoInternal : IDisposable
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06003E0F RID: 15887 RVA: 0x00085AC8 File Offset: 0x00083CC8
		// (set) Token: 0x06003E10 RID: 15888 RVA: 0x00085AEA File Offset: 0x00083CEA
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

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x00085AFC File Offset: 0x00083CFC
		// (set) Token: 0x06003E12 RID: 15890 RVA: 0x00085B1E File Offset: 0x00083D1E
		public string SessionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionId, value);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06003E13 RID: 15891 RVA: 0x00085B30 File Offset: 0x00083D30
		// (set) Token: 0x06003E14 RID: 15892 RVA: 0x00085B52 File Offset: 0x00083D52
		public string HostAddress
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_HostAddress, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_HostAddress, value);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x00085B64 File Offset: 0x00083D64
		// (set) Token: 0x06003E16 RID: 15894 RVA: 0x00085B86 File Offset: 0x00083D86
		public uint NumOpenPublicConnections
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_NumOpenPublicConnections, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_NumOpenPublicConnections, value);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x00085B98 File Offset: 0x00083D98
		// (set) Token: 0x06003E18 RID: 15896 RVA: 0x00085BBA File Offset: 0x00083DBA
		public SessionDetailsSettingsInternal? Settings
		{
			get
			{
				SessionDetailsSettingsInternal? @default = Helper.GetDefault<SessionDetailsSettingsInternal?>();
				Helper.TryMarshalGet<SessionDetailsSettingsInternal>(this.m_Settings, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<SessionDetailsSettingsInternal>(ref this.m_Settings, value);
			}
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00085BC9 File Offset: 0x00083DC9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Settings);
		}

		// Token: 0x04001795 RID: 6037
		private int m_ApiVersion;

		// Token: 0x04001796 RID: 6038
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionId;

		// Token: 0x04001797 RID: 6039
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_HostAddress;

		// Token: 0x04001798 RID: 6040
		private uint m_NumOpenPublicConnections;

		// Token: 0x04001799 RID: 6041
		private IntPtr m_Settings;
	}
}
