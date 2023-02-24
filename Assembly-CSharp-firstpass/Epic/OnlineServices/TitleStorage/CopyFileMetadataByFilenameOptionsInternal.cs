using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200057B RID: 1403
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataByFilenameOptionsInternal : IDisposable
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x0008244C File Offset: 0x0008064C
		// (set) Token: 0x06003A3E RID: 14910 RVA: 0x0008246E File Offset: 0x0008066E
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

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x00082480 File Offset: 0x00080680
		// (set) Token: 0x06003A40 RID: 14912 RVA: 0x000824A2 File Offset: 0x000806A2
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06003A41 RID: 14913 RVA: 0x000824B4 File Offset: 0x000806B4
		// (set) Token: 0x06003A42 RID: 14914 RVA: 0x000824D6 File Offset: 0x000806D6
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Filename, value);
			}
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000824E5 File Offset: 0x000806E5
		public void Dispose()
		{
		}

		// Token: 0x04001630 RID: 5680
		private int m_ApiVersion;

		// Token: 0x04001631 RID: 5681
		private IntPtr m_LocalUserId;

		// Token: 0x04001632 RID: 5682
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
