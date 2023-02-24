using System;
using TUNING;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class SunLampConfig : IBuildingConfig
{
	// Token: 0x0600104C RID: 4172 RVA: 0x00058854 File Offset: 0x00056A54
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SunLamp";
		int num = 2;
		int num2 = 4;
		string text2 = "sun_lamp_kanim";
		int num3 = 10;
		float num4 = 60f;
		float[] array = new float[] { 200f, 50f };
		string[] array2 = new string[] { "RefinedMetal", "Glass" };
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER3, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 960f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.ExhaustKilowattsWhenActive = 1f;
		buildingDef.ViewMode = OverlayModes.Light.ID;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x000588FC File Offset: 0x00056AFC
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		LightShapePreview lightShapePreview = go.AddComponent<LightShapePreview>();
		lightShapePreview.lux = 40000;
		lightShapePreview.radius = 16f;
		lightShapePreview.shape = global::LightShape.Cone;
		lightShapePreview.offset = new CellOffset((int)LIGHT2D.SUNLAMP_OFFSET.x, (int)LIGHT2D.SUNLAMP_OFFSET.y);
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0005894C File Offset: 0x00056B4C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource, false);
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x00058960 File Offset: 0x00056B60
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<EnergyConsumer>();
		go.AddOrGet<LoopingSounds>();
		Light2D light2D = go.AddOrGet<Light2D>();
		light2D.Lux = 40000;
		light2D.overlayColour = LIGHT2D.SUNLAMP_OVERLAYCOLOR;
		light2D.Color = LIGHT2D.SUNLAMP_COLOR;
		light2D.Range = 16f;
		light2D.Angle = 5.2f;
		light2D.Direction = LIGHT2D.SUNLAMP_DIRECTION;
		light2D.Offset = LIGHT2D.SUNLAMP_OFFSET;
		light2D.shape = global::LightShape.Cone;
		light2D.drawOverlay = true;
		go.AddOrGetDef<LightController.Def>();
	}

	// Token: 0x040008E0 RID: 2272
	public const string ID = "SunLamp";
}
