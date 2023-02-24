using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x02000088 RID: 136
public static class CrabTuning
{
	// Token: 0x04000168 RID: 360
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabEgg".ToTag(),
			weight = 0.97f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabWoodEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabFreshWaterEgg".ToTag(),
			weight = 0.01f
		}
	};

	// Token: 0x04000169 RID: 361
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_WOOD = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabWoodEgg".ToTag(),
			weight = 0.65f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabFreshWaterEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400016A RID: 362
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_FRESH = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabWoodEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabFreshWaterEgg".ToTag(),
			weight = 0.65f
		}
	};

	// Token: 0x0400016B RID: 363
	public static float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x0400016C RID: 364
	public static float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x0400016D RID: 365
	public static float STANDARD_STOMACH_SIZE = CrabTuning.STANDARD_CALORIES_PER_CYCLE * CrabTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400016E RID: 366
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x0400016F RID: 367
	public static float EGG_MASS = 2f;
}
