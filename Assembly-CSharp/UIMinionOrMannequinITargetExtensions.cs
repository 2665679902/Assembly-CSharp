using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Database;

// Token: 0x02000C1B RID: 3099
public static class UIMinionOrMannequinITargetExtensions
{
	// Token: 0x06006221 RID: 25121 RVA: 0x00244223 File Offset: 0x00242423
	public static void SetOutfit(this UIMinionOrMannequin.ITarget self, ClothingOutfitResource outfit)
	{
		self.SetOutfit(outfit.itemsInOutfit.Select((string itemId) => Db.Get().Permits.ClothingItems.Get(itemId)));
	}

	// Token: 0x06006222 RID: 25122 RVA: 0x00244255 File Offset: 0x00242455
	public static void SetOutfit(this UIMinionOrMannequin.ITarget self, OutfitDesignerScreen_OutfitState outfit)
	{
		self.SetOutfit(from itemId in outfit.GetItems()
			select Db.Get().Permits.ClothingItems.Get(itemId));
	}

	// Token: 0x06006223 RID: 25123 RVA: 0x00244287 File Offset: 0x00242487
	public static void SetOutfit(this UIMinionOrMannequin.ITarget self, ClothingOutfitTarget outfit)
	{
		self.SetOutfit(outfit.ReadItemValues());
	}

	// Token: 0x06006224 RID: 25124 RVA: 0x00244296 File Offset: 0x00242496
	public static void SetOutfit(this UIMinionOrMannequin.ITarget self, Option<ClothingOutfitTarget> outfit)
	{
		if (outfit.HasValue)
		{
			self.SetOutfit(outfit.Value);
			return;
		}
		self.ClearOutfit();
	}

	// Token: 0x06006225 RID: 25125 RVA: 0x002442B5 File Offset: 0x002424B5
	public static void ClearOutfit(this UIMinionOrMannequin.ITarget self)
	{
		self.SetOutfit(UIMinionOrMannequinITargetExtensions.EMPTY_OUTFIT);
	}

	// Token: 0x06006226 RID: 25126 RVA: 0x002442C2 File Offset: 0x002424C2
	public static void React(this UIMinionOrMannequin.ITarget self)
	{
		self.React(UIMinionOrMannequinReactSource.None);
	}

	// Token: 0x06006227 RID: 25127 RVA: 0x002442CB File Offset: 0x002424CB
	public static void ReactToClothingItemChange(this UIMinionOrMannequin.ITarget self, PermitCategory clothingChangedCategory)
	{
		self.React(UIMinionOrMannequinITargetExtensions.<ReactToClothingItemChange>g__GetSource|7_0(clothingChangedCategory));
	}

	// Token: 0x06006228 RID: 25128 RVA: 0x002442D9 File Offset: 0x002424D9
	public static void ReactToPersonalityChange(this UIMinionOrMannequin.ITarget self)
	{
		self.React(UIMinionOrMannequinReactSource.OnPersonalityChanged);
	}

	// Token: 0x06006229 RID: 25129 RVA: 0x002442E2 File Offset: 0x002424E2
	public static void ReactToFullOutfitChange(this UIMinionOrMannequin.ITarget self)
	{
		self.React(UIMinionOrMannequinReactSource.OnWholeOutfitChanged);
	}

	// Token: 0x0600622B RID: 25131 RVA: 0x002442F8 File Offset: 0x002424F8
	[CompilerGenerated]
	internal static UIMinionOrMannequinReactSource <ReactToClothingItemChange>g__GetSource|7_0(PermitCategory clothingChangedCategory)
	{
		switch (clothingChangedCategory)
		{
		case PermitCategory.DupeTops:
			return UIMinionOrMannequinReactSource.OnTopChanged;
		case PermitCategory.DupeBottoms:
			return UIMinionOrMannequinReactSource.OnBottomChanged;
		case PermitCategory.DupeGloves:
			return UIMinionOrMannequinReactSource.OnGlovesChanged;
		case PermitCategory.DupeShoes:
			return UIMinionOrMannequinReactSource.OnShoesChanged;
		default:
			DebugUtil.DevAssert(false, string.Format("Couldn't find a reaction for \"{0}\" clothing item category being changed", clothingChangedCategory), null);
			return UIMinionOrMannequinReactSource.None;
		}
	}

	// Token: 0x040043E4 RID: 17380
	public static readonly ClothingItemResource[] EMPTY_OUTFIT = new ClothingItemResource[0];
}
