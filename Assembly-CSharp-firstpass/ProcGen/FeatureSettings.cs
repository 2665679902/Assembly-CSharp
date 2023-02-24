using System;
using System.Collections.Generic;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004D8 RID: 1240
	[Serializable]
	public class FeatureSettings
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000750C9 File Offset: 0x000732C9
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000750D1 File Offset: 0x000732D1
		[StringEnumConverter]
		public Room.Shape shape { get; private set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000750DA File Offset: 0x000732DA
		// (set) Token: 0x06003543 RID: 13635 RVA: 0x000750E2 File Offset: 0x000732E2
		public List<int> borders { get; private set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000750EB File Offset: 0x000732EB
		// (set) Token: 0x06003545 RID: 13637 RVA: 0x000750F3 File Offset: 0x000732F3
		public MinMax blobSize { get; private set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x000750FC File Offset: 0x000732FC
		// (set) Token: 0x06003547 RID: 13639 RVA: 0x00075104 File Offset: 0x00073304
		public string forceBiome { get; private set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x0007510D File Offset: 0x0007330D
		// (set) Token: 0x06003549 RID: 13641 RVA: 0x00075115 File Offset: 0x00073315
		public List<string> biomeTags { get; private set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x0007511E File Offset: 0x0007331E
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x00075126 File Offset: 0x00073326
		public List<MobReference> internalMobs { get; private set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x0007512F File Offset: 0x0007332F
		// (set) Token: 0x0600354D RID: 13645 RVA: 0x00075137 File Offset: 0x00073337
		public List<string> tags { get; private set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x00075140 File Offset: 0x00073340
		// (set) Token: 0x0600354F RID: 13647 RVA: 0x00075148 File Offset: 0x00073348
		public Dictionary<string, ElementChoiceGroup<WeightedSimHash>> ElementChoiceGroups { get; private set; }

		// Token: 0x06003550 RID: 13648 RVA: 0x00075151 File Offset: 0x00073351
		public FeatureSettings()
		{
			this.ElementChoiceGroups = new Dictionary<string, ElementChoiceGroup<WeightedSimHash>>();
			this.borders = new List<int>();
			this.tags = new List<string>();
			this.internalMobs = new List<MobReference>();
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x00075185 File Offset: 0x00073385
		public bool HasGroup(string item)
		{
			return this.ElementChoiceGroups.ContainsKey(item);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x00075193 File Offset: 0x00073393
		public WeightedSimHash GetOneWeightedSimHash(string item, SeededRandom rnd)
		{
			if (this.ElementChoiceGroups.ContainsKey(item))
			{
				return WeightedRandom.Choose<WeightedSimHash>(this.ElementChoiceGroups[item].choices, rnd);
			}
			Debug.LogError("Couldnt get SimHash [" + item + "]");
			return null;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000751D4 File Offset: 0x000733D4
		public WeightedSimHash GetWeightedSimHashAtChoice(string item, float percentage)
		{
			if (this.ElementChoiceGroups.ContainsKey(item))
			{
				List<WeightedSimHash> choices = this.ElementChoiceGroups[item].choices;
				if (choices.Count > 0)
				{
					float num = 0f;
					for (int i = 0; i < choices.Count; i++)
					{
						num += choices[i].weight;
					}
					float num2 = 0f;
					for (int j = 0; j < choices.Count; j++)
					{
						num2 += choices[j].weight;
						if (num2 > percentage)
						{
							return choices[j];
						}
					}
					return choices[choices.Count - 1];
				}
			}
			Debug.LogError("Couldnt get SimHash [" + item + "]");
			return null;
		}
	}
}
