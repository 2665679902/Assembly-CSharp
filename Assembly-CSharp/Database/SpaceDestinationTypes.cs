﻿using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CB2 RID: 3250
	public class SpaceDestinationTypes : ResourceSet<SpaceDestinationType>
	{
		// Token: 0x060065EA RID: 26090 RVA: 0x00270F7C File Offset: 0x0026F17C
		public SpaceDestinationTypes(ResourceSet parent)
			: base("SpaceDestinations", parent)
		{
			this.Satellite = base.Add(new SpaceDestinationType("Satellite", parent, UI.SPACEDESTINATIONS.DEBRIS.SATELLITE.NAME, UI.SPACEDESTINATIONS.DEBRIS.SATELLITE.DESCRIPTION, 16, "asteroid", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Steel,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Copper,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Glass,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Bad, 64000000, 63994000, 18, true));
			this.MetallicAsteroid = base.Add(new SpaceDestinationType("MetallicAsteroid", parent, UI.SPACEDESTINATIONS.ASTEROIDS.METALLICASTEROID.NAME, UI.SPACEDESTINATIONS.ASTEROIDS.METALLICASTEROID.DESCRIPTION, 32, "nebula", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Iron,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Copper,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Obsidian,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int> { { "HatchMetal", 3 } }, Db.Get().ArtifactDropRates.Mediocre, 128000000, 127988000, 12, true));
			this.RockyAsteroid = base.Add(new SpaceDestinationType("RockyAsteroid", parent, UI.SPACEDESTINATIONS.ASTEROIDS.ROCKYASTEROID.NAME, UI.SPACEDESTINATIONS.ASTEROIDS.ROCKYASTEROID.DESCRIPTION, 32, "new_12", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Cuprite,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SedimentaryRock,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.IgneousRock,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int> { { "HatchHard", 3 } }, Db.Get().ArtifactDropRates.Good, 128000000, 127988000, 18, true));
			this.CarbonaceousAsteroid = base.Add(new SpaceDestinationType("CarbonaceousAsteroid", parent, UI.SPACEDESTINATIONS.ASTEROIDS.CARBONACEOUSASTEROID.NAME, UI.SPACEDESTINATIONS.ASTEROIDS.CARBONACEOUSASTEROID.DESCRIPTION, 32, "new_08", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.RefinedCarbon,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Carbon,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Diamond,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Mediocre, 128000000, 127988000, 6, true));
			this.IcyDwarf = base.Add(new SpaceDestinationType("IcyDwarf", parent, UI.SPACEDESTINATIONS.DWARFPLANETS.ICYDWARF.NAME, UI.SPACEDESTINATIONS.DWARFPLANETS.ICYDWARF.DESCRIPTION, 64, "icyMoon", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Ice,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidCarbonDioxide,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidOxygen,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int>
			{
				{ "ColdBreatherSeed", 3 },
				{ "ColdWheatSeed", 4 }
			}, Db.Get().ArtifactDropRates.Great, 256000000, 255982000, 24, true));
			this.OrganicDwarf = base.Add(new SpaceDestinationType("OrganicDwarf", parent, UI.SPACEDESTINATIONS.DWARFPLANETS.ORGANICDWARF.NAME, UI.SPACEDESTINATIONS.DWARFPLANETS.ORGANICDWARF.DESCRIPTION, 64, "organicAsteroid", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.SlimeMold,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Algae,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.ContaminatedOxygen,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int>
			{
				{ "Moo", 1 },
				{ "GasGrassSeed", 4 }
			}, Db.Get().ArtifactDropRates.Great, 256000000, 255982000, 30, true));
			this.DustyMoon = base.Add(new SpaceDestinationType("DustyMoon", parent, UI.SPACEDESTINATIONS.DWARFPLANETS.DUSTYDWARF.NAME, UI.SPACEDESTINATIONS.DWARFPLANETS.DUSTYDWARF.DESCRIPTION, 64, "new_05", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Regolith,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.MaficRock,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SedimentaryRock,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Amazing, 256000000, 255982000, 42, true));
			this.TerraPlanet = base.Add(new SpaceDestinationType("TerraPlanet", parent, UI.SPACEDESTINATIONS.PLANETS.TERRAPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.TERRAPLANET.DESCRIPTION, 96, "terra", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Water,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Algae,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Oxygen,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Dirt,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int>
			{
				{ "PrickleFlowerSeed", 4 },
				{ "PacuEgg", 4 }
			}, Db.Get().ArtifactDropRates.Amazing, 384000000, 383980000, 54, true));
			this.VolcanoPlanet = base.Add(new SpaceDestinationType("VolcanoPlanet", parent, UI.SPACEDESTINATIONS.PLANETS.VOLCANOPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.VOLCANOPLANET.DESCRIPTION, 96, "planet", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Magma,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.IgneousRock,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Katairite,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Amazing, 384000000, 383980000, 54, true));
			this.GasGiant = base.Add(new SpaceDestinationType("GasGiant", parent, UI.SPACEDESTINATIONS.GIANTS.GASGIANT.NAME, UI.SPACEDESTINATIONS.GIANTS.GASGIANT.DESCRIPTION, 96, "gasGiant", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Methane,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Hydrogen,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Perfect, 384000000, 383980000, 60, true));
			this.IceGiant = base.Add(new SpaceDestinationType("IceGiant", parent, UI.SPACEDESTINATIONS.GIANTS.ICEGIANT.NAME, UI.SPACEDESTINATIONS.GIANTS.ICEGIANT.DESCRIPTION, 96, "icyMoon", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Ice,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidCarbonDioxide,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidOxygen,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidMethane,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Perfect, 384000000, 383980000, 60, true));
			this.SaltDwarf = base.Add(new SpaceDestinationType("SaltDwarf", parent, UI.SPACEDESTINATIONS.DWARFPLANETS.SALTDWARF.NAME, UI.SPACEDESTINATIONS.DWARFPLANETS.SALTDWARF.DESCRIPTION, 64, "new_01", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.SaltWater,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidCarbonDioxide,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Brine,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int> { { "SaltPlantSeed", 3 } }, Db.Get().ArtifactDropRates.Bad, 256000000, 255982000, 30, true));
			this.RustPlanet = base.Add(new SpaceDestinationType("RustPlanet", parent, UI.SPACEDESTINATIONS.PLANETS.RUSTPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.RUSTPLANET.DESCRIPTION, 96, "new_06", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Rust,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidCarbonDioxide,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Perfect, 384000000, 383980000, 60, true));
			this.ForestPlanet = base.Add(new SpaceDestinationType("ForestPlanet", parent, UI.SPACEDESTINATIONS.PLANETS.FORESTPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.FORESTPLANET.DESCRIPTION, 96, "new_07", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.AluminumOre,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidOxygen,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int>
			{
				{ "Squirrel", 1 },
				{ "ForestTreeSeed", 4 }
			}, Db.Get().ArtifactDropRates.Mediocre, 384000000, 383980000, 24, true));
			this.RedDwarf = base.Add(new SpaceDestinationType("RedDwarf", parent, UI.SPACEDESTINATIONS.DWARFPLANETS.REDDWARF.NAME, UI.SPACEDESTINATIONS.DWARFPLANETS.REDDWARF.DESCRIPTION, 64, "sun", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Aluminum,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.LiquidMethane,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Fossil,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Amazing, 256000000, 255982000, 42, true));
			this.GoldAsteroid = base.Add(new SpaceDestinationType("GoldAsteroid", parent, UI.SPACEDESTINATIONS.ASTEROIDS.GOLDASTEROID.NAME, UI.SPACEDESTINATIONS.ASTEROIDS.GOLDASTEROID.DESCRIPTION, 32, "new_02", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Gold,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Fullerene,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.FoolsGold,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Bad, 128000000, 127988000, 90, true));
			this.HydrogenGiant = base.Add(new SpaceDestinationType("HeliumGiant", parent, UI.SPACEDESTINATIONS.GIANTS.HYDROGENGIANT.NAME, UI.SPACEDESTINATIONS.GIANTS.HYDROGENGIANT.DESCRIPTION, 96, "new_11", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.LiquidHydrogen,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Water,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Niobium,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Mediocre, 384000000, 383980000, 78, true));
			this.OilyAsteroid = base.Add(new SpaceDestinationType("OilyAsteriod", parent, UI.SPACEDESTINATIONS.ASTEROIDS.OILYASTEROID.NAME, UI.SPACEDESTINATIONS.ASTEROIDS.OILYASTEROID.DESCRIPTION, 32, "new_09", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.SolidMethane,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.SolidCarbonDioxide,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.CrudeOil,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Petroleum,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Mediocre, 128000000, 127988000, 12, true));
			this.ShinyPlanet = base.Add(new SpaceDestinationType("ShinyPlanet", parent, UI.SPACEDESTINATIONS.PLANETS.SHINYPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.SHINYPLANET.DESCRIPTION, 96, "new_04", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Tungsten,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.Wolframite,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Good, 384000000, 383980000, 84, true));
			this.ChlorinePlanet = base.Add(new SpaceDestinationType("ChlorinePlanet", parent, UI.SPACEDESTINATIONS.PLANETS.CHLORINEPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.CHLORINEPLANET.DESCRIPTION, 96, "new_10", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.SolidChlorine,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.BleachStone,
					new MathUtil.MinMax(100f, 200f)
				}
			}, null, Db.Get().ArtifactDropRates.Bad, 256000000, 255982000, 90, true));
			this.SaltDesertPlanet = base.Add(new SpaceDestinationType("SaltDesertPlanet", parent, UI.SPACEDESTINATIONS.PLANETS.SALTDESERTPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.SALTDESERTPLANET.DESCRIPTION, 96, "new_10", new Dictionary<SimHashes, MathUtil.MinMax>
			{
				{
					SimHashes.Salt,
					new MathUtil.MinMax(100f, 200f)
				},
				{
					SimHashes.CrushedRock,
					new MathUtil.MinMax(100f, 200f)
				}
			}, new Dictionary<string, int> { { "Crab", 1 } }, Db.Get().ArtifactDropRates.Bad, 384000000, 383980000, 60, true));
			this.Wormhole = base.Add(new SpaceDestinationType("Wormhole", parent, UI.SPACEDESTINATIONS.WORMHOLE.NAME, UI.SPACEDESTINATIONS.WORMHOLE.DESCRIPTION, 96, "new_03", new Dictionary<SimHashes, MathUtil.MinMax> { 
			{
				SimHashes.Vacuum,
				new MathUtil.MinMax(100f, 200f)
			} }, null, Db.Get().ArtifactDropRates.Perfect, 0, 0, 0, true));
			this.Earth = base.Add(new SpaceDestinationType("Earth", parent, UI.SPACEDESTINATIONS.PLANETS.SHATTEREDPLANET.NAME, UI.SPACEDESTINATIONS.PLANETS.SHATTEREDPLANET.DESCRIPTION, 96, "earth", new Dictionary<SimHashes, MathUtil.MinMax>(), null, Db.Get().ArtifactDropRates.None, 0, 0, 0, false));
		}

		// Token: 0x04004A28 RID: 18984
		public SpaceDestinationType Satellite;

		// Token: 0x04004A29 RID: 18985
		public SpaceDestinationType MetallicAsteroid;

		// Token: 0x04004A2A RID: 18986
		public SpaceDestinationType RockyAsteroid;

		// Token: 0x04004A2B RID: 18987
		public SpaceDestinationType CarbonaceousAsteroid;

		// Token: 0x04004A2C RID: 18988
		public SpaceDestinationType IcyDwarf;

		// Token: 0x04004A2D RID: 18989
		public SpaceDestinationType OrganicDwarf;

		// Token: 0x04004A2E RID: 18990
		public SpaceDestinationType DustyMoon;

		// Token: 0x04004A2F RID: 18991
		public SpaceDestinationType TerraPlanet;

		// Token: 0x04004A30 RID: 18992
		public SpaceDestinationType VolcanoPlanet;

		// Token: 0x04004A31 RID: 18993
		public SpaceDestinationType GasGiant;

		// Token: 0x04004A32 RID: 18994
		public SpaceDestinationType IceGiant;

		// Token: 0x04004A33 RID: 18995
		public SpaceDestinationType Wormhole;

		// Token: 0x04004A34 RID: 18996
		public SpaceDestinationType SaltDwarf;

		// Token: 0x04004A35 RID: 18997
		public SpaceDestinationType RustPlanet;

		// Token: 0x04004A36 RID: 18998
		public SpaceDestinationType ForestPlanet;

		// Token: 0x04004A37 RID: 18999
		public SpaceDestinationType RedDwarf;

		// Token: 0x04004A38 RID: 19000
		public SpaceDestinationType GoldAsteroid;

		// Token: 0x04004A39 RID: 19001
		public SpaceDestinationType HydrogenGiant;

		// Token: 0x04004A3A RID: 19002
		public SpaceDestinationType OilyAsteroid;

		// Token: 0x04004A3B RID: 19003
		public SpaceDestinationType ShinyPlanet;

		// Token: 0x04004A3C RID: 19004
		public SpaceDestinationType ChlorinePlanet;

		// Token: 0x04004A3D RID: 19005
		public SpaceDestinationType SaltDesertPlanet;

		// Token: 0x04004A3E RID: 19006
		public SpaceDestinationType Earth;

		// Token: 0x04004A3F RID: 19007
		public static Dictionary<SimHashes, MathUtil.MinMax> extendedElementTable = new Dictionary<SimHashes, MathUtil.MinMax>
		{
			{
				SimHashes.Niobium,
				new MathUtil.MinMax(10f, 20f)
			},
			{
				SimHashes.Katairite,
				new MathUtil.MinMax(50f, 100f)
			},
			{
				SimHashes.Isoresin,
				new MathUtil.MinMax(30f, 60f)
			},
			{
				SimHashes.Fullerene,
				new MathUtil.MinMax(0.5f, 1f)
			}
		};
	}
}
