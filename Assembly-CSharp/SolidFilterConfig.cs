using System;
using TUNING;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class SolidFilterConfig : IBuildingConfig
{
	// Token: 0x06000FC3 RID: 4035 RVA: 0x000558C8 File Offset: 0x00053AC8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolidFilter";
		int num = 3;
		int num2 = 1;
		string text2 = "filter_material_conveyor_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.InputConduitType = ConduitType.Solid;
		buildingDef.OutputConduitType = ConduitType.Solid;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		buildingDef.PermittedRotations = PermittedRotations.R360;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "SolidFilter");
		return buildingDef;
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00055991 File Offset: 0x00053B91
	private void AttachPort(GameObject go)
	{
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.secondaryPort;
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x000559A4 File Offset: 0x00053BA4
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AttachPort(go);
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x000559B5 File Offset: 0x00053BB5
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AttachPort(go);
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x000559C5 File Offset: 0x00053BC5
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<ElementFilter>().portInfo = this.secondaryPort;
		go.AddOrGet<Filterable>().filterElementState = Filterable.ElementState.Solid;
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x000559F5 File Offset: 0x00053BF5
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<PoweredActiveController.Def>().showWorkingStatus = true;
	}

	// Token: 0x0400089C RID: 2204
	public const string ID = "SolidFilter";

	// Token: 0x0400089D RID: 2205
	private const ConduitType CONDUIT_TYPE = ConduitType.Solid;

	// Token: 0x0400089E RID: 2206
	private ConduitPortInfo secondaryPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(0, 0));
}
