using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class GeneticAnalysisStationConfig : IBuildingConfig
{
	// Token: 0x06000840 RID: 2112 RVA: 0x000302F1 File Offset: 0x0002E4F1
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x000302F8 File Offset: 0x0002E4F8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GeneticAnalysisStation";
		int num = 7;
		int num2 = 2;
		string text2 = "genetic_analysisstation_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		BuildingTemplates.CreateElectricalBuildingDef(buildingDef);
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.Deprecated = !DlcManager.FeaturePlantMutationsEnabled();
		return buildingDef;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0003038C File Offset: 0x0002E58C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGetDef<GeneticAnalysisStation.Def>();
		go.AddOrGet<GeneticAnalysisStationWorkable>().finishedSeedDropOffset = new Vector3(-3f, 1.5f, 0f);
		Prioritizable.AddRef(go);
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGetDef<PoweredActiveController.Def>();
		Storage storage = go.AddOrGet<Storage>();
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		manualDeliveryKG.RequestedItemTag = GameTags.UnidentifiedSeed;
		manualDeliveryKG.refillMass = 1.1f;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.capacity = 5f;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00030449 File Offset: 0x0002E649
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0003044C File Offset: 0x0002E64C
	public override void ConfigurePost(BuildingDef def)
	{
		List<Tag> list = new List<Tag>();
		foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.CropSeed))
		{
			if (gameObject.GetComponent<MutantPlant>() != null)
			{
				list.Add(gameObject.PrefabID());
			}
		}
		def.BuildingComplete.GetComponent<Storage>().storageFilters = list;
	}

	// Token: 0x04000531 RID: 1329
	public const string ID = "GeneticAnalysisStation";
}
