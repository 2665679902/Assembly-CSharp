using System;
using System.Collections;
using STRINGS;

namespace Database
{
	// Token: 0x02000CC4 RID: 3268
	public class MonumentBuilt : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006630 RID: 26160 RVA: 0x00275852 File Offset: 0x00273A52
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.BUILT_MONUMENT;
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x0027585E File Offset: 0x00273A5E
		public override string Description()
		{
			return COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.BUILT_MONUMENT_DESCRIPTION;
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x0027586C File Offset: 0x00273A6C
		public override bool Success()
		{
			using (IEnumerator enumerator = Components.MonumentParts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((MonumentPart)enumerator.Current).IsMonumentCompleted())
					{
						Game.Instance.unlocks.Unlock("thriving", true);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x002758E0 File Offset: 0x00273AE0
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06006634 RID: 26164 RVA: 0x002758E2 File Offset: 0x00273AE2
		public override string GetProgress(bool complete)
		{
			return this.Name();
		}
	}
}
