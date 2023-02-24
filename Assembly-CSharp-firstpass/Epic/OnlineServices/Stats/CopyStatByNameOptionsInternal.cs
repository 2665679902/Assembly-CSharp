using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005A6 RID: 1446
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyStatByNameOptionsInternal : IDisposable
	{
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x00083484 File Offset: 0x00081684
		// (set) Token: 0x06003B53 RID: 15187 RVA: 0x000834A6 File Offset: 0x000816A6
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

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000834B8 File Offset: 0x000816B8
		// (set) Token: 0x06003B55 RID: 15189 RVA: 0x000834DA File Offset: 0x000816DA
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000834EC File Offset: 0x000816EC
		// (set) Token: 0x06003B57 RID: 15191 RVA: 0x0008350E File Offset: 0x0008170E
		public string Name
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Name, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Name, value);
			}
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x0008351D File Offset: 0x0008171D
		public void Dispose()
		{
		}

		// Token: 0x0400169D RID: 5789
		private int m_ApiVersion;

		// Token: 0x0400169E RID: 5790
		private IntPtr m_TargetUserId;

		// Token: 0x0400169F RID: 5791
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Name;
	}
}
