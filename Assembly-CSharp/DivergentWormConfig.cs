using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000E7 RID: 231
[EntityConfigOrder(1)]
public class DivergentWormConfig : IEntityConfig
{
	// Token: 0x06000428 RID: 1064 RVA: 0x0001F43C File Offset: 0x0001D63C
	public static GameObject CreateWorm(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseDivergentConfig.BaseDivergent(id, name, desc, 200f, anim_file, "DivergentWormBaseTrait", is_baby, 8f, null, "DivergentCropTendedWorm", 3, false), DivergentTuning.PEN_SIZE_PER_CREATURE_WORM);
		Trait trait = Db.Get().CreateTrait("DivergentWormBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, DivergentTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -DivergentTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 150f, name, false, false, true));
		gameObject.AddWeapon(2f, 3f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.Single, 1, 0f);
		List<Diet.Info> list = BaseDivergentConfig.BasicSulfurDiet(SimHashes.Mud.CreateTag(), DivergentWormConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, null, 0f);
		list.Add(new Diet.Info(new HashSet<Tag> { SimHashes.Sucrose.CreateTag() }, SimHashes.Mud.CreateTag(), DivergentWormConfig.CALORIES_PER_KG_OF_SUCROSE, 1f, null, 0f, false, false));
		GameObject gameObject2 = BaseDivergentConfig.SetupDiet(gameObject, list, DivergentWormConfig.CALORIES_PER_KG_OF_ORE, DivergentWormConfig.MINI_POOP_SIZE_IN_KG);
		SegmentedCreature.Def def = gameObject2.AddOrGetDef<SegmentedCreature.Def>();
		def.segmentTrackerSymbol = new HashedString("segmenttracker");
		def.numBodySegments = 5;
		def.midAnim = Assets.GetAnim("worm_torso_kanim");
		def.tailAnim = Assets.GetAnim("worm_tail_kanim");
		def.animFrameOffset = 2;
		def.pathSpacing = 0.2f;
		def.numPathNodes = 15;
		def.minSegmentSpacing = 0.1f;
		def.maxSegmentSpacing = 0.4f;
		def.retractionSegmentSpeed = 1f;
		def.retractionPathSpeed = 2f;
		def.compressedMaxScale = 0.25f;
		def.headOffset = new Vector3(0.12f, 0.4f, 0f);
		return gameObject2;
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0001F67C File Offset: 0x0001D87C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0001F684 File Offset: 0x0001D884
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(DivergentWormConfig.CreateWorm("DivergentWorm", STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.NAME, STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.DESC, "worm_head_kanim", false), "DivergentWormEgg", STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.EGG_NAME, STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.DESC, "egg_worm_kanim", DivergentTuning.EGG_MASS, "DivergentWormBaby", 90f, 30f, DivergentTuning.EGG_CHANCES_WORM, DivergentWormConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0001F6FF File Offset: 0x0001D8FF
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0001F701 File Offset: 0x0001D901
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002AA RID: 682
	public const string ID = "DivergentWorm";

	// Token: 0x040002AB RID: 683
	public const string BASE_TRAIT_ID = "DivergentWormBaseTrait";

	// Token: 0x040002AC RID: 684
	public const string EGG_ID = "DivergentWormEgg";

	// Token: 0x040002AD RID: 685
	private const float LIFESPAN = 150f;

	// Token: 0x040002AE RID: 686
	public const float CROP_TENDED_MULTIPLIER_EFFECT = 0.5f;

	// Token: 0x040002AF RID: 687
	public const float CROP_TENDED_MULTIPLIER_DURATION = 600f;

	// Token: 0x040002B0 RID: 688
	private const int NUM_SEGMENTS = 5;

	// Token: 0x040002B1 RID: 689
	private const SimHashes EMIT_ELEMENT = SimHashes.Mud;

	// Token: 0x040002B2 RID: 690
	private static float KG_ORE_EATEN_PER_CYCLE = 50f;

	// Token: 0x040002B3 RID: 691
	private static float KG_SUCROSE_EATEN_PER_CYCLE = 30f;

	// Token: 0x040002B4 RID: 692
	private static float CALORIES_PER_KG_OF_ORE = DivergentTuning.STANDARD_CALORIES_PER_CYCLE / DivergentWormConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040002B5 RID: 693
	private static float CALORIES_PER_KG_OF_SUCROSE = DivergentTuning.STANDARD_CALORIES_PER_CYCLE / DivergentWormConfig.KG_SUCROSE_EATEN_PER_CYCLE;

	// Token: 0x040002B6 RID: 694
	public static int EGG_SORT_ORDER = 0;

	// Token: 0x040002B7 RID: 695
	private static float MINI_POOP_SIZE_IN_KG = 4f;
}
