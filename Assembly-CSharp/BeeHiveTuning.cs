using System;

// Token: 0x02000086 RID: 134
public static class BeeHiveTuning
{
	// Token: 0x04000158 RID: 344
	public static float ORE_DELIVERY_AMOUNT = 1f;

	// Token: 0x04000159 RID: 345
	public static float KG_ORE_EATEN_PER_CYCLE = BeeHiveTuning.ORE_DELIVERY_AMOUNT * 10f;

	// Token: 0x0400015A RID: 346
	public static float STANDARD_CALORIES_PER_CYCLE = 1500000f;

	// Token: 0x0400015B RID: 347
	public static float STANDARD_STARVE_CYCLES = 30f;

	// Token: 0x0400015C RID: 348
	public static float STANDARD_STOMACH_SIZE = BeeHiveTuning.STANDARD_CALORIES_PER_CYCLE * BeeHiveTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400015D RID: 349
	public static float CALORIES_PER_KG_OF_ORE = BeeHiveTuning.STANDARD_CALORIES_PER_CYCLE / BeeHiveTuning.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x0400015E RID: 350
	public static float POOP_CONVERSTION_RATE = 0.9f;

	// Token: 0x0400015F RID: 351
	public static Tag CONSUMED_ORE = SimHashes.UraniumOre.CreateTag();

	// Token: 0x04000160 RID: 352
	public static Tag PRODUCED_ORE = SimHashes.EnrichedUranium.CreateTag();

	// Token: 0x04000161 RID: 353
	public static float HIVE_GROWTH_TIME = 2f;

	// Token: 0x04000162 RID: 354
	public static float WASTE_DROPPED_ON_DEATH = 5f;

	// Token: 0x04000163 RID: 355
	public static int GERMS_DROPPED_ON_DEATH = 10000;
}
