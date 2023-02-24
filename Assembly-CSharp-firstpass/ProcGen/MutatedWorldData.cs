using System;
using System.Collections.Generic;
using ObjectCloner;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004DE RID: 1246
	public class MutatedWorldData
	{
		// Token: 0x060035C0 RID: 13760 RVA: 0x00075B08 File Offset: 0x00073D08
		public MutatedWorldData(World world, List<WorldTrait> worldTraits, List<WorldTrait> storyTraits)
		{
			this.world = SerializingCloner.Copy<World>(world);
			this.worldTraits = new List<WorldTrait>();
			if (worldTraits != null)
			{
				this.worldTraits.AddRange(worldTraits);
			}
			this.storyTraits = new List<WorldTrait>();
			if (storyTraits != null)
			{
				this.storyTraits.AddRange(storyTraits);
			}
			this.storyTraitCandidates = new List<WorldTrait>();
			SettingsCache.CloneInToNewWorld(this);
			this.ApplyWorldTraits();
			foreach (ElementBandConfiguration elementBandConfiguration in this.biomes.BiomeBackgroundElementBandConfigurations.Values)
			{
				elementBandConfiguration.ConvertBandSizeToMaxSize();
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x00075BC0 File Offset: 0x00073DC0
		private void ApplyWorldTraits()
		{
			foreach (WorldTrait worldTrait in this.worldTraits)
			{
				this.ApplyTrait(worldTrait);
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x00075C14 File Offset: 0x00073E14
		private void ApplyTrait(WorldTrait trait)
		{
			this.world.ModStartLocation(trait.startingBasePositionHorizontalMod, trait.startingBasePositionVerticalMod);
			foreach (WeightedSubworldName weightedSubworldName in trait.additionalSubworldFiles)
			{
				this.world.subworldFiles.Add(weightedSubworldName);
			}
			foreach (World.AllowedCellsFilter allowedCellsFilter in trait.additionalUnknownCellFilters)
			{
				this.world.unknownCellsAllowedSubworlds.Add(allowedCellsFilter);
			}
			foreach (KeyValuePair<string, int> keyValuePair in trait.globalFeatureMods)
			{
				if (!this.world.globalFeatures.ContainsKey(keyValuePair.Key))
				{
					this.world.globalFeatures[keyValuePair.Key] = 0;
				}
				int num = Mathf.FloorToInt(this.world.worldTraitScale * (float)keyValuePair.Value);
				Dictionary<string, int> globalFeatures = this.world.globalFeatures;
				string key = keyValuePair.Key;
				globalFeatures[key] += num;
			}
			using (List<string>.Enumerator enumerator4 = trait.removeWorldTemplateRulesById.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					string rule = enumerator4.Current;
					this.world.worldTemplateRules.RemoveAll((World.TemplateSpawnRules x) => x.ruleId == rule);
				}
			}
			foreach (World.TemplateSpawnRules templateSpawnRules in trait.additionalWorldTemplateRules)
			{
				this.world.worldTemplateRules.Add(templateSpawnRules);
			}
			foreach (KeyValuePair<string, ElementBandConfiguration> keyValuePair2 in this.biomes.BiomeBackgroundElementBandConfigurations)
			{
				foreach (ElementGradient elementGradient in keyValuePair2.Value)
				{
					foreach (WorldTrait.ElementBandModifier elementBandModifier in trait.elementBandModifiers)
					{
						if (elementBandModifier.element == elementGradient.content)
						{
							elementGradient.Mod(elementBandModifier);
						}
					}
				}
			}
		}

		// Token: 0x040012E7 RID: 4839
		public World world;

		// Token: 0x040012E8 RID: 4840
		public List<WorldTrait> worldTraits;

		// Token: 0x040012E9 RID: 4841
		public List<WorldTrait> storyTraits;

		// Token: 0x040012EA RID: 4842
		public Dictionary<string, SubWorld> subworlds;

		// Token: 0x040012EB RID: 4843
		public Dictionary<string, FeatureSettings> features;

		// Token: 0x040012EC RID: 4844
		public TerrainElementBandSettings biomes;

		// Token: 0x040012ED RID: 4845
		public MobSettings mobs;

		// Token: 0x040012EE RID: 4846
		public List<WorldTrait> storyTraitCandidates;
	}
}
