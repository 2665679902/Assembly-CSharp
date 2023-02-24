using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class GantryConfig : IBuildingConfig
{
	// Token: 0x060007C8 RID: 1992 RVA: 0x0002DA9C File Offset: 0x0002BC9C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Gantry";
		int num = 6;
		int num2 = 2;
		string text2 = "gantry_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 3200f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, none, 1f);
		buildingDef.ObjectLayer = ObjectLayer.Gantry;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.Entombable = true;
		buildingDef.IsFoundation = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(-2, 0);
		buildingDef.EnergyConsumptionWhenActive = 1200f;
		buildingDef.ExhaustKilowattsWhenActive = 1f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.AudioCategory = "Metal";
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(Gantry.PORT_ID, new CellOffset(-1, 1), STRINGS.BUILDINGS.PREFABS.GANTRY.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.GANTRY.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.GANTRY.LOGIC_PORT_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0002DBAC File Offset: 0x0002BDAC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Gantry>();
		go.AddOrGetDef<MakeBaseSolid.Def>().solidOffsets = GantryConfig.SOLID_OFFSETS;
		FakeFloorAdder fakeFloorAdder = go.AddOrGet<FakeFloorAdder>();
		fakeFloorAdder.floorOffsets = new CellOffset[]
		{
			new CellOffset(0, 1),
			new CellOffset(1, 1),
			new CellOffset(2, 1),
			new CellOffset(3, 1)
		};
		fakeFloorAdder.initiallyActive = false;
		UnityEngine.Object.DestroyImmediate(go.GetComponent<LogicOperationalController>());
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0002DC45 File Offset: 0x0002BE45
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		go.AddOrGetDef<MakeBaseSolid.Def>().solidOffsets = GantryConfig.SOLID_OFFSETS;
	}

	// Token: 0x04000500 RID: 1280
	public const string ID = "Gantry";

	// Token: 0x04000501 RID: 1281
	private static readonly CellOffset[] SOLID_OFFSETS = new CellOffset[]
	{
		new CellOffset(-2, 1),
		new CellOffset(-1, 1)
	};
}
