using System;
using TUNING;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class IceCooledFanConfig : IBuildingConfig
{
	// Token: 0x060008DF RID: 2271 RVA: 0x000349EC File Offset: 0x00032BEC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "IceCooledFan";
		int num = 2;
		int num2 = 2;
		string text2 = "fanice_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.SelfHeatKilowattsWhenActive = -this.COOLING_RATE * 0.25f;
		buildingDef.ExhaustKilowattsWhenActive = -this.COOLING_RATE * 0.75f;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Temperature.ID;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00034A78 File Offset: 0x00032C78
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddComponent<Storage>();
		storage.capacityKg = 50f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		Storage storage2 = go.AddComponent<Storage>();
		storage2.capacityKg = 50f;
		storage2.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<MinimumOperatingTemperature>().minimumTemperature = 273.15f;
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
		IceCooledFan iceCooledFan = go.AddOrGet<IceCooledFan>();
		iceCooledFan.coolingRate = this.COOLING_RATE;
		iceCooledFan.targetTemperature = this.TARGET_TEMPERATURE;
		iceCooledFan.iceStorage = storage;
		iceCooledFan.liquidStorage = storage2;
		iceCooledFan.minCooledTemperature = 278.15f;
		iceCooledFan.minEnvironmentMass = 0.25f;
		iceCooledFan.minCoolingRange = new Vector2I(-2, 0);
		iceCooledFan.maxCoolingRange = new Vector2I(2, 4);
		iceCooledFan.consumptionTag = GameTags.IceOre;
		ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = GameTags.IceOre;
		manualDeliveryKG.capacity = this.ICE_CAPACITY;
		manualDeliveryKG.refillMass = this.ICE_CAPACITY * 0.2f;
		manualDeliveryKG.MinimumMass = 10f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		go.AddOrGet<IceCooledFanWorkable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_icefan_kanim") };
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00034BCC File Offset: 0x00032DCC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(game_object);
			StructureTemperaturePayload payload = GameComps.StructureTemperatures.GetPayload(handle);
			int num = Grid.PosToCell(game_object);
			payload.OverrideExtents(new Extents(num, IceCooledFanConfig.overrideOffsets));
			GameComps.StructureTemperatures.SetPayload(handle, ref payload);
		};
	}

	// Token: 0x04000590 RID: 1424
	public const string ID = "IceCooledFan";

	// Token: 0x04000591 RID: 1425
	private float COOLING_RATE = 32f;

	// Token: 0x04000592 RID: 1426
	private float TARGET_TEMPERATURE = 278.15f;

	// Token: 0x04000593 RID: 1427
	private float ICE_CAPACITY = 50f;

	// Token: 0x04000594 RID: 1428
	private static readonly CellOffset[] overrideOffsets = new CellOffset[]
	{
		new CellOffset(-2, 1),
		new CellOffset(2, 1),
		new CellOffset(-1, 0),
		new CellOffset(1, 0)
	};
}
