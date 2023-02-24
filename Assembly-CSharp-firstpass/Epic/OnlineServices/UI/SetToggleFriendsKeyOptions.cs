using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000571 RID: 1393
	public class SetToggleFriendsKeyOptions
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x00081E9F File Offset: 0x0008009F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x00081EA2 File Offset: 0x000800A2
		// (set) Token: 0x060039F5 RID: 14837 RVA: 0x00081EAA File Offset: 0x000800AA
		public KeyCombination KeyCombination { get; set; }
	}
}
