﻿using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class SteamTurbineConfig2 : IBuildingConfig
{
	// Token: 0x06001021 RID: 4129 RVA: 0x00057270 File Offset: 0x00055470
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SteamTurbine2";
		int num = 5;
		int num2 = 3;
		string text2 = "steamturbine2_kanim";
		int num3 = 30;
		float num4 = 60f;
		string[] array = new string[] { "RefinedMetal", "Plastic" };
		float[] array2 = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER5[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0]
		};
		string[] array3 = array;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 1f);
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(2, 2);
		buildingDef.GeneratorWattageRating = SteamTurbineConfig2.MAX_WATTAGE;
		buildingDef.GeneratorBaseCapacity = SteamTurbineConfig2.MAX_WATTAGE;
		buildingDef.Entombable = true;
		buildingDef.IsFoundation = false;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(1, 0);
		buildingDef.OverheatTemperature = 1273.15f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		return buildingDef;
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0005736F File Offset: 0x0005556F
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.CanPowerTinker.Id;
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x00057398 File Offset: 0x00055598
	public override void DoPostConfigureComplete(GameObject go)
	{
		Storage storage = go.AddComponent<Storage>();
		storage.showDescriptor = false;
		storage.showInUI = false;
		storage.storageFilters = STORAGEFILTERS.LIQUIDS;
		storage.SetDefaultStoredItemModifiers(SteamTurbineConfig2.StoredItemModifiers);
		storage.capacityKg = 10f;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.showDescriptor = false;
		storage2.showInUI = false;
		storage2.storageFilters = STORAGEFILTERS.GASES;
		storage2.SetDefaultStoredItemModifiers(SteamTurbineConfig2.StoredItemModifiers);
		SteamTurbine steamTurbine = go.AddOrGet<SteamTurbine>();
		steamTurbine.srcElem = SimHashes.Steam;
		steamTurbine.destElem = SimHashes.Water;
		steamTurbine.pumpKGRate = 2f;
		steamTurbine.maxSelfHeat = 64f;
		steamTurbine.wasteHeatToTurbinePercent = 0.1f;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.Water };
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.storage = storage;
		conduitDispenser.alwaysDispense = true;
		go.AddOrGet<LogicOperationalController>();
		Prioritizable.AddRef(go);
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(game_object);
			StructureTemperaturePayload payload = GameComps.StructureTemperatures.GetPayload(handle);
			Extents extents = game_object.GetComponent<Building>().GetExtents();
			Extents extents2 = new Extents(extents.x, extents.y - 1, extents.width, extents.height + 1);
			payload.OverrideExtents(extents2);
			GameComps.StructureTemperatures.SetPayload(handle, ref payload);
			Storage[] components = game_object.GetComponents<Storage>();
			game_object.GetComponent<SteamTurbine>().SetStorage(components[1], components[0]);
		};
		Tinkerable.MakePowerTinkerable(go);
	}

	// Token: 0x040008CC RID: 2252
	public const string ID = "SteamTurbine2";

	// Token: 0x040008CD RID: 2253
	public static float MAX_WATTAGE = 850f;

	// Token: 0x040008CE RID: 2254
	private const int HEIGHT = 3;

	// Token: 0x040008CF RID: 2255
	private const int WIDTH = 5;

	// Token: 0x040008D0 RID: 2256
	private static readonly List<Storage.StoredItemModifier> StoredItemModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Insulate,
		Storage.StoredItemModifier.Seal
	};
}
