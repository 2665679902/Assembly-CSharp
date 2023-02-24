using System;
using Klei.AI;

// Token: 0x02000320 RID: 800
[Serializable]
public struct SpiceInstance
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000FED RID: 4077 RVA: 0x0005647F File Offset: 0x0005467F
	public AttributeModifier CalorieModifier
	{
		get
		{
			return SpiceGrinder.SettingOptions[this.Id].Spice.CalorieModifier;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0005649B File Offset: 0x0005469B
	public AttributeModifier FoodModifier
	{
		get
		{
			return SpiceGrinder.SettingOptions[this.Id].Spice.FoodModifier;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x06000FEF RID: 4079 RVA: 0x000564B7 File Offset: 0x000546B7
	public Effect StatBonus
	{
		get
		{
			return SpiceGrinder.SettingOptions[this.Id].StatBonus;
		}
	}

	// Token: 0x040008B5 RID: 2229
	public Tag Id;

	// Token: 0x040008B6 RID: 2230
	public float TotalKG;
}
