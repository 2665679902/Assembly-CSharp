using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x0200009E RID: 158
public static class SquirrelTuning
{
	// Token: 0x040001C9 RID: 457
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "SquirrelEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "SquirrelHugEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001CA RID: 458
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_HUG = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "SquirrelEgg".ToTag(),
			weight = 0.35f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "SquirrelHugEgg".ToTag(),
			weight = 0.65f
		}
	};

	// Token: 0x040001CB RID: 459
	public static float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x040001CC RID: 460
	public static float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x040001CD RID: 461
	public static float STANDARD_STOMACH_SIZE = SquirrelTuning.STANDARD_CALORIES_PER_CYCLE * SquirrelTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001CE RID: 462
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x040001CF RID: 463
	public static int PEN_SIZE_PER_CREATURE_HUG = CREATURES.SPACE_REQUIREMENTS.TIER1;

	// Token: 0x040001D0 RID: 464
	public static float EGG_MASS = 2f;
}
