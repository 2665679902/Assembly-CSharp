using System;
using System.Collections.Generic;

// Token: 0x020000A0 RID: 160
public static class StaterpillarTuning
{
	// Token: 0x040001D1 RID: 465
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarGasEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarLiquidEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001D2 RID: 466
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_GAS = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarGasEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarLiquidEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001D3 RID: 467
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_LIQUID = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarGasEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarLiquidEgg".ToTag(),
			weight = 0.66f
		}
	};

	// Token: 0x040001D4 RID: 468
	public static float STANDARD_CALORIES_PER_CYCLE = 2000000f;

	// Token: 0x040001D5 RID: 469
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x040001D6 RID: 470
	public static float STANDARD_STOMACH_SIZE = StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE * StaterpillarTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001D7 RID: 471
	public static float POOP_CONVERSTION_RATE = 0.05f;

	// Token: 0x040001D8 RID: 472
	public static float EGG_MASS = 2f;
}
