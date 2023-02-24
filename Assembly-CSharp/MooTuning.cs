using System;
using TUNING;

// Token: 0x02000095 RID: 149
public static class MooTuning
{
	// Token: 0x040001A4 RID: 420
	public static float STANDARD_CALORIES_PER_CYCLE = 200000f;

	// Token: 0x040001A5 RID: 421
	public static float STANDARD_STARVE_CYCLES = 6f;

	// Token: 0x040001A6 RID: 422
	public static float STANDARD_STOMACH_SIZE = MooTuning.STANDARD_CALORIES_PER_CYCLE * MooTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001A7 RID: 423
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER4;

	// Token: 0x040001A8 RID: 424
	public static float EGG_MASS = 0.5f;
}
