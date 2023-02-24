using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000779 RID: 1913
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationAddMemberAttributeOptionsInternal : IDisposable
	{
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x0008F570 File Offset: 0x0008D770
		// (set) Token: 0x060046D9 RID: 18137 RVA: 0x0008F592 File Offset: 0x0008D792
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

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
		// (set) Token: 0x060046DB RID: 18139 RVA: 0x0008F5C6 File Offset: 0x0008D7C6
		public AttributeDataInternal? Attribute
		{
			get
			{
				AttributeDataInternal? @default = Helper.GetDefault<AttributeDataInternal?>();
				Helper.TryMarshalGet<AttributeDataInternal>(this.m_Attribute, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal>(ref this.m_Attribute, value);
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x0008F5D8 File Offset: 0x0008D7D8
		// (set) Token: 0x060046DD RID: 18141 RVA: 0x0008F5FA File Offset: 0x0008D7FA
		public LobbyAttributeVisibility Visibility
		{
			get
			{
				LobbyAttributeVisibility @default = Helper.GetDefault<LobbyAttributeVisibility>();
				Helper.TryMarshalGet<LobbyAttributeVisibility>(this.m_Visibility, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LobbyAttributeVisibility>(ref this.m_Visibility, value);
			}
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x0008F609 File Offset: 0x0008D809
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Attribute);
		}

		// Token: 0x04001B83 RID: 7043
		private int m_ApiVersion;

		// Token: 0x04001B84 RID: 7044
		private IntPtr m_Attribute;

		// Token: 0x04001B85 RID: 7045
		private LobbyAttributeVisibility m_Visibility;
	}
}
