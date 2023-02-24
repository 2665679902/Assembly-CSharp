using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class TeleportalPadConfig : IBuildingConfig
{
	// Token: 0x06001063 RID: 4195 RVA: 0x000592F4 File Offset: 0x000574F4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "TeleportalPad";
		int num = 4;
		int num2 = 4;
		string text2 = "hqbase_kanim";
		int num3 = 250;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER7;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.BONUS.TIER5, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = 400f;
		buildingDef.DefaultAnimState = "idle";
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(2, 0);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port>
		{
			LogicPorts.Port.InputPort("TeleportalPad_ID_PORT_0", new CellOffset(-1, 0), STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_INACTIVE, false, false),
			LogicPorts.Port.InputPort("TeleportalPad_ID_PORT_1", new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_INACTIVE, false, false),
			LogicPorts.Port.InputPort("TeleportalPad_ID_PORT_2", new CellOffset(1, 0), STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_INACTIVE, false, false),
			LogicPorts.Port.InputPort("TeleportalPad_ID_PORT_3", new CellOffset(2, 0), STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_INACTIVE, false, false),
			LogicPorts.Port.InputPort(LogicOperationalController.PORT_ID, new CellOffset(-1, 1), STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.TELEPORTALPAD.LOGIC_PORT_INACTIVE, false, false)
		};
		buildingDef.EnergyConsumptionWhenActive = 1600f;
		buildingDef.ExhaustKilowattsWhenActive = 16f;
		buildingDef.SelfHeatKilowattsWhenActive = 64f;
		SoundEventVolumeCache.instance.AddVolume("hqbase_kanim", "Portal_LP", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("hqbase_kanim", "Portal_open", NOISE_POLLUTION.NOISY.TIER4);
		SoundEventVolumeCache.instance.AddVolume("hqbase_kanim", "Portal_close", NOISE_POLLUTION.NOISY.TIER4);
		return buildingDef;
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x00059525 File Offset: 0x00057725
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<TeleportalPad>();
		go.AddOrGet<Teleporter>();
		go.AddOrGet<PrimaryElement>().SetElement(SimHashes.Unobtanium, true);
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x00059546 File Offset: 0x00057746
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
	}

	// Token: 0x040008EB RID: 2283
	public const string ID = "TeleportalPad";

	// Token: 0x040008EC RID: 2284
	public const string PORTAL_ID_PORT_0 = "TeleportalPad_ID_PORT_0";

	// Token: 0x040008ED RID: 2285
	public const string PORTAL_ID_PORT_1 = "TeleportalPad_ID_PORT_1";

	// Token: 0x040008EE RID: 2286
	public const string PORTAL_ID_PORT_2 = "TeleportalPad_ID_PORT_2";

	// Token: 0x040008EF RID: 2287
	public const string PORTAL_ID_PORT_3 = "TeleportalPad_ID_PORT_3";
}
