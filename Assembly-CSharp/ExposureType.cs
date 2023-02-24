using System;
using System.Collections.Generic;

// Token: 0x0200082F RID: 2095
public class ExposureType
{
	// Token: 0x04002781 RID: 10113
	public string germ_id;

	// Token: 0x04002782 RID: 10114
	public string sickness_id;

	// Token: 0x04002783 RID: 10115
	public string infection_effect;

	// Token: 0x04002784 RID: 10116
	public int exposure_threshold;

	// Token: 0x04002785 RID: 10117
	public bool infect_immediately;

	// Token: 0x04002786 RID: 10118
	public List<string> required_traits;

	// Token: 0x04002787 RID: 10119
	public List<string> excluded_traits;

	// Token: 0x04002788 RID: 10120
	public List<string> excluded_effects;

	// Token: 0x04002789 RID: 10121
	public int base_resistance;
}
