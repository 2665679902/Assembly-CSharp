using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000829 RID: 2089
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogReleaseInternal : IDisposable
	{
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x00092DDC File Offset: 0x00090FDC
		// (set) Token: 0x06004AED RID: 19181 RVA: 0x00092DFE File Offset: 0x00090FFE
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

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x00092E10 File Offset: 0x00091010
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x00092E38 File Offset: 0x00091038
		public string[] CompatibleAppIds
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_CompatibleAppIds, out @default, this.m_CompatibleAppIdCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CompatibleAppIds, value, out this.m_CompatibleAppIdCount);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004AF0 RID: 19184 RVA: 0x00092E50 File Offset: 0x00091050
		// (set) Token: 0x06004AF1 RID: 19185 RVA: 0x00092E78 File Offset: 0x00091078
		public string[] CompatiblePlatforms
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_CompatiblePlatforms, out @default, this.m_CompatiblePlatformCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CompatiblePlatforms, value, out this.m_CompatiblePlatformCount);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004AF2 RID: 19186 RVA: 0x00092E90 File Offset: 0x00091090
		// (set) Token: 0x06004AF3 RID: 19187 RVA: 0x00092EB2 File Offset: 0x000910B2
		public string ReleaseNote
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ReleaseNote, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ReleaseNote, value);
			}
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x00092EC1 File Offset: 0x000910C1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_CompatibleAppIds);
			Helper.TryMarshalDispose(ref this.m_CompatiblePlatforms);
		}

		// Token: 0x04001CF1 RID: 7409
		private int m_ApiVersion;

		// Token: 0x04001CF2 RID: 7410
		private uint m_CompatibleAppIdCount;

		// Token: 0x04001CF3 RID: 7411
		private IntPtr m_CompatibleAppIds;

		// Token: 0x04001CF4 RID: 7412
		private uint m_CompatiblePlatformCount;

		// Token: 0x04001CF5 RID: 7413
		private IntPtr m_CompatiblePlatforms;

		// Token: 0x04001CF6 RID: 7414
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ReleaseNote;
	}
}
