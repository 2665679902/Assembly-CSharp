using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class StaterpillarGasConfig : IEntityConfig
{
	// Token: 0x060005A1 RID: 1441 RVA: 0x00025244 File Offset: 0x00023444
	public static GameObject CreateStaterpillarGas(string id, string name, string desc, string anim_file, bool is_baby)
	{
		InhaleStates.Def def = new InhaleStates.Def
		{
			inhaleSound = "gas_Staterpillar_intake",
			behaviourTag = GameTags.Creatures.WantsToStore,
			inhaleAnimPre = "gas_consume_pre",
			inhaleAnimLoop = "gas_consume_loop",
			inhaleAnimPst = "gas_consume_pst",
			useStorage = true,
			alwaysPlayPstAnim = true,
			inhaleTime = StaterpillarGasConfig.INHALE_TIME,
			storageStatusItem = Db.Get().CreatureStatusItems.LookingForGas
		};
		GameObject gameObject = BaseStaterpillarConfig.BaseStaterpillar(id, name, desc, anim_file, "StaterpillarGasBaseTrait", is_baby, ObjectLayer.GasConduit, StaterpillarGasConnectorConfig.ID, GameTags.Unbreathable, "gas_", StaterpillarGasConfig.WARNING_LOW_TEMPERATURE, StaterpillarGasConfig.WARNING_HIGH_TEMPERATURE, StaterpillarGasConfig.LETHAL_LOW_TEMPERATURE, StaterpillarGasConfig.LETHAL_HIGH_TEMPERATURE, def);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, TUNING.CREATURES.SPACE_REQUIREMENTS.TIER3);
		if (!is_baby)
		{
			GasAndLiquidConsumerMonitor.Def def2 = gameObject.AddOrGetDef<GasAndLiquidConsumerMonitor.Def>();
			def2.behaviourTag = GameTags.Creatures.WantsToStore;
			def2.consumableElementTag = GameTags.Unbreathable;
			def2.transitionTag = new Tag[] { GameTags.Creature };
			def2.minCooldown = StaterpillarGasConfig.COOLDOWN_MIN;
			def2.maxCooldown = StaterpillarGasConfig.COOLDOWN_MAX;
			def2.consumptionRate = StaterpillarGasConfig.CONSUMPTION_RATE;
		}
		Trait trait = Db.Get().CreateTrait("StaterpillarGasBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, StaterpillarTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = new List<Diet.Info>();
		list.AddRange(BaseStaterpillarConfig.RawMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarGasConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		list.AddRange(BaseStaterpillarConfig.RefinedMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarGasConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		gameObject = BaseStaterpillarConfig.SetupDiet(gameObject, list);
		gameObject.AddComponent<Storage>().capacityKg = StaterpillarGasConfig.STORAGE_CAPACITY;
		return gameObject;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00025492 File Offset: 0x00023692
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0002549C File Offset: 0x0002369C
	public virtual GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(StaterpillarGasConfig.CreateStaterpillarGas("StaterpillarGas", STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.NAME, STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.DESC, "caterpillar_kanim", false), "StaterpillarGasEgg", STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.EGG_NAME, STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.DESC, "egg_caterpillar_kanim", StaterpillarTuning.EGG_MASS, "StaterpillarGasBaby", 60.000004f, 20f, StaterpillarTuning.EGG_CHANCES_GAS, 1, true, false, true, 1f, false);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00025513 File Offset: 0x00023713
	public void OnPrefabInit(GameObject prefab)
	{
		KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
		component.SetSymbolVisiblity("electric_bolt_c_bloom", false);
		component.SetSymbolVisiblity("gulp", false);
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0002553C File Offset: 0x0002373C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003D2 RID: 978
	public const string ID = "StaterpillarGas";

	// Token: 0x040003D3 RID: 979
	public const string BASE_TRAIT_ID = "StaterpillarGasBaseTrait";

	// Token: 0x040003D4 RID: 980
	public const string EGG_ID = "StaterpillarGasEgg";

	// Token: 0x040003D5 RID: 981
	public const int EGG_SORT_ORDER = 1;

	// Token: 0x040003D6 RID: 982
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x040003D7 RID: 983
	private static float CALORIES_PER_KG_OF_ORE = StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / StaterpillarGasConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040003D8 RID: 984
	private static float STORAGE_CAPACITY = 100f;

	// Token: 0x040003D9 RID: 985
	private static float COOLDOWN_MIN = 20f;

	// Token: 0x040003DA RID: 986
	private static float COOLDOWN_MAX = 40f;

	// Token: 0x040003DB RID: 987
	private static float CONSUMPTION_RATE = 0.5f;

	// Token: 0x040003DC RID: 988
	private static float INHALE_TIME = 6f;

	// Token: 0x040003DD RID: 989
	private static float LETHAL_LOW_TEMPERATURE = 243.15f;

	// Token: 0x040003DE RID: 990
	private static float LETHAL_HIGH_TEMPERATURE = 363.15f;

	// Token: 0x040003DF RID: 991
	private static float WARNING_LOW_TEMPERATURE = StaterpillarGasConfig.LETHAL_LOW_TEMPERATURE + 20f;

	// Token: 0x040003E0 RID: 992
	private static float WARNING_HIGH_TEMPERATURE = StaterpillarGasConfig.LETHAL_HIGH_TEMPERATURE - 20f;
}
