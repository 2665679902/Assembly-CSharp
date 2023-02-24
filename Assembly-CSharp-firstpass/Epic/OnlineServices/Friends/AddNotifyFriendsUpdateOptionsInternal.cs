using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000803 RID: 2051
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyFriendsUpdateOptionsInternal : IDisposable
	{
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x00091B08 File Offset: 0x0008FD08
		// (set) Token: 0x060049B2 RID: 18866 RVA: 0x00091B2A File Offset: 0x0008FD2A
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

		// Token: 0x060049B3 RID: 18867 RVA: 0x00091B39 File Offset: 0x0008FD39
		public void Dispose()
		{
		}

		// Token: 0x04001C6F RID: 7279
		private int m_ApiVersion;
	}
}
