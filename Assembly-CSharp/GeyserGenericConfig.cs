using System;
using System.Collections.Generic;
using Klei;
using TUNING;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class GeyserGenericConfig : IMultiEntityConfig
{
	// Token: 0x0600064D RID: 1613 RVA: 0x0002881C File Offset: 0x00026A1C
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		List<GeyserGenericConfig.GeyserPrefabParams> configs = this.GenerateConfigs();
		foreach (GeyserGenericConfig.GeyserPrefabParams geyserPrefabParams in configs)
		{
			list.Add(GeyserGenericConfig.CreateGeyser(geyserPrefabParams.id, geyserPrefabParams.anim, geyserPrefabParams.width, geyserPrefabParams.height, Strings.Get(geyserPrefabParams.nameStringKey), Strings.Get(geyserPrefabParams.descStringKey), geyserPrefabParams.geyserType.idHash, geyserPrefabParams.geyserType.geyserTemperature));
		}
		configs.RemoveAll((GeyserGenericConfig.GeyserPrefabParams x) => !x.isGenericGeyser);
		GameObject gameObject = EntityTemplates.CreateEntity("GeyserGeneric", "Random Geyser Spawner", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			int num = 0;
			if (SaveLoader.Instance.clusterDetailSave != null)
			{
				num = SaveLoader.Instance.clusterDetailSave.globalWorldSeed;
			}
			else
			{
				global::Debug.LogWarning("Could not load global world seed for geysers");
			}
			num = num + (int)inst.transform.GetPosition().x + (int)inst.transform.GetPosition().y;
			int num2 = new KRandom(num).Next(0, configs.Count);
			GameUtil.KInstantiate(Assets.GetPrefab(configs[num2].id), inst.transform.GetPosition(), Grid.SceneLayer.BuildingBack, null, 0).SetActive(true);
			inst.DeleteObject();
		};
		list.Add(gameObject);
		return list;
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x00028944 File Offset: 0x00026B44
	public static GameObject CreateGeyser(string id, string anim, int width, int height, string name, string desc, HashedString presetType, float geyserTemperature)
	{
		float num = 2000f;
		EffectorValues tier = BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, name, desc, num, Assets.GetAnim(anim), "inactive", Grid.SceneLayer.BuildingBack, width, height, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.GeyserFeature }, 293f);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Katairite, true);
		component.Temperature = geyserTemperature;
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uncoverable>();
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		gameObject.AddOrGet<GeyserConfigurator>().presetType = presetType;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00028A53 File Offset: 0x00026C53
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x00028A55 File Offset: 0x00026C55
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00028A58 File Offset: 0x00026C58
	private List<GeyserGenericConfig.GeyserPrefabParams> GenerateConfigs()
	{
		List<GeyserGenericConfig.GeyserPrefabParams> list = new List<GeyserGenericConfig.GeyserPrefabParams>();
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_steam_kanim", 2, 4, new GeyserConfigurator.GeyserType("steam", SimHashes.Steam, GeyserConfigurator.GeyserShape.Gas, 383.15f, 1000f, 2000f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_steam_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_steam", SimHashes.Steam, GeyserConfigurator.GeyserShape.Gas, 773.15f, 500f, 1000f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_water_hot_kanim", 4, 2, new GeyserConfigurator.GeyserType("hot_water", SimHashes.Water, GeyserConfigurator.GeyserShape.Liquid, 368.15f, 2000f, 4000f, 500f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_water_slush_kanim", 4, 2, new GeyserConfigurator.GeyserType("slush_water", SimHashes.DirtyWater, GeyserConfigurator.GeyserShape.Liquid, 263.15f, 1000f, 2000f, 500f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 263f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_water_filthy_kanim", 4, 2, new GeyserConfigurator.GeyserType("filthy_water", SimHashes.DirtyWater, GeyserConfigurator.GeyserShape.Liquid, 303.15f, 2000f, 4000f, 500f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "").AddDisease(new SimUtil.DiseaseInfo
		{
			idx = Db.Get().Diseases.GetIndex("FoodPoisoning"),
			count = 20000
		}), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_salt_water_kanim", 4, 2, new GeyserConfigurator.GeyserType("slush_salt_water", SimHashes.Brine, GeyserConfigurator.GeyserShape.Liquid, 263.15f, 1000f, 2000f, 500f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 263f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_salt_water_kanim", 4, 2, new GeyserConfigurator.GeyserType("salt_water", SimHashes.SaltWater, GeyserConfigurator.GeyserShape.Liquid, 368.15f, 2000f, 4000f, 500f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_volcano_small_kanim", 3, 3, new GeyserConfigurator.GeyserType("small_volcano", SimHashes.Magma, GeyserConfigurator.GeyserShape.Molten, 2000f, 400f, 800f, 150f, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_volcano_big_kanim", 3, 3, new GeyserConfigurator.GeyserType("big_volcano", SimHashes.Magma, GeyserConfigurator.GeyserShape.Molten, 2000f, 800f, 1600f, 150f, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_co2_kanim", 4, 2, new GeyserConfigurator.GeyserType("liquid_co2", SimHashes.LiquidCarbonDioxide, GeyserConfigurator.GeyserShape.Liquid, 218f, 100f, 200f, 50f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 218f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_co2_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_co2", SimHashes.CarbonDioxide, GeyserConfigurator.GeyserShape.Gas, 773.15f, 70f, 140f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_hydrogen_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_hydrogen", SimHashes.Hydrogen, GeyserConfigurator.GeyserShape.Gas, 773.15f, 70f, 140f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_po2_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_po2", SimHashes.ContaminatedOxygen, GeyserConfigurator.GeyserShape.Gas, 773.15f, 70f, 140f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_po2_slimy_kanim", 2, 4, new GeyserConfigurator.GeyserType("slimy_po2", SimHashes.ContaminatedOxygen, GeyserConfigurator.GeyserShape.Gas, 333.15f, 70f, 140f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "").AddDisease(new SimUtil.DiseaseInfo
		{
			idx = Db.Get().Diseases.GetIndex("SlimeLung"),
			count = 5000
		}), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_chlorine_kanim", 2, 4, new GeyserConfigurator.GeyserType("chlorine_gas", SimHashes.ChlorineGas, GeyserConfigurator.GeyserShape.Gas, 333.15f, 70f, 140f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_methane_kanim", 2, 4, new GeyserConfigurator.GeyserType("methane", SimHashes.Methane, GeyserConfigurator.GeyserShape.Gas, 423.15f, 70f, 140f, 5f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_copper_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_copper", SimHashes.MoltenCopper, GeyserConfigurator.GeyserShape.Molten, 2500f, 200f, 400f, 150f, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_iron_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_iron", SimHashes.MoltenIron, GeyserConfigurator.GeyserShape.Molten, 2800f, 200f, 400f, 150f, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_gold_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_gold", SimHashes.MoltenGold, GeyserConfigurator.GeyserShape.Molten, 2900f, 200f, 400f, 150f, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_aluminum_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_aluminum", SimHashes.MoltenAluminum, GeyserConfigurator.GeyserShape.Molten, 2000f, 200f, 400f, 150f, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "EXPANSION1_ID"), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_tungsten_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_tungsten", SimHashes.MoltenTungsten, GeyserConfigurator.GeyserShape.Molten, 4000f, 200f, 400f, 150f, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "EXPANSION1_ID"), false));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_niobium_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_niobium", SimHashes.MoltenNiobium, GeyserConfigurator.GeyserShape.Molten, 3500f, 800f, 1600f, 150f, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "EXPANSION1_ID"), false));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_cobalt_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_cobalt", SimHashes.MoltenCobalt, GeyserConfigurator.GeyserShape.Molten, 2500f, 200f, 400f, 150f, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "EXPANSION1_ID"), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_oil_kanim", 4, 2, new GeyserConfigurator.GeyserType("oil_drip", SimHashes.CrudeOil, GeyserConfigurator.GeyserShape.Liquid, 600f, 1f, 250f, 50f, 600f, 600f, 1f, 1f, 100f, 500f, 0.4f, 0.8f, 372.15f, ""), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_sulfur_kanim", 4, 2, new GeyserConfigurator.GeyserType("liquid_sulfur", SimHashes.LiquidSulfur, GeyserConfigurator.GeyserShape.Liquid, 438.34998f, 1000f, 2000f, 500f, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f, "EXPANSION1_ID"), true));
		list.RemoveAll((GeyserGenericConfig.GeyserPrefabParams geyser) => !geyser.geyserType.DlcID.IsNullOrWhiteSpace() && !DlcManager.IsContentActive(geyser.geyserType.DlcID));
		return list;
	}

	// Token: 0x04000446 RID: 1094
	public const string ID = "GeyserGeneric";

	// Token: 0x04000447 RID: 1095
	public const string Steam = "steam";

	// Token: 0x04000448 RID: 1096
	public const string HotSteam = "hot_steam";

	// Token: 0x04000449 RID: 1097
	public const string HotWater = "hot_water";

	// Token: 0x0400044A RID: 1098
	public const string SlushWater = "slush_water";

	// Token: 0x0400044B RID: 1099
	public const string FilthyWater = "filthy_water";

	// Token: 0x0400044C RID: 1100
	public const string SlushSaltWater = "slush_salt_water";

	// Token: 0x0400044D RID: 1101
	public const string SaltWater = "salt_water";

	// Token: 0x0400044E RID: 1102
	public const string SmallVolcano = "small_volcano";

	// Token: 0x0400044F RID: 1103
	public const string BigVolcano = "big_volcano";

	// Token: 0x04000450 RID: 1104
	public const string LiquidCO2 = "liquid_co2";

	// Token: 0x04000451 RID: 1105
	public const string HotCO2 = "hot_co2";

	// Token: 0x04000452 RID: 1106
	public const string HotHydrogen = "hot_hydrogen";

	// Token: 0x04000453 RID: 1107
	public const string HotPO2 = "hot_po2";

	// Token: 0x04000454 RID: 1108
	public const string SlimyPO2 = "slimy_po2";

	// Token: 0x04000455 RID: 1109
	public const string ChlorineGas = "chlorine_gas";

	// Token: 0x04000456 RID: 1110
	public const string Methane = "methane";

	// Token: 0x04000457 RID: 1111
	public const string MoltenCopper = "molten_copper";

	// Token: 0x04000458 RID: 1112
	public const string MoltenIron = "molten_iron";

	// Token: 0x04000459 RID: 1113
	public const string MoltenGold = "molten_gold";

	// Token: 0x0400045A RID: 1114
	public const string MoltenAluminum = "molten_aluminum";

	// Token: 0x0400045B RID: 1115
	public const string MoltenTungsten = "molten_tungsten";

	// Token: 0x0400045C RID: 1116
	public const string MoltenNiobium = "molten_niobium";

	// Token: 0x0400045D RID: 1117
	public const string MoltenCobalt = "molten_cobalt";

	// Token: 0x0400045E RID: 1118
	public const string OilDrip = "oil_drip";

	// Token: 0x0400045F RID: 1119
	public const string LiquidSulfur = "liquid_sulfur";

	// Token: 0x02000EC2 RID: 3778
	public struct GeyserPrefabParams
	{
		// Token: 0x06006D0C RID: 27916 RVA: 0x002998AC File Offset: 0x00297AAC
		public GeyserPrefabParams(string anim, int width, int height, GeyserConfigurator.GeyserType geyserType, bool isGenericGeyser)
		{
			this.id = "GeyserGeneric_" + geyserType.id;
			this.anim = anim;
			this.width = width;
			this.height = height;
			this.nameStringKey = new StringKey("STRINGS.CREATURES.SPECIES.GEYSER." + geyserType.id.ToUpper() + ".NAME");
			this.descStringKey = new StringKey("STRINGS.CREATURES.SPECIES.GEYSER." + geyserType.id.ToUpper() + ".DESC");
			this.geyserType = geyserType;
			this.isGenericGeyser = isGenericGeyser;
		}

		// Token: 0x0400523B RID: 21051
		public string id;

		// Token: 0x0400523C RID: 21052
		public string anim;

		// Token: 0x0400523D RID: 21053
		public int width;

		// Token: 0x0400523E RID: 21054
		public int height;

		// Token: 0x0400523F RID: 21055
		public StringKey nameStringKey;

		// Token: 0x04005240 RID: 21056
		public StringKey descStringKey;

		// Token: 0x04005241 RID: 21057
		public GeyserConfigurator.GeyserType geyserType;

		// Token: 0x04005242 RID: 21058
		public bool isGenericGeyser;
	}

	// Token: 0x02000EC3 RID: 3779
	private static class TEMPERATURES
	{
		// Token: 0x04005243 RID: 21059
		public const float BELOW_FREEZING = 263.15f;

		// Token: 0x04005244 RID: 21060
		public const float DUPE_NORMAL = 303.15f;

		// Token: 0x04005245 RID: 21061
		public const float DUPE_HOT = 333.15f;

		// Token: 0x04005246 RID: 21062
		public const float BELOW_BOILING = 368.15f;

		// Token: 0x04005247 RID: 21063
		public const float ABOVE_BOILING = 383.15f;

		// Token: 0x04005248 RID: 21064
		public const float HOT1 = 423.15f;

		// Token: 0x04005249 RID: 21065
		public const float HOT2 = 773.15f;

		// Token: 0x0400524A RID: 21066
		public const float MOLTEN_MAGMA = 2000f;
	}

	// Token: 0x02000EC4 RID: 3780
	public static class RATES
	{
		// Token: 0x0400524B RID: 21067
		public const float GAS_SMALL_MIN = 40f;

		// Token: 0x0400524C RID: 21068
		public const float GAS_SMALL_MAX = 80f;

		// Token: 0x0400524D RID: 21069
		public const float GAS_NORMAL_MIN = 70f;

		// Token: 0x0400524E RID: 21070
		public const float GAS_NORMAL_MAX = 140f;

		// Token: 0x0400524F RID: 21071
		public const float GAS_BIG_MIN = 100f;

		// Token: 0x04005250 RID: 21072
		public const float GAS_BIG_MAX = 200f;

		// Token: 0x04005251 RID: 21073
		public const float LIQUID_SMALL_MIN = 500f;

		// Token: 0x04005252 RID: 21074
		public const float LIQUID_SMALL_MAX = 1000f;

		// Token: 0x04005253 RID: 21075
		public const float LIQUID_NORMAL_MIN = 1000f;

		// Token: 0x04005254 RID: 21076
		public const float LIQUID_NORMAL_MAX = 2000f;

		// Token: 0x04005255 RID: 21077
		public const float LIQUID_BIG_MIN = 2000f;

		// Token: 0x04005256 RID: 21078
		public const float LIQUID_BIG_MAX = 4000f;

		// Token: 0x04005257 RID: 21079
		public const float MOLTEN_NORMAL_MIN = 200f;

		// Token: 0x04005258 RID: 21080
		public const float MOLTEN_NORMAL_MAX = 400f;

		// Token: 0x04005259 RID: 21081
		public const float MOLTEN_BIG_MIN = 400f;

		// Token: 0x0400525A RID: 21082
		public const float MOLTEN_BIG_MAX = 800f;

		// Token: 0x0400525B RID: 21083
		public const float MOLTEN_HUGE_MIN = 800f;

		// Token: 0x0400525C RID: 21084
		public const float MOLTEN_HUGE_MAX = 1600f;
	}

	// Token: 0x02000EC5 RID: 3781
	public static class MAX_PRESSURES
	{
		// Token: 0x0400525D RID: 21085
		public const float GAS = 5f;

		// Token: 0x0400525E RID: 21086
		public const float GAS_HIGH = 15f;

		// Token: 0x0400525F RID: 21087
		public const float MOLTEN = 150f;

		// Token: 0x04005260 RID: 21088
		public const float LIQUID_SMALL = 50f;

		// Token: 0x04005261 RID: 21089
		public const float LIQUID = 500f;
	}

	// Token: 0x02000EC6 RID: 3782
	public static class ITERATIONS
	{
		// Token: 0x02001E94 RID: 7828
		public static class INFREQUENT_MOLTEN
		{
			// Token: 0x04008906 RID: 35078
			public const float PCT_MIN = 0.005f;

			// Token: 0x04008907 RID: 35079
			public const float PCT_MAX = 0.01f;

			// Token: 0x04008908 RID: 35080
			public const float LEN_MIN = 6000f;

			// Token: 0x04008909 RID: 35081
			public const float LEN_MAX = 12000f;
		}

		// Token: 0x02001E95 RID: 7829
		public static class FREQUENT_MOLTEN
		{
			// Token: 0x0400890A RID: 35082
			public const float PCT_MIN = 0.016666668f;

			// Token: 0x0400890B RID: 35083
			public const float PCT_MAX = 0.1f;

			// Token: 0x0400890C RID: 35084
			public const float LEN_MIN = 480f;

			// Token: 0x0400890D RID: 35085
			public const float LEN_MAX = 1080f;
		}
	}
}
