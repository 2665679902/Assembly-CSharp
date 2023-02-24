using System;
using System.Collections.Generic;

namespace Klei.CustomSettings
{
	// Token: 0x02000D5F RID: 3423
	public class ToggleSettingConfig : SettingConfig
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600689A RID: 26778 RVA: 0x0028B84C File Offset: 0x00289A4C
		// (set) Token: 0x0600689B RID: 26779 RVA: 0x0028B854 File Offset: 0x00289A54
		public SettingLevel on_level { get; private set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600689C RID: 26780 RVA: 0x0028B85D File Offset: 0x00289A5D
		// (set) Token: 0x0600689D RID: 26781 RVA: 0x0028B865 File Offset: 0x00289A65
		public SettingLevel off_level { get; private set; }

		// Token: 0x0600689E RID: 26782 RVA: 0x0028B870 File Offset: 0x00289A70
		public ToggleSettingConfig(string id, string label, string tooltip, SettingLevel off_level, SettingLevel on_level, string default_level_id, string nosweat_default_level_id, long coordinate_dimension = -1L, long coordinate_dimension_width = -1L, bool debug_only = false, bool triggers_custom_game = true, string required_content = "", string missing_content_default = "")
			: base(id, label, tooltip, default_level_id, nosweat_default_level_id, coordinate_dimension, coordinate_dimension_width, debug_only, triggers_custom_game, required_content, missing_content_default, false)
		{
			this.off_level = off_level;
			this.on_level = on_level;
		}

		// Token: 0x0600689F RID: 26783 RVA: 0x0028B8A8 File Offset: 0x00289AA8
		public override SettingLevel GetLevel(string level_id)
		{
			if (this.on_level.id == level_id)
			{
				return this.on_level;
			}
			if (this.off_level.id == level_id)
			{
				return this.off_level;
			}
			if (this.default_level_id == this.on_level.id)
			{
				Debug.LogWarning(string.Concat(new string[] { "Unable to find level for setting:", base.id, "(", level_id, ") Using default level." }));
				return this.on_level;
			}
			if (this.default_level_id == this.off_level.id)
			{
				Debug.LogWarning(string.Concat(new string[] { "Unable to find level for setting:", base.id, "(", level_id, ") Using default level." }));
				return this.off_level;
			}
			Debug.LogError("Unable to find setting level for setting:" + base.id + " level: " + level_id);
			return null;
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x0028B9AD File Offset: 0x00289BAD
		public override List<SettingLevel> GetLevels()
		{
			return new List<SettingLevel> { this.off_level, this.on_level };
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x0028B9CC File Offset: 0x00289BCC
		public string ToggleSettingLevelID(string current_id)
		{
			if (this.on_level.id == current_id)
			{
				return this.off_level.id;
			}
			return this.on_level.id;
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x0028B9F8 File Offset: 0x00289BF8
		public bool IsOnLevel(string level_id)
		{
			return level_id == this.on_level.id;
		}
	}
}
