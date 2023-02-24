using System;
using System.Collections.Generic;

namespace Klei.CustomSettings
{
	// Token: 0x02000D60 RID: 3424
	public class SeedSettingConfig : SettingConfig
	{
		// Token: 0x060068A3 RID: 26787 RVA: 0x0028BA0C File Offset: 0x00289C0C
		public SeedSettingConfig(string id, string label, string tooltip, bool debug_only = false, bool triggers_custom_game = true)
			: base(id, label, tooltip, "", "", -1L, -1L, debug_only, triggers_custom_game, "", "", false)
		{
		}

		// Token: 0x060068A4 RID: 26788 RVA: 0x0028BA3F File Offset: 0x00289C3F
		public override SettingLevel GetLevel(string level_id)
		{
			return new SettingLevel(level_id, level_id, level_id, 0L, null);
		}

		// Token: 0x060068A5 RID: 26789 RVA: 0x0028BA4C File Offset: 0x00289C4C
		public override List<SettingLevel> GetLevels()
		{
			return new List<SettingLevel>();
		}
	}
}
