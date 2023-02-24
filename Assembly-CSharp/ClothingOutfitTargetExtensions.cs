using System;
using System.Collections.Generic;
using Database;
using STRINGS;

// Token: 0x0200068B RID: 1675
public static class ClothingOutfitTargetExtensions
{
	// Token: 0x06002D5C RID: 11612 RVA: 0x000EE1BC File Offset: 0x000EC3BC
	public static string ReadName(this Option<ClothingOutfitTarget> self)
	{
		if (self.HasValue)
		{
			return self.Value.ReadName();
		}
		return UI.OUTFIT_NAME.NONE;
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x000EE1EC File Offset: 0x000EC3EC
	public static IEnumerable<string> ReadItems(this Option<ClothingOutfitTarget> self)
	{
		if (self.HasValue)
		{
			return self.Value.ReadItems();
		}
		return ClothingOutfitTargetExtensions.NO_ITEMS;
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x000EE218 File Offset: 0x000EC418
	public static IEnumerable<ClothingItemResource> ReadItemValues(this Option<ClothingOutfitTarget> self)
	{
		if (self.HasValue)
		{
			return self.Value.ReadItemValues();
		}
		return ClothingOutfitTargetExtensions.NO_ITEM_VALUES;
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x000EE244 File Offset: 0x000EC444
	public static Option<string> GetId(this Option<ClothingOutfitTarget> self)
	{
		if (self.HasValue)
		{
			return self.Value.Id;
		}
		return Option.None;
	}

	// Token: 0x04001AF2 RID: 6898
	public static readonly string[] NO_ITEMS = new string[0];

	// Token: 0x04001AF3 RID: 6899
	public static readonly ClothingItemResource[] NO_ITEM_VALUES = new ClothingItemResource[0];
}
