using System;
using System.Collections.Generic;

namespace TUNING
{
	// Token: 0x02000D29 RID: 3369
	public class CROPS
	{
		// Token: 0x04004D1F RID: 19743
		public const float WILD_GROWTH_RATE_MODIFIER = 0.25f;

		// Token: 0x04004D20 RID: 19744
		public const float GROWTH_RATE = 0.0016666667f;

		// Token: 0x04004D21 RID: 19745
		public const float WILD_GROWTH_RATE = 0.00041666668f;

		// Token: 0x04004D22 RID: 19746
		public const float PLANTERPLOT_GROWTH_PENTALY = -0.5f;

		// Token: 0x04004D23 RID: 19747
		public const float BASE_BONUS_SEED_PROBABILITY = 0.1f;

		// Token: 0x04004D24 RID: 19748
		public const float SELF_HARVEST_TIME = 2400f;

		// Token: 0x04004D25 RID: 19749
		public const float SELF_PLANT_TIME = 2400f;

		// Token: 0x04004D26 RID: 19750
		public const float TREE_BRANCH_SELF_HARVEST_TIME = 12000f;

		// Token: 0x04004D27 RID: 19751
		public const float FERTILIZATION_GAIN_RATE = 1.6666666f;

		// Token: 0x04004D28 RID: 19752
		public const float FERTILIZATION_LOSS_RATE = -0.16666667f;

		// Token: 0x04004D29 RID: 19753
		public static List<Crop.CropVal> CROP_TYPES = new List<Crop.CropVal>
		{
			new Crop.CropVal("BasicPlantFood", 1800f, 1, true),
			new Crop.CropVal(PrickleFruitConfig.ID, 3600f, 1, true),
			new Crop.CropVal(SwampFruitConfig.ID, 3960f, 1, true),
			new Crop.CropVal(MushroomConfig.ID, 4500f, 1, true),
			new Crop.CropVal("ColdWheatSeed", 10800f, 18, true),
			new Crop.CropVal(SpiceNutConfig.ID, 4800f, 4, true),
			new Crop.CropVal(BasicFabricConfig.ID, 1200f, 1, true),
			new Crop.CropVal(SwampLilyFlowerConfig.ID, 7200f, 2, true),
			new Crop.CropVal("GasGrassHarvested", 2400f, 1, true),
			new Crop.CropVal("WoodLog", 2700f, 300, true),
			new Crop.CropVal("Lettuce", 7200f, 12, true),
			new Crop.CropVal("BeanPlantSeed", 12600f, 12, true),
			new Crop.CropVal("OxyfernSeed", 7200f, 1, true),
			new Crop.CropVal("PlantMeat", 18000f, 10, true),
			new Crop.CropVal("WormBasicFruit", 2400f, 1, true),
			new Crop.CropVal("WormSuperFruit", 4800f, 8, true),
			new Crop.CropVal(SimHashes.Salt.ToString(), 3600f, 65, true),
			new Crop.CropVal(SimHashes.Water.ToString(), 6000f, 350, true)
		};
	}
}
