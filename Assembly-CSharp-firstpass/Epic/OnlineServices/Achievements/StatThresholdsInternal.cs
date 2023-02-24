using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200094D RID: 2381
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StatThresholdsInternal : IDisposable
	{
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600527F RID: 21119 RVA: 0x0009A6F4 File Offset: 0x000988F4
		// (set) Token: 0x06005280 RID: 21120 RVA: 0x0009A716 File Offset: 0x00098916
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

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06005281 RID: 21121 RVA: 0x0009A728 File Offset: 0x00098928
		// (set) Token: 0x06005282 RID: 21122 RVA: 0x0009A74A File Offset: 0x0009894A
		public string Name
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Name, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Name, value);
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06005283 RID: 21123 RVA: 0x0009A75C File Offset: 0x0009895C
		// (set) Token: 0x06005284 RID: 21124 RVA: 0x0009A77E File Offset: 0x0009897E
		public int Threshold
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_Threshold, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_Threshold, value);
			}
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0009A78D File Offset: 0x0009898D
		public void Dispose()
		{
		}

		// Token: 0x0400201B RID: 8219
		private int m_ApiVersion;

		// Token: 0x0400201C RID: 8220
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Name;

		// Token: 0x0400201D RID: 8221
		private int m_Threshold;
	}
}
