using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200066D RID: 1645
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DataRecordInternal : IDisposable
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x00087C98 File Offset: 0x00085E98
		// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x00087CBA File Offset: 0x00085EBA
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

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x00087CCC File Offset: 0x00085ECC
		// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x00087CEE File Offset: 0x00085EEE
		public string Key
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Key, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Key, value);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x00087D00 File Offset: 0x00085F00
		// (set) Token: 0x06003FD6 RID: 16342 RVA: 0x00087D22 File Offset: 0x00085F22
		public string Value
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Value, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Value, value);
			}
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x00087D31 File Offset: 0x00085F31
		public void Dispose()
		{
		}

		// Token: 0x0400185B RID: 6235
		private int m_ApiVersion;

		// Token: 0x0400185C RID: 6236
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;

		// Token: 0x0400185D RID: 6237
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Value;
	}
}
