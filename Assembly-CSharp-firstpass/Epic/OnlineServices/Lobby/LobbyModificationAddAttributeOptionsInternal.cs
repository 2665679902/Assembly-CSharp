using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000777 RID: 1911
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationAddAttributeOptionsInternal : IDisposable
	{
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060046CB RID: 18123 RVA: 0x0008F49C File Offset: 0x0008D69C
		// (set) Token: 0x060046CC RID: 18124 RVA: 0x0008F4BE File Offset: 0x0008D6BE
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

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060046CD RID: 18125 RVA: 0x0008F4D0 File Offset: 0x0008D6D0
		// (set) Token: 0x060046CE RID: 18126 RVA: 0x0008F4F2 File Offset: 0x0008D6F2
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

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x0008F504 File Offset: 0x0008D704
		// (set) Token: 0x060046D0 RID: 18128 RVA: 0x0008F526 File Offset: 0x0008D726
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

		// Token: 0x060046D1 RID: 18129 RVA: 0x0008F535 File Offset: 0x0008D735
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Attribute);
		}

		// Token: 0x04001B7E RID: 7038
		private int m_ApiVersion;

		// Token: 0x04001B7F RID: 7039
		private IntPtr m_Attribute;

		// Token: 0x04001B80 RID: 7040
		private LobbyAttributeVisibility m_Visibility;
	}
}
