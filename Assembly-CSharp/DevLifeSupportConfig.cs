using System;
using TUNING;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class DevLifeSupportConfig : IBuildingConfig
{
	// Token: 0x060001AD RID: 429 RVA: 0x0000C18C File Offset: 0x0000A38C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DevLifeSupport";
		int num = 1;
		int num2 = 1;
		string text2 = "dev_life_support_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER3, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.DebugOnly = true;
		return buildingDef;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000C200 File Offset: 0x0000A400
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.DevBuilding);
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showInUI = true;
		storage.capacityKg = 200f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		CellOffset cellOffset = new CellOffset(0, 1);
		ElementEmitter elementEmitter = go.AddOrGet<ElementEmitter>();
		elementEmitter.outputElement = new ElementConverter.OutputElement(50.000004f, SimHashes.Oxygen, 303.15f, false, false, (float)cellOffset.x, (float)cellOffset.y, 1f, byte.MaxValue, 0, true);
		elementEmitter.emissionFrequency = 1f;
		elementEmitter.maxPressure = 1.5f;
		PassiveElementConsumer passiveElementConsumer = go.AddOrGet<PassiveElementConsumer>();
		passiveElementConsumer.elementToConsume = SimHashes.CarbonDioxide;
		passiveElementConsumer.consumptionRate = 50.000004f;
		passiveElementConsumer.capacityKG = 50.000004f;
		passiveElementConsumer.consumptionRadius = 10;
		passiveElementConsumer.showInStatusPanel = true;
		passiveElementConsumer.sampleCellOffset = new Vector3(0f, 0f, 0f);
		passiveElementConsumer.isRequired = false;
		passiveElementConsumer.storeOnConsume = false;
		passiveElementConsumer.showDescriptor = false;
		passiveElementConsumer.ignoreActiveChanged = true;
		go.AddOrGet<DevLifeSupport>();
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000C307 File Offset: 0x0000A507
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000FA RID: 250
	public const string ID = "DevLifeSupport";

	// Token: 0x040000FB RID: 251
	private const float OXYGEN_GENERATION_RATE = 50.000004f;

	// Token: 0x040000FC RID: 252
	private const float OXYGEN_TEMPERATURE = 303.15f;

	// Token: 0x040000FD RID: 253
	private const float OXYGEN_MAX_PRESSURE = 1.5f;

	// Token: 0x040000FE RID: 254
	private const float CO2_CONSUMPTION_RATE = 50.000004f;
}
