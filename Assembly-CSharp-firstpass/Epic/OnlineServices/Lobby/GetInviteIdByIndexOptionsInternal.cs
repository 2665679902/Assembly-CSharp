using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000744 RID: 1860
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteIdByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x0008D5A4 File Offset: 0x0008B7A4
		// (set) Token: 0x0600454C RID: 17740 RVA: 0x0008D5C6 File Offset: 0x0008B7C6
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

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x0008D5D8 File Offset: 0x0008B7D8
		// (set) Token: 0x0600454E RID: 17742 RVA: 0x0008D5FA File Offset: 0x0008B7FA
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

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x0008D60C File Offset: 0x0008B80C
		// (set) Token: 0x06004550 RID: 17744 RVA: 0x0008D62E File Offset: 0x0008B82E
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

		// Token: 0x06004551 RID: 17745 RVA: 0x0008D63D File Offset: 0x0008B83D
		public void Dispose()
		{
		}

		// Token: 0x04001AC5 RID: 6853
		private int m_ApiVersion;

		// Token: 0x04001AC6 RID: 6854
		private IntPtr m_LocalUserId;

		// Token: 0x04001AC7 RID: 6855
		private uint m_Index;
	}
}
