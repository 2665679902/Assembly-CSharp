﻿using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200008B RID: 139
public static class BaseDivergentConfig
{
	// Token: 0x06000283 RID: 643 RVA: 0x00012B80 File Offset: 0x00010D80
	public static GameObject BaseDivergent(string id, string name, string desc, float mass, string anim_file, string traitId, bool is_baby, float num_tended_per_cycle = 8f, string symbolOverridePrefix = null, string cropTendingEffect = "DivergentCropTended", int meatAmount = 1, bool is_pacifist = true)
	{
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, name, desc, mass, Assets.GetAnim(anim_file), "idle_loop", Grid.SceneLayer.Creatures, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		string text = "WalkerNavGrid1x1";
		if (is_baby)
		{
			text = "WalkerBabyNavGrid";
		}
		EntityTemplates.ExtendEntityToBasicCreature(gameObject, FactionManager.FactionID.Pest, traitId, text, NavType.Floor, 32, 2f, "Meat", meatAmount, true, false, 283.15f, 293.15f, 243.15f, 343.15f);
		if (symbolOverridePrefix != null)
		{
			gameObject.AddOrGet<SymbolOverrideController>().ApplySymbolOverridesByAffix(Assets.GetAnim(anim_file), symbolOverridePrefix, null, 0);
		}
		gameObject.AddOrGet<Trappable>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGetDef<CreatureFallMonitor.Def>();
		gameObject.AddOrGetDef<BurrowMonitor.Def>();
		gameObject.AddOrGetDef<CropTendingMonitor.Def>().numCropsTendedPerCycle = num_tended_per_cycle;
		gameObject.AddOrGetDef<ThreatMonitor.Def>().fleethresholdState = Health.HealthState.Dead;
		EntityTemplates.CreateAndRegisterBaggedCreature(gameObject, true, true, false);
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Creatures.Walker, false);
		component.prefabInitFn += delegate(GameObject inst)
		{
			inst.GetAttributes().Add(Db.Get().Attributes.MaxUnderwaterTravelCost);
		};
		CropTendingStates.Def def = new CropTendingStates.Def();
		def.effectId = cropTendingEffect;
		def.interests.Add("WormPlant", 10);
		def.animSetOverrides.Add("WormPlant", new CropTendingStates.AnimSet
		{
			crop_tending_pre = "wormwood_tending_pre",
			crop_tending = "wormwood_tending",
			crop_tending_pst = "wormwood_tending_pst",
			hide_symbols_after_pre = new string[] { "flower", "flower_wilted" }
		});
		def.ignoreEffectGroup = BaseDivergentConfig.ignoreEffectGroup;
		ChoreTable.Builder builder = new ChoreTable.Builder().Add(new DeathStates.Def(), true, -1).Add(new AnimInterruptStates.Def(), true, -1).Add(new GrowUpStates.Def(), is_baby, -1)
			.Add(new TrappedStates.Def(), true, -1)
			.Add(new IncubatingStates.Def(), is_baby, -1)
			.Add(new BaggedStates.Def(), true, -1)
			.Add(new FallStates.Def(), true, -1)
			.Add(new StunnedStates.Def(), true, -1)
			.Add(new DrowningStates.Def(), true, -1)
			.Add(new DebugGoToStates.Def(), true, -1)
			.Add(new FleeStates.Def(), true, -1)
			.Add(new AttackStates.Def("eat_pre", "eat_pst", null), !is_baby && !is_pacifist, -1)
			.PushInterruptGroup()
			.Add(new CreatureSleepStates.Def(), true, -1)
			.Add(new FixedCaptureStates.Def(), true, -1)
			.Add(new RanchedStates.Def(), !is_baby, -1)
			.Add(new LayEggStates.Def(), !is_baby, -1)
			.Add(new EatStates.Def(), true, -1)
			.Add(new PlayAnimsStates.Def(GameTags.Creatures.Poop, false, "poop", STRINGS.CREATURES.STATUSITEMS.EXPELLING_SOLID.NAME, STRINGS.CREATURES.STATUSITEMS.EXPELLING_SOLID.TOOLTIP), true, -1)
			.Add(new CallAdultStates.Def(), is_baby, -1)
			.Add(def, !is_baby, -1)
			.PopInterruptGroup()
			.Add(new IdleStates.Def(), true, -1);
		EntityTemplates.AddCreatureBrain(gameObject, builder, GameTags.Creatures.Species.DivergentSpecies, symbolOverridePrefix);
		return gameObject;
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00012E88 File Offset: 0x00011088
	public static List<Diet.Info> BasicSulfurDiet(Tag poopTag, float caloriesPerKg, float producedConversionRate, string diseaseId, float diseasePerKgProduced)
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		hashSet.Add(SimHashes.Sulfur.CreateTag());
		return new List<Diet.Info>
		{
			new Diet.Info(hashSet, poopTag, caloriesPerKg, producedConversionRate, diseaseId, diseasePerKgProduced, false, false)
		};
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00012EC8 File Offset: 0x000110C8
	public static GameObject SetupDiet(GameObject prefab, List<Diet.Info> diet_infos, float referenceCaloriesPerKg, float minPoopSizeInKg)
	{
		Diet diet = new Diet(diet_infos.ToArray());
		CreatureCalorieMonitor.Def def = prefab.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minPoopSizeInCalories = referenceCaloriesPerKg * minPoopSizeInKg;
		prefab.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		return prefab;
	}

	// Token: 0x04000179 RID: 377
	public const float CROP_TENDED_MULTIPLIER_DURATION = 600f;

	// Token: 0x0400017A RID: 378
	public const float CROP_TENDED_MULTIPLIER_EFFECT = 0.05f;

	// Token: 0x0400017B RID: 379
	public static string[] ignoreEffectGroup = new string[] { "DivergentCropTended", "DivergentCropTendedWorm" };
}
