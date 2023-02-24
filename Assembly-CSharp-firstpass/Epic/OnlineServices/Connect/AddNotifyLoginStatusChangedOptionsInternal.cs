using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000889 RID: 2185
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLoginStatusChangedOptionsInternal : IDisposable
	{
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x00095908 File Offset: 0x00093B08
		// (set) Token: 0x06004D82 RID: 19842 RVA: 0x0009592A File Offset: 0x00093B2A
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

		// Token: 0x06004D83 RID: 19843 RVA: 0x00095939 File Offset: 0x00093B39
		public void Dispose()
		{
		}

		// Token: 0x04001E0B RID: 7691
		private int m_ApiVersion;
	}
}
