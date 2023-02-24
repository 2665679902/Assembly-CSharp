using System;
using TUNING;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class RadiationLightConfig : IBuildingConfig
{
	// Token: 0x06000E7E RID: 3710 RVA: 0x0004E678 File Offset: 0x0004C878
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0004E680 File Offset: 0x0004C880
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RadiationLight";
		int num = 1;
		int num2 = 1;
		string text2 = "radiation_lamp_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnWall;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x0004E700 File Offset: 0x0004C900
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showInUI = true;
		storage.capacityKg = 50f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = this.FUEL_ELEMENT;
		manualDeliveryKG.capacity = 50f;
		manualDeliveryKG.refillMass = 5f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		RadiationEmitter radiationEmitter = go.AddComponent<RadiationEmitter>();
		radiationEmitter.emitAngle = 90f;
		radiationEmitter.emitDirection = 0f;
		radiationEmitter.emissionOffset = Vector3.right;
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.emitRadiusX = 16;
		radiationEmitter.emitRadiusY = 4;
		radiationEmitter.emitRads = 240f;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(this.FUEL_ELEMENT, 0.016666668f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.008333334f, this.WASTE_ELEMENT, 0f, false, true, 0f, 0.5f, 0.5f, byte.MaxValue, 0, true)
		};
		ElementDropper elementDropper = go.AddOrGet<ElementDropper>();
		elementDropper.emitTag = this.WASTE_ELEMENT.CreateTag();
		elementDropper.emitMass = 5f;
		RadiationLight radiationLight = go.AddComponent<RadiationLight>();
		radiationLight.elementToConsume = this.FUEL_ELEMENT;
		radiationLight.consumptionRate = 0.016666668f;
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0004E876 File Offset: 0x0004CA76
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0004E878 File Offset: 0x0004CA78
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x040007F2 RID: 2034
	public const string ID = "RadiationLight";

	// Token: 0x040007F3 RID: 2035
	private Tag FUEL_ELEMENT = SimHashes.UraniumOre.CreateTag();

	// Token: 0x040007F4 RID: 2036
	private SimHashes WASTE_ELEMENT = SimHashes.DepletedUranium;

	// Token: 0x040007F5 RID: 2037
	private const float FUEL_PER_CYCLE = 10f;

	// Token: 0x040007F6 RID: 2038
	private const float CYCLES_PER_REFILL = 5f;

	// Token: 0x040007F7 RID: 2039
	private const float FUEL_TO_WASTE_RATIO = 0.5f;

	// Token: 0x040007F8 RID: 2040
	private const float FUEL_STORAGE_AMOUNT = 50f;

	// Token: 0x040007F9 RID: 2041
	private const float FUEL_CONSUMPTION_RATE = 0.016666668f;

	// Token: 0x040007FA RID: 2042
	private const short RAD_LIGHT_SIZE_X = 16;

	// Token: 0x040007FB RID: 2043
	private const short RAD_LIGHT_SIZE_Y = 4;
}
