using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000710 RID: 1808
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	internal struct BeginPlayerSessionOptionsAccountIdInternal : IDisposable
	{
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x0008C478 File Offset: 0x0008A678
		// (set) Token: 0x0600444E RID: 17486 RVA: 0x0008C49A File Offset: 0x0008A69A
		public MetricsAccountIdType AccountIdType
		{
			get
			{
				MetricsAccountIdType @default = Helper.GetDefault<MetricsAccountIdType>();
				Helper.TryMarshalGet<MetricsAccountIdType>(this.m_AccountIdType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<MetricsAccountIdType>(ref this.m_AccountIdType, value);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x0008C4AC File Offset: 0x0008A6AC
		// (set) Token: 0x06004450 RID: 17488 RVA: 0x0008C4D5 File Offset: 0x0008A6D5
		public EpicAccountId Epic
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId, MetricsAccountIdType>(this.m_Epic, out @default, this.m_AccountIdType, MetricsAccountIdType.Epic);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<EpicAccountId, MetricsAccountIdType>(ref this.m_Epic, value, ref this.m_AccountIdType, MetricsAccountIdType.Epic, this);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x0008C4F8 File Offset: 0x0008A6F8
		// (set) Token: 0x06004452 RID: 17490 RVA: 0x0008C521 File Offset: 0x0008A721
		public string External
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<MetricsAccountIdType>(this.m_External, out @default, this.m_AccountIdType, MetricsAccountIdType.External);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<MetricsAccountIdType>(ref this.m_External, value, ref this.m_AccountIdType, MetricsAccountIdType.External, this);
			}
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x0008C542 File Offset: 0x0008A742
		public void Dispose()
		{
			Helper.TryMarshalDispose<MetricsAccountIdType>(ref this.m_External, this.m_AccountIdType, MetricsAccountIdType.External);
		}

		// Token: 0x04001A3F RID: 6719
		[FieldOffset(0)]
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x04001A40 RID: 6720
		[FieldOffset(4)]
		private IntPtr m_Epic;

		// Token: 0x04001A41 RID: 6721
		[FieldOffset(4)]
		private IntPtr m_External;
	}
}
