using System;

namespace Klei.CustomSettings
{
	// Token: 0x02000D5C RID: 3420
	public class SettingLevel
	{
		// Token: 0x0600686C RID: 26732 RVA: 0x0028B440 File Offset: 0x00289640
		public SettingLevel(string id, string label, string tooltip, long coordinate_offset = 0L, object userdata = null)
		{
			this.id = id;
			this.label = label;
			this.tooltip = tooltip;
			this.userdata = userdata;
			this.coordinate_offset = coordinate_offset;
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600686D RID: 26733 RVA: 0x0028B46D File Offset: 0x0028966D
		// (set) Token: 0x0600686E RID: 26734 RVA: 0x0028B475 File Offset: 0x00289675
		public string id { get; private set; }

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600686F RID: 26735 RVA: 0x0028B47E File Offset: 0x0028967E
		// (set) Token: 0x06006870 RID: 26736 RVA: 0x0028B486 File Offset: 0x00289686
		public string tooltip { get; private set; }

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06006871 RID: 26737 RVA: 0x0028B48F File Offset: 0x0028968F
		// (set) Token: 0x06006872 RID: 26738 RVA: 0x0028B497 File Offset: 0x00289697
		public string label { get; private set; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06006873 RID: 26739 RVA: 0x0028B4A0 File Offset: 0x002896A0
		// (set) Token: 0x06006874 RID: 26740 RVA: 0x0028B4A8 File Offset: 0x002896A8
		public object userdata { get; private set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06006875 RID: 26741 RVA: 0x0028B4B1 File Offset: 0x002896B1
		// (set) Token: 0x06006876 RID: 26742 RVA: 0x0028B4B9 File Offset: 0x002896B9
		public long coordinate_offset { get; private set; }
	}
}
