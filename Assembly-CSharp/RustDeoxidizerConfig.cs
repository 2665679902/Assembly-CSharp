using System;
using TUNING;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class RustDeoxidizerConfig : IBuildingConfig
{
	// Token: 0x06000F39 RID: 3897 RVA: 0x00052D24 File Offset: 0x00050F24
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RustDeoxidizer";
		int num = 2;
		int num2 = 3;
		string text2 = "rust_deoxidizer_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(1, 1));
		buildingDef.ViewMode = OverlayModes.Oxygen.ID;
		buildingDef.AudioCategory = "HollowMetal";
		return buildingDef;
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00052DC8 File Offset: 0x00050FC8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<RustDeoxidizer>().maxMass = 1.8f;
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = new Tag("Rust");
		manualDeliveryKG.capacity = 585f;
		manualDeliveryKG.refillMass = 193.05f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		ManualDeliveryKG manualDeliveryKG2 = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG2.SetStorage(storage);
		manualDeliveryKG2.RequestedItemTag = new Tag("Salt");
		manualDeliveryKG2.capacity = 195f;
		manualDeliveryKG2.refillMass = 64.350006f;
		manualDeliveryKG2.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(new Tag("Rust"), 0.75f, true),
			new ElementConverter.ConsumedElement(new Tag("Salt"), 0.25f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.57f, SimHashes.Oxygen, 348.15f, false, false, 0f, 1f, 1f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.029999971f, SimHashes.ChlorineGas, 348.15f, false, false, 0f, 1f, 1f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.4f, SimHashes.IronOre, 348.15f, false, true, 0f, 1f, 1f, byte.MaxValue, 0, true)
		};
		ElementDropper elementDropper = go.AddComponent<ElementDropper>();
		elementDropper.emitMass = 24f;
		elementDropper.emitTag = SimHashes.IronOre.CreateTag();
		elementDropper.emitOffset = new Vector3(0f, 1f, 0f);
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00052FD7 File Offset: 0x000511D7
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x0400086A RID: 2154
	public const string ID = "RustDeoxidizer";

	// Token: 0x0400086B RID: 2155
	private const float RUST_KG_CONSUMPTION_RATE = 0.75f;

	// Token: 0x0400086C RID: 2156
	private const float SALT_KG_CONSUMPTION_RATE = 0.25f;

	// Token: 0x0400086D RID: 2157
	private const float RUST_KG_PER_REFILL = 585f;

	// Token: 0x0400086E RID: 2158
	private const float SALT_KG_PER_REFILL = 195f;

	// Token: 0x0400086F RID: 2159
	private const float TOTAL_CONSUMPTION_RATE = 1f;

	// Token: 0x04000870 RID: 2160
	private const float IRON_CONVERSION_RATIO = 0.4f;

	// Token: 0x04000871 RID: 2161
	private const float OXYGEN_CONVERSION_RATIO = 0.57f;

	// Token: 0x04000872 RID: 2162
	private const float CHLORINE_CONVERSION_RATIO = 0.029999971f;

	// Token: 0x04000873 RID: 2163
	public const float OXYGEN_TEMPERATURE = 348.15f;
}
