using System;
using TUNING;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class HydrogenEngineConfig : IBuildingConfig
{
	// Token: 0x060008D1 RID: 2257 RVA: 0x0003450A File Offset: 0x0003270A
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00034514 File Offset: 0x00032714
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HydrogenEngine";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_hydrogen_engine_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_LARGE = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_LARGE;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_LARGE, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x000345C4 File Offset: 0x000327C4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00034628 File Offset: 0x00032828
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0003462A File Offset: 0x0003282A
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0003462C File Offset: 0x0003282C
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngine rocketEngine = go.AddOrGet<RocketEngine>();
		rocketEngine.fuelTag = ElementLoader.FindElementByHash(SimHashes.LiquidHydrogen).tag;
		rocketEngine.efficiency = ROCKETRY.ENGINE_EFFICIENCY.STRONG;
		rocketEngine.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngine.exhaustElement = SimHashes.Steam;
		rocketEngine.exhaustTemperature = 2000f;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_hydrogen_engine_bg_kanim", false);
	}

	// Token: 0x0400058D RID: 1421
	public const string ID = "HydrogenEngine";
}
