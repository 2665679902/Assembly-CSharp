using System;
using UnityEngine;

// Token: 0x020006A0 RID: 1696
public static class WeaponExtensions
{
	// Token: 0x06002E05 RID: 11781 RVA: 0x000F1FEC File Offset: 0x000F01EC
	public static Weapon AddWeapon(this GameObject prefab, float base_damage_min, float base_damage_max, AttackProperties.DamageType attackType = AttackProperties.DamageType.Standard, AttackProperties.TargetType targetType = AttackProperties.TargetType.Single, int maxHits = 1, float aoeRadius = 0f)
	{
		Weapon weapon = prefab.AddOrGet<Weapon>();
		weapon.Configure(base_damage_min, base_damage_max, attackType, targetType, maxHits, aoeRadius);
		return weapon;
	}
}
