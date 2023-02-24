using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020006D7 RID: 1751
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InitializeOptionsInternal : IDisposable
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x0008A55C File Offset: 0x0008875C
		// (set) Token: 0x06004284 RID: 17028 RVA: 0x0008A57E File Offset: 0x0008877E
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

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x0008A590 File Offset: 0x00088790
		// (set) Token: 0x06004286 RID: 17030 RVA: 0x0008A5B2 File Offset: 0x000887B2
		public AllocateMemoryFunc AllocateMemoryFunction
		{
			get
			{
				AllocateMemoryFunc @default = Helper.GetDefault<AllocateMemoryFunc>();
				Helper.TryMarshalGet<AllocateMemoryFunc>(this.m_AllocateMemoryFunction, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AllocateMemoryFunc>(ref this.m_AllocateMemoryFunction, value);
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x0008A5C4 File Offset: 0x000887C4
		// (set) Token: 0x06004288 RID: 17032 RVA: 0x0008A5E6 File Offset: 0x000887E6
		public ReallocateMemoryFunc ReallocateMemoryFunction
		{
			get
			{
				ReallocateMemoryFunc @default = Helper.GetDefault<ReallocateMemoryFunc>();
				Helper.TryMarshalGet<ReallocateMemoryFunc>(this.m_ReallocateMemoryFunction, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ReallocateMemoryFunc>(ref this.m_ReallocateMemoryFunction, value);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x0008A5F8 File Offset: 0x000887F8
		// (set) Token: 0x0600428A RID: 17034 RVA: 0x0008A61A File Offset: 0x0008881A
		public ReleaseMemoryFunc ReleaseMemoryFunction
		{
			get
			{
				ReleaseMemoryFunc @default = Helper.GetDefault<ReleaseMemoryFunc>();
				Helper.TryMarshalGet<ReleaseMemoryFunc>(this.m_ReleaseMemoryFunction, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ReleaseMemoryFunc>(ref this.m_ReleaseMemoryFunction, value);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x0008A62C File Offset: 0x0008882C
		// (set) Token: 0x0600428C RID: 17036 RVA: 0x0008A64E File Offset: 0x0008884E
		public string ProductName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ProductName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ProductName, value);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x0008A660 File Offset: 0x00088860
		// (set) Token: 0x0600428E RID: 17038 RVA: 0x0008A682 File Offset: 0x00088882
		public string ProductVersion
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ProductVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ProductVersion, value);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x0008A694 File Offset: 0x00088894
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x0008A6B6 File Offset: 0x000888B6
		public IntPtr Reserved
		{
			get
			{
				IntPtr @default = Helper.GetDefault<IntPtr>();
				Helper.TryMarshalGet(this.m_Reserved, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<IntPtr>(ref this.m_Reserved, value);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x0008A6C8 File Offset: 0x000888C8
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x0008A6EA File Offset: 0x000888EA
		public IntPtr SystemInitializeOptions
		{
			get
			{
				IntPtr @default = Helper.GetDefault<IntPtr>();
				Helper.TryMarshalGet(this.m_SystemInitializeOptions, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<IntPtr>(ref this.m_SystemInitializeOptions, value);
			}
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x0008A6F9 File Offset: 0x000888F9
		public void Dispose()
		{
		}

		// Token: 0x0400197B RID: 6523
		private int m_ApiVersion;

		// Token: 0x0400197C RID: 6524
		private AllocateMemoryFunc m_AllocateMemoryFunction;

		// Token: 0x0400197D RID: 6525
		private ReallocateMemoryFunc m_ReallocateMemoryFunction;

		// Token: 0x0400197E RID: 6526
		private ReleaseMemoryFunc m_ReleaseMemoryFunction;

		// Token: 0x0400197F RID: 6527
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ProductName;

		// Token: 0x04001980 RID: 6528
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ProductVersion;

		// Token: 0x04001981 RID: 6529
		private IntPtr m_Reserved;

		// Token: 0x04001982 RID: 6530
		private IntPtr m_SystemInitializeOptions;
	}
}
