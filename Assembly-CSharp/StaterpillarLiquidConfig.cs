using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class StaterpillarLiquidConfig : IEntityConfig
{
	// Token: 0x060005AD RID: 1453 RVA: 0x0002563C File Offset: 0x0002383C
	public static GameObject CreateStaterpillarLiquid(string id, string name, string desc, string anim_file, bool is_baby)
	{
		InhaleStates.Def def = new InhaleStates.Def
		{
			inhaleSound = "wtr_Staterpillar_intake",
			behaviourTag = GameTags.Creatures.WantsToStore,
			inhaleAnimPre = "liquid_consume_pre",
			inhaleAnimLoop = "liquid_consume_loop",
			inhaleAnimPst = "liquid_consume_pst",
			useStorage = true,
			alwaysPlayPstAnim = true,
			inhaleTime = StaterpillarLiquidConfig.INHALE_TIME,
			storageStatusItem = Db.Get().CreatureStatusItems.LookingForLiquid
		};
		GameObject gameObject = BaseStaterpillarConfig.BaseStaterpillar(id, name, desc, anim_file, "StaterpillarLiquidBaseTrait", is_baby, ObjectLayer.LiquidConduit, StaterpillarLiquidConnectorConfig.ID, GameTags.Unbreathable, "wtr_", StaterpillarLiquidConfig.WARNING_LOW_TEMPERATURE, StaterpillarLiquidConfig.WARNING_HIGH_TEMPERATURE, StaterpillarLiquidConfig.LETHAL_LOW_TEMPERATURE, StaterpillarLiquidConfig.LETHAL_HIGH_TEMPERATURE, def);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, TUNING.CREATURES.SPACE_REQUIREMENTS.TIER3);
		if (!is_baby)
		{
			GasAndLiquidConsumerMonitor.Def def2 = gameObject.AddOrGetDef<GasAndLiquidConsumerMonitor.Def>();
			def2.behaviourTag = GameTags.Creatures.WantsToStore;
			def2.consumableElementTag = GameTags.Liquid;
			def2.transitionTag = new Tag[] { GameTags.Creature };
			def2.minCooldown = StaterpillarLiquidConfig.COOLDOWN_MIN;
			def2.maxCooldown = StaterpillarLiquidConfig.COOLDOWN_MAX;
			def2.consumptionRate = StaterpillarLiquidConfig.CONSUMPTION_RATE;
		}
		Trait trait = Db.Get().CreateTrait("StaterpillarLiquidBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, StaterpillarTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = new List<Diet.Info>();
		list.AddRange(BaseStaterpillarConfig.RawMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarLiquidConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		list.AddRange(BaseStaterpillarConfig.RefinedMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarLiquidConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		gameObject = BaseStaterpillarConfig.SetupDiet(gameObject, list);
		gameObject.AddComponent<Storage>().capacityKg = StaterpillarLiquidConfig.STORAGE_CAPACITY;
		return gameObject;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0002588A File Offset: 0x00023A8A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00025894 File Offset: 0x00023A94
	public virtual GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(StaterpillarLiquidConfig.CreateStaterpillarLiquid("StaterpillarLiquid", STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.NAME, STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.DESC, "caterpillar_kanim", false), "StaterpillarLiquidEgg", STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.EGG_NAME, STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.DESC, "egg_caterpillar_kanim", StaterpillarTuning.EGG_MASS, "StaterpillarLiquidBaby", 60.000004f, 20f, StaterpillarTuning.EGG_CHANCES_LIQUID, 2, true, false, true, 1f, false);
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0002590B File Offset: 0x00023B0B
	public void OnPrefabInit(GameObject prefab)
	{
		KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
		component.SetSymbolVisiblity("electric_bolt_c_bloom", false);
		component.SetSymbolVisiblity("gulp", false);
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00025934 File Offset: 0x00023B34
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003E2 RID: 994
	public const string ID = "StaterpillarLiquid";

	// Token: 0x040003E3 RID: 995
	public const string BASE_TRAIT_ID = "StaterpillarLiquidBaseTrait";

	// Token: 0x040003E4 RID: 996
	public const string EGG_ID = "StaterpillarLiquidEgg";

	// Token: 0x040003E5 RID: 997
	public const int EGG_SORT_ORDER = 2;

	// Token: 0x040003E6 RID: 998
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x040003E7 RID: 999
	private static float CALORIES_PER_KG_OF_ORE = StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / StaterpillarLiquidConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040003E8 RID: 1000
	private static float STORAGE_CAPACITY = 1000f;

	// Token: 0x040003E9 RID: 1001
	private static float COOLDOWN_MIN = 20f;

	// Token: 0x040003EA RID: 1002
	private static float COOLDOWN_MAX = 40f;

	// Token: 0x040003EB RID: 1003
	private static float CONSUMPTION_RATE = 10f;

	// Token: 0x040003EC RID: 1004
	private static float INHALE_TIME = 6f;

	// Token: 0x040003ED RID: 1005
	private static float LETHAL_LOW_TEMPERATURE = 243.15f;

	// Token: 0x040003EE RID: 1006
	private static float LETHAL_HIGH_TEMPERATURE = 363.15f;

	// Token: 0x040003EF RID: 1007
	private static float WARNING_LOW_TEMPERATURE = StaterpillarLiquidConfig.LETHAL_LOW_TEMPERATURE + 20f;

	// Token: 0x040003F0 RID: 1008
	private static float WARNING_HIGH_TEMPERATURE = StaterpillarLiquidConfig.LETHAL_HIGH_TEMPERATURE - 20f;
}
