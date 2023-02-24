using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CA0 RID: 3232
	public static class PermitCategories
	{
		// Token: 0x060065B2 RID: 26034 RVA: 0x0026E29E File Offset: 0x0026C49E
		public static string GetDisplayName(PermitCategory category)
		{
			return PermitCategories.CategoryInfos[category].displayName;
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x0026E2B0 File Offset: 0x0026C4B0
		public static string GetUppercaseDisplayName(PermitCategory category)
		{
			return PermitCategories.CategoryInfos[category].displayName.ToUpper();
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x0026E2C7 File Offset: 0x0026C4C7
		public static string GetIconName(PermitCategory category)
		{
			return PermitCategories.CategoryInfos[category].iconName;
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x0026E2DC File Offset: 0x0026C4DC
		public static PermitCategory GetCategoryForId(string id)
		{
			try
			{
				return (PermitCategory)Enum.Parse(typeof(PermitCategory), id);
			}
			catch (ArgumentException)
			{
				Debug.LogError(id + " is not a valid PermitCategory.");
			}
			return PermitCategory.Equipment;
		}

		// Token: 0x040049B0 RID: 18864
		private static Dictionary<PermitCategory, PermitCategories.CategoryInfo> CategoryInfos = new Dictionary<PermitCategory, PermitCategories.CategoryInfo>
		{
			{
				PermitCategory.Equipment,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.EQUIPMENT, "icon_inventory_equipment")
			},
			{
				PermitCategory.DupeTops,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.DUPE_TOPS, "icon_inventory_tops")
			},
			{
				PermitCategory.DupeBottoms,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.DUPE_BOTTOMS, "icon_inventory_bottoms")
			},
			{
				PermitCategory.DupeGloves,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.DUPE_GLOVES, "icon_inventory_gloves")
			},
			{
				PermitCategory.DupeShoes,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.DUPE_SHOES, "icon_inventory_shoes")
			},
			{
				PermitCategory.DupeHats,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.DUPE_HATS, "icon_inventory_hats")
			},
			{
				PermitCategory.DupeAccessories,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.DUPE_ACCESSORIES, "icon_inventory_accessories")
			},
			{
				PermitCategory.Building,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.BUILDINGS, "icon_inventory_buildings")
			},
			{
				PermitCategory.Critter,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.CRITTERS, "icon_inventory_critters")
			},
			{
				PermitCategory.Sweepy,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.SWEEPYS, "icon_inventory_sweepys")
			},
			{
				PermitCategory.Duplicant,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.DUPLICANTS, "icon_inventory_duplicants")
			},
			{
				PermitCategory.Artwork,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.ARTWORKS, "icon_inventory_artworks")
			},
			{
				PermitCategory.JoyResponse,
				new PermitCategories.CategoryInfo(UI.KLEI_INVENTORY_SCREEN.CATEGORIES.JOY_RESPONSE, "icon_inventory_joyresponses")
			}
		};

		// Token: 0x02001B29 RID: 6953
		private class CategoryInfo
		{
			// Token: 0x060095B2 RID: 38322 RVA: 0x00321AEA File Offset: 0x0031FCEA
			public CategoryInfo(string name, string icon_name)
			{
				this.displayName = name;
				this.iconName = icon_name;
			}

			// Token: 0x04007AAE RID: 31406
			public string displayName;

			// Token: 0x04007AAF RID: 31407
			public string iconName;
		}
	}
}
