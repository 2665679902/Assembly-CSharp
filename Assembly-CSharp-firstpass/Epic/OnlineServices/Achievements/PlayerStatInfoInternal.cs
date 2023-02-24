using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000947 RID: 2375
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerStatInfoInternal : IDisposable
	{
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06005256 RID: 21078 RVA: 0x0009A44C File Offset: 0x0009864C
		// (set) Token: 0x06005257 RID: 21079 RVA: 0x0009A46E File Offset: 0x0009866E
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

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06005258 RID: 21080 RVA: 0x0009A480 File Offset: 0x00098680
		// (set) Token: 0x06005259 RID: 21081 RVA: 0x0009A4A2 File Offset: 0x000986A2
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

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x0600525A RID: 21082 RVA: 0x0009A4B4 File Offset: 0x000986B4
		// (set) Token: 0x0600525B RID: 21083 RVA: 0x0009A4D6 File Offset: 0x000986D6
		public int CurrentValue
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_CurrentValue, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_CurrentValue, value);
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x0600525C RID: 21084 RVA: 0x0009A4E8 File Offset: 0x000986E8
		// (set) Token: 0x0600525D RID: 21085 RVA: 0x0009A50A File Offset: 0x0009870A
		public int ThresholdValue
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ThresholdValue, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ThresholdValue, value);
			}
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0009A519 File Offset: 0x00098719
		public void Dispose()
		{
		}

		// Token: 0x0400200A RID: 8202
		private int m_ApiVersion;

		// Token: 0x0400200B RID: 8203
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Name;

		// Token: 0x0400200C RID: 8204
		private int m_CurrentValue;

		// Token: 0x0400200D RID: 8205
		private int m_ThresholdValue;
	}
}
