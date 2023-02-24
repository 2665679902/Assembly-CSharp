using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E8 RID: 1512
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IsUserInSessionOptionsInternal : IDisposable
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x00084E54 File Offset: 0x00083054
		// (set) Token: 0x06003CE2 RID: 15586 RVA: 0x00084E76 File Offset: 0x00083076
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

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x00084E88 File Offset: 0x00083088
		// (set) Token: 0x06003CE4 RID: 15588 RVA: 0x00084EAA File Offset: 0x000830AA
		public string SessionName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionName, value);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x00084EBC File Offset: 0x000830BC
		// (set) Token: 0x06003CE6 RID: 15590 RVA: 0x00084EDE File Offset: 0x000830DE
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x00084EED File Offset: 0x000830ED
		public void Dispose()
		{
		}

		// Token: 0x04001737 RID: 5943
		private int m_ApiVersion;

		// Token: 0x04001738 RID: 5944
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x04001739 RID: 5945
		private IntPtr m_TargetUserId;
	}
}
