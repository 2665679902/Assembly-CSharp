using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x02000093 RID: 147
public static class MoleTuning
{
	// Token: 0x04000199 RID: 409
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleDelicacyEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400019A RID: 410
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_DELICACY = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleDelicacyEgg".ToTag(),
			weight = 0.65f
		}
	};

	// Token: 0x0400019B RID: 411
	public static float STANDARD_CALORIES_PER_CYCLE = 4800000f;

	// Token: 0x0400019C RID: 412
	public static float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x0400019D RID: 413
	public static float STANDARD_STOMACH_SIZE = MoleTuning.STANDARD_CALORIES_PER_CYCLE * MoleTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400019E RID: 414
	public static float DELICACY_STOMACH_SIZE = MoleTuning.STANDARD_STOMACH_SIZE / 2f;

	// Token: 0x0400019F RID: 415
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER2;

	// Token: 0x040001A0 RID: 416
	public static float EGG_MASS = 2f;

	// Token: 0x040001A1 RID: 417
	public static int DEPTH_TO_HIDE = 2;

	// Token: 0x040001A2 RID: 418
	public static HashedString[] GINGER_SYMBOL_NAMES = new HashedString[] { "del_ginger", "del_ginger1", "del_ginger2", "del_ginger3", "del_ginger4", "del_ginger5" };
}
