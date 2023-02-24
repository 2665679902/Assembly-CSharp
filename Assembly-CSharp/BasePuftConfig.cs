﻿using System;
using System.Collections.Generic;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200009D RID: 157
public static class BasePuftConfig
{
	// Token: 0x060002B3 RID: 691 RVA: 0x00015A94 File Offset: 0x00013C94
	public static GameObject BasePuft(string id, string name, string desc, string traitId, string anim_file, bool is_baby, string symbol_override_prefix, float warningLowTemperature, float warningHighTemperature)
	{
		float num = 50f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, name, desc, num, Assets.GetAnim(anim_file), "idle_loop", Grid.SceneLayer.Creatures, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicCreature(gameObject, FactionManager.FactionID.Prey, traitId, "FlyerNavGrid1x1", NavType.Hover, 32, 2f, "Meat", 1, true, true, warningLowTemperature, warningHighTemperature, warningLowTemperature - 45f, warningHighTemperature + 50f);
		if (!string.IsNullOrEmpty(symbol_override_prefix))
		{
			gameObject.AddOrGet<SymbolOverrideController>().ApplySymbolOverridesByAffix(Assets.GetAnim(anim_file), symbol_override_prefix, null, 0);
		}
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Creatures.Flyer, false);
		component.prefabInitFn += delegate(GameObject inst)
		{
			inst.GetAttributes().Add(Db.Get().Attributes.MaxUnderwaterTravelCost);
		};
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[] { GameTags.SlimeMold };
		gameObject.AddOrGetDef<ThreatMonitor.Def>();
		gameObject.AddOrGetDef<SubmergedMonitor.Def>();
		SoundEventVolumeCache.instance.AddVolume("puft_kanim", "Puft_voice_idle", NOISE_POLLUTION.CREATURES.TIER2);
		SoundEventVolumeCache.instance.AddVolume("puft_kanim", "Puft_air_intake", NOISE_POLLUTION.CREATURES.TIER4);
		SoundEventVolumeCache.instance.AddVolume("puft_kanim", "Puft_toot", NOISE_POLLUTION.CREATURES.TIER5);
		SoundEventVolumeCache.instance.AddVolume("puft_kanim", "Puft_air_inflated", NOISE_POLLUTION.CREATURES.TIER5);
		SoundEventVolumeCache.instance.AddVolume("puft_kanim", "Puft_voice_die", NOISE_POLLUTION.CREATURES.TIER5);
		SoundEventVolumeCache.instance.AddVolume("puft_kanim", "Puft_voice_hurt", NOISE_POLLUTION.CREATURES.TIER5);
		EntityTemplates.CreateAndRegisterBaggedCreature(gameObject, true, false, false);
		string text = "Puft_air_intake";
		if (is_baby)
		{
			text = "PuftBaby_air_intake";
		}
		ChoreTable.Builder builder = new ChoreTable.Builder().Add(new DeathStates.Def(), true, -1).Add(new AnimInterruptStates.Def(), true, -1).Add(new GrowUpStates.Def(), is_baby, -1)
			.Add(new IncubatingStates.Def(), is_baby, -1)
			.Add(new BaggedStates.Def(), true, -1)
			.Add(new StunnedStates.Def(), true, -1)
			.Add(new DebugGoToStates.Def(), true, -1)
			.Add(new DrowningStates.Def(), true, -1)
			.PushInterruptGroup()
			.Add(new CreatureSleepStates.Def(), true, -1)
			.Add(new FixedCaptureStates.Def(), true, -1)
			.Add(new RanchedStates.Def(), !is_baby, -1)
			.Add(new UpTopPoopStates.Def(), true, -1)
			.Add(new LayEggStates.Def(), !is_baby, -1)
			.Add(new InhaleStates.Def
			{
				inhaleSound = text
			}, true, -1)
			.Add(new MoveToLureStates.Def(), true, -1)
			.Add(new CallAdultStates.Def(), is_baby, -1)
			.PopInterruptGroup()
			.Add(new IdleStates.Def
			{
				customIdleAnim = new IdleStates.Def.IdleAnimCallback(BasePuftConfig.CustomIdleAnim)
			}, true, -1);
		EntityTemplates.AddCreatureBrain(gameObject, builder, GameTags.Creatures.Species.PuftSpecies, symbol_override_prefix);
		return gameObject;
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00015D60 File Offset: 0x00013F60
	public static GameObject SetupDiet(GameObject prefab, Tag consumed_tag, Tag producedTag, float caloriesPerKg, float producedConversionRate, string diseaseId, float diseasePerKgProduced, float minPoopSizeInKg)
	{
		Diet.Info[] array = new Diet.Info[]
		{
			new Diet.Info(new HashSet<Tag> { consumed_tag }, producedTag, caloriesPerKg, producedConversionRate, diseaseId, diseasePerKgProduced, false, false)
		};
		return BasePuftConfig.SetupDiet(prefab, array, caloriesPerKg, minPoopSizeInKg);
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00015DA0 File Offset: 0x00013FA0
	public static GameObject SetupDiet(GameObject prefab, Diet.Info[] diet_infos, float caloriesPerKg, float minPoopSizeInKg)
	{
		Diet diet = new Diet(diet_infos);
		CreatureCalorieMonitor.Def def = prefab.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minPoopSizeInCalories = minPoopSizeInKg * caloriesPerKg;
		prefab.AddOrGetDef<GasAndLiquidConsumerMonitor.Def>().diet = diet;
		return prefab;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00015DD8 File Offset: 0x00013FD8
	private static HashedString CustomIdleAnim(IdleStates.Instance smi, ref HashedString pre_anim)
	{
		CreatureCalorieMonitor.Instance smi2 = smi.GetSMI<CreatureCalorieMonitor.Instance>();
		return (smi2 != null && smi2.stomach.IsReadyToPoop()) ? "idle_loop_full" : "idle_loop";
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00015E10 File Offset: 0x00014010
	public static void OnSpawn(GameObject inst)
	{
		Navigator component = inst.GetComponent<Navigator>();
		component.transitionDriver.overrideLayers.Add(new FullPuftTransitionLayer(component));
	}
}
