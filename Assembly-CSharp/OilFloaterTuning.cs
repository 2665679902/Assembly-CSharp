using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x02000097 RID: 151
public static class OilFloaterTuning
{
	// Token: 0x040001A9 RID: 425
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterHighTempEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterDecorEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001AA RID: 426
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_HIGHTEMP = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterHighTempEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterDecorEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001AB RID: 427
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_DECOR = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterHighTempEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "OilfloaterDecorEgg".ToTag(),
			weight = 0.66f
		}
	};

	// Token: 0x040001AC RID: 428
	public static float STANDARD_CALORIES_PER_CYCLE = 120000f;

	// Token: 0x040001AD RID: 429
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x040001AE RID: 430
	public static float STANDARD_STOMACH_SIZE = OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE * OilFloaterTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001AF RID: 431
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x040001B0 RID: 432
	public static float EGG_MASS = 2f;
}
