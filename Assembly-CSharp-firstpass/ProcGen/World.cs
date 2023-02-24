using System;
using System.Collections.Generic;
using System.Linq;
using Klei;

namespace ProcGen
{
	// Token: 0x020004DD RID: 1245
	[Serializable]
	public class World
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x0007561C File Offset: 0x0007381C
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x00075624 File Offset: 0x00073824
		public string name { get; private set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x0007562D File Offset: 0x0007382D
		// (set) Token: 0x0600358D RID: 13709 RVA: 0x00075635 File Offset: 0x00073835
		public string description { get; private set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x0007563E File Offset: 0x0007383E
		// (set) Token: 0x0600358F RID: 13711 RVA: 0x00075646 File Offset: 0x00073846
		public string[] nameTables { get; private set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x0007564F File Offset: 0x0007384F
		// (set) Token: 0x06003591 RID: 13713 RVA: 0x00075657 File Offset: 0x00073857
		public string asteroidIcon { get; private set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x00075660 File Offset: 0x00073860
		// (set) Token: 0x06003593 RID: 13715 RVA: 0x00075668 File Offset: 0x00073868
		public float iconScale { get; private set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x00075671 File Offset: 0x00073871
		// (set) Token: 0x06003595 RID: 13717 RVA: 0x00075679 File Offset: 0x00073879
		public bool disableWorldTraits { get; private set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x00075682 File Offset: 0x00073882
		// (set) Token: 0x06003597 RID: 13719 RVA: 0x0007568A File Offset: 0x0007388A
		public List<World.TraitRule> worldTraitRules { get; private set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x00075693 File Offset: 0x00073893
		// (set) Token: 0x06003599 RID: 13721 RVA: 0x0007569B File Offset: 0x0007389B
		public float worldTraitScale { get; private set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000756A4 File Offset: 0x000738A4
		// (set) Token: 0x0600359B RID: 13723 RVA: 0x000756AC File Offset: 0x000738AC
		public World.Skip skip { get; private set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000756B5 File Offset: 0x000738B5
		// (set) Token: 0x0600359D RID: 13725 RVA: 0x000756BD File Offset: 0x000738BD
		public bool moduleInterior { get; private set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600359E RID: 13726 RVA: 0x000756C6 File Offset: 0x000738C6
		// (set) Token: 0x0600359F RID: 13727 RVA: 0x000756CE File Offset: 0x000738CE
		public World.WorldCategory category { get; private set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000756D7 File Offset: 0x000738D7
		// (set) Token: 0x060035A1 RID: 13729 RVA: 0x000756DF File Offset: 0x000738DF
		public Vector2I worldsize { get; private set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000756E8 File Offset: 0x000738E8
		// (set) Token: 0x060035A3 RID: 13731 RVA: 0x000756F0 File Offset: 0x000738F0
		public DefaultSettings defaultsOverrides { get; private set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000756F9 File Offset: 0x000738F9
		// (set) Token: 0x060035A5 RID: 13733 RVA: 0x00075701 File Offset: 0x00073901
		public World.LayoutMethod layoutMethod { get; private set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x0007570A File Offset: 0x0007390A
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x00075712 File Offset: 0x00073912
		public List<WeightedSubworldName> subworldFiles { get; private set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x0007571B File Offset: 0x0007391B
		// (set) Token: 0x060035A9 RID: 13737 RVA: 0x00075723 File Offset: 0x00073923
		public List<World.AllowedCellsFilter> unknownCellsAllowedSubworlds { get; private set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x0007572C File Offset: 0x0007392C
		// (set) Token: 0x060035AB RID: 13739 RVA: 0x00075734 File Offset: 0x00073934
		public string startSubworldName { get; private set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x0007573D File Offset: 0x0007393D
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x00075745 File Offset: 0x00073945
		public string startingBaseTemplate { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x0007574E File Offset: 0x0007394E
		// (set) Token: 0x060035AF RID: 13743 RVA: 0x00075756 File Offset: 0x00073956
		public MinMax startingBasePositionHorizontal { get; private set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x0007575F File Offset: 0x0007395F
		// (set) Token: 0x060035B1 RID: 13745 RVA: 0x00075767 File Offset: 0x00073967
		public MinMax startingBasePositionVertical { get; private set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x00075770 File Offset: 0x00073970
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x00075778 File Offset: 0x00073978
		public Dictionary<string, int> globalFeatures { get; private set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x00075781 File Offset: 0x00073981
		// (set) Token: 0x060035B5 RID: 13749 RVA: 0x00075789 File Offset: 0x00073989
		public List<World.TemplateSpawnRules> worldTemplateRules { get; private set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x00075792 File Offset: 0x00073992
		// (set) Token: 0x060035B7 RID: 13751 RVA: 0x0007579A File Offset: 0x0007399A
		public List<string> seasons { get; private set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x000757A3 File Offset: 0x000739A3
		// (set) Token: 0x060035B9 RID: 13753 RVA: 0x000757AB File Offset: 0x000739AB
		public List<string> fixedTraits { get; private set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000757B4 File Offset: 0x000739B4
		// (set) Token: 0x060035BB RID: 13755 RVA: 0x000757BC File Offset: 0x000739BC
		public bool adjacentTemporalTear { get; private set; }

		// Token: 0x060035BC RID: 13756 RVA: 0x000757C8 File Offset: 0x000739C8
		public World()
		{
			this.subworldFiles = new List<WeightedSubworldName>();
			this.unknownCellsAllowedSubworlds = new List<World.AllowedCellsFilter>();
			this.startingBasePositionHorizontal = new MinMax(0.5f, 0.5f);
			this.startingBasePositionVertical = new MinMax(0.5f, 0.5f);
			this.globalFeatures = new Dictionary<string, int>();
			this.seasons = new List<string>();
			this.fixedTraits = new List<string>();
			this.category = World.WorldCategory.Asteroid;
			this.worldTraitScale = 1f;
			this.iconScale = 1f;
			this.worldTraitRules = new List<World.TraitRule>
			{
				new World.TraitRule(2, 4)
			};
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x00075874 File Offset: 0x00073A74
		public void ModStartLocation(MinMax hMod, MinMax vMod)
		{
			MinMax startingBasePositionHorizontal = this.startingBasePositionHorizontal;
			MinMax startingBasePositionVertical = this.startingBasePositionVertical;
			startingBasePositionHorizontal.Mod(hMod);
			startingBasePositionVertical.Mod(vMod);
			this.startingBasePositionHorizontal = startingBasePositionHorizontal;
			this.startingBasePositionVertical = startingBasePositionVertical;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000758B0 File Offset: 0x00073AB0
		public void Validate()
		{
			if (this.unknownCellsAllowedSubworlds != null)
			{
				List<string> usedSubworldFiles = new List<string>();
				this.subworldFiles.ForEach(delegate(WeightedSubworldName x)
				{
					usedSubworldFiles.Add(x.name);
				});
				foreach (World.AllowedCellsFilter allowedCellsFilter in this.unknownCellsAllowedSubworlds)
				{
					allowedCellsFilter.Validate(this.name, this.subworldFiles);
					if (allowedCellsFilter.subworldNames != null)
					{
						foreach (string text in allowedCellsFilter.subworldNames)
						{
							usedSubworldFiles.Remove(text);
						}
					}
				}
				usedSubworldFiles.Remove(this.startSubworldName);
				if (usedSubworldFiles.Count > 0)
				{
					DebugUtil.LogWarningArgs(new object[] { "World " + this.filePath + ": defines subworldNames that are not used in unknownCellsAllowedSubworlds: \n" + string.Join(", ", usedSubworldFiles) });
				}
			}
			if (this.worldTraitRules != null)
			{
				foreach (World.TraitRule traitRule in this.worldTraitRules)
				{
					traitRule.Validate();
				}
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x00075A30 File Offset: 0x00073C30
		public bool IsValidTrait(WorldTrait trait)
		{
			foreach (World.TraitRule traitRule in this.worldTraitRules)
			{
				if (traitRule.specificTraits == null)
				{
					TagSet tagSet = ((traitRule.requiredTags != null) ? new TagSet(traitRule.requiredTags) : null);
					TagSet tagSet2 = ((traitRule.forbiddenTags != null) ? new TagSet(traitRule.forbiddenTags) : null);
					if ((tagSet == null || trait.traitTagsSet.ContainsAll(tagSet)) && (tagSet2 == null || !trait.traitTagsSet.ContainsOne(tagSet2)) && (traitRule.forbiddenTraits == null || !traitRule.forbiddenTraits.Contains(trait.filePath)) && trait.IsValid(this, false))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040012E6 RID: 4838
		public string filePath;

		// Token: 0x02000B03 RID: 2819
		public enum WorldCategory
		{
			// Token: 0x040025B8 RID: 9656
			Asteroid,
			// Token: 0x040025B9 RID: 9657
			Moon
		}

		// Token: 0x02000B04 RID: 2820
		public enum Skip
		{
			// Token: 0x040025BB RID: 9659
			Never,
			// Token: 0x040025BC RID: 9660
			False = 0,
			// Token: 0x040025BD RID: 9661
			Always = 99,
			// Token: 0x040025BE RID: 9662
			True = 99,
			// Token: 0x040025BF RID: 9663
			EditorOnly
		}

		// Token: 0x02000B05 RID: 2821
		public enum LayoutMethod
		{
			// Token: 0x040025C1 RID: 9665
			Default,
			// Token: 0x040025C2 RID: 9666
			VoronoiTree = 0,
			// Token: 0x040025C3 RID: 9667
			PowerTree
		}

		// Token: 0x02000B06 RID: 2822
		[Serializable]
		public class TraitRule
		{
			// Token: 0x17000EDC RID: 3804
			// (get) Token: 0x0600580D RID: 22541 RVA: 0x000A41C3 File Offset: 0x000A23C3
			// (set) Token: 0x0600580E RID: 22542 RVA: 0x000A41CB File Offset: 0x000A23CB
			public int min { get; private set; }

			// Token: 0x17000EDD RID: 3805
			// (get) Token: 0x0600580F RID: 22543 RVA: 0x000A41D4 File Offset: 0x000A23D4
			// (set) Token: 0x06005810 RID: 22544 RVA: 0x000A41DC File Offset: 0x000A23DC
			public int max { get; private set; }

			// Token: 0x17000EDE RID: 3806
			// (get) Token: 0x06005811 RID: 22545 RVA: 0x000A41E5 File Offset: 0x000A23E5
			// (set) Token: 0x06005812 RID: 22546 RVA: 0x000A41ED File Offset: 0x000A23ED
			public List<string> requiredTags { get; private set; }

			// Token: 0x17000EDF RID: 3807
			// (get) Token: 0x06005813 RID: 22547 RVA: 0x000A41F6 File Offset: 0x000A23F6
			// (set) Token: 0x06005814 RID: 22548 RVA: 0x000A41FE File Offset: 0x000A23FE
			public List<string> specificTraits { get; private set; }

			// Token: 0x17000EE0 RID: 3808
			// (get) Token: 0x06005815 RID: 22549 RVA: 0x000A4207 File Offset: 0x000A2407
			// (set) Token: 0x06005816 RID: 22550 RVA: 0x000A420F File Offset: 0x000A240F
			public List<string> forbiddenTags { get; private set; }

			// Token: 0x17000EE1 RID: 3809
			// (get) Token: 0x06005817 RID: 22551 RVA: 0x000A4218 File Offset: 0x000A2418
			// (set) Token: 0x06005818 RID: 22552 RVA: 0x000A4220 File Offset: 0x000A2420
			public List<string> forbiddenTraits { get; private set; }

			// Token: 0x06005819 RID: 22553 RVA: 0x000A4229 File Offset: 0x000A2429
			public TraitRule()
			{
			}

			// Token: 0x0600581A RID: 22554 RVA: 0x000A4231 File Offset: 0x000A2431
			public TraitRule(int min, int max)
			{
				this.min = min;
				this.max = max;
			}

			// Token: 0x0600581B RID: 22555 RVA: 0x000A4248 File Offset: 0x000A2448
			public void Validate()
			{
				if (this.specificTraits != null)
				{
					DebugUtil.DevAssert(this.requiredTags == null, "TraitRule using specificTraits does not support requiredTags", null);
					DebugUtil.DevAssert(this.forbiddenTags == null, "TraitRule using specificTraits does not support forbiddenTags", null);
					DebugUtil.DevAssert(this.forbiddenTraits == null, "TraitRule using specificTraits does not support forbiddenTraits", null);
				}
			}
		}

		// Token: 0x02000B07 RID: 2823
		[Serializable]
		public class TemplateSpawnRules
		{
			// Token: 0x0600581C RID: 22556 RVA: 0x000A4299 File Offset: 0x000A2499
			public TemplateSpawnRules()
			{
				this.times = 1;
				this.allowedCellsFilter = new List<World.AllowedCellsFilter>();
				this.allowDuplicates = false;
				this.useRelaxedFiltering = false;
				this.overrideOffset = Vector2I.zero;
			}

			// Token: 0x17000EE2 RID: 3810
			// (get) Token: 0x0600581D RID: 22557 RVA: 0x000A42CC File Offset: 0x000A24CC
			// (set) Token: 0x0600581E RID: 22558 RVA: 0x000A42D4 File Offset: 0x000A24D4
			public string ruleId { get; private set; }

			// Token: 0x17000EE3 RID: 3811
			// (get) Token: 0x0600581F RID: 22559 RVA: 0x000A42DD File Offset: 0x000A24DD
			// (set) Token: 0x06005820 RID: 22560 RVA: 0x000A42E5 File Offset: 0x000A24E5
			public List<string> names { get; private set; }

			// Token: 0x17000EE4 RID: 3812
			// (get) Token: 0x06005821 RID: 22561 RVA: 0x000A42EE File Offset: 0x000A24EE
			// (set) Token: 0x06005822 RID: 22562 RVA: 0x000A42F6 File Offset: 0x000A24F6
			public World.TemplateSpawnRules.ListRule listRule { get; private set; }

			// Token: 0x17000EE5 RID: 3813
			// (get) Token: 0x06005823 RID: 22563 RVA: 0x000A42FF File Offset: 0x000A24FF
			// (set) Token: 0x06005824 RID: 22564 RVA: 0x000A4307 File Offset: 0x000A2507
			public int someCount { get; private set; }

			// Token: 0x17000EE6 RID: 3814
			// (get) Token: 0x06005825 RID: 22565 RVA: 0x000A4310 File Offset: 0x000A2510
			// (set) Token: 0x06005826 RID: 22566 RVA: 0x000A4318 File Offset: 0x000A2518
			public int moreCount { get; private set; }

			// Token: 0x17000EE7 RID: 3815
			// (get) Token: 0x06005827 RID: 22567 RVA: 0x000A4321 File Offset: 0x000A2521
			// (set) Token: 0x06005828 RID: 22568 RVA: 0x000A4329 File Offset: 0x000A2529
			public int times { get; private set; }

			// Token: 0x17000EE8 RID: 3816
			// (get) Token: 0x06005829 RID: 22569 RVA: 0x000A4332 File Offset: 0x000A2532
			// (set) Token: 0x0600582A RID: 22570 RVA: 0x000A433A File Offset: 0x000A253A
			public float priority { get; private set; }

			// Token: 0x17000EE9 RID: 3817
			// (get) Token: 0x0600582B RID: 22571 RVA: 0x000A4343 File Offset: 0x000A2543
			// (set) Token: 0x0600582C RID: 22572 RVA: 0x000A434B File Offset: 0x000A254B
			public bool allowDuplicates { get; private set; }

			// Token: 0x17000EEA RID: 3818
			// (get) Token: 0x0600582D RID: 22573 RVA: 0x000A4354 File Offset: 0x000A2554
			// (set) Token: 0x0600582E RID: 22574 RVA: 0x000A435C File Offset: 0x000A255C
			public bool allowExtremeTemperatureOverlap { get; private set; }

			// Token: 0x17000EEB RID: 3819
			// (get) Token: 0x0600582F RID: 22575 RVA: 0x000A4365 File Offset: 0x000A2565
			// (set) Token: 0x06005830 RID: 22576 RVA: 0x000A436D File Offset: 0x000A256D
			public bool useRelaxedFiltering { get; private set; }

			// Token: 0x17000EEC RID: 3820
			// (get) Token: 0x06005831 RID: 22577 RVA: 0x000A4376 File Offset: 0x000A2576
			// (set) Token: 0x06005832 RID: 22578 RVA: 0x000A437E File Offset: 0x000A257E
			public Vector2I overrideOffset { get; set; }

			// Token: 0x17000EED RID: 3821
			// (get) Token: 0x06005833 RID: 22579 RVA: 0x000A4387 File Offset: 0x000A2587
			// (set) Token: 0x06005834 RID: 22580 RVA: 0x000A438F File Offset: 0x000A258F
			public List<World.AllowedCellsFilter> allowedCellsFilter { get; private set; }

			// Token: 0x06005835 RID: 22581 RVA: 0x000A4398 File Offset: 0x000A2598
			public bool IsGuaranteeRule()
			{
				switch (this.listRule)
				{
				case World.TemplateSpawnRules.ListRule.GuaranteeOne:
					return true;
				case World.TemplateSpawnRules.ListRule.GuaranteeSome:
					return true;
				case World.TemplateSpawnRules.ListRule.GuaranteeSomeTryMore:
					return true;
				case World.TemplateSpawnRules.ListRule.GuaranteeAll:
					return true;
				default:
					return false;
				}
			}

			// Token: 0x02000B53 RID: 2899
			public enum ListRule
			{
				// Token: 0x040026B1 RID: 9905
				GuaranteeOne,
				// Token: 0x040026B2 RID: 9906
				GuaranteeSome,
				// Token: 0x040026B3 RID: 9907
				GuaranteeSomeTryMore,
				// Token: 0x040026B4 RID: 9908
				GuaranteeAll,
				// Token: 0x040026B5 RID: 9909
				TryOne,
				// Token: 0x040026B6 RID: 9910
				TrySome,
				// Token: 0x040026B7 RID: 9911
				TryAll
			}
		}

		// Token: 0x02000B08 RID: 2824
		[Serializable]
		public class AllowedCellsFilter
		{
			// Token: 0x06005836 RID: 22582 RVA: 0x000A43CD File Offset: 0x000A25CD
			public AllowedCellsFilter()
			{
				this.temperatureRanges = new List<Temperature.Range>();
				this.zoneTypes = new List<SubWorld.ZoneType>();
				this.subworldNames = new List<string>();
				this.command = World.AllowedCellsFilter.Command.Replace;
				this.optional = false;
			}

			// Token: 0x17000EEE RID: 3822
			// (get) Token: 0x06005837 RID: 22583 RVA: 0x000A4404 File Offset: 0x000A2604
			// (set) Token: 0x06005838 RID: 22584 RVA: 0x000A440C File Offset: 0x000A260C
			public World.AllowedCellsFilter.TagCommand tagcommand { get; private set; }

			// Token: 0x17000EEF RID: 3823
			// (get) Token: 0x06005839 RID: 22585 RVA: 0x000A4415 File Offset: 0x000A2615
			// (set) Token: 0x0600583A RID: 22586 RVA: 0x000A441D File Offset: 0x000A261D
			public string tag { get; private set; }

			// Token: 0x17000EF0 RID: 3824
			// (get) Token: 0x0600583B RID: 22587 RVA: 0x000A4426 File Offset: 0x000A2626
			// (set) Token: 0x0600583C RID: 22588 RVA: 0x000A442E File Offset: 0x000A262E
			public int minDistance { get; private set; }

			// Token: 0x17000EF1 RID: 3825
			// (get) Token: 0x0600583D RID: 22589 RVA: 0x000A4437 File Offset: 0x000A2637
			// (set) Token: 0x0600583E RID: 22590 RVA: 0x000A443F File Offset: 0x000A263F
			public int maxDistance { get; private set; }

			// Token: 0x17000EF2 RID: 3826
			// (get) Token: 0x0600583F RID: 22591 RVA: 0x000A4448 File Offset: 0x000A2648
			// (set) Token: 0x06005840 RID: 22592 RVA: 0x000A4450 File Offset: 0x000A2650
			public World.AllowedCellsFilter.Command command { get; private set; }

			// Token: 0x17000EF3 RID: 3827
			// (get) Token: 0x06005841 RID: 22593 RVA: 0x000A4459 File Offset: 0x000A2659
			// (set) Token: 0x06005842 RID: 22594 RVA: 0x000A4461 File Offset: 0x000A2661
			public List<Temperature.Range> temperatureRanges { get; private set; }

			// Token: 0x17000EF4 RID: 3828
			// (get) Token: 0x06005843 RID: 22595 RVA: 0x000A446A File Offset: 0x000A266A
			// (set) Token: 0x06005844 RID: 22596 RVA: 0x000A4472 File Offset: 0x000A2672
			public List<SubWorld.ZoneType> zoneTypes { get; private set; }

			// Token: 0x17000EF5 RID: 3829
			// (get) Token: 0x06005845 RID: 22597 RVA: 0x000A447B File Offset: 0x000A267B
			// (set) Token: 0x06005846 RID: 22598 RVA: 0x000A4483 File Offset: 0x000A2683
			public List<string> subworldNames { get; private set; }

			// Token: 0x17000EF6 RID: 3830
			// (get) Token: 0x06005847 RID: 22599 RVA: 0x000A448C File Offset: 0x000A268C
			// (set) Token: 0x06005848 RID: 22600 RVA: 0x000A4494 File Offset: 0x000A2694
			public bool optional { get; set; }

			// Token: 0x06005849 RID: 22601 RVA: 0x000A44A0 File Offset: 0x000A26A0
			public void Validate(string parentFile, List<WeightedSubworldName> parentCachedFiles)
			{
				if (this.subworldNames != null)
				{
					using (List<string>.Enumerator enumerator = this.subworldNames.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string subworld = enumerator.Current;
							DebugUtil.DevAssert(parentCachedFiles.Any((WeightedSubworldName val) => val.name == subworld), string.Concat(new string[] { "World ", parentFile, ": should include ", subworld, " in its subworldFiles since it's used in a command" }), null);
							DebugUtil.DevAssert(FileSystem.FileExists(SettingsCache.RewriteWorldgenPathYaml(subworld)), "World " + parentFile + ": Incorrect subworldFile " + subworld, null);
						}
					}
				}
			}

			// Token: 0x02000B54 RID: 2900
			public enum TagCommand
			{
				// Token: 0x040026B9 RID: 9913
				Default,
				// Token: 0x040026BA RID: 9914
				AtTag,
				// Token: 0x040026BB RID: 9915
				NotAtTag,
				// Token: 0x040026BC RID: 9916
				DistanceFromTag
			}

			// Token: 0x02000B55 RID: 2901
			public enum Command
			{
				// Token: 0x040026BE RID: 9918
				Clear,
				// Token: 0x040026BF RID: 9919
				Replace,
				// Token: 0x040026C0 RID: 9920
				UnionWith,
				// Token: 0x040026C1 RID: 9921
				IntersectWith,
				// Token: 0x040026C2 RID: 9922
				ExceptWith,
				// Token: 0x040026C3 RID: 9923
				SymmetricExceptWith,
				// Token: 0x040026C4 RID: 9924
				All
			}
		}
	}
}
