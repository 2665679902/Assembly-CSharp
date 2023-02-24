using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000572 RID: 1394
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetToggleFriendsKeyOptionsInternal : IDisposable
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x00081EBC File Offset: 0x000800BC
		// (set) Token: 0x060039F8 RID: 14840 RVA: 0x00081EDE File Offset: 0x000800DE
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x00081EF0 File Offset: 0x000800F0
		// (set) Token: 0x060039FA RID: 14842 RVA: 0x00081F12 File Offset: 0x00080112
		public KeyCombination KeyCombination
		{
			get
			{
				KeyCombination @default = Helper.GetDefault<KeyCombination>();
				Helper.TryMarshalGet<KeyCombination>(this.m_KeyCombination, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<KeyCombination>(ref this.m_KeyCombination, value);
			}
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x00081F21 File Offset: 0x00080121
		public void Dispose()
		{
		}

		// Token: 0x04001614 RID: 5652
		private int m_ApiVersion;

		// Token: 0x04001615 RID: 5653
		private KeyCombination m_KeyCombination;
	}
}
