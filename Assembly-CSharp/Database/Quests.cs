using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000CA9 RID: 3241
	public class Quests : ResourceSet<Quest>
	{
		// Token: 0x060065D1 RID: 26065 RVA: 0x0026F2FC File Offset: 0x0026D4FC
		public Quests(ResourceSet parent)
			: base("Quests", parent)
		{
			this.LonelyMinionGreetingQuest = base.Add(new Quest("KnockQuest", new QuestCriteria[]
			{
				new QuestCriteria("Neighbor", null, 1, null, QuestCriteria.BehaviorFlags.None)
			}));
			this.LonelyMinionFoodQuest = base.Add(new Quest("FoodQuest", new QuestCriteria[]
			{
				new QuestCriteria_GreaterOrEqual("FoodQuality", new float[] { 4f }, 3, new HashSet<Tag> { GameTags.Edible }, QuestCriteria.BehaviorFlags.UniqueItems)
			}));
			this.LonelyMinionPowerQuest = base.Add(new Quest("PluggedIn", new QuestCriteria[]
			{
				new QuestCriteria_GreaterOrEqual("SuppliedPower", new float[] { 3000f }, 1, null, QuestCriteria.BehaviorFlags.TrackValues)
			}));
			this.LonelyMinionDecorQuest = base.Add(new Quest("HighDecor", new QuestCriteria[]
			{
				new QuestCriteria_GreaterOrEqual("Decor", new float[] { 120f }, 1, null, (QuestCriteria.BehaviorFlags)6)
			}));
		}

		// Token: 0x040049DA RID: 18906
		public Quest LonelyMinionGreetingQuest;

		// Token: 0x040049DB RID: 18907
		public Quest LonelyMinionFoodQuest;

		// Token: 0x040049DC RID: 18908
		public Quest LonelyMinionPowerQuest;

		// Token: 0x040049DD RID: 18909
		public Quest LonelyMinionDecorQuest;
	}
}
