using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000B50 RID: 2896
public class OutfitDesignerScreen_OutfitState
{
	// Token: 0x060059F2 RID: 23026 RVA: 0x00208964 File Offset: 0x00206B64
	private OutfitDesignerScreen_OutfitState(ClothingOutfitTarget sourceTarget, ClothingOutfitTarget destinationTarget)
	{
		this.destinationTarget = destinationTarget;
		this.sourceTarget = sourceTarget;
		this.name = sourceTarget.ReadName();
		foreach (ClothingItemResource clothingItemResource in sourceTarget.ReadItemValues())
		{
			this.ApplyItem(clothingItemResource);
		}
	}

	// Token: 0x060059F3 RID: 23027 RVA: 0x002089D4 File Offset: 0x00206BD4
	public static OutfitDesignerScreen_OutfitState ForTemplateOutfit(ClothingOutfitTarget outfitTemplate)
	{
		global::Debug.Assert(outfitTemplate.IsTemplateOutfit());
		return new OutfitDesignerScreen_OutfitState(outfitTemplate, outfitTemplate);
	}

	// Token: 0x060059F4 RID: 23028 RVA: 0x002089E9 File Offset: 0x00206BE9
	public static OutfitDesignerScreen_OutfitState ForMinionInstance(ClothingOutfitTarget sourceTarget, GameObject minionInstance)
	{
		return new OutfitDesignerScreen_OutfitState(sourceTarget, ClothingOutfitTarget.FromMinion(minionInstance));
	}

	// Token: 0x060059F5 RID: 23029 RVA: 0x002089F7 File Offset: 0x00206BF7
	public unsafe void ApplyItem(ClothingItemResource item)
	{
		*this.GetItemSlotForCategory(item.Category) = item;
	}

	// Token: 0x060059F6 RID: 23030 RVA: 0x00208A10 File Offset: 0x00206C10
	public ref Option<ClothingItemResource> GetItemSlotForCategory(PermitCategory category)
	{
		if (category == PermitCategory.DupeHats)
		{
			return ref this.hatSlot;
		}
		if (category == PermitCategory.DupeTops)
		{
			return ref this.topSlot;
		}
		if (category == PermitCategory.DupeGloves)
		{
			return ref this.glovesSlot;
		}
		if (category == PermitCategory.DupeBottoms)
		{
			return ref this.bottomSlot;
		}
		if (category == PermitCategory.DupeShoes)
		{
			return ref this.shoesSlot;
		}
		if (category == PermitCategory.DupeAccessories)
		{
			return ref this.accessorySlot;
		}
		DebugUtil.DevAssert(false, string.Format("Couldn't get a {0}<{1}> for {2} \"{3}\" on {4} \"{5}\".", new object[] { "Option", "ClothingItemResource", "PermitCategory", category, "OutfitDesignerScreen_OutfitState", this.name }), null);
		return ref OutfitDesignerScreen_OutfitState.dummySlot;
	}

	// Token: 0x060059F7 RID: 23031 RVA: 0x00208AB0 File Offset: 0x00206CB0
	public void AddItemValuesTo(ICollection<ClothingItemResource> clothingItems)
	{
		if (this.hatSlot.IsSome())
		{
			clothingItems.Add(this.hatSlot.Unwrap());
		}
		if (this.topSlot.IsSome())
		{
			clothingItems.Add(this.topSlot.Unwrap());
		}
		if (this.glovesSlot.IsSome())
		{
			clothingItems.Add(this.glovesSlot.Unwrap());
		}
		if (this.bottomSlot.IsSome())
		{
			clothingItems.Add(this.bottomSlot.Unwrap());
		}
		if (this.shoesSlot.IsSome())
		{
			clothingItems.Add(this.shoesSlot.Unwrap());
		}
		if (this.accessorySlot.IsSome())
		{
			clothingItems.Add(this.accessorySlot.Unwrap());
		}
	}

	// Token: 0x060059F8 RID: 23032 RVA: 0x00208B74 File Offset: 0x00206D74
	public void AddItemsTo(ICollection<string> itemIds)
	{
		if (this.hatSlot.IsSome())
		{
			itemIds.Add(this.hatSlot.Unwrap().Id);
		}
		if (this.topSlot.IsSome())
		{
			itemIds.Add(this.topSlot.Unwrap().Id);
		}
		if (this.glovesSlot.IsSome())
		{
			itemIds.Add(this.glovesSlot.Unwrap().Id);
		}
		if (this.bottomSlot.IsSome())
		{
			itemIds.Add(this.bottomSlot.Unwrap().Id);
		}
		if (this.shoesSlot.IsSome())
		{
			itemIds.Add(this.shoesSlot.Unwrap().Id);
		}
		if (this.accessorySlot.IsSome())
		{
			itemIds.Add(this.accessorySlot.Unwrap().Id);
		}
	}

	// Token: 0x060059F9 RID: 23033 RVA: 0x00208C54 File Offset: 0x00206E54
	public string[] GetItems()
	{
		List<string> list = new List<string>();
		this.AddItemsTo(list);
		return list.ToArray();
	}

	// Token: 0x060059FA RID: 23034 RVA: 0x00208C74 File Offset: 0x00206E74
	public bool DoesContainNonOwnedItems()
	{
		bool flag;
		using (ListPool<string, OutfitDesignerScreen_OutfitState>.PooledList pooledList = PoolsFor<OutfitDesignerScreen_OutfitState>.AllocateList<string>())
		{
			this.AddItemsTo(pooledList);
			flag = ClothingOutfitTarget.DoesContainNonOwnedItems(pooledList);
		}
		return flag;
	}

	// Token: 0x060059FB RID: 23035 RVA: 0x00208CB4 File Offset: 0x00206EB4
	public bool IsDirty()
	{
		using (HashSetPool<string, OutfitDesignerScreen>.PooledHashSet pooledHashSet = PoolsFor<OutfitDesignerScreen>.AllocateHashSet<string>())
		{
			this.AddItemsTo(pooledHashSet);
			string[] array = this.destinationTarget.ReadItems();
			if (pooledHashSet.Count != array.Length)
			{
				return true;
			}
			foreach (string text in array)
			{
				if (!pooledHashSet.Contains(text))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04003CC2 RID: 15554
	public string name;

	// Token: 0x04003CC3 RID: 15555
	public Option<ClothingItemResource> hatSlot;

	// Token: 0x04003CC4 RID: 15556
	public Option<ClothingItemResource> topSlot;

	// Token: 0x04003CC5 RID: 15557
	public Option<ClothingItemResource> glovesSlot;

	// Token: 0x04003CC6 RID: 15558
	public Option<ClothingItemResource> bottomSlot;

	// Token: 0x04003CC7 RID: 15559
	public Option<ClothingItemResource> shoesSlot;

	// Token: 0x04003CC8 RID: 15560
	public Option<ClothingItemResource> accessorySlot;

	// Token: 0x04003CC9 RID: 15561
	public ClothingOutfitUtility.OutfitType outfitType;

	// Token: 0x04003CCA RID: 15562
	public ClothingOutfitTarget sourceTarget;

	// Token: 0x04003CCB RID: 15563
	public ClothingOutfitTarget destinationTarget;

	// Token: 0x04003CCC RID: 15564
	private static Option<ClothingItemResource> dummySlot;
}
