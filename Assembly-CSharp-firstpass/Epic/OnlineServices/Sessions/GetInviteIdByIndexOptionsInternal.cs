using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E6 RID: 1510
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteIdByIndexOptionsInternal : IDisposable
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x00084D8C File Offset: 0x00082F8C
		// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x00084DAE File Offset: 0x00082FAE
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

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x00084DC0 File Offset: 0x00082FC0
		// (set) Token: 0x06003CD7 RID: 15575 RVA: 0x00084DE2 File Offset: 0x00082FE2
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x00084DF4 File Offset: 0x00082FF4
		// (set) Token: 0x06003CD9 RID: 15577 RVA: 0x00084E16 File Offset: 0x00083016
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

		// Token: 0x06003CDA RID: 15578 RVA: 0x00084E25 File Offset: 0x00083025
		public void Dispose()
		{
		}

		// Token: 0x04001732 RID: 5938
		private int m_ApiVersion;

		// Token: 0x04001733 RID: 5939
		private IntPtr m_LocalUserId;

		// Token: 0x04001734 RID: 5940
		private uint m_Index;
	}
}
