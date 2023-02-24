﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class CompostConfig : IBuildingConfig
{
	// Token: 0x0600012C RID: 300 RVA: 0x00008E8C File Offset: 0x0000708C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Compost";
		int num = 2;
		int num2 = 2;
		string text2 = "compost_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER3, none, 0.2f);
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		SoundEventVolumeCache.instance.AddVolume("anim_interacts_compost_kanim", "Compost_shovel_in", NOISE_POLLUTION.NOISY.TIER2);
		SoundEventVolumeCache.instance.AddVolume("anim_interacts_compost_kanim", "Compost_shovel_out", NOISE_POLLUTION.NOISY.TIER2);
		return buildingDef;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00008F48 File Offset: 0x00007148
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 2000f;
		go.AddOrGet<Compost>().simulatedInternalTemperature = 348.15f;
		CompostWorkable compostWorkable = go.AddOrGet<CompostWorkable>();
		compostWorkable.workTime = 20f;
		compostWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_compost_kanim") };
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(CompostConfig.COMPOST_TAG, 0.1f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.1f, SimHashes.Dirt, 348.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
		};
		ElementDropper elementDropper = go.AddComponent<ElementDropper>();
		elementDropper.emitMass = 10f;
		elementDropper.emitTag = SimHashes.Dirt.CreateTag();
		elementDropper.emitOffset = new Vector3(0.5f, 1f, 0f);
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = CompostConfig.COMPOST_TAG;
		manualDeliveryKG.capacity = 300f;
		manualDeliveryKG.refillMass = 60f;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FarmFetch.IdHash;
		Prioritizable.AddRef(go);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x000090AC File Offset: 0x000072AC
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400009F RID: 159
	public const string ID = "Compost";

	// Token: 0x040000A0 RID: 160
	public static readonly Tag COMPOST_TAG = GameTags.Compostable;

	// Token: 0x040000A1 RID: 161
	public const float SAND_INPUT_PER_SECOND = 0.1f;

	// Token: 0x040000A2 RID: 162
	public const float FERTILIZER_OUTPUT_PER_SECOND = 0.1f;

	// Token: 0x040000A3 RID: 163
	public const float FERTILIZER_OUTPUT_TEMP = 348.15f;

	// Token: 0x040000A4 RID: 164
	public const float INPUT_CAPACITY = 300f;

	// Token: 0x040000A5 RID: 165
	private const SimHashes OUTPUT_ELEMENT = SimHashes.Dirt;
}
