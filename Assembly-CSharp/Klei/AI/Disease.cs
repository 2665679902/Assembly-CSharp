using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Klei.AI.DiseaseGrowthRules;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D6F RID: 3439
	[DebuggerDisplay("{base.Id}")]
	public abstract class Disease : Resource
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06006922 RID: 26914 RVA: 0x0028D3B3 File Offset: 0x0028B5B3
		public new string Name
		{
			get
			{
				return Strings.Get(this.name);
			}
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x0028D3C8 File Offset: 0x0028B5C8
		public Disease(string id, byte strength, Disease.RangeInfo temperature_range, Disease.RangeInfo temperature_half_lives, Disease.RangeInfo pressure_range, Disease.RangeInfo pressure_half_lives, float radiation_kill_rate, bool statsOnly)
			: base(id, null, null)
		{
			this.name = new StringKey("STRINGS.DUPLICANTS.DISEASES." + id.ToUpper() + ".NAME");
			this.id = id;
			this.temperatureRange = temperature_range;
			this.temperatureHalfLives = temperature_half_lives;
			this.pressureRange = pressure_range;
			this.pressureHalfLives = pressure_half_lives;
			this.radiationKillRate = radiation_kill_rate;
			this.PopulateElemGrowthInfo();
			this.ApplyRules();
			if (!statsOnly)
			{
				DiseaseVisualization.Info info = Assets.instance.DiseaseVisualization.GetInfo(id);
				this.overlayColourName = info.overlayColourName;
				string text = Strings.Get("STRINGS.DUPLICANTS.DISEASES." + id.ToUpper() + ".LEGEND_HOVERTEXT").ToString();
				this.overlayLegendHovertext = text + DUPLICANTS.DISEASES.LEGEND_POSTAMBLE;
				Attribute attribute = new Attribute(id + "Min", "Minimum" + id.ToString(), "", "", 0f, Attribute.Display.Normal, false, null, null, null);
				Attribute attribute2 = new Attribute(id + "Max", "Maximum" + id.ToString(), "", "", 10000000f, Attribute.Display.Normal, false, null, null, null);
				this.amountDeltaAttribute = new Attribute(id + "Delta", id.ToString(), "", "", 0f, Attribute.Display.Normal, false, null, null, null);
				this.amount = new Amount(id, id + " " + DUPLICANTS.DISEASES.GERMS, id + " " + DUPLICANTS.DISEASES.GERMS, attribute, attribute2, this.amountDeltaAttribute, false, Units.Flat, 0.01f, true, null, null);
				Db.Get().Attributes.Add(attribute);
				Db.Get().Attributes.Add(attribute2);
				Db.Get().Attributes.Add(this.amountDeltaAttribute);
				this.cureSpeedBase = new Attribute(id + "CureSpeed", false, Attribute.Display.Normal, false, 0f, null, null, null, null);
				this.cureSpeedBase.BaseValue = 1f;
				this.cureSpeedBase.SetFormatter(new ToPercentAttributeFormatter(1f, GameUtil.TimeSlice.None));
				Db.Get().Attributes.Add(this.cureSpeedBase);
			}
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x0028D614 File Offset: 0x0028B814
		protected virtual void PopulateElemGrowthInfo()
		{
			this.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
			this.AddGrowthRule(new GrowthRule
			{
				underPopulationDeathRate = new float?(0f),
				minCountPerKG = new float?((float)100),
				populationHalfLife = new float?(float.PositiveInfinity),
				maxCountPerKG = new float?((float)1000),
				overPopulationHalfLife = new float?(float.PositiveInfinity),
				minDiffusionCount = new int?(1000),
				diffusionScale = new float?(0.001f),
				minDiffusionInfestationTickCount = new byte?((byte)1)
			});
			this.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
			this.AddExposureRule(new ExposureRule
			{
				populationHalfLife = new float?(float.PositiveInfinity)
			});
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x0028D6E8 File Offset: 0x0028B8E8
		protected void AddGrowthRule(GrowthRule g)
		{
			if (this.growthRules == null)
			{
				this.growthRules = new List<GrowthRule>();
				global::Debug.Assert(g.GetType() == typeof(GrowthRule), "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.underPopulationDeathRate != null, "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.populationHalfLife != null, "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.overPopulationHalfLife != null, "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.diffusionScale != null, "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.minCountPerKG != null, "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.maxCountPerKG != null, "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.minDiffusionCount != null, "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.minDiffusionInfestationTickCount != null, "First rule must be a fully defined base rule.");
			}
			else
			{
				global::Debug.Assert(g.GetType() != typeof(GrowthRule), "Subsequent rules should not be base rules");
			}
			this.growthRules.Add(g);
		}

		// Token: 0x06006926 RID: 26918 RVA: 0x0028D800 File Offset: 0x0028BA00
		protected void AddExposureRule(ExposureRule g)
		{
			if (this.exposureRules == null)
			{
				this.exposureRules = new List<ExposureRule>();
				global::Debug.Assert(g.GetType() == typeof(ExposureRule), "First rule must be a fully defined base rule.");
				global::Debug.Assert(g.populationHalfLife != null, "First rule must be a fully defined base rule.");
			}
			else
			{
				global::Debug.Assert(g.GetType() != typeof(ExposureRule), "Subsequent rules should not be base rules");
			}
			this.exposureRules.Add(g);
		}

		// Token: 0x06006927 RID: 26919 RVA: 0x0028D884 File Offset: 0x0028BA84
		public CompositeGrowthRule GetGrowthRuleForElement(Element e)
		{
			CompositeGrowthRule compositeGrowthRule = new CompositeGrowthRule();
			if (this.growthRules != null)
			{
				for (int i = 0; i < this.growthRules.Count; i++)
				{
					if (this.growthRules[i].Test(e))
					{
						compositeGrowthRule.Overlay(this.growthRules[i]);
					}
				}
			}
			return compositeGrowthRule;
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x0028D8DC File Offset: 0x0028BADC
		public CompositeExposureRule GetExposureRuleForElement(Element e)
		{
			CompositeExposureRule compositeExposureRule = new CompositeExposureRule();
			if (this.exposureRules != null)
			{
				for (int i = 0; i < this.exposureRules.Count; i++)
				{
					if (this.exposureRules[i].Test(e))
					{
						compositeExposureRule.Overlay(this.exposureRules[i]);
					}
				}
			}
			return compositeExposureRule;
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x0028D934 File Offset: 0x0028BB34
		public TagGrowthRule GetGrowthRuleForTag(Tag t)
		{
			if (this.growthRules != null)
			{
				for (int i = 0; i < this.growthRules.Count; i++)
				{
					TagGrowthRule tagGrowthRule = this.growthRules[i] as TagGrowthRule;
					if (tagGrowthRule != null && tagGrowthRule.tag == t)
					{
						return tagGrowthRule;
					}
				}
			}
			return null;
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x0028D988 File Offset: 0x0028BB88
		protected void ApplyRules()
		{
			if (this.growthRules != null)
			{
				for (int i = 0; i < this.growthRules.Count; i++)
				{
					this.growthRules[i].Apply(this.elemGrowthInfo);
				}
			}
			if (this.exposureRules != null)
			{
				for (int j = 0; j < this.exposureRules.Count; j++)
				{
					this.exposureRules[j].Apply(this.elemExposureInfo);
				}
			}
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x0028DA00 File Offset: 0x0028BC00
		protected void InitializeElemGrowthArray(ref ElemGrowthInfo[] infoArray, ElemGrowthInfo default_value)
		{
			List<Element> elements = ElementLoader.elements;
			infoArray = new ElemGrowthInfo[elements.Count];
			for (int i = 0; i < elements.Count; i++)
			{
				infoArray[i] = default_value;
			}
			infoArray[(int)ElementLoader.GetElementIndex(SimHashes.Polypropylene)] = new ElemGrowthInfo
			{
				underPopulationDeathRate = 2.6666667f,
				populationHalfLife = 10f,
				overPopulationHalfLife = 10f,
				minCountPerKG = 0f,
				maxCountPerKG = float.PositiveInfinity,
				minDiffusionCount = int.MaxValue,
				diffusionScale = 1f,
				minDiffusionInfestationTickCount = byte.MaxValue
			};
			infoArray[(int)ElementLoader.GetElementIndex(SimHashes.Vacuum)] = new ElemGrowthInfo
			{
				underPopulationDeathRate = 0f,
				populationHalfLife = 0f,
				overPopulationHalfLife = 0f,
				minCountPerKG = 0f,
				maxCountPerKG = float.PositiveInfinity,
				diffusionScale = 0f,
				minDiffusionInfestationTickCount = byte.MaxValue
			};
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x0028DB24 File Offset: 0x0028BD24
		protected void InitializeElemExposureArray(ref ElemExposureInfo[] infoArray, ElemExposureInfo default_value)
		{
			List<Element> elements = ElementLoader.elements;
			infoArray = new ElemExposureInfo[elements.Count];
			for (int i = 0; i < elements.Count; i++)
			{
				infoArray[i] = default_value;
			}
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x0028DB60 File Offset: 0x0028BD60
		public float GetGrowthRateForTags(HashSet<Tag> tags, bool overpopulated)
		{
			float num = 1f;
			if (this.growthRules != null)
			{
				for (int i = 0; i < this.growthRules.Count; i++)
				{
					TagGrowthRule tagGrowthRule = this.growthRules[i] as TagGrowthRule;
					if (tagGrowthRule != null && tags.Contains(tagGrowthRule.tag))
					{
						num *= Disease.HalfLifeToGrowthRate((overpopulated ? tagGrowthRule.overPopulationHalfLife : tagGrowthRule.populationHalfLife).Value, 1f);
					}
				}
			}
			return num;
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x0028DBDC File Offset: 0x0028BDDC
		public static float HalfLifeToGrowthRate(float half_life_in_seconds, float dt)
		{
			float num;
			if (half_life_in_seconds == 0f)
			{
				num = 0f;
			}
			else if (half_life_in_seconds == float.PositiveInfinity)
			{
				num = 1f;
			}
			else
			{
				float num2 = half_life_in_seconds / dt;
				num = Mathf.Pow(2f, -1f / num2);
			}
			return num;
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x0028DC28 File Offset: 0x0028BE28
		public static float GrowthRateToHalfLife(float growth_rate)
		{
			float num;
			if (growth_rate == 0f)
			{
				num = 0f;
			}
			else if (growth_rate == 1f)
			{
				num = float.PositiveInfinity;
			}
			else
			{
				num = Mathf.Log(2f, growth_rate);
			}
			return num;
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x0028DC68 File Offset: 0x0028BE68
		public float CalculateTemperatureHalfLife(float temperature)
		{
			return Disease.CalculateRangeHalfLife(temperature, ref this.temperatureRange, ref this.temperatureHalfLives);
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x0028DC7C File Offset: 0x0028BE7C
		public static float CalculateRangeHalfLife(float range_value, ref Disease.RangeInfo range, ref Disease.RangeInfo half_lives)
		{
			int num = 3;
			int num2 = 3;
			for (int i = 0; i < 4; i++)
			{
				if (range_value <= range.GetValue(i))
				{
					num = i - 1;
					num2 = i;
					break;
				}
			}
			if (num < 0)
			{
				num = num2;
			}
			float value = half_lives.GetValue(num);
			float value2 = half_lives.GetValue(num2);
			if (num == 1 && num2 == 2)
			{
				return float.PositiveInfinity;
			}
			if (float.IsInfinity(value) || float.IsInfinity(value2))
			{
				return float.PositiveInfinity;
			}
			float value3 = range.GetValue(num);
			float value4 = range.GetValue(num2);
			float num3 = 0f;
			float num4 = value4 - value3;
			if (num4 > 0f)
			{
				num3 = (range_value - value3) / num4;
			}
			return Mathf.Lerp(value, value2, num3);
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x0028DD24 File Offset: 0x0028BF24
		public List<Descriptor> GetQuantitativeDescriptors()
		{
			List<Descriptor> list = new List<Descriptor>();
			list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.TEMPERATURE_RANGE, GameUtil.GetFormattedTemperature(this.temperatureRange.minViable, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(this.temperatureRange.maxViable, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.TEMPERATURE_RANGE_TOOLTIP, new object[]
			{
				GameUtil.GetFormattedTemperature(this.temperatureRange.minViable, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
				GameUtil.GetFormattedTemperature(this.temperatureRange.maxViable, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
				GameUtil.GetFormattedTemperature(this.temperatureRange.minGrowth, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
				GameUtil.GetFormattedTemperature(this.temperatureRange.maxGrowth, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)
			}), Descriptor.DescriptorType.Information, false));
			list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.PRESSURE_RANGE, GameUtil.GetFormattedMass(this.pressureRange.minViable, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedMass(this.pressureRange.maxViable, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.PRESSURE_RANGE_TOOLTIP, new object[]
			{
				GameUtil.GetFormattedMass(this.pressureRange.minViable, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"),
				GameUtil.GetFormattedMass(this.pressureRange.maxViable, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"),
				GameUtil.GetFormattedMass(this.pressureRange.minGrowth, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"),
				GameUtil.GetFormattedMass(this.pressureRange.maxGrowth, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
			}), Descriptor.DescriptorType.Information, false));
			List<GrowthRule> list2 = new List<GrowthRule>();
			List<GrowthRule> list3 = new List<GrowthRule>();
			List<GrowthRule> list4 = new List<GrowthRule>();
			List<GrowthRule> list5 = new List<GrowthRule>();
			List<GrowthRule> list6 = new List<GrowthRule>();
			foreach (GrowthRule growthRule in this.growthRules)
			{
				if (growthRule.populationHalfLife != null && growthRule.Name() != null)
				{
					if (growthRule.populationHalfLife.Value < 0f)
					{
						list2.Add(growthRule);
					}
					else if (growthRule.populationHalfLife.Value == float.PositiveInfinity)
					{
						list3.Add(growthRule);
					}
					else if (growthRule.populationHalfLife.Value >= 12000f)
					{
						list4.Add(growthRule);
					}
					else if (growthRule.populationHalfLife.Value >= 1200f)
					{
						list5.Add(growthRule);
					}
					else
					{
						list6.Add(growthRule);
					}
				}
			}
			list.AddRange(this.BuildGrowthInfoDescriptors(list2, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.GROWS_ON, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.GROWS_ON_TOOLTIP, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.GROWS_TOOLTIP));
			list.AddRange(this.BuildGrowthInfoDescriptors(list3, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.NEUTRAL_ON, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.NEUTRAL_ON_TOOLTIP, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.NEUTRAL_TOOLTIP));
			list.AddRange(this.BuildGrowthInfoDescriptors(list4, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_SLOWLY_ON, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_SLOWLY_ON_TOOLTIP, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_SLOWLY_TOOLTIP));
			list.AddRange(this.BuildGrowthInfoDescriptors(list5, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_ON, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_ON_TOOLTIP, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_TOOLTIP));
			list.AddRange(this.BuildGrowthInfoDescriptors(list6, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_QUICKLY_ON, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_QUICKLY_ON_TOOLTIP, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.DIES_QUICKLY_TOOLTIP));
			return list;
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x0028E09C File Offset: 0x0028C29C
		private List<Descriptor> BuildGrowthInfoDescriptors(List<GrowthRule> rules, string section_text, string section_tooltip, string item_tooltip)
		{
			List<Descriptor> list = new List<Descriptor>();
			if (rules.Count > 0)
			{
				list.Add(new Descriptor(section_text, section_tooltip, Descriptor.DescriptorType.Information, false));
				for (int i = 0; i < rules.Count; i++)
				{
					list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.GROWTH_FORMAT, rules[i].Name()), string.Format(item_tooltip, GameUtil.GetFormattedCycles(Mathf.Abs(rules[i].populationHalfLife.Value), "F1", false)), Descriptor.DescriptorType.Information, false));
				}
			}
			return list;
		}

		// Token: 0x04004F0A RID: 20234
		private StringKey name;

		// Token: 0x04004F0B RID: 20235
		public HashedString id;

		// Token: 0x04004F0C RID: 20236
		public float strength;

		// Token: 0x04004F0D RID: 20237
		public Disease.RangeInfo temperatureRange;

		// Token: 0x04004F0E RID: 20238
		public Disease.RangeInfo temperatureHalfLives;

		// Token: 0x04004F0F RID: 20239
		public Disease.RangeInfo pressureRange;

		// Token: 0x04004F10 RID: 20240
		public Disease.RangeInfo pressureHalfLives;

		// Token: 0x04004F11 RID: 20241
		public List<GrowthRule> growthRules;

		// Token: 0x04004F12 RID: 20242
		public List<ExposureRule> exposureRules;

		// Token: 0x04004F13 RID: 20243
		public ElemGrowthInfo[] elemGrowthInfo;

		// Token: 0x04004F14 RID: 20244
		public ElemExposureInfo[] elemExposureInfo;

		// Token: 0x04004F15 RID: 20245
		public string overlayColourName;

		// Token: 0x04004F16 RID: 20246
		public string overlayLegendHovertext;

		// Token: 0x04004F17 RID: 20247
		public float radiationKillRate;

		// Token: 0x04004F18 RID: 20248
		public Amount amount;

		// Token: 0x04004F19 RID: 20249
		public Attribute amountDeltaAttribute;

		// Token: 0x04004F1A RID: 20250
		public Attribute cureSpeedBase;

		// Token: 0x04004F1B RID: 20251
		public static readonly ElemGrowthInfo DEFAULT_GROWTH_INFO = new ElemGrowthInfo
		{
			underPopulationDeathRate = 0f,
			populationHalfLife = float.PositiveInfinity,
			overPopulationHalfLife = float.PositiveInfinity,
			minCountPerKG = 0f,
			maxCountPerKG = float.PositiveInfinity,
			minDiffusionCount = 0,
			diffusionScale = 1f,
			minDiffusionInfestationTickCount = byte.MaxValue
		};

		// Token: 0x04004F1C RID: 20252
		public static ElemExposureInfo DEFAULT_EXPOSURE_INFO = new ElemExposureInfo
		{
			populationHalfLife = float.PositiveInfinity
		};

		// Token: 0x02001E45 RID: 7749
		public struct RangeInfo
		{
			// Token: 0x06009B2D RID: 39725 RVA: 0x00336228 File Offset: 0x00334428
			public RangeInfo(float min_viable, float min_growth, float max_growth, float max_viable)
			{
				this.minViable = min_viable;
				this.minGrowth = min_growth;
				this.maxGrowth = max_growth;
				this.maxViable = max_viable;
			}

			// Token: 0x06009B2E RID: 39726 RVA: 0x00336247 File Offset: 0x00334447
			public void Write(BinaryWriter writer)
			{
				writer.Write(this.minViable);
				writer.Write(this.minGrowth);
				writer.Write(this.maxGrowth);
				writer.Write(this.maxViable);
			}

			// Token: 0x06009B2F RID: 39727 RVA: 0x00336279 File Offset: 0x00334479
			public float GetValue(int idx)
			{
				switch (idx)
				{
				case 0:
					return this.minViable;
				case 1:
					return this.minGrowth;
				case 2:
					return this.maxGrowth;
				case 3:
					return this.maxViable;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}

			// Token: 0x06009B30 RID: 39728 RVA: 0x003362B4 File Offset: 0x003344B4
			public static Disease.RangeInfo Idempotent()
			{
				return new Disease.RangeInfo(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			}

			// Token: 0x04008837 RID: 34871
			public float minViable;

			// Token: 0x04008838 RID: 34872
			public float minGrowth;

			// Token: 0x04008839 RID: 34873
			public float maxGrowth;

			// Token: 0x0400883A RID: 34874
			public float maxViable;
		}
	}
}
