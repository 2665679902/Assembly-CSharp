using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class HarvestablePOIConfig : IMultiEntityConfig
{
	// Token: 0x06000BF7 RID: 3063 RVA: 0x00042FA0 File Offset: 0x000411A0
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (HarvestablePOIConfig.HarvestablePOIParams harvestablePOIParams in this.GenerateConfigs())
		{
			list.Add(HarvestablePOIConfig.CreateHarvestablePOI(harvestablePOIParams.id, harvestablePOIParams.anim, Strings.Get(harvestablePOIParams.nameStringKey), harvestablePOIParams.descStringKey, harvestablePOIParams.poiType.idHash, harvestablePOIParams.poiType.canProvideArtifacts));
		}
		return list;
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00043038 File Offset: 0x00041238
	public static GameObject CreateHarvestablePOI(string id, string anim, string name, StringKey descStringKey, HashedString poiType, bool canProvideArtifacts = false)
	{
		GameObject gameObject = EntityTemplates.CreateEntity(id, id, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<HarvestablePOIConfigurator>().presetType = poiType;
		HarvestablePOIClusterGridEntity harvestablePOIClusterGridEntity = gameObject.AddOrGet<HarvestablePOIClusterGridEntity>();
		harvestablePOIClusterGridEntity.m_name = name;
		harvestablePOIClusterGridEntity.m_Anim = anim;
		gameObject.AddOrGetDef<HarvestablePOIStates.Def>();
		if (canProvideArtifacts)
		{
			gameObject.AddOrGetDef<ArtifactPOIStates.Def>();
			gameObject.AddOrGet<ArtifactPOIConfigurator>().presetType = ArtifactPOIConfigurator.defaultArtifactPoiType.idHash;
		}
		gameObject.AddOrGet<InfoDescription>().description = Strings.Get(descStringKey);
		return gameObject;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x000430B3 File Offset: 0x000412B3
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x000430B5 File Offset: 0x000412B5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x000430B8 File Offset: 0x000412B8
	private List<HarvestablePOIConfig.HarvestablePOIParams> GenerateConfigs()
	{
		List<HarvestablePOIConfig.HarvestablePOIParams> list = new List<HarvestablePOIConfig.HarvestablePOIParams>();
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("cloud", new HarvestablePOIConfigurator.HarvestablePOIType("CarbonAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.RefinedCarbon,
				1.5f
			},
			{
				SimHashes.Carbon,
				5.5f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("metallic_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("MetallicAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.MoltenIron,
				1.25f
			},
			{
				SimHashes.Cuprite,
				1.75f
			},
			{
				SimHashes.Obsidian,
				7f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("satellite_field", new HarvestablePOIConfigurator.HarvestablePOIType("SatelliteField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Sand,
				3f
			},
			{
				SimHashes.IronOre,
				3f
			},
			{
				SimHashes.MoltenCopper,
				2.67f
			},
			{
				SimHashes.Glass,
				1.33f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("rocky_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("RockyAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Cuprite,
				2f
			},
			{
				SimHashes.SedimentaryRock,
				4f
			},
			{
				SimHashes.IgneousRock,
				4f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("interstellar_ice_field", new HarvestablePOIConfigurator.HarvestablePOIType("InterstellarIceField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Ice,
				2.5f
			},
			{
				SimHashes.SolidCarbonDioxide,
				7f
			},
			{
				SimHashes.SolidOxygen,
				0.5f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, new List<string>
		{
			Db.Get().OrbitalTypeCategories.iceCloud.Id,
			Db.Get().OrbitalTypeCategories.iceRock.Id
		}, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("organic_mass_field", new HarvestablePOIConfigurator.HarvestablePOIType("OrganicMassField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SlimeMold,
				3f
			},
			{
				SimHashes.Algae,
				3f
			},
			{
				SimHashes.ContaminatedOxygen,
				1f
			},
			{
				SimHashes.Dirt,
				3f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("ice_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("IceAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Ice,
				6f
			},
			{
				SimHashes.SolidCarbonDioxide,
				2f
			},
			{
				SimHashes.Oxygen,
				1.5f
			},
			{
				SimHashes.SolidMethane,
				0.5f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, new List<string>
		{
			Db.Get().OrbitalTypeCategories.iceCloud.Id,
			Db.Get().OrbitalTypeCategories.iceRock.Id
		}, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("gas_giant_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("GasGiantCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Methane,
				1f
			},
			{
				SimHashes.LiquidMethane,
				1f
			},
			{
				SimHashes.SolidMethane,
				1f
			},
			{
				SimHashes.Hydrogen,
				7f
			}
		}, 15000f, 20000f, 30000f, 60000f, true, HarvestablePOIConfig.GasFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("chlorine_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("ChlorineCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Chlorine,
				2.5f
			},
			{
				SimHashes.BleachStone,
				7.5f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.GasFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("gilded_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("GildedAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Gold,
				2.5f
			},
			{
				SimHashes.Fullerene,
				1f
			},
			{
				SimHashes.RefinedCarbon,
				1f
			},
			{
				SimHashes.SedimentaryRock,
				4.5f
			},
			{
				SimHashes.Regolith,
				1f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("glimmering_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("GlimmeringAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.MoltenTungsten,
				2f
			},
			{
				SimHashes.Wolframite,
				6f
			},
			{
				SimHashes.Carbon,
				1f
			},
			{
				SimHashes.CarbonDioxide,
				1f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("helium_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("HeliumCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Hydrogen,
				2f
			},
			{
				SimHashes.Water,
				8f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.GasFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("oily_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("OilyAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SolidCarbonDioxide,
				7.75f
			},
			{
				SimHashes.SolidMethane,
				1.125f
			},
			{
				SimHashes.CrudeOil,
				1.125f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("oxidized_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("OxidizedAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Rust,
				8f
			},
			{
				SimHashes.SolidCarbonDioxide,
				2f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("salty_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("SaltyAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SaltWater,
				5f
			},
			{
				SimHashes.Brine,
				4f
			},
			{
				SimHashes.SolidCarbonDioxide,
				1f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("frozen_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("FrozenOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Ice,
				2.33f
			},
			{
				SimHashes.DirtyIce,
				2.33f
			},
			{
				SimHashes.Snow,
				1.83f
			},
			{
				SimHashes.AluminumOre,
				2f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("foresty_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("ForestyOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.IgneousRock,
				7f
			},
			{
				SimHashes.AluminumOre,
				1f
			},
			{
				SimHashes.CarbonDioxide,
				2f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("swampy_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("SwampyOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Mud,
				2f
			},
			{
				SimHashes.ToxicSand,
				7f
			},
			{
				SimHashes.Cobaltite,
				1f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("sandy_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("SandyOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SandStone,
				4f
			},
			{
				SimHashes.Algae,
				2f
			},
			{
				SimHashes.Cuprite,
				1f
			},
			{
				SimHashes.Sand,
				3f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("radioactive_gas_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("RadioactiveGasCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.UraniumOre,
				2f
			},
			{
				SimHashes.Chlorine,
				2f
			},
			{
				SimHashes.CarbonDioxide,
				7f
			}
		}, 5000f, 10000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("radioactive_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("RadioactiveAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.UraniumOre,
				2f
			},
			{
				SimHashes.Sulfur,
				3f
			},
			{
				SimHashes.BleachStone,
				2f
			},
			{
				SimHashes.Rust,
				4f
			}
		}, 5000f, 10000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("oxygen_rich_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("OxygenRichAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Water,
				4f
			},
			{
				SimHashes.ContaminatedOxygen,
				2f
			},
			{
				SimHashes.Ice,
				4f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("interstellar_ocean", new HarvestablePOIConfigurator.HarvestablePOIType("InterstellarOcean", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SaltWater,
				2.5f
			},
			{
				SimHashes.Brine,
				2.5f
			},
			{
				SimHashes.Salt,
				2.5f
			},
			{
				SimHashes.Ice,
				2.5f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, "EXPANSION1_ID")));
		list.RemoveAll((HarvestablePOIConfig.HarvestablePOIParams poi) => !poi.poiType.dlcID.IsNullOrWhiteSpace() && !DlcManager.IsContentActive(poi.poiType.dlcID));
		return list;
	}

	// Token: 0x040006F3 RID: 1779
	public const string CarbonAsteroidField = "CarbonAsteroidField";

	// Token: 0x040006F4 RID: 1780
	public const string MetallicAsteroidField = "MetallicAsteroidField";

	// Token: 0x040006F5 RID: 1781
	public const string SatelliteField = "SatelliteField";

	// Token: 0x040006F6 RID: 1782
	public const string RockyAsteroidField = "RockyAsteroidField";

	// Token: 0x040006F7 RID: 1783
	public const string InterstellarIceField = "InterstellarIceField";

	// Token: 0x040006F8 RID: 1784
	public const string OrganicMassField = "OrganicMassField";

	// Token: 0x040006F9 RID: 1785
	public const string IceAsteroidField = "IceAsteroidField";

	// Token: 0x040006FA RID: 1786
	public const string GasGiantCloud = "GasGiantCloud";

	// Token: 0x040006FB RID: 1787
	public const string ChlorineCloud = "ChlorineCloud";

	// Token: 0x040006FC RID: 1788
	public const string GildedAsteroidField = "GildedAsteroidField";

	// Token: 0x040006FD RID: 1789
	public const string GlimmeringAsteroidField = "GlimmeringAsteroidField";

	// Token: 0x040006FE RID: 1790
	public const string HeliumCloud = "HeliumCloud";

	// Token: 0x040006FF RID: 1791
	public const string OilyAsteroidField = "OilyAsteroidField";

	// Token: 0x04000700 RID: 1792
	public const string OxidizedAsteroidField = "OxidizedAsteroidField";

	// Token: 0x04000701 RID: 1793
	public const string SaltyAsteroidField = "SaltyAsteroidField";

	// Token: 0x04000702 RID: 1794
	public const string FrozenOreField = "FrozenOreField";

	// Token: 0x04000703 RID: 1795
	public const string ForestyOreField = "ForestyOreField";

	// Token: 0x04000704 RID: 1796
	public const string SwampyOreField = "SwampyOreField";

	// Token: 0x04000705 RID: 1797
	public const string SandyOreField = "SandyOreField";

	// Token: 0x04000706 RID: 1798
	public const string RadioactiveGasCloud = "RadioactiveGasCloud";

	// Token: 0x04000707 RID: 1799
	public const string RadioactiveAsteroidField = "RadioactiveAsteroidField";

	// Token: 0x04000708 RID: 1800
	public const string OxygenRichAsteroidField = "OxygenRichAsteroidField";

	// Token: 0x04000709 RID: 1801
	public const string InterstellarOcean = "InterstellarOcean";

	// Token: 0x0400070A RID: 1802
	private static readonly List<string> GasFieldOrbit = new List<string>
	{
		Db.Get().OrbitalTypeCategories.iceCloud.Id,
		Db.Get().OrbitalTypeCategories.heliumCloud.Id,
		Db.Get().OrbitalTypeCategories.purpleGas.Id,
		Db.Get().OrbitalTypeCategories.radioactiveGas.Id
	};

	// Token: 0x0400070B RID: 1803
	private static readonly List<string> AsteroidFieldOrbit = new List<string>
	{
		Db.Get().OrbitalTypeCategories.iceRock.Id,
		Db.Get().OrbitalTypeCategories.frozenOre.Id,
		Db.Get().OrbitalTypeCategories.rocky.Id
	};

	// Token: 0x02000EE7 RID: 3815
	public struct HarvestablePOIParams
	{
		// Token: 0x06006D61 RID: 28001 RVA: 0x0029A1F4 File Offset: 0x002983F4
		public HarvestablePOIParams(string anim, HarvestablePOIConfigurator.HarvestablePOIType poiType)
		{
			this.id = "HarvestableSpacePOI_" + poiType.id;
			this.anim = anim;
			this.nameStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.HARVESTABLE_POI." + poiType.id.ToUpper() + ".NAME");
			this.descStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.HARVESTABLE_POI." + poiType.id.ToUpper() + ".DESC");
			this.poiType = poiType;
		}

		// Token: 0x040052A9 RID: 21161
		public string id;

		// Token: 0x040052AA RID: 21162
		public string anim;

		// Token: 0x040052AB RID: 21163
		public StringKey nameStringKey;

		// Token: 0x040052AC RID: 21164
		public StringKey descStringKey;

		// Token: 0x040052AD RID: 21165
		public HarvestablePOIConfigurator.HarvestablePOIType poiType;
	}
}
