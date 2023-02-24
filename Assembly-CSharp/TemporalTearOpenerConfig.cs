using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class TemporalTearOpenerConfig : IBuildingConfig
{
	// Token: 0x06001072 RID: 4210 RVA: 0x000597F6 File Offset: 0x000579F6
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x00059800 File Offset: 0x00057A00
	public override BuildingDef CreateBuildingDef()
	{
		string text = "TemporalTearOpener";
		int num = 3;
		int num2 = 4;
		string text2 = "temporal_tear_opener_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.BONUS.TIER2, tier2, 0.2f);
		buildingDef.DefaultAnimState = "off";
		buildingDef.Entombable = false;
		buildingDef.Invincible = true;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 2);
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_ACTIVE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x000598C4 File Offset: 0x00057AC4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.capacity = 1000f;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
		TemporalTearOpener.Def def = go.AddOrGetDef<TemporalTearOpener.Def>();
		def.numParticlesToOpen = 10000f;
		def.consumeRate = 5f;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x00059946 File Offset: 0x00057B46
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
	}

	// Token: 0x040008F6 RID: 2294
	public const string ID = "TemporalTearOpener";

	// Token: 0x040008F7 RID: 2295
	public const string PORT_ID = "HEP_STORAGE";

	// Token: 0x040008F8 RID: 2296
	public const float PARTICLES_CAPACITY = 1000f;

	// Token: 0x040008F9 RID: 2297
	public const float NUM_PARTICLES_TO_OPEN_TEAR = 10000f;

	// Token: 0x040008FA RID: 2298
	public const float PARTICLE_CONSUME_RATE = 5f;
}
