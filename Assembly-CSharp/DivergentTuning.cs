using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x0200008A RID: 138
public static class DivergentTuning
{
	// Token: 0x04000170 RID: 368
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BEETLE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "DivergentBeetleEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "DivergentWormEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000171 RID: 369
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_WORM = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "DivergentBeetleEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "DivergentWormEgg".ToTag(),
			weight = 0.67f
		}
	};

	// Token: 0x04000172 RID: 370
	public static int TIMES_TENDED_PER_CYCLE_FOR_EVOLUTION = 2;

	// Token: 0x04000173 RID: 371
	public static float STANDARD_CALORIES_PER_CYCLE = 700000f;

	// Token: 0x04000174 RID: 372
	public static float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x04000175 RID: 373
	public static float STANDARD_STOMACH_SIZE = DivergentTuning.STANDARD_CALORIES_PER_CYCLE * DivergentTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x04000176 RID: 374
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x04000177 RID: 375
	public static int PEN_SIZE_PER_CREATURE_WORM = CREATURES.SPACE_REQUIREMENTS.TIER4;

	// Token: 0x04000178 RID: 376
	public static float EGG_MASS = 2f;
}
