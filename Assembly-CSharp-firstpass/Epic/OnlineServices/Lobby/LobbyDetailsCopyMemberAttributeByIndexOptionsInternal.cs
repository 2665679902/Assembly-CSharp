using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200075C RID: 1884
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyMemberAttributeByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060045E3 RID: 17891 RVA: 0x0008E038 File Offset: 0x0008C238
		// (set) Token: 0x060045E4 RID: 17892 RVA: 0x0008E05A File Offset: 0x0008C25A
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

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x0008E06C File Offset: 0x0008C26C
		// (set) Token: 0x060045E6 RID: 17894 RVA: 0x0008E08E File Offset: 0x0008C28E
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

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x0008E0A0 File Offset: 0x0008C2A0
		// (set) Token: 0x060045E8 RID: 17896 RVA: 0x0008E0C2 File Offset: 0x0008C2C2
		public uint AttrIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_AttrIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_AttrIndex, value);
			}
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x0008E0D1 File Offset: 0x0008C2D1
		public void Dispose()
		{
		}

		// Token: 0x04001AFF RID: 6911
		private int m_ApiVersion;

		// Token: 0x04001B00 RID: 6912
		private IntPtr m_TargetUserId;

		// Token: 0x04001B01 RID: 6913
		private uint m_AttrIndex;
	}
}
