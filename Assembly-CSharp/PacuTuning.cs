using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x0200009A RID: 154
public static class PacuTuning
{
	// Token: 0x040001B5 RID: 437
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuTropicalEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuCleanerEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001B6 RID: 438
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_TROPICAL = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuTropicalEgg".ToTag(),
			weight = 0.65f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuCleanerEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001B7 RID: 439
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_CLEANER = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuCleanerEgg".ToTag(),
			weight = 0.65f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuTropicalEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001B8 RID: 440
	public static float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x040001B9 RID: 441
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x040001BA RID: 442
	public static float STANDARD_STOMACH_SIZE = PacuTuning.STANDARD_CALORIES_PER_CYCLE * PacuTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001BB RID: 443
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER2;

	// Token: 0x040001BC RID: 444
	public static float EGG_MASS = 4f;
}
