using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class SteamTurbineConfig : IBuildingConfig
{
	// Token: 0x0600101C RID: 4124 RVA: 0x00057024 File Offset: 0x00055224
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SteamTurbine";
		int num = 5;
		int num2 = 4;
		string text2 = "steamturbine_kanim";
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
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 1f);
		buildingDef.GeneratorWattageRating = 2000f;
		buildingDef.GeneratorBaseCapacity = 2000f;
		buildingDef.Entombable = true;
		buildingDef.IsFoundation = false;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(1, 0);
		buildingDef.OverheatTemperature = 1273.15f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0005710B File Offset: 0x0005530B
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.CanPowerTinker.Id;
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x00057134 File Offset: 0x00055334
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(SteamTurbineConfig.StoredItemModifiers);
		Turbine turbine = go.AddOrGet<Turbine>();
		turbine.srcElem = SimHashes.Steam;
		MakeBaseSolid.Def def = go.AddOrGetDef<MakeBaseSolid.Def>();
		def.solidOffsets = new CellOffset[5];
		for (int i = 0; i < 5; i++)
		{
			def.solidOffsets[i] = new CellOffset(i - 2, 0);
		}
		turbine.pumpKGRate = 10f;
		turbine.requiredMassFlowDifferential = 3f;
		turbine.minEmitMass = 10f;
		turbine.maxRPM = 4000f;
		turbine.rpmAcceleration = turbine.maxRPM / 30f;
		turbine.rpmDeceleration = turbine.maxRPM / 20f;
		turbine.minGenerationRPM = 3000f;
		turbine.minActiveTemperature = 500f;
		turbine.emitTemperature = 425f;
		go.AddOrGet<Generator>();
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
		};
	}

	// Token: 0x040008C8 RID: 2248
	public const string ID = "SteamTurbine";

	// Token: 0x040008C9 RID: 2249
	private const int HEIGHT = 4;

	// Token: 0x040008CA RID: 2250
	private const int WIDTH = 5;

	// Token: 0x040008CB RID: 2251
	private static readonly List<Storage.StoredItemModifier> StoredItemModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Insulate,
		Storage.StoredItemModifier.Seal
	};
}
