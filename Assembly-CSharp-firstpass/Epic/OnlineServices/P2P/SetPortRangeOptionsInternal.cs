using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000708 RID: 1800
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPortRangeOptionsInternal : IDisposable
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x0008C078 File Offset: 0x0008A278
		// (set) Token: 0x06004413 RID: 17427 RVA: 0x0008C09A File Offset: 0x0008A29A
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

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x0008C0AC File Offset: 0x0008A2AC
		// (set) Token: 0x06004415 RID: 17429 RVA: 0x0008C0CE File Offset: 0x0008A2CE
		public ushort Port
		{
			get
			{
				ushort @default = Helper.GetDefault<ushort>();
				Helper.TryMarshalGet<ushort>(this.m_Port, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ushort>(ref this.m_Port, value);
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x0008C0E0 File Offset: 0x0008A2E0
		// (set) Token: 0x06004417 RID: 17431 RVA: 0x0008C102 File Offset: 0x0008A302
		public ushort MaxAdditionalPortsToTry
		{
			get
			{
				ushort @default = Helper.GetDefault<ushort>();
				Helper.TryMarshalGet<ushort>(this.m_MaxAdditionalPortsToTry, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ushort>(ref this.m_MaxAdditionalPortsToTry, value);
			}
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x0008C111 File Offset: 0x0008A311
		public void Dispose()
		{
		}

		// Token: 0x04001A28 RID: 6696
		private int m_ApiVersion;

		// Token: 0x04001A29 RID: 6697
		private ushort m_Port;

		// Token: 0x04001A2A RID: 6698
		private ushort m_MaxAdditionalPortsToTry;
	}
}
