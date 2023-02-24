using System;
using System.Collections.Generic;
using KSerialization.Converters;

namespace ProcGen
{
	// Token: 0x020004BE RID: 1214
	[Serializable]
	public class SubWorld : SampleDescriber
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000718AD File Offset: 0x0006FAAD
		// (set) Token: 0x06003410 RID: 13328 RVA: 0x000718B5 File Offset: 0x0006FAB5
		public string nameKey { get; protected set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000718BE File Offset: 0x0006FABE
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x000718C6 File Offset: 0x0006FAC6
		public string descriptionKey { get; protected set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000718CF File Offset: 0x0006FACF
		// (set) Token: 0x06003414 RID: 13332 RVA: 0x000718D7 File Offset: 0x0006FAD7
		public string utilityKey { get; protected set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000718E0 File Offset: 0x0006FAE0
		// (set) Token: 0x06003416 RID: 13334 RVA: 0x000718E8 File Offset: 0x0006FAE8
		public string biomeNoise { get; protected set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000718F1 File Offset: 0x0006FAF1
		// (set) Token: 0x06003418 RID: 13336 RVA: 0x000718F9 File Offset: 0x0006FAF9
		public string overrideNoise { get; protected set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06003419 RID: 13337 RVA: 0x00071902 File Offset: 0x0006FB02
		// (set) Token: 0x0600341A RID: 13338 RVA: 0x0007190A File Offset: 0x0006FB0A
		public string densityNoise { get; protected set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x00071913 File Offset: 0x0006FB13
		// (set) Token: 0x0600341C RID: 13340 RVA: 0x0007191B File Offset: 0x0006FB1B
		public string borderOverride { get; protected set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x00071924 File Offset: 0x0006FB24
		// (set) Token: 0x0600341E RID: 13342 RVA: 0x0007192C File Offset: 0x0006FB2C
		public int borderOverridePriority { get; protected set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x00071935 File Offset: 0x0006FB35
		// (set) Token: 0x06003420 RID: 13344 RVA: 0x0007193D File Offset: 0x0006FB3D
		public MinMax borderSizeOverride { get; protected set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x00071946 File Offset: 0x0006FB46
		// (set) Token: 0x06003422 RID: 13346 RVA: 0x0007194E File Offset: 0x0006FB4E
		[StringEnumConverter]
		public Temperature.Range temperatureRange { get; protected set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x00071957 File Offset: 0x0006FB57
		// (set) Token: 0x06003424 RID: 13348 RVA: 0x0007195F File Offset: 0x0006FB5F
		public Feature centralFeature { get; protected set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x00071968 File Offset: 0x0006FB68
		// (set) Token: 0x06003426 RID: 13350 RVA: 0x00071970 File Offset: 0x0006FB70
		public List<Feature> features { get; protected set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x00071979 File Offset: 0x0006FB79
		// (set) Token: 0x06003428 RID: 13352 RVA: 0x00071981 File Offset: 0x0006FB81
		public SampleDescriber.Override overrides { get; protected set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x0007198A File Offset: 0x0006FB8A
		// (set) Token: 0x0600342A RID: 13354 RVA: 0x00071992 File Offset: 0x0006FB92
		public List<string> tags { get; protected set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x0007199B File Offset: 0x0006FB9B
		// (set) Token: 0x0600342C RID: 13356 RVA: 0x000719A3 File Offset: 0x0006FBA3
		public int minChildCount { get; protected set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000719AC File Offset: 0x0006FBAC
		// (set) Token: 0x0600342E RID: 13358 RVA: 0x000719B4 File Offset: 0x0006FBB4
		public bool singleChildCount { get; protected set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000719BD File Offset: 0x0006FBBD
		// (set) Token: 0x06003430 RID: 13360 RVA: 0x000719C5 File Offset: 0x0006FBC5
		public int extraBiomeChildren { get; protected set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000719CE File Offset: 0x0006FBCE
		// (set) Token: 0x06003432 RID: 13362 RVA: 0x000719D6 File Offset: 0x0006FBD6
		public List<WeightedBiome> biomes { get; protected set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000719DF File Offset: 0x0006FBDF
		// (set) Token: 0x06003434 RID: 13364 RVA: 0x000719E7 File Offset: 0x0006FBE7
		public Dictionary<string, int> featureTemplates { get; protected set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000719F0 File Offset: 0x0006FBF0
		// (set) Token: 0x06003436 RID: 13366 RVA: 0x000719F8 File Offset: 0x0006FBF8
		public List<World.TemplateSpawnRules> subworldTemplateRules { get; protected set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x00071A01 File Offset: 0x0006FC01
		// (set) Token: 0x06003438 RID: 13368 RVA: 0x00071A09 File Offset: 0x0006FC09
		public int iterations { get; protected set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x00071A12 File Offset: 0x0006FC12
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x00071A1A File Offset: 0x0006FC1A
		public float minEnergy { get; protected set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x00071A23 File Offset: 0x0006FC23
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x00071A2B File Offset: 0x0006FC2B
		public SubWorld.ZoneType zoneType { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x00071A34 File Offset: 0x0006FC34
		// (set) Token: 0x0600343E RID: 13374 RVA: 0x00071A3C File Offset: 0x0006FC3C
		public List<SampleDescriber> samplers { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x00071A45 File Offset: 0x0006FC45
		// (set) Token: 0x06003440 RID: 13376 RVA: 0x00071A4D File Offset: 0x0006FC4D
		public float pdWeight { get; private set; }

		// Token: 0x06003441 RID: 13377 RVA: 0x00071A58 File Offset: 0x0006FC58
		public SubWorld()
		{
			this.minChildCount = 2;
			this.features = new List<Feature>();
			this.tags = new List<string>();
			this.biomes = new List<WeightedBiome>();
			this.samplers = new List<SampleDescriber>();
			this.featureTemplates = new Dictionary<string, int>();
			this.pdWeight = 1f;
			this.borderSizeOverride = new MinMax(1f, 2.5f);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00071ACC File Offset: 0x0006FCCC
		public void EnforceTemplateSpawnRuleSelfConsistency()
		{
			if (this.subworldTemplateRules == null)
			{
				return;
			}
			foreach (World.TemplateSpawnRules templateSpawnRules in this.subworldTemplateRules)
			{
				bool flag = true;
				foreach (World.AllowedCellsFilter allowedCellsFilter in templateSpawnRules.allowedCellsFilter)
				{
					DebugUtil.DevAssert(allowedCellsFilter.command != World.AllowedCellsFilter.Command.Replace, "subworldTemplateRules in " + base.name + " contains an AllowedCellsFilter with Command.Replace, which replaces the implicit subworld filter.", null);
					DebugUtil.Assert(allowedCellsFilter.zoneTypes == null || allowedCellsFilter.zoneTypes.Count == 0, "subworldTemplateRules in " + base.name + " contains zoneTypes, which is unsupported since there is an implicit subworld filter. Use worldTemplateRules instead.");
					DebugUtil.Assert(allowedCellsFilter.command != World.AllowedCellsFilter.Command.All || flag, "subworldTemplateRules in " + base.name + " contains an All command that's not the first filter in the list.");
					flag = false;
				}
				DebugUtil.Assert(!templateSpawnRules.IsGuaranteeRule(), "subworldTemplateRules in " + base.name + " contains a guaranteed rule, which is not allowed. Include such rules in worldTemplateRules.");
				World.AllowedCellsFilter allowedCellsFilter2 = new World.AllowedCellsFilter();
				allowedCellsFilter2.subworldNames.Add(base.name);
				templateSpawnRules.allowedCellsFilter.Insert(0, allowedCellsFilter2);
			}
		}

		// Token: 0x02000AF4 RID: 2804
		public enum ZoneType
		{
			// Token: 0x0400255A RID: 9562
			FrozenWastes,
			// Token: 0x0400255B RID: 9563
			CrystalCaverns,
			// Token: 0x0400255C RID: 9564
			BoggyMarsh,
			// Token: 0x0400255D RID: 9565
			Sandstone,
			// Token: 0x0400255E RID: 9566
			ToxicJungle,
			// Token: 0x0400255F RID: 9567
			MagmaCore,
			// Token: 0x04002560 RID: 9568
			OilField,
			// Token: 0x04002561 RID: 9569
			Space,
			// Token: 0x04002562 RID: 9570
			Ocean,
			// Token: 0x04002563 RID: 9571
			Rust,
			// Token: 0x04002564 RID: 9572
			Forest,
			// Token: 0x04002565 RID: 9573
			Radioactive,
			// Token: 0x04002566 RID: 9574
			Swamp,
			// Token: 0x04002567 RID: 9575
			Wasteland,
			// Token: 0x04002568 RID: 9576
			RocketInterior,
			// Token: 0x04002569 RID: 9577
			Metallic,
			// Token: 0x0400256A RID: 9578
			Barren,
			// Token: 0x0400256B RID: 9579
			Moo
		}
	}
}
