using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class SapTreeConfig : IEntityConfig
{
	// Token: 0x06000687 RID: 1671 RVA: 0x0002A618 File Offset: 0x00028818
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0002A620 File Offset: 0x00028820
	public GameObject CreatePrefab()
	{
		string text = "SapTree";
		string text2 = STRINGS.CREATURES.SPECIES.SAPTREE.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.SAPTREE.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = SapTreeConfig.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_sap_tree_kanim"), "idle", Grid.SceneLayer.BuildingFront, 5, 5, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, new List<Tag> { GameTags.Decoration }, 293f);
		SapTree.Def def = gameObject.AddOrGetDef<SapTree.Def>();
		def.foodSenseArea = new Vector2I(5, 1);
		def.massEatRate = 0.05f;
		def.kcalorieToKGConversionRatio = 0.005f;
		def.stomachSize = 5f;
		def.oozeRate = 2f;
		def.oozeOffsets = new List<Vector3>
		{
			new Vector3(-2f, 2f),
			new Vector3(2f, 1f)
		};
		def.attackSenseArea = new Vector2I(5, 5);
		def.attackCooldown = 5f;
		gameObject.AddOrGet<Storage>();
		FactionAlignment factionAlignment = gameObject.AddOrGet<FactionAlignment>();
		factionAlignment.Alignment = FactionManager.FactionID.Hostile;
		factionAlignment.canBePlayerTargeted = false;
		gameObject.AddOrGet<RangedAttackable>();
		gameObject.AddWeapon(5f, 5f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.AreaOfEffect, 1, 2f);
		gameObject.AddOrGet<WiltCondition>();
		gameObject.AddOrGet<TemperatureVulnerable>().Configure(173.15f, 0f, 373.15f, 1023.15f);
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002A7A2 File Offset: 0x000289A2
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002A7A4 File Offset: 0x000289A4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400047E RID: 1150
	public const string ID = "SapTree";

	// Token: 0x0400047F RID: 1151
	public static readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER5;

	// Token: 0x04000480 RID: 1152
	private const int WIDTH = 5;

	// Token: 0x04000481 RID: 1153
	private const int HEIGHT = 5;

	// Token: 0x04000482 RID: 1154
	private const int ATTACK_RADIUS = 2;

	// Token: 0x04000483 RID: 1155
	public const float MASS_EAT_RATE = 0.05f;

	// Token: 0x04000484 RID: 1156
	public const float KCAL_TO_KG_RATIO = 0.005f;
}
