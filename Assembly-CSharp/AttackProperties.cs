using System;
using System.Collections.Generic;

// Token: 0x02000699 RID: 1689
[Serializable]
public class AttackProperties
{
	// Token: 0x04001B26 RID: 6950
	public Weapon attacker;

	// Token: 0x04001B27 RID: 6951
	public AttackProperties.DamageType damageType;

	// Token: 0x04001B28 RID: 6952
	public AttackProperties.TargetType targetType;

	// Token: 0x04001B29 RID: 6953
	public float base_damage_min;

	// Token: 0x04001B2A RID: 6954
	public float base_damage_max;

	// Token: 0x04001B2B RID: 6955
	public int maxHits;

	// Token: 0x04001B2C RID: 6956
	public float aoe_radius = 2f;

	// Token: 0x04001B2D RID: 6957
	public List<AttackEffect> effects;

	// Token: 0x02001368 RID: 4968
	public enum DamageType
	{
		// Token: 0x04006075 RID: 24693
		Standard
	}

	// Token: 0x02001369 RID: 4969
	public enum TargetType
	{
		// Token: 0x04006077 RID: 24695
		Single,
		// Token: 0x04006078 RID: 24696
		AreaOfEffect
	}
}
