using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000835 RID: 2101
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByNameAndIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06004B3F RID: 19263 RVA: 0x0009333C File Offset: 0x0009153C
		// (set) Token: 0x06004B40 RID: 19264 RVA: 0x0009335E File Offset: 0x0009155E
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

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x00093370 File Offset: 0x00091570
		// (set) Token: 0x06004B42 RID: 19266 RVA: 0x00093392 File Offset: 0x00091592
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

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x000933A4 File Offset: 0x000915A4
		// (set) Token: 0x06004B44 RID: 19268 RVA: 0x000933C6 File Offset: 0x000915C6
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

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x000933D8 File Offset: 0x000915D8
		// (set) Token: 0x06004B46 RID: 19270 RVA: 0x000933FA File Offset: 0x000915FA
		public uint Index
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_Index, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_Index, value);
			}
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00093409 File Offset: 0x00091609
		public void Dispose()
		{
		}

		// Token: 0x04001D17 RID: 7447
		private int m_ApiVersion;

		// Token: 0x04001D18 RID: 7448
		private IntPtr m_LocalUserId;

		// Token: 0x04001D19 RID: 7449
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_EntitlementName;

		// Token: 0x04001D1A RID: 7450
		private uint m_Index;
	}
}
