using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200072E RID: 1838
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeDataInternal : IDisposable
	{
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x0008CBA0 File Offset: 0x0008ADA0
		// (set) Token: 0x060044BC RID: 17596 RVA: 0x0008CBC2 File Offset: 0x0008ADC2
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

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x0008CBD4 File Offset: 0x0008ADD4
		// (set) Token: 0x060044BE RID: 17598 RVA: 0x0008CBF6 File Offset: 0x0008ADF6
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

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x0008CC08 File Offset: 0x0008AE08
		// (set) Token: 0x060044C0 RID: 17600 RVA: 0x0008CC2A File Offset: 0x0008AE2A
		public AttributeDataValueInternal Value
		{
			get
			{
				AttributeDataValueInternal @default = Helper.GetDefault<AttributeDataValueInternal>();
				Helper.TryMarshalGet<AttributeDataValueInternal>(this.m_Value, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataValueInternal>(ref this.m_Value, value);
			}
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x0008CC39 File Offset: 0x0008AE39
		public void Dispose()
		{
			Helper.TryMarshalDispose<AttributeDataValueInternal>(ref this.m_Value);
		}

		// Token: 0x04001A8B RID: 6795
		private int m_ApiVersion;

		// Token: 0x04001A8C RID: 6796
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;

		// Token: 0x04001A8D RID: 6797
		private AttributeDataValueInternal m_Value;
	}
}
