﻿using System;
using Klei.AI.DiseaseGrowthRules;

namespace Klei.AI
{
	// Token: 0x02000D79 RID: 3449
	public class SlimeGerms : Disease
	{
		// Token: 0x06006960 RID: 26976 RVA: 0x0028F990 File Offset: 0x0028DB90
		public SlimeGerms(bool statsOnly)
			: base("SlimeLung", 20, new Disease.RangeInfo(283.15f, 293.15f, 363.15f, 373.15f), new Disease.RangeInfo(10f, 1200f, 1200f, 10f), new Disease.RangeInfo(0f, 0f, 1000f, 1000f), Disease.RangeInfo.Idempotent(), 2.5f, statsOnly)
		{
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x0028FA00 File Offset: 0x0028DC00
		protected override void PopulateElemGrowthInfo()
		{
			base.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
			base.AddGrowthRule(new GrowthRule
			{
				underPopulationDeathRate = new float?(2.6666667f),
				minCountPerKG = new float?(0.4f),
				populationHalfLife = new float?(12000f),
				maxCountPerKG = new float?((float)500),
				overPopulationHalfLife = new float?(1200f),
				minDiffusionCount = new int?(1000),
				diffusionScale = new float?(0.001f),
				minDiffusionInfestationTickCount = new byte?((byte)1)
			});
			base.AddGrowthRule(new StateGrowthRule(Element.State.Solid)
			{
				minCountPerKG = new float?(0.4f),
				populationHalfLife = new float?(3000f),
				overPopulationHalfLife = new float?(1200f),
				diffusionScale = new float?(1E-06f),
				minDiffusionCount = new int?(1000000)
			});
			base.AddGrowthRule(new ElementGrowthRule(SimHashes.SlimeMold)
			{
				underPopulationDeathRate = new float?(0f),
				populationHalfLife = new float?(-3000f),
				overPopulationHalfLife = new float?(3000f),
				maxCountPerKG = new float?((float)4500),
				diffusionScale = new float?(0.05f)
			});
			base.AddGrowthRule(new ElementGrowthRule(SimHashes.BleachStone)
			{
				populationHalfLife = new float?(10f),
				overPopulationHalfLife = new float?(10f),
				minDiffusionCount = new int?(100000),
				diffusionScale = new float?(0.001f)
			});
			base.AddGrowthRule(new StateGrowthRule(Element.State.Gas)
			{
				minCountPerKG = new float?(250f),
				populationHalfLife = new float?(12000f),
				overPopulationHalfLife = new float?(1200f),
				maxCountPerKG = new float?((float)10000),
				minDiffusionCount = new int?(5100),
				diffusionScale = new float?(0.005f)
			});
			base.AddGrowthRule(new ElementGrowthRule(SimHashes.ContaminatedOxygen)
			{
				underPopulationDeathRate = new float?(0f),
				populationHalfLife = new float?(-300f),
				overPopulationHalfLife = new float?(1200f)
			});
			base.AddGrowthRule(new ElementGrowthRule(SimHashes.Oxygen)
			{
				populationHalfLife = new float?(1200f),
				overPopulationHalfLife = new float?(10f)
			});
			base.AddGrowthRule(new ElementGrowthRule(SimHashes.ChlorineGas)
			{
				populationHalfLife = new float?(10f),
				overPopulationHalfLife = new float?(10f),
				minDiffusionCount = new int?(100000),
				diffusionScale = new float?(0.001f)
			});
			base.AddGrowthRule(new StateGrowthRule(Element.State.Liquid)
			{
				minCountPerKG = new float?(0.4f),
				populationHalfLife = new float?(1200f),
				overPopulationHalfLife = new float?(300f),
				maxCountPerKG = new float?((float)100),
				diffusionScale = new float?(0.01f)
			});
			base.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
			base.AddExposureRule(new ExposureRule
			{
				populationHalfLife = new float?(float.PositiveInfinity)
			});
			base.AddExposureRule(new ElementExposureRule(SimHashes.DirtyWater)
			{
				populationHalfLife = new float?(-12000f)
			});
			base.AddExposureRule(new ElementExposureRule(SimHashes.ContaminatedOxygen)
			{
				populationHalfLife = new float?(-12000f)
			});
			base.AddExposureRule(new ElementExposureRule(SimHashes.Oxygen)
			{
				populationHalfLife = new float?(3000f)
			});
			base.AddExposureRule(new ElementExposureRule(SimHashes.ChlorineGas)
			{
				populationHalfLife = new float?(10f)
			});
		}

		// Token: 0x04004F39 RID: 20281
		private const float COUGH_FREQUENCY = 20f;

		// Token: 0x04004F3A RID: 20282
		private const int DISEASE_AMOUNT = 1000;

		// Token: 0x04004F3B RID: 20283
		public const string ID = "SlimeLung";
	}
}
