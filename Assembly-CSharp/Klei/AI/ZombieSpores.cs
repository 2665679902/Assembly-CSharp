﻿using System;
using Klei.AI.DiseaseGrowthRules;

namespace Klei.AI
{
	// Token: 0x02000D7D RID: 3453
	public class ZombieSpores : Disease
	{
		// Token: 0x06006965 RID: 26981 RVA: 0x00290274 File Offset: 0x0028E474
		public ZombieSpores(bool statsOnly)
			: base("ZombieSpores", 50, new Disease.RangeInfo(168.15f, 258.15f, 513.15f, 563.15f), new Disease.RangeInfo(10f, 1200f, 1200f, 10f), new Disease.RangeInfo(0f, 0f, 1000f, 1000f), Disease.RangeInfo.Idempotent(), 1f, statsOnly)
		{
		}

		// Token: 0x06006966 RID: 26982 RVA: 0x002902E4 File Offset: 0x0028E4E4
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
			foreach (SimHashes simHashes in new SimHashes[]
			{
				SimHashes.Carbon,
				SimHashes.Diamond
			})
			{
				base.AddGrowthRule(new ElementGrowthRule(simHashes)
				{
					underPopulationDeathRate = new float?(0f),
					populationHalfLife = new float?(float.PositiveInfinity),
					overPopulationHalfLife = new float?(3000f),
					maxCountPerKG = new float?((float)1000),
					diffusionScale = new float?(0.005f)
				});
			}
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
			foreach (SimHashes simHashes2 in new SimHashes[]
			{
				SimHashes.CarbonDioxide,
				SimHashes.Methane,
				SimHashes.SourGas
			})
			{
				base.AddGrowthRule(new ElementGrowthRule(simHashes2)
				{
					underPopulationDeathRate = new float?(0f),
					populationHalfLife = new float?(float.PositiveInfinity),
					overPopulationHalfLife = new float?(6000f)
				});
			}
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
			foreach (SimHashes simHashes3 in new SimHashes[]
			{
				SimHashes.CrudeOil,
				SimHashes.Petroleum,
				SimHashes.Naphtha,
				SimHashes.LiquidMethane
			})
			{
				base.AddGrowthRule(new ElementGrowthRule(simHashes3)
				{
					populationHalfLife = new float?(float.PositiveInfinity),
					overPopulationHalfLife = new float?(6000f),
					maxCountPerKG = new float?((float)1000),
					diffusionScale = new float?(0.005f)
				});
			}
			base.AddGrowthRule(new ElementGrowthRule(SimHashes.Chlorine)
			{
				populationHalfLife = new float?(10f),
				overPopulationHalfLife = new float?(10f),
				minDiffusionCount = new int?(100000),
				diffusionScale = new float?(0.001f)
			});
			base.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
			base.AddExposureRule(new ExposureRule
			{
				populationHalfLife = new float?(float.PositiveInfinity)
			});
			base.AddExposureRule(new ElementExposureRule(SimHashes.Chlorine)
			{
				populationHalfLife = new float?(10f)
			});
			base.AddExposureRule(new ElementExposureRule(SimHashes.ChlorineGas)
			{
				populationHalfLife = new float?(10f)
			});
		}

		// Token: 0x04004F45 RID: 20293
		public const string ID = "ZombieSpores";
	}
}
