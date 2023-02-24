﻿using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class EntityTemplates
{
	// Token: 0x060001E5 RID: 485 RVA: 0x0000D594 File Offset: 0x0000B794
	public static void CreateTemplates()
	{
		EntityTemplates.unselectableEntityTemplate = new GameObject("unselectableEntityTemplate");
		EntityTemplates.unselectableEntityTemplate.SetActive(false);
		EntityTemplates.unselectableEntityTemplate.AddComponent<KPrefabID>();
		UnityEngine.Object.DontDestroyOnLoad(EntityTemplates.unselectableEntityTemplate);
		EntityTemplates.selectableEntityTemplate = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.unselectableEntityTemplate);
		EntityTemplates.selectableEntityTemplate.name = "selectableEntityTemplate";
		EntityTemplates.selectableEntityTemplate.AddComponent<KSelectable>();
		UnityEngine.Object.DontDestroyOnLoad(EntityTemplates.selectableEntityTemplate);
		EntityTemplates.baseEntityTemplate = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.selectableEntityTemplate);
		EntityTemplates.baseEntityTemplate.name = "baseEntityTemplate";
		EntityTemplates.baseEntityTemplate.AddComponent<KBatchedAnimController>();
		EntityTemplates.baseEntityTemplate.AddComponent<SaveLoadRoot>();
		EntityTemplates.baseEntityTemplate.AddComponent<StateMachineController>();
		EntityTemplates.baseEntityTemplate.AddComponent<PrimaryElement>();
		EntityTemplates.baseEntityTemplate.AddComponent<SimTemperatureTransfer>();
		EntityTemplates.baseEntityTemplate.AddComponent<InfoDescription>();
		EntityTemplates.baseEntityTemplate.AddComponent<Notifier>();
		UnityEngine.Object.DontDestroyOnLoad(EntityTemplates.baseEntityTemplate);
		EntityTemplates.placedEntityTemplate = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.baseEntityTemplate);
		EntityTemplates.placedEntityTemplate.name = "placedEntityTemplate";
		EntityTemplates.placedEntityTemplate.AddComponent<KBoxCollider2D>();
		EntityTemplates.placedEntityTemplate.AddComponent<OccupyArea>();
		EntityTemplates.placedEntityTemplate.AddComponent<Modifiers>();
		EntityTemplates.placedEntityTemplate.AddComponent<DecorProvider>();
		UnityEngine.Object.DontDestroyOnLoad(EntityTemplates.placedEntityTemplate);
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000D6CC File Offset: 0x0000B8CC
	private static void ConfigEntity(GameObject template, string id, string name, bool is_selectable = true)
	{
		template.name = id;
		template.AddOrGet<KPrefabID>().PrefabTag = TagManager.Create(id, name);
		if (is_selectable)
		{
			template.AddOrGet<KSelectable>().SetName(name);
		}
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
	public static GameObject CreateEntity(string id, string name, bool is_selectable = true)
	{
		GameObject gameObject;
		if (is_selectable)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.selectableEntityTemplate);
		}
		else
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.unselectableEntityTemplate);
		}
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		EntityTemplates.ConfigEntity(gameObject, id, name, is_selectable);
		return gameObject;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000D734 File Offset: 0x0000B934
	public static GameObject ConfigBasicEntity(GameObject template, string id, string name, string desc, float mass, bool unitMass, KAnimFile anim, string initialAnim, Grid.SceneLayer sceneLayer, SimHashes element = SimHashes.Creature, List<Tag> additionalTags = null, float defaultTemperature = 293f)
	{
		EntityTemplates.ConfigEntity(template, id, name, true);
		KPrefabID kprefabID = template.AddOrGet<KPrefabID>();
		if (additionalTags != null)
		{
			foreach (Tag tag in additionalTags)
			{
				kprefabID.AddTag(tag, false);
			}
		}
		KBatchedAnimController kbatchedAnimController = template.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { anim };
		kbatchedAnimController.sceneLayer = sceneLayer;
		kbatchedAnimController.initialAnim = initialAnim;
		template.AddOrGet<StateMachineController>();
		PrimaryElement primaryElement = template.AddOrGet<PrimaryElement>();
		primaryElement.ElementID = element;
		primaryElement.Temperature = defaultTemperature;
		if (unitMass)
		{
			primaryElement.MassPerUnit = mass;
			primaryElement.Units = 1f;
			GameTags.DisplayAsUnits.Add(kprefabID.PrefabTag);
		}
		else
		{
			primaryElement.Mass = mass;
		}
		template.AddOrGet<InfoDescription>().description = desc;
		template.AddOrGet<Notifier>();
		return template;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000D828 File Offset: 0x0000BA28
	public static GameObject CreateBasicEntity(string id, string name, string desc, float mass, bool unitMass, KAnimFile anim, string initialAnim, Grid.SceneLayer sceneLayer, SimHashes element = SimHashes.Creature, List<Tag> additionalTags = null, float defaultTemperature = 293f)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.baseEntityTemplate);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		EntityTemplates.ConfigBasicEntity(gameObject, id, name, desc, mass, unitMass, anim, initialAnim, sceneLayer, element, additionalTags, defaultTemperature);
		return gameObject;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000D860 File Offset: 0x0000BA60
	private static GameObject ConfigPlacedEntity(GameObject template, string id, string name, string desc, float mass, KAnimFile anim, string initialAnim, Grid.SceneLayer sceneLayer, int width, int height, EffectorValues decor, EffectorValues noise = default(EffectorValues), SimHashes element = SimHashes.Creature, List<Tag> additionalTags = null, float defaultTemperature = 293f)
	{
		if (anim == null)
		{
			global::Debug.LogErrorFormat("Cant create [{0}] entity without an anim", new object[] { name });
		}
		EntityTemplates.ConfigBasicEntity(template, id, name, desc, mass, true, anim, initialAnim, sceneLayer, element, additionalTags, defaultTemperature);
		KBoxCollider2D kboxCollider2D = template.AddOrGet<KBoxCollider2D>();
		kboxCollider2D.size = new Vector2f(width, height);
		float num = 0.5f * (float)((width + 1) % 2);
		kboxCollider2D.offset = new Vector2f(num, (float)height / 2f);
		template.GetComponent<KBatchedAnimController>().Offset = new Vector3(num, 0f, 0f);
		template.AddOrGet<OccupyArea>().OccupiedCellsOffsets = EntityTemplates.GenerateOffsets(width, height);
		DecorProvider decorProvider = template.AddOrGet<DecorProvider>();
		decorProvider.SetValues(decor);
		decorProvider.overrideName = name;
		return template;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000D92C File Offset: 0x0000BB2C
	public static GameObject CreatePlacedEntity(string id, string name, string desc, float mass, KAnimFile anim, string initialAnim, Grid.SceneLayer sceneLayer, int width, int height, EffectorValues decor, EffectorValues noise = default(EffectorValues), SimHashes element = SimHashes.Creature, List<Tag> additionalTags = null, float defaultTemperature = 293f)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.placedEntityTemplate);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		EntityTemplates.ConfigPlacedEntity(gameObject, id, name, desc, mass, anim, initialAnim, sceneLayer, width, height, decor, noise, element, additionalTags, defaultTemperature);
		return gameObject;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000D968 File Offset: 0x0000BB68
	public static GameObject MakeHangingOffsets(GameObject template, int width, int height)
	{
		KBoxCollider2D component = template.GetComponent<KBoxCollider2D>();
		if (component)
		{
			component.size = new Vector2f(width, height);
			float num = 0.5f * (float)((width + 1) % 2);
			component.offset = new Vector2f(num, (float)(-(float)height) / 2f + 1f);
		}
		OccupyArea component2 = template.GetComponent<OccupyArea>();
		if (component2)
		{
			component2.OccupiedCellsOffsets = EntityTemplates.GenerateHangingOffsets(width, height);
		}
		return template;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
	public static GameObject ExtendEntityToBasicPlant(GameObject template, float temperature_lethal_low = 218.15f, float temperature_warning_low = 283.15f, float temperature_warning_high = 303.15f, float temperature_lethal_high = 398.15f, SimHashes[] safe_elements = null, bool pressure_sensitive = true, float pressure_lethal_low = 0f, float pressure_warning_low = 0.15f, string crop_id = null, bool can_drown = true, bool can_tinker = true, bool require_solid_tile = true, bool should_grow_old = true, float max_age = 2400f, float min_radiation = 0f, float max_radiation = 2200f, string baseTraitId = null, string baseTraitName = null)
	{
		Modifiers component = template.GetComponent<Modifiers>();
		Trait trait = Db.Get().CreateTrait(baseTraitId, baseTraitName, baseTraitName, null, false, null, true, true);
		template.AddTag(GameTags.Plant);
		template.AddOrGet<EntombVulnerable>();
		PressureVulnerable pressureVulnerable = template.AddOrGet<PressureVulnerable>();
		if (pressure_sensitive)
		{
			pressureVulnerable.Configure(pressure_warning_low, pressure_lethal_low, 10f, 30f, safe_elements);
		}
		else
		{
			pressureVulnerable.Configure(safe_elements);
		}
		template.AddOrGet<WiltCondition>();
		template.AddOrGet<Prioritizable>();
		template.AddOrGet<Uprootable>();
		template.AddOrGet<Effects>();
		if (require_solid_tile)
		{
			template.AddOrGet<UprootedMonitor>();
		}
		template.AddOrGet<ReceptacleMonitor>();
		template.AddOrGet<Notifier>();
		if (can_drown)
		{
			template.AddOrGet<DrowningMonitor>();
		}
		template.AddOrGet<KAnimControllerBase>().randomiseLoopedOffset = true;
		component.initialAttributes.Add(Db.Get().PlantAttributes.WiltTempRangeMod.Id);
		template.AddOrGet<TemperatureVulnerable>().Configure(temperature_warning_low, temperature_lethal_low, temperature_warning_high, temperature_lethal_high);
		if (DlcManager.FeaturePlantMutationsEnabled())
		{
			component.initialAttributes.Add(Db.Get().PlantAttributes.MinRadiationThreshold.Id);
			if (min_radiation != 0f)
			{
				trait.Add(new AttributeModifier(Db.Get().PlantAttributes.MinRadiationThreshold.Id, min_radiation, baseTraitName, false, false, true));
			}
			component.initialAttributes.Add(Db.Get().PlantAttributes.MaxRadiationThreshold.Id);
			trait.Add(new AttributeModifier(Db.Get().PlantAttributes.MaxRadiationThreshold.Id, max_radiation, baseTraitName, false, false, true));
			template.AddOrGetDef<RadiationVulnerable.Def>();
		}
		template.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		KPrefabID component2 = template.GetComponent<KPrefabID>();
		if (crop_id != null)
		{
			GeneratedBuildings.RegisterWithOverlay(OverlayScreen.HarvestableIDs, component2.PrefabID().ToString());
			Crop.CropVal cropVal = CROPS.CROP_TYPES.Find((Crop.CropVal m) => m.cropId == crop_id);
			global::Debug.Assert(baseTraitId != null && baseTraitName != null, "Extending " + template.name + " to a crop plant failed because the base trait wasn't specified.");
			component.initialAttributes.Add(Db.Get().PlantAttributes.YieldAmount.Id);
			component.initialAmounts.Add(Db.Get().Amounts.Maturity.Id);
			trait.Add(new AttributeModifier(Db.Get().PlantAttributes.YieldAmount.Id, (float)cropVal.numProduced, baseTraitName, false, false, true));
			trait.Add(new AttributeModifier(Db.Get().Amounts.Maturity.maxAttribute.Id, cropVal.cropDuration / 600f, baseTraitName, false, false, true));
			if (DlcManager.FeaturePlantMutationsEnabled())
			{
				template.AddOrGet<MutantPlant>().SpeciesID = component2.PrefabTag;
				SymbolOverrideControllerUtil.AddToPrefab(template);
			}
			template.AddOrGet<Crop>().Configure(cropVal);
			Growing growing = template.AddOrGet<Growing>();
			growing.shouldGrowOld = should_grow_old;
			growing.maxAge = max_age;
			template.AddOrGet<Harvestable>();
			template.AddOrGet<HarvestDesignatable>();
		}
		if (trait.SelfModifiers != null && trait.SelfModifiers.Count > 0)
		{
			template.AddOrGet<Traits>();
			component.initialTraits.Add(baseTraitId);
		}
		component2.prefabInitFn += delegate(GameObject inst)
		{
			PressureVulnerable component3 = inst.GetComponent<PressureVulnerable>();
			if (component3 != null && safe_elements != null)
			{
				foreach (SimHashes simHashes in safe_elements)
				{
					component3.safe_atmospheres.Add(ElementLoader.FindElementByHash(simHashes));
				}
			}
		};
		if (can_tinker)
		{
			Tinkerable.MakeFarmTinkerable(template);
		}
		return template;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000DD44 File Offset: 0x0000BF44
	public static GameObject ExtendEntityToWildCreature(GameObject prefab, int space_required_per_creature)
	{
		prefab.AddOrGetDef<AgeMonitor.Def>();
		prefab.AddOrGetDef<HappinessMonitor.Def>();
		Tag prefabTag = prefab.GetComponent<KPrefabID>().PrefabTag;
		WildnessMonitor.Def def = prefab.AddOrGetDef<WildnessMonitor.Def>();
		def.wildEffect = new Effect("Wild" + prefabTag.Name, STRINGS.CREATURES.MODIFIERS.WILD.NAME, STRINGS.CREATURES.MODIFIERS.WILD.TOOLTIP, 0f, true, true, false, null, -1f, 0f, null, "");
		def.wildEffect.Add(new AttributeModifier(Db.Get().Amounts.Wildness.deltaAttribute.Id, 0.008333334f, STRINGS.CREATURES.MODIFIERS.WILD.NAME, false, false, true));
		def.wildEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, 25f, STRINGS.CREATURES.MODIFIERS.WILD.NAME, false, false, true));
		def.wildEffect.Add(new AttributeModifier(Db.Get().Amounts.ScaleGrowth.deltaAttribute.Id, -0.75f, STRINGS.CREATURES.MODIFIERS.WILD.NAME, true, false, true));
		def.tameEffect = new Effect("Tame" + prefabTag.Name, STRINGS.CREATURES.MODIFIERS.TAME.NAME, STRINGS.CREATURES.MODIFIERS.TAME.TOOLTIP, 0f, true, true, false, null, -1f, 0f, null, "");
		def.tameEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -1f, STRINGS.CREATURES.MODIFIERS.TAME.NAME, false, false, true));
		def.tameEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, 100f, STRINGS.CREATURES.MODIFIERS.TAME.NAME, false, false, true));
		prefab.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = space_required_per_creature;
		return prefab;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000DF28 File Offset: 0x0000C128
	public static GameObject ExtendEntityToFertileCreature(GameObject prefab, string eggId, string eggName, string eggDesc, string egg_anim, float egg_mass, string baby_id, float fertility_cycles, float incubation_cycles, List<FertilityMonitor.BreedingChance> egg_chances, int eggSortOrder = -1, bool is_ranchable = true, bool add_fish_overcrowding_monitor = false, bool add_fixed_capturable_monitor = true, float egg_anim_scale = 1f, bool deprecated = false)
	{
		FertilityMonitor.Def def = prefab.AddOrGetDef<FertilityMonitor.Def>();
		def.baseFertileCycles = fertility_cycles;
		DebugUtil.DevAssert(eggSortOrder > -1, "Added a fertile creature without an egg sort order!", null);
		float num = 100f / (600f * incubation_cycles);
		GameObject gameObject = EggConfig.CreateEgg(eggId, eggName, eggDesc, baby_id, egg_anim, egg_mass, eggSortOrder, num);
		def.eggPrefab = new Tag(eggId);
		def.initialBreedingWeights = egg_chances;
		if (egg_anim_scale != 1f)
		{
			KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
			component.animWidth = egg_anim_scale;
			component.animHeight = egg_anim_scale;
		}
		KPrefabID egg_prefab_id = gameObject.GetComponent<KPrefabID>();
		SymbolOverrideController symbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		string symbolPrefix = prefab.GetComponent<CreatureBrain>().symbolPrefix;
		if (!string.IsNullOrEmpty(symbolPrefix))
		{
			symbolOverrideController.ApplySymbolOverridesByAffix(Assets.GetAnim(egg_anim), symbolPrefix, null, 0);
		}
		KPrefabID creature_prefab_id = prefab.GetComponent<KPrefabID>();
		creature_prefab_id.prefabSpawnFn += delegate(GameObject inst)
		{
			DiscoveredResources.Instance.Discover(eggId.ToTag(), DiscoveredResources.GetCategoryForTags(egg_prefab_id.Tags));
			DiscoveredResources.Instance.Discover(baby_id.ToTag(), DiscoveredResources.GetCategoryForTags(creature_prefab_id.Tags));
		};
		if (is_ranchable)
		{
			prefab.AddOrGetDef<RanchableMonitor.Def>();
		}
		if (add_fixed_capturable_monitor)
		{
			prefab.AddOrGetDef<FixedCapturableMonitor.Def>();
		}
		if (add_fish_overcrowding_monitor)
		{
			gameObject.AddOrGetDef<FishOvercrowdingMonitor.Def>();
		}
		if (deprecated)
		{
			gameObject.AddTag(GameTags.DeprecatedContent);
			prefab.AddTag(GameTags.DeprecatedContent);
		}
		return prefab;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000E06C File Offset: 0x0000C26C
	public static GameObject ExtendEntityToBeingABaby(GameObject prefab, Tag adult_prefab_id, string on_grow_item_drop_id = null, bool force_adult_nav_type = false, float adult_threshold = 5f)
	{
		prefab.AddOrGetDef<BabyMonitor.Def>().adultPrefab = adult_prefab_id;
		prefab.AddOrGetDef<BabyMonitor.Def>().onGrowDropID = on_grow_item_drop_id;
		prefab.AddOrGetDef<BabyMonitor.Def>().forceAdultNavType = force_adult_nav_type;
		prefab.AddOrGetDef<BabyMonitor.Def>().adultThreshold = adult_threshold;
		prefab.AddOrGetDef<IncubatorMonitor.Def>();
		prefab.AddOrGetDef<CreatureSleepMonitor.Def>();
		prefab.AddOrGetDef<CallAdultMonitor.Def>();
		prefab.AddOrGetDef<AgeMonitor.Def>().maxAgePercentOnSpawn = 0.01f;
		return prefab;
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
	public static GameObject ExtendEntityToBasicCreature(GameObject template, FactionManager.FactionID faction = FactionManager.FactionID.Prey, string initialTraitID = null, string NavGridName = "WalkerNavGrid1x1", NavType navType = NavType.Floor, int max_probing_radius = 32, float moveSpeed = 2f, string onDeathDropID = "Meat", int onDeathDropCount = 1, bool drownVulnerable = true, bool entombVulnerable = true, float warningLowTemperature = 283.15f, float warningHighTemperature = 293.15f, float lethalLowTemperature = 243.15f, float lethalHighTemperature = 343.15f)
	{
		template.GetComponent<KBatchedAnimController>().isMovable = true;
		template.AddOrGet<KPrefabID>().AddTag(GameTags.Creature, false);
		Modifiers modifiers = template.AddOrGet<Modifiers>();
		if (initialTraitID != null)
		{
			modifiers.initialTraits.Add(initialTraitID);
		}
		modifiers.initialAmounts.Add(Db.Get().Amounts.HitPoints.Id);
		template.AddOrGet<KBatchedAnimController>().SetSymbolVisiblity("snapto_pivot", false);
		template.AddOrGet<Pickupable>();
		template.AddOrGet<Clearable>().isClearable = false;
		template.AddOrGet<Traits>();
		template.AddOrGet<Health>();
		template.AddOrGet<CharacterOverlay>();
		template.AddOrGet<RangedAttackable>();
		template.AddOrGet<FactionAlignment>().Alignment = faction;
		template.AddOrGet<Prioritizable>();
		template.AddOrGet<Effects>();
		template.AddOrGetDef<CreatureDebugGoToMonitor.Def>();
		template.AddOrGetDef<DeathMonitor.Def>();
		template.AddOrGetDef<AnimInterruptMonitor.Def>();
		template.AddOrGet<AnimEventHandler>();
		SymbolOverrideControllerUtil.AddToPrefab(template);
		template.AddOrGet<TemperatureVulnerable>().Configure(warningLowTemperature, lethalLowTemperature, warningHighTemperature, lethalHighTemperature);
		if (drownVulnerable)
		{
			template.AddOrGet<DrowningMonitor>();
		}
		if (entombVulnerable)
		{
			template.AddOrGet<EntombVulnerable>();
		}
		if (onDeathDropCount > 0 && onDeathDropID != "")
		{
			string[] array = new string[onDeathDropCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = onDeathDropID;
			}
			template.AddOrGet<Butcherable>().SetDrops(array);
		}
		Navigator navigator = template.AddOrGet<Navigator>();
		navigator.NavGridName = NavGridName;
		navigator.CurrentNavType = navType;
		navigator.defaultSpeed = moveSpeed;
		navigator.updateProber = true;
		navigator.maxProbingRadius = max_probing_radius;
		navigator.sceneLayer = Grid.SceneLayer.Creatures;
		return template;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000E248 File Offset: 0x0000C448
	public static void AddCreatureBrain(GameObject prefab, ChoreTable.Builder chore_table, Tag species, string symbol_prefix)
	{
		CreatureBrain creatureBrain = prefab.AddOrGet<CreatureBrain>();
		creatureBrain.species = species;
		creatureBrain.symbolPrefix = symbol_prefix;
		ChoreConsumer chore_consumer = prefab.AddOrGet<ChoreConsumer>();
		chore_consumer.choreTable = chore_table.CreateTable();
		KPrefabID kprefabID = prefab.AddOrGet<KPrefabID>();
		kprefabID.AddTag(GameTags.CreatureBrain, false);
		kprefabID.instantiateFn += delegate(GameObject go)
		{
			go.GetComponent<ChoreConsumer>().choreTable = chore_consumer.choreTable;
		};
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000E2AE File Offset: 0x0000C4AE
	public static Tag GetBaggedCreatureTag(Tag tag)
	{
		return TagManager.Create("Bagged" + tag.Name);
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000E2C6 File Offset: 0x0000C4C6
	public static Tag GetUnbaggedCreatureTag(Tag bagged_tag)
	{
		return TagManager.Create(bagged_tag.Name.Substring(6));
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000E2DA File Offset: 0x0000C4DA
	public static string GetBaggedCreatureID(string name)
	{
		return "Bagged" + name;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
	public static GameObject CreateAndRegisterBaggedCreature(GameObject creature, bool must_stand_on_top_for_pickup, bool allow_mark_for_capture, bool use_gun_for_pickup = false)
	{
		KPrefabID creature_prefab_id = creature.GetComponent<KPrefabID>();
		creature_prefab_id.AddTag(GameTags.BagableCreature, false);
		Baggable baggable = creature.AddOrGet<Baggable>();
		baggable.mustStandOntopOfTrapForPickup = must_stand_on_top_for_pickup;
		baggable.useGunForPickup = use_gun_for_pickup;
		creature.AddOrGet<Capturable>().allowCapture = allow_mark_for_capture;
		creature_prefab_id.prefabSpawnFn += delegate(GameObject inst)
		{
			DiscoveredResources.Instance.Discover(creature_prefab_id.PrefabTag, DiscoveredResources.GetCategoryForTags(creature_prefab_id.Tags));
		};
		return creature;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000E350 File Offset: 0x0000C550
	public static GameObject CreateLooseEntity(string id, string name, string desc, float mass, bool unitMass, KAnimFile anim, string initialAnim, Grid.SceneLayer sceneLayer, EntityTemplates.CollisionShape collisionShape, float width = 1f, float height = 1f, bool isPickupable = false, int sortOrder = 0, SimHashes element = SimHashes.Creature, List<Tag> additionalTags = null)
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity(id, name, desc, mass, unitMass, anim, initialAnim, sceneLayer, element, additionalTags, 293f);
		gameObject = EntityTemplates.AddCollision(gameObject, collisionShape, width, height);
		gameObject.GetComponent<KBatchedAnimController>().isMovable = true;
		gameObject.AddOrGet<Modifiers>();
		if (isPickupable)
		{
			Pickupable pickupable = gameObject.AddOrGet<Pickupable>();
			pickupable.SetWorkTime(5f);
			pickupable.sortOrder = sortOrder;
		}
		return gameObject;
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
	public static void CreateBaseOreTemplates()
	{
		EntityTemplates.baseOreTemplate = new GameObject("OreTemplate");
		UnityEngine.Object.DontDestroyOnLoad(EntityTemplates.baseOreTemplate);
		EntityTemplates.baseOreTemplate.SetActive(false);
		EntityTemplates.baseOreTemplate.AddComponent<KPrefabID>();
		EntityTemplates.baseOreTemplate.AddComponent<PrimaryElement>();
		EntityTemplates.baseOreTemplate.AddComponent<Pickupable>();
		EntityTemplates.baseOreTemplate.AddComponent<KSelectable>();
		EntityTemplates.baseOreTemplate.AddComponent<SaveLoadRoot>();
		EntityTemplates.baseOreTemplate.AddComponent<StateMachineController>();
		EntityTemplates.baseOreTemplate.AddComponent<Clearable>();
		EntityTemplates.baseOreTemplate.AddComponent<Prioritizable>();
		EntityTemplates.baseOreTemplate.AddComponent<KBatchedAnimController>();
		EntityTemplates.baseOreTemplate.AddComponent<SimTemperatureTransfer>();
		EntityTemplates.baseOreTemplate.AddComponent<Modifiers>();
		EntityTemplates.baseOreTemplate.AddOrGet<OccupyArea>().OccupiedCellsOffsets = new CellOffset[1];
		DecorProvider decorProvider = EntityTemplates.baseOreTemplate.AddOrGet<DecorProvider>();
		decorProvider.baseDecor = -10f;
		decorProvider.baseRadius = 1f;
		EntityTemplates.baseOreTemplate.AddOrGet<ElementChunk>();
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000E4A1 File Offset: 0x0000C6A1
	public static void DestroyBaseOreTemplates()
	{
		UnityEngine.Object.Destroy(EntityTemplates.baseOreTemplate);
		EntityTemplates.baseOreTemplate = null;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
	public static GameObject CreateOreEntity(SimHashes elementID, EntityTemplates.CollisionShape shape, float width, float height, List<Tag> additionalTags = null, float default_temperature = 293f)
	{
		Element element = ElementLoader.FindElementByHash(elementID);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EntityTemplates.baseOreTemplate);
		gameObject.name = element.name;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		KPrefabID kprefabID = gameObject.AddOrGet<KPrefabID>();
		kprefabID.PrefabTag = element.tag;
		kprefabID.InitializeTags(false);
		if (additionalTags != null)
		{
			foreach (Tag tag in additionalTags)
			{
				kprefabID.AddTag(tag, false);
			}
		}
		if (element.lowTemp < 296.15f && element.highTemp > 296.15f)
		{
			kprefabID.AddTag(GameTags.PedestalDisplayable, false);
		}
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(elementID, true);
		primaryElement.Mass = 1f;
		primaryElement.Temperature = default_temperature;
		Pickupable pickupable = gameObject.AddOrGet<Pickupable>();
		pickupable.SetWorkTime(5f);
		pickupable.sortOrder = element.buildMenuSort;
		gameObject.AddOrGet<KSelectable>().SetName(element.name);
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { element.substance.anim };
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.Front;
		kbatchedAnimController.initialAnim = "idle1";
		kbatchedAnimController.isMovable = true;
		gameObject = EntityTemplates.AddCollision(gameObject, shape, width, height);
		return gameObject;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000E604 File Offset: 0x0000C804
	public static GameObject CreateSolidOreEntity(SimHashes elementId, List<Tag> additionalTags = null)
	{
		return EntityTemplates.CreateOreEntity(elementId, EntityTemplates.CollisionShape.CIRCLE, 0.5f, 0.5f, additionalTags, 293f);
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000E61D File Offset: 0x0000C81D
	public static GameObject CreateLiquidOreEntity(SimHashes elementId, List<Tag> additionalTags = null)
	{
		GameObject gameObject = EntityTemplates.CreateOreEntity(elementId, EntityTemplates.CollisionShape.RECTANGLE, 0.5f, 0.6f, additionalTags, 293f);
		gameObject.AddOrGet<Dumpable>().SetWorkTime(5f);
		gameObject.AddOrGet<SubstanceChunk>();
		return gameObject;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000E64D File Offset: 0x0000C84D
	public static GameObject CreateGasOreEntity(SimHashes elementId, List<Tag> additionalTags = null)
	{
		GameObject gameObject = EntityTemplates.CreateOreEntity(elementId, EntityTemplates.CollisionShape.RECTANGLE, 0.5f, 0.6f, additionalTags, 293f);
		gameObject.AddOrGet<Dumpable>().SetWorkTime(5f);
		gameObject.AddOrGet<SubstanceChunk>();
		return gameObject;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000E680 File Offset: 0x0000C880
	public static GameObject ExtendEntityToFood(GameObject template, EdiblesManager.FoodInfo foodInfo)
	{
		template.AddOrGet<EntitySplitter>();
		if (foodInfo.CanRot)
		{
			Rottable.Def def = template.AddOrGetDef<Rottable.Def>();
			def.preserveTemperature = foodInfo.PreserveTemperature;
			def.rotTemperature = foodInfo.RotTemperature;
			def.spoilTime = foodInfo.SpoilTime;
			def.staleTime = foodInfo.StaleTime;
			EntityTemplates.CreateAndRegisterCompostableFromPrefab(template);
		}
		KPrefabID component = template.GetComponent<KPrefabID>();
		component.AddTag(GameTags.PedestalDisplayable, false);
		if (foodInfo.CaloriesPerUnit > 0f)
		{
			component.AddTag(GameTags.Edible, false);
			template.AddOrGet<Edible>().FoodInfo = foodInfo;
			component.instantiateFn += delegate(GameObject go)
			{
				go.GetComponent<Edible>().FoodInfo = foodInfo;
			};
			GameTags.DisplayAsCalories.Add(component.PrefabTag);
		}
		else
		{
			component.AddTag(GameTags.CookingIngredient, false);
			template.AddOrGet<HasSortOrder>();
		}
		return template;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000E778 File Offset: 0x0000C978
	public static GameObject ExtendEntityToMedicine(GameObject template, MedicineInfo medicineInfo)
	{
		template.AddOrGet<EntitySplitter>();
		KPrefabID component = template.GetComponent<KPrefabID>();
		global::Debug.Assert(component.PrefabID() == medicineInfo.id, "Tried assigning a medicine info to a non-matching prefab!");
		MedicinalPill medicinalPill = template.AddOrGet<MedicinalPill>();
		medicinalPill.info = medicineInfo;
		if (medicineInfo.doctorStationId == null)
		{
			template.AddOrGet<MedicinalPillWorkable>().pill = medicinalPill;
			component.AddTag(GameTags.Medicine, false);
		}
		else
		{
			component.AddTag(GameTags.MedicalSupplies, false);
			component.AddTag(medicineInfo.GetSupplyTag(), false);
		}
		return template;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000E800 File Offset: 0x0000CA00
	public static GameObject ExtendPlantToFertilizable(GameObject template, PlantElementAbsorber.ConsumeInfo[] fertilizers)
	{
		template.GetComponent<Modifiers>().initialAttributes.Add(Db.Get().PlantAttributes.FertilizerUsageMod.Id);
		HashedString idHash = Db.Get().ChoreTypes.FarmFetch.IdHash;
		foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in fertilizers)
		{
			ManualDeliveryKG manualDeliveryKG = template.AddComponent<ManualDeliveryKG>();
			manualDeliveryKG.RequestedItemTag = consumeInfo.tag;
			manualDeliveryKG.capacity = consumeInfo.massConsumptionRate * 600f * 3f;
			manualDeliveryKG.refillMass = consumeInfo.massConsumptionRate * 600f * 0.5f;
			manualDeliveryKG.MinimumMass = consumeInfo.massConsumptionRate * 600f * 0.5f;
			manualDeliveryKG.operationalRequirement = Operational.State.Functional;
			manualDeliveryKG.choreTypeIDHash = idHash;
		}
		KPrefabID component = template.GetComponent<KPrefabID>();
		FertilizationMonitor.Def def = template.AddOrGetDef<FertilizationMonitor.Def>();
		def.wrongFertilizerTestTag = GameTags.Solid;
		def.consumedElements = fertilizers;
		component.prefabInitFn += delegate(GameObject inst)
		{
			ManualDeliveryKG[] components = inst.GetComponents<ManualDeliveryKG>();
			for (int j = 0; j < components.Length; j++)
			{
				components[j].Pause(true, "init");
			}
		};
		return template;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000E907 File Offset: 0x0000CB07
	public static GameObject ExtendPlantToIrrigated(GameObject template, PlantElementAbsorber.ConsumeInfo info)
	{
		return EntityTemplates.ExtendPlantToIrrigated(template, new PlantElementAbsorber.ConsumeInfo[] { info });
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000E920 File Offset: 0x0000CB20
	public static GameObject ExtendPlantToIrrigated(GameObject template, PlantElementAbsorber.ConsumeInfo[] consume_info)
	{
		template.GetComponent<Modifiers>().initialAttributes.Add(Db.Get().PlantAttributes.FertilizerUsageMod.Id);
		HashedString idHash = Db.Get().ChoreTypes.FarmFetch.IdHash;
		foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in consume_info)
		{
			ManualDeliveryKG manualDeliveryKG = template.AddComponent<ManualDeliveryKG>();
			manualDeliveryKG.RequestedItemTag = consumeInfo.tag;
			manualDeliveryKG.capacity = consumeInfo.massConsumptionRate * 600f * 3f;
			manualDeliveryKG.refillMass = consumeInfo.massConsumptionRate * 600f * 0.5f;
			manualDeliveryKG.MinimumMass = consumeInfo.massConsumptionRate * 600f * 0.5f;
			manualDeliveryKG.operationalRequirement = Operational.State.Functional;
			manualDeliveryKG.choreTypeIDHash = idHash;
		}
		IrrigationMonitor.Def def = template.AddOrGetDef<IrrigationMonitor.Def>();
		def.wrongIrrigationTestTag = GameTags.Liquid;
		def.consumedElements = consume_info;
		return template;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000EA00 File Offset: 0x0000CC00
	public static GameObject CreateAndRegisterCompostableFromPrefab(GameObject original)
	{
		if (original.GetComponent<Compostable>() != null)
		{
			return null;
		}
		original.AddComponent<Compostable>().isMarkedForCompost = false;
		KPrefabID component = original.GetComponent<KPrefabID>();
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		string text = "Compost" + component.PrefabTag.Name;
		string text2 = MISC.TAGS.COMPOST_FORMAT.Replace("{Item}", component.PrefabTag.ProperName());
		gameObject.GetComponent<KPrefabID>().PrefabTag = TagManager.Create(text, text2);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.Compostable, false);
		gameObject.name = text2;
		gameObject.GetComponent<Compostable>().isMarkedForCompost = true;
		gameObject.GetComponent<KSelectable>().SetName(text2);
		gameObject.GetComponent<Compostable>().originalPrefab = original;
		gameObject.GetComponent<Compostable>().compostPrefab = gameObject;
		original.GetComponent<Compostable>().originalPrefab = original;
		original.GetComponent<Compostable>().compostPrefab = gameObject;
		Assets.AddPrefab(gameObject.GetComponent<KPrefabID>());
		return gameObject;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000EAEC File Offset: 0x0000CCEC
	public static GameObject CreateAndRegisterSeedForPlant(GameObject plant, SeedProducer.ProductionType productionType, string id, string name, string desc, KAnimFile anim, string initialAnim = "object", int numberOfSeeds = 1, List<Tag> additionalTags = null, SingleEntityReceptacle.ReceptacleDirection planterDirection = SingleEntityReceptacle.ReceptacleDirection.Top, Tag replantGroundTag = default(Tag), int sortOrder = 0, string domesticatedDescription = "", EntityTemplates.CollisionShape collisionShape = EntityTemplates.CollisionShape.CIRCLE, float width = 0.25f, float height = 0.25f, Recipe.Ingredient[] recipe_ingredients = null, string recipe_description = "", bool ignoreDefaultSeedTag = false)
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(id, name, desc, 1f, true, anim, initialAnim, Grid.SceneLayer.Front, collisionShape, width, height, true, SORTORDER.SEEDS + sortOrder, SimHashes.Creature, null);
		gameObject.AddOrGet<EntitySplitter>();
		GameObject gameObject2 = EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		PlantableSeed plantableSeed = gameObject.AddOrGet<PlantableSeed>();
		plantableSeed.PlantID = new Tag(plant.name);
		plantableSeed.replantGroundTag = replantGroundTag;
		plantableSeed.domesticatedDescription = domesticatedDescription;
		plantableSeed.direction = planterDirection;
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		foreach (Tag tag in additionalTags)
		{
			component.AddTag(tag, false);
		}
		if (!ignoreDefaultSeedTag)
		{
			component.AddTag(GameTags.Seed, false);
		}
		component.AddTag(GameTags.PedestalDisplayable, false);
		MutantPlant component2 = plant.GetComponent<MutantPlant>();
		if (component2 != null)
		{
			MutantPlant mutantPlant = gameObject.AddOrGet<MutantPlant>();
			MutantPlant mutantPlant2 = gameObject2.AddOrGet<MutantPlant>();
			mutantPlant.SpeciesID = component2.SpeciesID;
			mutantPlant2.SpeciesID = component2.SpeciesID;
		}
		Assets.AddPrefab(component);
		plant.AddOrGet<SeedProducer>().Configure(id, productionType, numberOfSeeds);
		return gameObject;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000EC18 File Offset: 0x0000CE18
	public static GameObject CreateAndRegisterPreview(string id, KAnimFile anim, string initial_anim, ObjectLayer object_layer, int width, int height)
	{
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, id, id, 1f, anim, initial_anim, Grid.SceneLayer.Front, width, height, TUNING.BUILDINGS.DECOR.NONE, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.UpdateComponentRequirement(false);
		gameObject.UpdateComponentRequirement(false);
		gameObject.AddOrGet<EntityPreview>().objectLayer = object_layer;
		OccupyArea occupyArea = gameObject.AddOrGet<OccupyArea>();
		occupyArea.objectLayers = new ObjectLayer[] { object_layer };
		occupyArea.ApplyToCells = false;
		gameObject.AddOrGet<Storage>();
		Assets.AddPrefab(gameObject.GetComponent<KPrefabID>());
		return gameObject;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000ECA1 File Offset: 0x0000CEA1
	public static GameObject CreateAndRegisterPreviewForPlant(GameObject seed, string id, KAnimFile anim, string initialAnim, int width, int height)
	{
		GameObject gameObject = EntityTemplates.CreateAndRegisterPreview(id, anim, initialAnim, ObjectLayer.Building, width, height);
		seed.GetComponent<PlantableSeed>().PreviewID = TagManager.Create(id);
		return gameObject;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
	public static CellOffset[] GenerateOffsets(int width, int height)
	{
		int num = width / 2;
		int num2 = num - width + 1;
		int num3 = 0;
		int num4 = height - 1;
		return EntityTemplates.GenerateOffsets(num2, num3, num, num4);
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
	private static CellOffset[] GenerateOffsets(int startX, int startY, int endX, int endY)
	{
		List<CellOffset> list = new List<CellOffset>();
		for (int i = startY; i <= endY; i++)
		{
			for (int j = startX; j <= endX; j++)
			{
				list.Add(new CellOffset
				{
					x = j,
					y = i
				});
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000ED38 File Offset: 0x0000CF38
	public static CellOffset[] GenerateHangingOffsets(int width, int height)
	{
		int num = width / 2;
		int num2 = num - width + 1;
		int num3 = -height + 1;
		int num4 = 0;
		return EntityTemplates.GenerateOffsets(num2, num3, num, num4);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000ED60 File Offset: 0x0000CF60
	public static GameObject AddCollision(GameObject template, EntityTemplates.CollisionShape shape, float width, float height)
	{
		if (shape != EntityTemplates.CollisionShape.RECTANGLE)
		{
			if (shape != EntityTemplates.CollisionShape.POLYGONAL)
			{
				template.AddOrGet<KCircleCollider2D>().radius = Mathf.Max(width, height);
			}
			else
			{
				template.AddOrGet<PolygonCollider2D>();
			}
		}
		else
		{
			template.AddOrGet<KBoxCollider2D>().size = new Vector2f(width, height);
		}
		return template;
	}

	// Token: 0x0400011E RID: 286
	private static GameObject selectableEntityTemplate;

	// Token: 0x0400011F RID: 287
	private static GameObject unselectableEntityTemplate;

	// Token: 0x04000120 RID: 288
	private static GameObject baseEntityTemplate;

	// Token: 0x04000121 RID: 289
	private static GameObject placedEntityTemplate;

	// Token: 0x04000122 RID: 290
	private static GameObject baseOreTemplate;

	// Token: 0x02000DE3 RID: 3555
	public enum CollisionShape
	{
		// Token: 0x04005075 RID: 20597
		CIRCLE,
		// Token: 0x04005076 RID: 20598
		RECTANGLE,
		// Token: 0x04005077 RID: 20599
		POLYGONAL
	}
}
