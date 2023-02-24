using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D6C RID: 3436
	public class Attributes
	{
		// Token: 0x06006913 RID: 26899 RVA: 0x0028CEC1 File Offset: 0x0028B0C1
		public IEnumerator<AttributeInstance> GetEnumerator()
		{
			return this.AttributeTable.GetEnumerator();
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06006914 RID: 26900 RVA: 0x0028CED3 File Offset: 0x0028B0D3
		public int Count
		{
			get
			{
				return this.AttributeTable.Count;
			}
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x0028CEE0 File Offset: 0x0028B0E0
		public Attributes(GameObject game_object)
		{
			this.gameObject = game_object;
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x0028CEFC File Offset: 0x0028B0FC
		public AttributeInstance Add(Attribute attribute)
		{
			AttributeInstance attributeInstance = this.Get(attribute.Id);
			if (attributeInstance == null)
			{
				attributeInstance = new AttributeInstance(this.gameObject, attribute);
				this.AttributeTable.Add(attributeInstance);
			}
			return attributeInstance;
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x0028CF34 File Offset: 0x0028B134
		public void Add(AttributeModifier modifier)
		{
			AttributeInstance attributeInstance = this.Get(modifier.AttributeId);
			if (attributeInstance != null)
			{
				attributeInstance.Add(modifier);
			}
		}

		// Token: 0x06006918 RID: 26904 RVA: 0x0028CF58 File Offset: 0x0028B158
		public void Remove(AttributeModifier modifier)
		{
			if (modifier == null)
			{
				return;
			}
			AttributeInstance attributeInstance = this.Get(modifier.AttributeId);
			if (attributeInstance != null)
			{
				attributeInstance.Remove(modifier);
			}
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x0028CF80 File Offset: 0x0028B180
		public float GetValuePercent(string attribute_id)
		{
			float num = 1f;
			AttributeInstance attributeInstance = this.Get(attribute_id);
			if (attributeInstance != null)
			{
				num = attributeInstance.GetTotalValue() / attributeInstance.GetBaseValue();
			}
			else
			{
				global::Debug.LogError("Could not find attribute " + attribute_id);
			}
			return num;
		}

		// Token: 0x0600691A RID: 26906 RVA: 0x0028CFC0 File Offset: 0x0028B1C0
		public AttributeInstance Get(string attribute_id)
		{
			for (int i = 0; i < this.AttributeTable.Count; i++)
			{
				if (this.AttributeTable[i].Id == attribute_id)
				{
					return this.AttributeTable[i];
				}
			}
			return null;
		}

		// Token: 0x0600691B RID: 26907 RVA: 0x0028D00A File Offset: 0x0028B20A
		public AttributeInstance Get(Attribute attribute)
		{
			return this.Get(attribute.Id);
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x0028D018 File Offset: 0x0028B218
		public float GetValue(string id)
		{
			float num = 0f;
			AttributeInstance attributeInstance = this.Get(id);
			if (attributeInstance != null)
			{
				num = attributeInstance.GetTotalValue();
			}
			else
			{
				global::Debug.LogError("Could not find attribute " + id);
			}
			return num;
		}

		// Token: 0x0600691D RID: 26909 RVA: 0x0028D050 File Offset: 0x0028B250
		public AttributeInstance GetProfession()
		{
			AttributeInstance attributeInstance = null;
			foreach (AttributeInstance attributeInstance2 in this)
			{
				if (attributeInstance2.modifier.IsProfession)
				{
					if (attributeInstance == null)
					{
						attributeInstance = attributeInstance2;
					}
					else if (attributeInstance.GetTotalValue() < attributeInstance2.GetTotalValue())
					{
						attributeInstance = attributeInstance2;
					}
				}
			}
			return attributeInstance;
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x0028D0B8 File Offset: 0x0028B2B8
		public string GetProfessionString(bool longform = true)
		{
			AttributeInstance profession = this.GetProfession();
			if ((int)profession.GetTotalValue() == 0)
			{
				return string.Format(longform ? UI.ATTRIBUTELEVEL : UI.ATTRIBUTELEVEL_SHORT, 0, DUPLICANTS.ATTRIBUTES.UNPROFESSIONAL_NAME);
			}
			return string.Format(longform ? UI.ATTRIBUTELEVEL : UI.ATTRIBUTELEVEL_SHORT, (int)profession.GetTotalValue(), profession.modifier.ProfessionName);
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x0028D12C File Offset: 0x0028B32C
		public string GetProfessionDescriptionString()
		{
			AttributeInstance profession = this.GetProfession();
			if ((int)profession.GetTotalValue() == 0)
			{
				return DUPLICANTS.ATTRIBUTES.UNPROFESSIONAL_DESC;
			}
			return string.Format(DUPLICANTS.ATTRIBUTES.PROFESSION_DESC, profession.modifier.Name);
		}

		// Token: 0x04004F05 RID: 20229
		public List<AttributeInstance> AttributeTable = new List<AttributeInstance>();

		// Token: 0x04004F06 RID: 20230
		public GameObject gameObject;
	}
}
