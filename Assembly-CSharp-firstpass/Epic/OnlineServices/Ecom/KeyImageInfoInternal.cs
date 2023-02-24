using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200085F RID: 2143
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KeyImageInfoInternal : IDisposable
	{
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x00094D30 File Offset: 0x00092F30
		// (set) Token: 0x06004C9E RID: 19614 RVA: 0x00094D52 File Offset: 0x00092F52
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

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x00094D64 File Offset: 0x00092F64
		// (set) Token: 0x06004CA0 RID: 19616 RVA: 0x00094D86 File Offset: 0x00092F86
		public string Type
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Type, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Type, value);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x00094D98 File Offset: 0x00092F98
		// (set) Token: 0x06004CA2 RID: 19618 RVA: 0x00094DBA File Offset: 0x00092FBA
		public string Url
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Url, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Url, value);
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x00094DCC File Offset: 0x00092FCC
		// (set) Token: 0x06004CA4 RID: 19620 RVA: 0x00094DEE File Offset: 0x00092FEE
		public uint Width
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_Width, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_Width, value);
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x00094E00 File Offset: 0x00093000
		// (set) Token: 0x06004CA6 RID: 19622 RVA: 0x00094E22 File Offset: 0x00093022
		public uint Height
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_Height, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_Height, value);
			}
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00094E31 File Offset: 0x00093031
		public void Dispose()
		{
		}

		// Token: 0x04001DB8 RID: 7608
		private int m_ApiVersion;

		// Token: 0x04001DB9 RID: 7609
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Type;

		// Token: 0x04001DBA RID: 7610
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Url;

		// Token: 0x04001DBB RID: 7611
		private uint m_Width;

		// Token: 0x04001DBC RID: 7612
		private uint m_Height;
	}
}
