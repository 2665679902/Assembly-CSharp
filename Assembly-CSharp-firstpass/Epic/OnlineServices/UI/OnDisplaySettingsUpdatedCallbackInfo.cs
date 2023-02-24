using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000569 RID: 1385
	public class OnDisplaySettingsUpdatedCallbackInfo
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x00081D6B File Offset: 0x0007FF6B
		// (set) Token: 0x060039D0 RID: 14800 RVA: 0x00081D73 File Offset: 0x0007FF73
		public object ClientData { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x00081D7C File Offset: 0x0007FF7C
		// (set) Token: 0x060039D2 RID: 14802 RVA: 0x00081D84 File Offset: 0x0007FF84
		public bool IsVisible { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x00081D8D File Offset: 0x0007FF8D
		// (set) Token: 0x060039D4 RID: 14804 RVA: 0x00081D95 File Offset: 0x0007FF95
		public bool IsExclusiveInput { get; set; }
	}
}
