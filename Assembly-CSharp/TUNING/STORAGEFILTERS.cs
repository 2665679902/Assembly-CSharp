using System;
using System.Collections.Generic;

namespace TUNING
{
	// Token: 0x02000D2F RID: 3375
	public class STORAGEFILTERS
	{
		// Token: 0x04004D8E RID: 19854
		public static List<Tag> FOOD = new List<Tag>
		{
			GameTags.Edible,
			GameTags.CookingIngredient,
			GameTags.Medicine
		};

		// Token: 0x04004D8F RID: 19855
		public static List<Tag> BAGABLE_CREATURES = new List<Tag> { GameTags.BagableCreature };

		// Token: 0x04004D90 RID: 19856
		public static List<Tag> SWIMMING_CREATURES = new List<Tag> { GameTags.SwimmingCreature };

		// Token: 0x04004D91 RID: 19857
		public static List<Tag> NOT_EDIBLE_SOLIDS = new List<Tag>
		{
			GameTags.Alloy,
			GameTags.RefinedMetal,
			GameTags.Metal,
			GameTags.BuildableRaw,
			GameTags.BuildableProcessed,
			GameTags.Farmable,
			GameTags.Organics,
			GameTags.Compostable,
			GameTags.Seed,
			GameTags.Agriculture,
			GameTags.Filter,
			GameTags.ConsumableOre,
			GameTags.Liquifiable,
			GameTags.IndustrialProduct,
			GameTags.IndustrialIngredient,
			GameTags.MedicalSupplies,
			GameTags.Clothes,
			GameTags.ManufacturedMaterial,
			GameTags.Egg,
			GameTags.RareMaterials,
			GameTags.Other,
			GameTags.StoryTraitResource
		};

		// Token: 0x04004D92 RID: 19858
		public static List<Tag> LIQUIDS = new List<Tag> { GameTags.Liquid };

		// Token: 0x04004D93 RID: 19859
		public static List<Tag> GASES = new List<Tag>
		{
			GameTags.Breathable,
			GameTags.Unbreathable
		};

		// Token: 0x04004D94 RID: 19860
		public static List<Tag> PAYLOADS = new List<Tag> { GameTags.RailGunPayloadEmptyable };
	}
}
