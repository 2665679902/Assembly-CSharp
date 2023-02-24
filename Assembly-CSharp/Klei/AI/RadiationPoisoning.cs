using System;
using Klei.AI.DiseaseGrowthRules;

namespace Klei.AI
{
	// Token: 0x02000D74 RID: 3444
	public class RadiationPoisoning : Disease
	{
		// Token: 0x0600693B RID: 26939 RVA: 0x0028EB28 File Offset: 0x0028CD28
		public RadiationPoisoning(bool statsOnly)
			: base("RadiationSickness", 100, Disease.RangeInfo.Idempotent(), Disease.RangeInfo.Idempotent(), Disease.RangeInfo.Idempotent(), Disease.RangeInfo.Idempotent(), 0f, statsOnly)
		{
		}

		// Token: 0x0600693C RID: 26940 RVA: 0x0028EB5C File Offset: 0x0028CD5C
		protected override void PopulateElemGrowthInfo()
		{
			base.InitializeElemGrowthArray(ref this.elemGrowthInfo, Disease.DEFAULT_GROWTH_INFO);
			base.AddGrowthRule(new GrowthRule
			{
				underPopulationDeathRate = new float?(0f),
				minCountPerKG = new float?(0f),
				populationHalfLife = new float?(600f),
				maxCountPerKG = new float?(float.PositiveInfinity),
				overPopulationHalfLife = new float?(600f),
				minDiffusionCount = new int?(10000),
				diffusionScale = new float?(0f),
				minDiffusionInfestationTickCount = new byte?((byte)1)
			});
			base.InitializeElemExposureArray(ref this.elemExposureInfo, Disease.DEFAULT_EXPOSURE_INFO);
		}

		// Token: 0x04004F24 RID: 20260
		public const string ID = "RadiationSickness";
	}
}
