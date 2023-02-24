using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000CD6 RID: 3286
	public class ProduceXEngeryWithoutUsingYList : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x0600668C RID: 26252 RVA: 0x002769C0 File Offset: 0x00274BC0
		public ProduceXEngeryWithoutUsingYList(float amountToProduce, List<Tag> disallowedBuildings)
		{
			this.disallowedBuildings = disallowedBuildings;
			this.amountToProduce = amountToProduce;
			this.usedDisallowedBuilding = false;
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x002769E8 File Offset: 0x00274BE8
		public override bool Success()
		{
			float num = 0f;
			foreach (KeyValuePair<Tag, float> keyValuePair in Game.Instance.savedInfo.powerCreatedbyGeneratorType)
			{
				if (!this.disallowedBuildings.Contains(keyValuePair.Key))
				{
					num += keyValuePair.Value;
				}
			}
			return num / 1000f > this.amountToProduce;
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x00276A70 File Offset: 0x00274C70
		public override bool Fail()
		{
			foreach (Tag tag in this.disallowedBuildings)
			{
				if (Game.Instance.savedInfo.powerCreatedbyGeneratorType.ContainsKey(tag))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x00276ADC File Offset: 0x00274CDC
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			this.disallowedBuildings = new List<Tag>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.disallowedBuildings.Add(new Tag(text));
			}
			this.amountProduced = (float)reader.ReadDouble();
			this.amountToProduce = (float)reader.ReadDouble();
			this.usedDisallowedBuilding = reader.ReadByte() > 0;
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x00276B4C File Offset: 0x00274D4C
		public float GetProductionAmount(bool complete)
		{
			float num = 0f;
			foreach (KeyValuePair<Tag, float> keyValuePair in Game.Instance.savedInfo.powerCreatedbyGeneratorType)
			{
				if (!this.disallowedBuildings.Contains(keyValuePair.Key))
				{
					num += keyValuePair.Value;
				}
			}
			if (!complete)
			{
				return num;
			}
			return this.amountToProduce;
		}

		// Token: 0x04004AD5 RID: 19157
		public List<Tag> disallowedBuildings = new List<Tag>();

		// Token: 0x04004AD6 RID: 19158
		public float amountToProduce;

		// Token: 0x04004AD7 RID: 19159
		private float amountProduced;

		// Token: 0x04004AD8 RID: 19160
		private bool usedDisallowedBuilding;
	}
}
