using System;
using TUNING;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class GasVentHighPressureConfig : IBuildingConfig
{
	// Token: 0x06000825 RID: 2085 RVA: 0x0002F7CC File Offset: 0x0002D9CC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasVentHighPressure";
		int num = 1;
		int num2 = 1;
		string text2 = "ventgas_powered_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] array = new string[] { "RefinedMetal", "Plastic" };
		float[] array2 = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		};
		string[] array3 = array;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, "GasVentHighPressure");
		SoundEventVolumeCache.instance.AddVolume("ventgas_kanim", "GasVent_clunk", NOISE_POLLUTION.NOISY.TIER0);
		return buildingDef;
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0002F8BC File Offset: 0x0002DABC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<Exhaust>();
		go.AddOrGet<LogicOperationalController>();
		Vent vent = go.AddOrGet<Vent>();
		vent.conduitType = ConduitType.Gas;
		vent.endpointType = Endpoint.Sink;
		vent.overpressureMass = 20f;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.ignoreMinMassCheck = true;
		BuildingTemplates.CreateDefaultStorage(go, false).showInUI = true;
		go.AddOrGet<SimpleVent>();
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0002F923 File Offset: 0x0002DB23
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<VentController.Def>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000529 RID: 1321
	public const string ID = "GasVentHighPressure";

	// Token: 0x0400052A RID: 1322
	private const ConduitType CONDUIT_TYPE = ConduitType.Gas;

	// Token: 0x0400052B RID: 1323
	public const float OVERPRESSURE_MASS = 20f;
}
