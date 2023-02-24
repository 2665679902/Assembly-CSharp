using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D6A RID: 3434
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/AttributeLevels")]
	public class AttributeLevels : KMonoBehaviour, ISaveLoadable
	{
		// Token: 0x060068F4 RID: 26868 RVA: 0x0028C8C0 File Offset: 0x0028AAC0
		public IEnumerator<AttributeLevel> GetEnumerator()
		{
			return this.levels.GetEnumerator();
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060068F5 RID: 26869 RVA: 0x0028C8D2 File Offset: 0x0028AAD2
		// (set) Token: 0x060068F6 RID: 26870 RVA: 0x0028C8DA File Offset: 0x0028AADA
		public AttributeLevels.LevelSaveLoad[] SaveLoadLevels
		{
			get
			{
				return this.saveLoadLevels;
			}
			set
			{
				this.saveLoadLevels = value;
			}
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x0028C8E4 File Offset: 0x0028AAE4
		protected override void OnPrefabInit()
		{
			foreach (AttributeInstance attributeInstance in this.GetAttributes())
			{
				if (attributeInstance.Attribute.IsTrainable)
				{
					AttributeLevel attributeLevel = new AttributeLevel(attributeInstance);
					this.levels.Add(attributeLevel);
					attributeLevel.Apply(this);
				}
			}
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x0028C954 File Offset: 0x0028AB54
		[OnSerializing]
		public void OnSerializing()
		{
			this.saveLoadLevels = new AttributeLevels.LevelSaveLoad[this.levels.Count];
			for (int i = 0; i < this.levels.Count; i++)
			{
				this.saveLoadLevels[i].attributeId = this.levels[i].attribute.Attribute.Id;
				this.saveLoadLevels[i].experience = this.levels[i].experience;
				this.saveLoadLevels[i].level = this.levels[i].level;
			}
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x0028CA00 File Offset: 0x0028AC00
		[OnDeserialized]
		public void OnDeserialized()
		{
			foreach (AttributeLevels.LevelSaveLoad levelSaveLoad in this.saveLoadLevels)
			{
				this.SetExperience(levelSaveLoad.attributeId, levelSaveLoad.experience);
				this.SetLevel(levelSaveLoad.attributeId, levelSaveLoad.level);
			}
		}

		// Token: 0x060068FA RID: 26874 RVA: 0x0028CA50 File Offset: 0x0028AC50
		public int GetLevel(Attribute attribute)
		{
			foreach (AttributeLevel attributeLevel in this.levels)
			{
				if (attribute == attributeLevel.attribute.Attribute)
				{
					return attributeLevel.GetLevel();
				}
			}
			return 1;
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x0028CAB8 File Offset: 0x0028ACB8
		public AttributeLevel GetAttributeLevel(string attribute_id)
		{
			foreach (AttributeLevel attributeLevel in this.levels)
			{
				if (attributeLevel.attribute.Attribute.Id == attribute_id)
				{
					return attributeLevel;
				}
			}
			return null;
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x0028CB24 File Offset: 0x0028AD24
		public bool AddExperience(string attribute_id, float time_spent, float multiplier)
		{
			AttributeLevel attributeLevel = this.GetAttributeLevel(attribute_id);
			if (attributeLevel == null)
			{
				global::Debug.LogWarning(attribute_id + " has no level.");
				return false;
			}
			time_spent *= multiplier;
			AttributeConverterInstance attributeConverterInstance = Db.Get().AttributeConverters.TrainingSpeed.Lookup(this);
			if (attributeConverterInstance != null)
			{
				float num = attributeConverterInstance.Evaluate();
				time_spent += time_spent * num;
			}
			bool flag = attributeLevel.AddExperience(this, time_spent);
			attributeLevel.Apply(this);
			return flag;
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x0028CB8C File Offset: 0x0028AD8C
		public void SetLevel(string attribute_id, int level)
		{
			AttributeLevel attributeLevel = this.GetAttributeLevel(attribute_id);
			if (attributeLevel != null)
			{
				attributeLevel.SetLevel(level);
				attributeLevel.Apply(this);
			}
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x0028CBB4 File Offset: 0x0028ADB4
		public void SetExperience(string attribute_id, float experience)
		{
			AttributeLevel attributeLevel = this.GetAttributeLevel(attribute_id);
			if (attributeLevel != null)
			{
				attributeLevel.SetExperience(experience);
				attributeLevel.Apply(this);
			}
		}

		// Token: 0x060068FF RID: 26879 RVA: 0x0028CBDA File Offset: 0x0028ADDA
		public float GetPercentComplete(string attribute_id)
		{
			return this.GetAttributeLevel(attribute_id).GetPercentComplete();
		}

		// Token: 0x06006900 RID: 26880 RVA: 0x0028CBE8 File Offset: 0x0028ADE8
		public int GetMaxLevel()
		{
			int num = 0;
			foreach (AttributeLevel attributeLevel in this)
			{
				if (attributeLevel.GetLevel() > num)
				{
					num = attributeLevel.GetLevel();
				}
			}
			return num;
		}

		// Token: 0x04004EFC RID: 20220
		private List<AttributeLevel> levels = new List<AttributeLevel>();

		// Token: 0x04004EFD RID: 20221
		[Serialize]
		private AttributeLevels.LevelSaveLoad[] saveLoadLevels = new AttributeLevels.LevelSaveLoad[0];

		// Token: 0x02001E44 RID: 7748
		[Serializable]
		public struct LevelSaveLoad
		{
			// Token: 0x04008834 RID: 34868
			public string attributeId;

			// Token: 0x04008835 RID: 34869
			public float experience;

			// Token: 0x04008836 RID: 34870
			public int level;
		}
	}
}
