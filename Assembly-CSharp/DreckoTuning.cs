using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x0200008C RID: 140
public static class DreckoTuning
{
	// Token: 0x0400017C RID: 380
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoPlasticEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400017D RID: 381
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_PLASTIC = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoEgg".ToTag(),
			weight = 0.35f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoPlasticEgg".ToTag(),
			weight = 0.65f
		}
	};

	// Token: 0x0400017E RID: 382
	public static float STANDARD_CALORIES_PER_CYCLE = 2000000f;

	// Token: 0x0400017F RID: 383
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x04000180 RID: 384
	public static float STANDARD_STOMACH_SIZE = DreckoTuning.STANDARD_CALORIES_PER_CYCLE * DreckoTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x04000181 RID: 385
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x04000182 RID: 386
	public static float EGG_MASS = 2f;
}
