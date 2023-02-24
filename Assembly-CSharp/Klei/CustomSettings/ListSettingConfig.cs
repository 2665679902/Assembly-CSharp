using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.CustomSettings
{
	// Token: 0x02000D5E RID: 3422
	public class ListSettingConfig : SettingConfig
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06006891 RID: 26769 RVA: 0x0028B63E File Offset: 0x0028983E
		// (set) Token: 0x06006892 RID: 26770 RVA: 0x0028B646 File Offset: 0x00289846
		public List<SettingLevel> levels { get; private set; }

		// Token: 0x06006893 RID: 26771 RVA: 0x0028B650 File Offset: 0x00289850
		public ListSettingConfig(string id, string label, string tooltip, List<SettingLevel> levels, string default_level_id, string nosweat_default_level_id, long coordinate_dimension = -1L, long coordinate_dimension_width = -1L, bool debug_only = false, bool triggers_custom_game = true, string required_content = "", string missing_content_default = "", bool editor_only = false)
			: base(id, label, tooltip, default_level_id, nosweat_default_level_id, coordinate_dimension, coordinate_dimension_width, debug_only, triggers_custom_game, required_content, missing_content_default, editor_only)
		{
			this.levels = levels;
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x0028B680 File Offset: 0x00289880
		public void StompLevels(List<SettingLevel> levels, string default_level_id, string nosweat_default_level_id)
		{
			this.levels = levels;
			this.default_level_id = default_level_id;
			this.nosweat_default_level_id = nosweat_default_level_id;
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x0028B698 File Offset: 0x00289898
		public override SettingLevel GetLevel(string level_id)
		{
			for (int i = 0; i < this.levels.Count; i++)
			{
				if (this.levels[i].id == level_id)
				{
					return this.levels[i];
				}
			}
			for (int j = 0; j < this.levels.Count; j++)
			{
				if (this.levels[j].id == this.default_level_id)
				{
					return this.levels[j];
				}
			}
			global::Debug.LogError("Unable to find setting level for setting:" + base.id + " level: " + level_id);
			return null;
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x0028B73E File Offset: 0x0028993E
		public override List<SettingLevel> GetLevels()
		{
			return this.levels;
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x0028B748 File Offset: 0x00289948
		public string CycleSettingLevelID(string current_id, int direction)
		{
			string text = "";
			if (current_id == "")
			{
				current_id = this.levels[0].id;
			}
			for (int i = 0; i < this.levels.Count; i++)
			{
				if (this.levels[i].id == current_id)
				{
					int num = Mathf.Clamp(i + direction, 0, this.levels.Count - 1);
					text = this.levels[num].id;
					break;
				}
			}
			return text;
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x0028B7D8 File Offset: 0x002899D8
		public bool IsFirstLevel(string level_id)
		{
			return this.levels.FindIndex((SettingLevel l) => l.id == level_id) == 0;
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x0028B80C File Offset: 0x00289A0C
		public bool IsLastLevel(string level_id)
		{
			return this.levels.FindIndex((SettingLevel l) => l.id == level_id) == this.levels.Count - 1;
		}
	}
}
