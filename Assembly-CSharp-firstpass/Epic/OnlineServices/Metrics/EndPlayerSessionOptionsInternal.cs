using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000712 RID: 1810
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndPlayerSessionOptionsInternal : IDisposable
	{
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x0008C574 File Offset: 0x0008A774
		// (set) Token: 0x06004459 RID: 17497 RVA: 0x0008C596 File Offset: 0x0008A796
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

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x0008C5A8 File Offset: 0x0008A7A8
		// (set) Token: 0x0600445B RID: 17499 RVA: 0x0008C5CA File Offset: 0x0008A7CA
		public EndPlayerSessionOptionsAccountIdInternal AccountId
		{
			get
			{
				EndPlayerSessionOptionsAccountIdInternal @default = Helper.GetDefault<EndPlayerSessionOptionsAccountIdInternal>();
				Helper.TryMarshalGet<EndPlayerSessionOptionsAccountIdInternal>(this.m_AccountId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<EndPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId, value);
			}
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x0008C5D9 File Offset: 0x0008A7D9
		public void Dispose()
		{
			Helper.TryMarshalDispose<EndPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId);
		}

		// Token: 0x04001A43 RID: 6723
		private int m_ApiVersion;

		// Token: 0x04001A44 RID: 6724
		private EndPlayerSessionOptionsAccountIdInternal m_AccountId;
	}
}
