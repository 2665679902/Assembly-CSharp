using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000894 RID: 2196
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserInfoOptionsInternal : IDisposable
	{
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004DF2 RID: 19954 RVA: 0x00096420 File Offset: 0x00094620
		// (set) Token: 0x06004DF3 RID: 19955 RVA: 0x00096442 File Offset: 0x00094642
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

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x00096454 File Offset: 0x00094654
		// (set) Token: 0x06004DF5 RID: 19957 RVA: 0x00096476 File Offset: 0x00094676
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

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00096485 File Offset: 0x00094685
		public void Dispose()
		{
		}

		// Token: 0x04001E3C RID: 7740
		private int m_ApiVersion;

		// Token: 0x04001E3D RID: 7741
		private IntPtr m_TargetUserId;
	}
}
