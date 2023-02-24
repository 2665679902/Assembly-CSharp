using System;
using TUNING;
using UnityEngine;

// Token: 0x02000020 RID: 32
public abstract class BaseBatteryConfig : IBuildingConfig
{
	// Token: 0x0600008E RID: 142 RVA: 0x000058C8 File Offset: 0x00003AC8
	public BuildingDef CreateBuildingDef(string id, int width, int height, int hitpoints, string anim, float construction_time, float[] construction_mass, string[] construction_materials, float melting_point, float exhaust_temperature_active, float self_heat_kilowatts_active, EffectorValues decor, EffectorValues noise)
	{
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, construction_mass, construction_materials, melting_point, buildLocationRule, decor, tier, 0.2f);
		buildingDef.ExhaustKilowattsWhenActive = exhaust_temperature_active;
		buildingDef.SelfHeatKilowattsWhenActive = self_heat_kilowatts_active;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerOutput = true;
		buildingDef.UseWhitePowerOutputConnectorColour = true;
		return buildingDef;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00005935 File Offset: 0x00003B35
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddComponent<RequireInputs>();
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000593E File Offset: 0x00003B3E
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Battery>().powerSortOrder = 1000;
		go.AddOrGetDef<PoweredActiveController.Def>();
	}
}
