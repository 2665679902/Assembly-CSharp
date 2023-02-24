using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000870 RID: 2160
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementsOptionsInternal : IDisposable
	{
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x00094F20 File Offset: 0x00093120
		// (set) Token: 0x06004CEC RID: 19692 RVA: 0x00094F42 File Offset: 0x00093142
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

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x00094F54 File Offset: 0x00093154
		// (set) Token: 0x06004CEE RID: 19694 RVA: 0x00094F76 File Offset: 0x00093176
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004CEF RID: 19695 RVA: 0x00094F88 File Offset: 0x00093188
		// (set) Token: 0x06004CF0 RID: 19696 RVA: 0x00094FB0 File Offset: 0x000931B0
		public string[] EntitlementNames
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_EntitlementNames, out @default, this.m_EntitlementNameCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementNames, value, out this.m_EntitlementNameCount);
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004CF1 RID: 19697 RVA: 0x00094FC8 File Offset: 0x000931C8
		// (set) Token: 0x06004CF2 RID: 19698 RVA: 0x00094FEA File Offset: 0x000931EA
		public bool IncludeRedeemed
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_IncludeRedeemed, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_IncludeRedeemed, value);
			}
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x00094FF9 File Offset: 0x000931F9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_EntitlementNames);
		}

		// Token: 0x04001DC9 RID: 7625
		private int m_ApiVersion;

		// Token: 0x04001DCA RID: 7626
		private IntPtr m_LocalUserId;

		// Token: 0x04001DCB RID: 7627
		private IntPtr m_EntitlementNames;

		// Token: 0x04001DCC RID: 7628
		private uint m_EntitlementNameCount;

		// Token: 0x04001DCD RID: 7629
		private int m_IncludeRedeemed;
	}
}
