using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000633 RID: 1587
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationRemoveAttributeOptionsInternal : IDisposable
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06003E76 RID: 15990 RVA: 0x00086234 File Offset: 0x00084434
		// (set) Token: 0x06003E77 RID: 15991 RVA: 0x00086256 File Offset: 0x00084456
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

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x00086268 File Offset: 0x00084468
		// (set) Token: 0x06003E79 RID: 15993 RVA: 0x0008628A File Offset: 0x0008448A
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

		// Token: 0x06003E7A RID: 15994 RVA: 0x00086299 File Offset: 0x00084499
		public void Dispose()
		{
		}

		// Token: 0x040017BD RID: 6077
		private int m_ApiVersion;

		// Token: 0x040017BE RID: 6078
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;
	}
}
