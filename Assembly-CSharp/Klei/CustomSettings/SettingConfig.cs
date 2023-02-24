using System;
using System.Collections.Generic;

namespace Klei.CustomSettings
{
	// Token: 0x02000D5D RID: 3421
	public abstract class SettingConfig
	{
		// Token: 0x06006877 RID: 26743 RVA: 0x0028B4C4 File Offset: 0x002896C4
		public SettingConfig(string id, string label, string tooltip, string default_level_id, string nosweat_default_level_id, long coordinate_dimension = -1L, long coordinate_dimension_width = -1L, bool debug_only = false, bool triggers_custom_game = true, string required_content = "", string missing_content_default = "", bool editor_only = false)
		{
			this.id = id;
			this.label = label;
			this.tooltip = tooltip;
			this.default_level_id = default_level_id;
			this.nosweat_default_level_id = nosweat_default_level_id;
			this.coordinate_dimension = coordinate_dimension;
			this.coordinate_dimension_width = coordinate_dimension_width;
			this.debug_only = debug_only;
			this.triggers_custom_game = triggers_custom_game;
			this.required_content = required_content;
			this.missing_content_default = missing_content_default;
			this.editor_only = editor_only;
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06006878 RID: 26744 RVA: 0x0028B534 File Offset: 0x00289734
		// (set) Token: 0x06006879 RID: 26745 RVA: 0x0028B53C File Offset: 0x0028973C
		public string id { get; private set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600687A RID: 26746 RVA: 0x0028B545 File Offset: 0x00289745
		// (set) Token: 0x0600687B RID: 26747 RVA: 0x0028B54D File Offset: 0x0028974D
		public string label { get; private set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600687C RID: 26748 RVA: 0x0028B556 File Offset: 0x00289756
		// (set) Token: 0x0600687D RID: 26749 RVA: 0x0028B55E File Offset: 0x0028975E
		public string tooltip { get; private set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600687E RID: 26750 RVA: 0x0028B567 File Offset: 0x00289767
		// (set) Token: 0x0600687F RID: 26751 RVA: 0x0028B56F File Offset: 0x0028976F
		public long coordinate_dimension { get; protected set; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06006880 RID: 26752 RVA: 0x0028B578 File Offset: 0x00289778
		// (set) Token: 0x06006881 RID: 26753 RVA: 0x0028B580 File Offset: 0x00289780
		public long coordinate_dimension_width { get; protected set; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x0028B589 File Offset: 0x00289789
		// (set) Token: 0x06006883 RID: 26755 RVA: 0x0028B591 File Offset: 0x00289791
		public string required_content { get; private set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06006884 RID: 26756 RVA: 0x0028B59A File Offset: 0x0028979A
		// (set) Token: 0x06006885 RID: 26757 RVA: 0x0028B5A2 File Offset: 0x002897A2
		public string missing_content_default { get; private set; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06006886 RID: 26758 RVA: 0x0028B5AB File Offset: 0x002897AB
		// (set) Token: 0x06006887 RID: 26759 RVA: 0x0028B5B3 File Offset: 0x002897B3
		public bool triggers_custom_game { get; protected set; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06006888 RID: 26760 RVA: 0x0028B5BC File Offset: 0x002897BC
		// (set) Token: 0x06006889 RID: 26761 RVA: 0x0028B5C4 File Offset: 0x002897C4
		public bool debug_only { get; protected set; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600688A RID: 26762 RVA: 0x0028B5CD File Offset: 0x002897CD
		// (set) Token: 0x0600688B RID: 26763 RVA: 0x0028B5D5 File Offset: 0x002897D5
		public bool editor_only { get; protected set; }

		// Token: 0x0600688C RID: 26764
		public abstract SettingLevel GetLevel(string level_id);

		// Token: 0x0600688D RID: 26765
		public abstract List<SettingLevel> GetLevels();

		// Token: 0x0600688E RID: 26766 RVA: 0x0028B5DE File Offset: 0x002897DE
		public bool IsDefaultLevel(string level_id)
		{
			return level_id == this.default_level_id;
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0028B5EC File Offset: 0x002897EC
		public string GetDefaultLevelId()
		{
			if (!DlcManager.IsContentActive(this.required_content) && !string.IsNullOrEmpty(this.missing_content_default))
			{
				return this.missing_content_default;
			}
			return this.default_level_id;
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x0028B615 File Offset: 0x00289815
		public string GetNoSweatDefaultLevelId()
		{
			if (!DlcManager.IsContentActive(this.required_content) && !string.IsNullOrEmpty(this.missing_content_default))
			{
				return this.missing_content_default;
			}
			return this.nosweat_default_level_id;
		}

		// Token: 0x04004EBE RID: 20158
		protected string default_level_id;

		// Token: 0x04004EBF RID: 20159
		protected string nosweat_default_level_id;
	}
}
