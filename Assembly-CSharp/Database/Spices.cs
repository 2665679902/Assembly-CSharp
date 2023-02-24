using System;
using Klei.AI;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CB4 RID: 3252
	public class Spices : ResourceSet<Spice>
	{
		// Token: 0x060065FB RID: 26107 RVA: 0x00271F74 File Offset: 0x00270174
		public Spices(ResourceSet parent)
			: base("Spices", parent)
		{
			this.PreservingSpice = new Spice(this, "PRESERVING_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "BasicSingleHarvestPlantSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.Salt.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.961f, 0.827f, 0.29f), Color.white, new AttributeModifier("RotDelta", 0.5f, "Spices", false, false, true), null, "spice_recipe1", null);
			this.PilotingSpice = new Spice(this, "PILOTING_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "MushroomSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.Sucrose.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.039f, 0.725f, 0.831f), Color.white, null, new AttributeModifier("SpaceNavigation", 3f, "Spices", false, false, true), "spice_recipe2", DlcManager.AVAILABLE_EXPANSION1_ONLY);
			this.StrengthSpice = new Spice(this, "STRENGTH_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "SeaLettuceSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.Iron.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.588f, 0.278f, 0.788f), Color.white, null, new AttributeModifier("Strength", 3f, "Spices", false, false, true), "spice_recipe3", null);
			this.MachinerySpice = new Spice(this, "MACHINERY_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "PrickleFlowerSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.SlimeMold.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.788f, 0.443f, 0.792f), Color.white, null, new AttributeModifier("Machinery", 3f, "Spices", false, false, true), "spice_recipe4", null);
		}

		// Token: 0x04004A49 RID: 19017
		public Spice PreservingSpice;

		// Token: 0x04004A4A RID: 19018
		public Spice PilotingSpice;

		// Token: 0x04004A4B RID: 19019
		public Spice StrengthSpice;

		// Token: 0x04004A4C RID: 19020
		public Spice MachinerySpice;
	}
}
