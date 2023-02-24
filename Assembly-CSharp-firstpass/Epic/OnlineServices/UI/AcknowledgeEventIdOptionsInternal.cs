using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200055A RID: 1370
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcknowledgeEventIdOptionsInternal : IDisposable
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x00081A98 File Offset: 0x0007FC98
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x00081ABA File Offset: 0x0007FCBA
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x00081ACC File Offset: 0x0007FCCC
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x00081AEE File Offset: 0x0007FCEE
		public ulong UiEventId
		{
			get
			{
				ulong @default = Helper.GetDefault<ulong>();
				Helper.TryMarshalGet<ulong>(this.m_UiEventId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ulong>(ref this.m_UiEventId, value);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x00081B00 File Offset: 0x0007FD00
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x00081B22 File Offset: 0x0007FD22
		public Result Result
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_Result, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<Result>(ref this.m_Result, value);
			}
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x00081B31 File Offset: 0x0007FD31
		public void Dispose()
		{
		}

		// Token: 0x04001584 RID: 5508
		private int m_ApiVersion;

		// Token: 0x04001585 RID: 5509
		private ulong m_UiEventId;

		// Token: 0x04001586 RID: 5510
		private Result m_Result;
	}
}
