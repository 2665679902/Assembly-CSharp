using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005CA RID: 1482
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeDataInternal : IDisposable
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x00084264 File Offset: 0x00082464
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x00084286 File Offset: 0x00082486
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

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x00084298 File Offset: 0x00082498
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000842BA File Offset: 0x000824BA
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

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000842CC File Offset: 0x000824CC
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000842EE File Offset: 0x000824EE
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

		// Token: 0x06003C35 RID: 15413 RVA: 0x000842FD File Offset: 0x000824FD
		public void Dispose()
		{
			Helper.TryMarshalDispose<AttributeDataValueInternal>(ref this.m_Value);
		}

		// Token: 0x040016F3 RID: 5875
		private int m_ApiVersion;

		// Token: 0x040016F4 RID: 5876
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;

		// Token: 0x040016F5 RID: 5877
		private AttributeDataValueInternal m_Value;
	}
}
