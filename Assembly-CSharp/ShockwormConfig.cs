using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class ShockwormConfig : IEntityConfig
{
	// Token: 0x06000578 RID: 1400 RVA: 0x00024A1A File Offset: 0x00022C1A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00024A24 File Offset: 0x00022C24
	public GameObject CreatePrefab()
	{
		string text = "ShockWorm";
		string text2 = STRINGS.CREATURES.SPECIES.SHOCKWORM.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.SHOCKWORM.DESC;
		float num = 50f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("shockworm_kanim"), "idle", Grid.SceneLayer.Creatures, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		FactionManager.FactionID factionID = FactionManager.FactionID.Hostile;
		string text4 = null;
		string text5 = "FlyerNavGrid1x2";
		NavType navType = NavType.Hover;
		int num2 = 32;
		float num3 = 2f;
		string text6 = "Meat";
		int num4 = 3;
		bool flag = true;
		bool flag2 = true;
		float freezing_ = TUNING.CREATURES.TEMPERATURE.FREEZING_2;
		EntityTemplates.ExtendEntityToBasicCreature(gameObject, factionID, text4, text5, navType, num2, num3, text6, num4, flag, flag2, TUNING.CREATURES.TEMPERATURE.FREEZING_1, TUNING.CREATURES.TEMPERATURE.HOT_1, freezing_, TUNING.CREATURES.TEMPERATURE.HOT_2);
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddWeapon(3f, 6f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.AreaOfEffect, 10, 4f).AddEffect("WasAttacked", 1f);
		SoundEventVolumeCache.instance.AddVolume("shockworm_kanim", "Shockworm_attack_arc", NOISE_POLLUTION.CREATURES.TIER6);
		return gameObject;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00024B03 File Offset: 0x00022D03
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00024B05 File Offset: 0x00022D05
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003B2 RID: 946
	public const string ID = "ShockWorm";
}
