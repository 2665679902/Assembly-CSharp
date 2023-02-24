using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200084B RID: 2123
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EntitlementInternal : IDisposable
	{
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x00094538 File Offset: 0x00092738
		// (set) Token: 0x06004C1C RID: 19484 RVA: 0x0009455A File Offset: 0x0009275A
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

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x0009456C File Offset: 0x0009276C
		// (set) Token: 0x06004C1E RID: 19486 RVA: 0x0009458E File Offset: 0x0009278E
		public string EntitlementName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_EntitlementName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementName, value);
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x000945A0 File Offset: 0x000927A0
		// (set) Token: 0x06004C20 RID: 19488 RVA: 0x000945C2 File Offset: 0x000927C2
		public string EntitlementId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_EntitlementId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementId, value);
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x000945D4 File Offset: 0x000927D4
		// (set) Token: 0x06004C22 RID: 19490 RVA: 0x000945F6 File Offset: 0x000927F6
		public string CatalogItemId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_CatalogItemId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CatalogItemId, value);
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x00094608 File Offset: 0x00092808
		// (set) Token: 0x06004C24 RID: 19492 RVA: 0x0009462A File Offset: 0x0009282A
		public int ServerIndex
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ServerIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ServerIndex, value);
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x0009463C File Offset: 0x0009283C
		// (set) Token: 0x06004C26 RID: 19494 RVA: 0x0009465E File Offset: 0x0009285E
		public bool Redeemed
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_Redeemed, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Redeemed, value);
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x00094670 File Offset: 0x00092870
		// (set) Token: 0x06004C28 RID: 19496 RVA: 0x00094692 File Offset: 0x00092892
		public long EndTimestamp
		{
			get
			{
				long @default = Helper.GetDefault<long>();
				Helper.TryMarshalGet<long>(this.m_EndTimestamp, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<long>(ref this.m_EndTimestamp, value);
			}
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x000946A1 File Offset: 0x000928A1
		public void Dispose()
		{
		}

		// Token: 0x04001D86 RID: 7558
		private int m_ApiVersion;

		// Token: 0x04001D87 RID: 7559
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_EntitlementName;

		// Token: 0x04001D88 RID: 7560
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_EntitlementId;

		// Token: 0x04001D89 RID: 7561
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CatalogItemId;

		// Token: 0x04001D8A RID: 7562
		private int m_ServerIndex;

		// Token: 0x04001D8B RID: 7563
		private int m_Redeemed;

		// Token: 0x04001D8C RID: 7564
		private long m_EndTimestamp;
	}
}
