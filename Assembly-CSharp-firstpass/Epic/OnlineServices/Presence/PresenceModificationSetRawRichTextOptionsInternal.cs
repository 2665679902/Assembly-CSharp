using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000689 RID: 1673
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetRawRichTextOptionsInternal : IDisposable
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x000889F0 File Offset: 0x00086BF0
		// (set) Token: 0x0600409B RID: 16539 RVA: 0x00088A12 File Offset: 0x00086C12
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

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x00088A24 File Offset: 0x00086C24
		// (set) Token: 0x0600409D RID: 16541 RVA: 0x00088A46 File Offset: 0x00086C46
		public string RichText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_RichText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_RichText, value);
			}
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x00088A55 File Offset: 0x00086C55
		public void Dispose()
		{
		}

		// Token: 0x040018B2 RID: 6322
		private int m_ApiVersion;

		// Token: 0x040018B3 RID: 6323
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_RichText;
	}
}
