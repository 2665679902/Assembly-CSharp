﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class VerticalWindTunnelConfig : IBuildingConfig
{
	// Token: 0x060010D2 RID: 4306 RVA: 0x0005AF9C File Offset: 0x0005919C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "VerticalWindTunnel";
		int num = 5;
		int num2 = 6;
		string text2 = "wind_tunnel_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER6;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plastics, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 1200f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		return buildingDef;
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x0005B030 File Offset: 0x00059230
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		VerticalWindTunnel verticalWindTunnel = go.AddOrGet<VerticalWindTunnel>();
		verticalWindTunnel.specificEffect = "VerticalWindTunnel";
		verticalWindTunnel.trackingEffect = "RecentlyVerticalWindTunnel";
		verticalWindTunnel.basePriority = RELAXATION.PRIORITY.TIER4;
		verticalWindTunnel.displacementAmount_DescriptorOnly = 3f;
		ElementConsumer elementConsumer = go.AddComponent<ElementConsumer>();
		elementConsumer.configuration = ElementConsumer.Configuration.AllGas;
		elementConsumer.consumptionRate = 3f;
		elementConsumer.storeOnConsume = false;
		elementConsumer.showInStatusPanel = false;
		elementConsumer.consumptionRadius = 2;
		elementConsumer.sampleCellOffset = new Vector3(0f, -2f, 0f);
		elementConsumer.showDescriptor = false;
		ElementConsumer elementConsumer2 = go.AddComponent<ElementConsumer>();
		elementConsumer2.configuration = ElementConsumer.Configuration.AllGas;
		elementConsumer2.consumptionRate = 3f;
		elementConsumer2.storeOnConsume = false;
		elementConsumer2.showInStatusPanel = false;
		elementConsumer2.consumptionRadius = 2;
		elementConsumer2.sampleCellOffset = new Vector3(0f, 6f, 0f);
		elementConsumer2.showDescriptor = false;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0005B146 File Offset: 0x00059346
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400091C RID: 2332
	public const string ID = "VerticalWindTunnel";

	// Token: 0x0400091D RID: 2333
	private const float DISPLACEMENT_AMOUNT = 3f;
}
