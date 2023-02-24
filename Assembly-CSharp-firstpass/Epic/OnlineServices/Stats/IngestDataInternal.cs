using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005AA RID: 1450
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestDataInternal : IDisposable
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000835D0 File Offset: 0x000817D0
		// (set) Token: 0x06003B69 RID: 15209 RVA: 0x000835F2 File Offset: 0x000817F2
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

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x00083604 File Offset: 0x00081804
		// (set) Token: 0x06003B6B RID: 15211 RVA: 0x00083626 File Offset: 0x00081826
		public string StatName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_StatName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_StatName, value);
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x00083638 File Offset: 0x00081838
		// (set) Token: 0x06003B6D RID: 15213 RVA: 0x0008365A File Offset: 0x0008185A
		public int IngestAmount
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_IngestAmount, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_IngestAmount, value);
			}
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x00083669 File Offset: 0x00081869
		public void Dispose()
		{
		}

		// Token: 0x040016A5 RID: 5797
		private int m_ApiVersion;

		// Token: 0x040016A6 RID: 5798
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_StatName;

		// Token: 0x040016A7 RID: 5799
		private int m_IngestAmount;
	}
}
