using System;
using TUNING;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class JetSuitLockerConfig : IBuildingConfig
{
	// Token: 0x06000947 RID: 2375 RVA: 0x00035CC0 File Offset: 0x00033EC0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "JetSuitLocker";
		int num = 2;
		int num2 = 4;
		string text2 = "changingarea_jetsuit_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float[] array = new float[] { BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0] };
		string[] array2 = refined_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.PreventIdleTraversalPastBuilding = true;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "JetSuitLocker");
		return buildingDef;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00035D4F File Offset: 0x00033F4F
	private void AttachPort(GameObject go)
	{
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.secondaryInputPort;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00035D64 File Offset: 0x00033F64
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<SuitLocker>().OutfitTags = new Tag[] { GameTags.JetSuit };
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.capacityKG = 200f;
		go.AddComponent<JetSuitLocker>().portInfo = this.secondaryInputPort;
		go.AddOrGet<AnimTileable>().tags = new Tag[]
		{
			new Tag("JetSuitLocker"),
			new Tag("JetSuitMarker")
		};
		go.AddOrGet<Storage>().capacityKg = 500f;
		Prioritizable.AddRef(go);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00035E2D File Offset: 0x0003402D
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AttachPort(go);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00035E3E File Offset: 0x0003403E
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AttachPort(go);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00035E4E File Offset: 0x0003404E
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
	}

	// Token: 0x040005CC RID: 1484
	public const string ID = "JetSuitLocker";

	// Token: 0x040005CD RID: 1485
	public const float O2_CAPACITY = 200f;

	// Token: 0x040005CE RID: 1486
	public const float SUIT_CAPACITY = 200f;

	// Token: 0x040005CF RID: 1487
	private ConduitPortInfo secondaryInputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(0, 1));
}
