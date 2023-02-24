using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x0200009C RID: 156
public static class PuftTuning
{
	// Token: 0x040001C0 RID: 448
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftOxyliteEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftBleachstoneEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001C1 RID: 449
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_ALPHA = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001C2 RID: 450
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_OXYLITE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.31f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftOxyliteEgg".ToTag(),
			weight = 0.67f
		}
	};

	// Token: 0x040001C3 RID: 451
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BLEACHSTONE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.31f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftBleachstoneEgg".ToTag(),
			weight = 0.67f
		}
	};

	// Token: 0x040001C4 RID: 452
	public static float STANDARD_CALORIES_PER_CYCLE = 200000f;

	// Token: 0x040001C5 RID: 453
	public static float STANDARD_STARVE_CYCLES = 6f;

	// Token: 0x040001C6 RID: 454
	public static float STANDARD_STOMACH_SIZE = PuftTuning.STANDARD_CALORIES_PER_CYCLE * PuftTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001C7 RID: 455
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER4;

	// Token: 0x040001C8 RID: 456
	public static float EGG_MASS = 0.5f;
}
