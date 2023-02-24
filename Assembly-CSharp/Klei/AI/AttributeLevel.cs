using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D69 RID: 3433
	public class AttributeLevel
	{
		// Token: 0x060068EA RID: 26858 RVA: 0x0028C5FC File Offset: 0x0028A7FC
		public AttributeLevel(AttributeInstance attribute)
		{
			this.notification = new Notification(MISC.NOTIFICATIONS.LEVELUP.NAME, NotificationType.Good, new Func<List<Notification>, object, string>(AttributeLevel.OnLevelUpTooltip), null, true, 0f, null, null, null, true, false, false);
			this.attribute = attribute;
		}

		// Token: 0x060068EB RID: 26859 RVA: 0x0028C645 File Offset: 0x0028A845
		public int GetLevel()
		{
			return this.level;
		}

		// Token: 0x060068EC RID: 26860 RVA: 0x0028C650 File Offset: 0x0028A850
		public void Apply(AttributeLevels levels)
		{
			Attributes attributes = levels.GetAttributes();
			if (this.modifier != null)
			{
				attributes.Remove(this.modifier);
				this.modifier = null;
			}
			this.modifier = new AttributeModifier(this.attribute.Id, (float)this.GetLevel(), DUPLICANTS.MODIFIERS.SKILLLEVEL.NAME, false, false, true);
			attributes.Add(this.modifier);
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x0028C6B5 File Offset: 0x0028A8B5
		public void SetExperience(float experience)
		{
			this.experience = experience;
		}

		// Token: 0x060068EE RID: 26862 RVA: 0x0028C6BE File Offset: 0x0028A8BE
		public void SetLevel(int level)
		{
			this.level = level;
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x0028C6C8 File Offset: 0x0028A8C8
		public float GetExperienceForNextLevel()
		{
			float num = Mathf.Pow((float)this.level / (float)DUPLICANTSTATS.ATTRIBUTE_LEVELING.MAX_GAINED_ATTRIBUTE_LEVEL, DUPLICANTSTATS.ATTRIBUTE_LEVELING.EXPERIENCE_LEVEL_POWER) * (float)DUPLICANTSTATS.ATTRIBUTE_LEVELING.TARGET_MAX_LEVEL_CYCLE * 600f;
			return Mathf.Pow(((float)this.level + 1f) / (float)DUPLICANTSTATS.ATTRIBUTE_LEVELING.MAX_GAINED_ATTRIBUTE_LEVEL, DUPLICANTSTATS.ATTRIBUTE_LEVELING.EXPERIENCE_LEVEL_POWER) * (float)DUPLICANTSTATS.ATTRIBUTE_LEVELING.TARGET_MAX_LEVEL_CYCLE * 600f - num;
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x0028C728 File Offset: 0x0028A928
		public float GetPercentComplete()
		{
			return this.experience / this.GetExperienceForNextLevel();
		}

		// Token: 0x060068F1 RID: 26865 RVA: 0x0028C738 File Offset: 0x0028A938
		public void LevelUp(AttributeLevels levels)
		{
			this.level++;
			this.experience = 0f;
			this.Apply(levels);
			this.experience = 0f;
			if (PopFXManager.Instance != null)
			{
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, this.attribute.modifier.Name, levels.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
			}
			levels.GetComponent<Notifier>().Add(this.notification, string.Format(MISC.NOTIFICATIONS.LEVELUP.SUFFIX, this.attribute.modifier.Name, this.level));
			StateMachine.Instance instance = new UpgradeFX.Instance(levels.GetComponent<KMonoBehaviour>(), new Vector3(0f, 0f, -0.1f));
			ReportManager.Instance.ReportValue(ReportManager.ReportType.LevelUp, 1f, levels.GetProperName(), null);
			instance.StartSM();
			levels.Trigger(-110704193, this.attribute.Id);
		}

		// Token: 0x060068F2 RID: 26866 RVA: 0x0028C850 File Offset: 0x0028AA50
		public bool AddExperience(AttributeLevels levels, float experience)
		{
			if (this.level >= DUPLICANTSTATS.ATTRIBUTE_LEVELING.MAX_GAINED_ATTRIBUTE_LEVEL)
			{
				return false;
			}
			this.experience += experience;
			this.experience = Mathf.Max(0f, this.experience);
			if (this.experience >= this.GetExperienceForNextLevel())
			{
				this.LevelUp(levels);
				return true;
			}
			return false;
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x0028C8A8 File Offset: 0x0028AAA8
		private static string OnLevelUpTooltip(List<Notification> notifications, object data)
		{
			return MISC.NOTIFICATIONS.LEVELUP.TOOLTIP + notifications.ReduceMessages(false);
		}

		// Token: 0x04004EF7 RID: 20215
		public float experience;

		// Token: 0x04004EF8 RID: 20216
		public int level;

		// Token: 0x04004EF9 RID: 20217
		public AttributeInstance attribute;

		// Token: 0x04004EFA RID: 20218
		public AttributeModifier modifier;

		// Token: 0x04004EFB RID: 20219
		public Notification notification;
	}
}
