using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;

namespace Database
{
	// Token: 0x02000CDB RID: 3291
	public class EatXKCalProducedByY : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066A2 RID: 26274 RVA: 0x00276EB3 File Offset: 0x002750B3
		public EatXKCalProducedByY(int numCalories, List<Tag> foodProducers)
		{
			this.numCalories = numCalories;
			this.foodProducers = foodProducers;
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x00276ECC File Offset: 0x002750CC
		public override bool Success()
		{
			List<string> list = new List<string>();
			foreach (ComplexRecipe complexRecipe in ComplexRecipeManager.Get().recipes)
			{
				foreach (Tag tag in this.foodProducers)
				{
					using (List<Tag>.Enumerator enumerator3 = complexRecipe.fabricators.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							if (enumerator3.Current == tag)
							{
								list.Add(complexRecipe.FirstResult.ToString());
							}
						}
					}
				}
			}
			return RationTracker.Get().GetCaloiresConsumedByFood(list.Distinct<string>().ToList<string>()) / 1000f > (float)this.numCalories;
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x00276FE8 File Offset: 0x002751E8
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			this.foodProducers = new List<Tag>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.foodProducers.Add(new Tag(text));
			}
			this.numCalories = reader.ReadInt32();
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x00277038 File Offset: 0x00275238
		public override string GetProgress(bool complete)
		{
			string text = "";
			for (int i = 0; i < this.foodProducers.Count; i++)
			{
				if (i != 0)
				{
					text += COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.PREPARED_SEPARATOR;
				}
				BuildingDef buildingDef = Assets.GetBuildingDef(this.foodProducers[i].Name);
				if (buildingDef != null)
				{
					text += buildingDef.Name;
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CONSUME_ITEM, text);
		}

		// Token: 0x04004ADD RID: 19165
		private int numCalories;

		// Token: 0x04004ADE RID: 19166
		private List<Tag> foodProducers;
	}
}
