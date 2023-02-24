using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000635 RID: 1589
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetBucketIdOptionsInternal : IDisposable
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06003E7F RID: 15999 RVA: 0x000862B8 File Offset: 0x000844B8
		// (set) Token: 0x06003E80 RID: 16000 RVA: 0x000862DA File Offset: 0x000844DA
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

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06003E81 RID: 16001 RVA: 0x000862EC File Offset: 0x000844EC
		// (set) Token: 0x06003E82 RID: 16002 RVA: 0x0008630E File Offset: 0x0008450E
		public string BucketId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_BucketId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_BucketId, value);
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0008631D File Offset: 0x0008451D
		public void Dispose()
		{
		}

		// Token: 0x040017C0 RID: 6080
		private int m_ApiVersion;

		// Token: 0x040017C1 RID: 6081
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_BucketId;
	}
}
