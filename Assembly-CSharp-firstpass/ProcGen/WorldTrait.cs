using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004DC RID: 1244
	[Serializable]
	public class WorldTrait
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x00075379 File Offset: 0x00073579
		// (set) Token: 0x06003568 RID: 13672 RVA: 0x00075381 File Offset: 0x00073581
		public string name { get; private set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x0007538A File Offset: 0x0007358A
		// (set) Token: 0x0600356A RID: 13674 RVA: 0x00075392 File Offset: 0x00073592
		public string description { get; private set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x0007539B File Offset: 0x0007359B
		// (set) Token: 0x0600356C RID: 13676 RVA: 0x000753A3 File Offset: 0x000735A3
		public string colorHex { get; private set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000753AC File Offset: 0x000735AC
		// (set) Token: 0x0600356E RID: 13678 RVA: 0x000753B4 File Offset: 0x000735B4
		public string icon { get; private set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000753BD File Offset: 0x000735BD
		// (set) Token: 0x06003570 RID: 13680 RVA: 0x000753C5 File Offset: 0x000735C5
		public List<string> forbiddenDLCIds { get; private set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000753CE File Offset: 0x000735CE
		// (set) Token: 0x06003572 RID: 13682 RVA: 0x000753D6 File Offset: 0x000735D6
		public List<string> exclusiveWith { get; private set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000753DF File Offset: 0x000735DF
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x000753E7 File Offset: 0x000735E7
		public List<string> exclusiveWithTags { get; private set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000753F0 File Offset: 0x000735F0
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000753F8 File Offset: 0x000735F8
		public List<string> traitTags { get; private set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x00075401 File Offset: 0x00073601
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x00075409 File Offset: 0x00073609
		public MinMax startingBasePositionHorizontalMod { get; private set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x00075412 File Offset: 0x00073612
		// (set) Token: 0x0600357A RID: 13690 RVA: 0x0007541A File Offset: 0x0007361A
		public MinMax startingBasePositionVerticalMod { get; private set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x00075423 File Offset: 0x00073623
		// (set) Token: 0x0600357C RID: 13692 RVA: 0x0007542B File Offset: 0x0007362B
		public List<WeightedSubworldName> additionalSubworldFiles { get; private set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x00075434 File Offset: 0x00073634
		// (set) Token: 0x0600357E RID: 13694 RVA: 0x0007543C File Offset: 0x0007363C
		public List<World.AllowedCellsFilter> additionalUnknownCellFilters { get; private set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x00075445 File Offset: 0x00073645
		// (set) Token: 0x06003580 RID: 13696 RVA: 0x0007544D File Offset: 0x0007364D
		public List<World.TemplateSpawnRules> additionalWorldTemplateRules { get; private set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x00075456 File Offset: 0x00073656
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x0007545E File Offset: 0x0007365E
		public Dictionary<string, int> globalFeatureMods { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x00075467 File Offset: 0x00073667
		// (set) Token: 0x06003584 RID: 13700 RVA: 0x0007546F File Offset: 0x0007366F
		public List<string> removeWorldTemplateRulesById { get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x00075478 File Offset: 0x00073678
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x00075480 File Offset: 0x00073680
		public List<WorldTrait.ElementBandModifier> elementBandModifiers { get; private set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x00075489 File Offset: 0x00073689
		public TagSet traitTagsSet
		{
			get
			{
				if (this.m_traitTagSet == null)
				{
					this.m_traitTagSet = new TagSet(this.traitTags);
				}
				return this.m_traitTagSet;
			}
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000754AC File Offset: 0x000736AC
		public WorldTrait()
		{
			this.additionalSubworldFiles = new List<WeightedSubworldName>();
			this.additionalUnknownCellFilters = new List<World.AllowedCellsFilter>();
			this.additionalWorldTemplateRules = new List<World.TemplateSpawnRules>();
			this.removeWorldTemplateRulesById = new List<string>();
			this.globalFeatureMods = new Dictionary<string, int>();
			this.elementBandModifiers = new List<WorldTrait.ElementBandModifier>();
			this.exclusiveWith = new List<string>();
			this.exclusiveWithTags = new List<string>();
			this.forbiddenDLCIds = new List<string>();
			this.traitTags = new List<string>();
			this.name = "";
			this.description = "";
			this.icon = "";
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x00075550 File Offset: 0x00073750
		public bool IsValid(World world, bool logErrors)
		{
			int num = 0;
			int num2 = 0;
			foreach (KeyValuePair<string, int> keyValuePair in this.globalFeatureMods)
			{
				num += keyValuePair.Value;
				num2 += Mathf.FloorToInt(world.worldTraitScale * (float)keyValuePair.Value);
			}
			if (this.globalFeatureMods.Count > 0 && num2 == 0)
			{
				if (logErrors)
				{
					DebugUtil.LogWarningArgs(new object[] { string.Concat(new string[] { "Trait '", this.filePath, "' cannot be applied to world '", world.name, "' due to globalFeatureMods and worldTraitScale resulting in no features being generated." }) });
				}
				return false;
			}
			return true;
		}

		// Token: 0x040012CB RID: 4811
		public string filePath;

		// Token: 0x040012CC RID: 4812
		private TagSet m_traitTagSet;

		// Token: 0x02000B02 RID: 2818
		[Serializable]
		public class ElementBandModifier
		{
			// Token: 0x17000ED9 RID: 3801
			// (get) Token: 0x06005806 RID: 22534 RVA: 0x000A4172 File Offset: 0x000A2372
			// (set) Token: 0x06005807 RID: 22535 RVA: 0x000A417A File Offset: 0x000A237A
			public string element { get; private set; }

			// Token: 0x17000EDA RID: 3802
			// (get) Token: 0x06005808 RID: 22536 RVA: 0x000A4183 File Offset: 0x000A2383
			// (set) Token: 0x06005809 RID: 22537 RVA: 0x000A418B File Offset: 0x000A238B
			public float massMultiplier { get; private set; }

			// Token: 0x17000EDB RID: 3803
			// (get) Token: 0x0600580A RID: 22538 RVA: 0x000A4194 File Offset: 0x000A2394
			// (set) Token: 0x0600580B RID: 22539 RVA: 0x000A419C File Offset: 0x000A239C
			public float bandMultiplier { get; private set; }

			// Token: 0x0600580C RID: 22540 RVA: 0x000A41A5 File Offset: 0x000A23A5
			public ElementBandModifier()
			{
				this.massMultiplier = 1f;
				this.bandMultiplier = 1f;
			}
		}
	}
}
