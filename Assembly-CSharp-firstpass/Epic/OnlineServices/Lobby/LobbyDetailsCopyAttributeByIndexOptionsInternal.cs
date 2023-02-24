using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000756 RID: 1878
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyAttributeByIndexOptionsInternal : IDisposable
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x0008DEE0 File Offset: 0x0008C0E0
		// (set) Token: 0x060045CB RID: 17867 RVA: 0x0008DF02 File Offset: 0x0008C102
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

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x0008DF14 File Offset: 0x0008C114
		// (set) Token: 0x060045CD RID: 17869 RVA: 0x0008DF36 File Offset: 0x0008C136
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

		// Token: 0x060045CE RID: 17870 RVA: 0x0008DF45 File Offset: 0x0008C145
		public void Dispose()
		{
		}

		// Token: 0x04001AF7 RID: 6903
		private int m_ApiVersion;

		// Token: 0x04001AF8 RID: 6904
		private uint m_AttrIndex;
	}
}
