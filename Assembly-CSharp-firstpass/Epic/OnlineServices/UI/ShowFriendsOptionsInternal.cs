using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000576 RID: 1398
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowFriendsOptionsInternal : IDisposable
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x00081FF0 File Offset: 0x000801F0
		// (set) Token: 0x06003A0C RID: 14860 RVA: 0x00082012 File Offset: 0x00080212
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

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x00082024 File Offset: 0x00080224
		// (set) Token: 0x06003A0E RID: 14862 RVA: 0x00082046 File Offset: 0x00080246
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

		// Token: 0x06003A0F RID: 14863 RVA: 0x00082055 File Offset: 0x00080255
		public void Dispose()
		{
		}

		// Token: 0x0400161D RID: 5661
		private int m_ApiVersion;

		// Token: 0x0400161E RID: 5662
		private IntPtr m_LocalUserId;
	}
}
