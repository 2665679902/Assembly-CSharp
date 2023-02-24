using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class GlomConfig : IEntityConfig
{
	// Token: 0x0600044C RID: 1100 RVA: 0x0001FE46 File Offset: 0x0001E046
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0001FE50 File Offset: 0x0001E050
	public GameObject CreatePrefab()
	{
		string text = STRINGS.CREATURES.SPECIES.GLOM.NAME;
		string text2 = "Glom";
		string text3 = text;
		string text4 = STRINGS.CREATURES.SPECIES.GLOM.DESC;
		float num = 25f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text2, text3, text4, num, Assets.GetAnim("glom_kanim"), "idle_loop", Grid.SceneLayer.Creatures, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		Db.Get().CreateTrait("GlomBaseTrait", text, text, null, false, null, true, true).Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, text, false, false, true));
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Creatures.Walker, false);
		component.AddTag(GameTags.OriginalCreature, false);
		component.prefabInitFn += delegate(GameObject inst)
		{
			inst.GetAttributes().Add(Db.Get().Attributes.MaxUnderwaterTravelCost);
		};
		EntityTemplates.ExtendEntityToBasicCreature(gameObject, FactionManager.FactionID.Pest, "GlomBaseTrait", "WalkerNavGrid1x1", NavType.Floor, 32, 2f, "", 0, true, true, 293.15f, 393.15f, 273.15f, 423.15f);
		gameObject.AddWeapon(1f, 1f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.Single, 1, 0f);
		gameObject.AddOrGet<Trappable>();
		gameObject.AddOrGetDef<ThreatMonitor.Def>();
		gameObject.AddOrGetDef<CreatureFallMonitor.Def>();
		ElementDropperMonitor.Def def = gameObject.AddOrGetDef<ElementDropperMonitor.Def>();
		def.dirtyEmitElement = SimHashes.ContaminatedOxygen;
		def.dirtyProbabilityPercent = 25f;
		def.dirtyCellToTargetMass = 1f;
		def.dirtyMassPerDirty = 0.2f;
		def.dirtyMassReleaseOnDeath = 3f;
		def.emitDiseaseIdx = Db.Get().Diseases.GetIndex("SlimeLung");
		def.emitDiseasePerKg = 1000f;
		gameObject.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = 0;
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.GetComponent<LoopingSounds>().updatePosition = true;
		gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = "SlimeLung";
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_movement_short", NOISE_POLLUTION.CREATURES.TIER2);
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_jump", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_land", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_expel", NOISE_POLLUTION.CREATURES.TIER4);
		EntityTemplates.CreateAndRegisterBaggedCreature(gameObject, true, false, false);
		ChoreTable.Builder builder = new ChoreTable.Builder().Add(new DeathStates.Def(), true, -1).Add(new TrappedStates.Def(), true, -1).Add(new BaggedStates.Def(), true, -1)
			.Add(new FallStates.Def(), true, -1)
			.Add(new StunnedStates.Def(), true, -1)
			.Add(new DrowningStates.Def(), true, -1)
			.Add(new DebugGoToStates.Def(), true, -1)
			.Add(new FleeStates.Def(), true, -1)
			.Add(new DropElementStates.Def(), true, -1)
			.Add(new IdleStates.Def(), true, -1);
		EntityTemplates.AddCreatureBrain(gameObject, builder, GameTags.Creatures.Species.GlomSpecies, null);
		return gameObject;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0002012B File Offset: 0x0001E32B
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0002012D File Offset: 0x0001E32D
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002D5 RID: 725
	public const string ID = "Glom";

	// Token: 0x040002D6 RID: 726
	public const string BASE_TRAIT_ID = "GlomBaseTrait";

	// Token: 0x040002D7 RID: 727
	public const SimHashes dirtyEmitElement = SimHashes.ContaminatedOxygen;

	// Token: 0x040002D8 RID: 728
	public const float dirtyProbabilityPercent = 25f;

	// Token: 0x040002D9 RID: 729
	public const float dirtyCellToTargetMass = 1f;

	// Token: 0x040002DA RID: 730
	public const float dirtyMassPerDirty = 0.2f;

	// Token: 0x040002DB RID: 731
	public const float dirtyMassReleaseOnDeath = 3f;

	// Token: 0x040002DC RID: 732
	public const string emitDisease = "SlimeLung";

	// Token: 0x040002DD RID: 733
	public const int emitDiseasePerKg = 1000;
}
