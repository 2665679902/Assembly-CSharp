using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CC9 RID: 3273
	public class MinimumMorale : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x0600664B RID: 26187 RVA: 0x00275B83 File Offset: 0x00273D83
		public override string Name()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_MORALE, this.minimumMorale);
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x00275B9F File Offset: 0x00273D9F
		public override string Description()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_MORALE_DESCRIPTION, this.minimumMorale);
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x00275BBB File Offset: 0x00273DBB
		public MinimumMorale(int minimumMorale = 16)
		{
			this.minimumMorale = minimumMorale;
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x00275BCC File Offset: 0x00273DCC
		public override bool Success()
		{
			bool flag = true;
			foreach (object obj in Components.MinionAssignablesProxy)
			{
				GameObject targetGameObject = ((MinionAssignablesProxy)obj).GetTargetGameObject();
				if (targetGameObject != null && !targetGameObject.HasTag(GameTags.Dead))
				{
					AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(targetGameObject.GetComponent<MinionModifiers>());
					flag = attributeInstance != null && attributeInstance.GetTotalValue() >= (float)this.minimumMorale && flag;
				}
			}
			return flag;
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x00275C74 File Offset: 0x00273E74
		public void Deserialize(IReader reader)
		{
			this.minimumMorale = reader.ReadInt32();
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x00275C82 File Offset: 0x00273E82
		public override string GetProgress(bool complete)
		{
			return this.Description();
		}

		// Token: 0x04004AC5 RID: 19141
		public int minimumMorale;
	}
}
