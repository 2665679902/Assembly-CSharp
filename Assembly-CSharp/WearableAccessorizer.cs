using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Database;
using KSerialization;
using UnityEngine;

// Token: 0x02000503 RID: 1283
[AddComponentMenu("KMonoBehaviour/scripts/WearableAccessorizer")]
public class WearableAccessorizer : KMonoBehaviour
{
	// Token: 0x06001E49 RID: 7753 RVA: 0x000A22CA File Offset: 0x000A04CA
	public List<ResourceRef<ClothingItemResource>> GetClothingItems()
	{
		return this.clothingItems;
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x000A22D4 File Offset: 0x000A04D4
	public string[] GetClothingItemIds()
	{
		string[] array = new string[this.clothingItems.Count];
		for (int i = 0; i < this.clothingItems.Count; i++)
		{
			array[i] = this.clothingItems[i].Get().Id;
		}
		return array;
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x000A2322 File Offset: 0x000A0522
	public Option<string> GetJoyResponseId()
	{
		return this.joyResponsePermitId;
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x000A232F File Offset: 0x000A052F
	public void SetJoyResponseId(Option<string> joyResponsePermitId)
	{
		this.joyResponsePermitId = joyResponsePermitId.UnwrapOr(null, null);
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x000A2340 File Offset: 0x000A0540
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.animController == null)
		{
			this.animController = base.GetComponent<KAnimControllerBase>();
		}
		base.Subscribe(-448952673, new Action<object>(this.EquippedItem));
		base.Subscribe(-1285462312, new Action<object>(this.UnequippedItem));
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x000A23A0 File Offset: 0x000A05A0
	[OnDeserialized]
	private void OnDeserialized()
	{
		foreach (KeyValuePair<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> keyValuePair in this.wearables)
		{
			keyValuePair.Value.Deserialize();
		}
		this.ApplyWearable();
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x000A2400 File Offset: 0x000A0600
	public void EquippedItem(object data)
	{
		KPrefabID kprefabID = data as KPrefabID;
		if (kprefabID != null)
		{
			Equippable component = kprefabID.GetComponent<Equippable>();
			this.ApplyEquipment(component, component.GetBuildOverride());
		}
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x000A2434 File Offset: 0x000A0634
	public void ApplyEquipment(Equippable equippable, KAnimFile animFile)
	{
		WearableAccessorizer.WearableType wearableType;
		if (equippable != null && animFile != null && Enum.TryParse<WearableAccessorizer.WearableType>(equippable.def.Slot, out wearableType))
		{
			if (this.wearables.ContainsKey(wearableType))
			{
				this.RemoveAnimBuild(this.wearables[wearableType].buildAnims[0], this.wearables[wearableType].buildOverridePriority);
			}
			this.wearables[wearableType] = new WearableAccessorizer.Wearable(animFile, equippable.def.BuildOverridePriority);
			this.ApplyWearable();
		}
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x000A24C8 File Offset: 0x000A06C8
	private WearableAccessorizer.WearableType GetHighestAccessory()
	{
		WearableAccessorizer.WearableType wearableType = WearableAccessorizer.WearableType.Basic;
		foreach (WearableAccessorizer.WearableType wearableType2 in this.wearables.Keys)
		{
			if (wearableType2 > wearableType)
			{
				wearableType = wearableType2;
			}
		}
		return wearableType;
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x000A2524 File Offset: 0x000A0724
	private void ApplyWearable()
	{
		if (this.animController == null)
		{
			global::Debug.LogWarning("Missing animcontroller for WearableAccessorizer, bailing early to prevent a crash!");
			return;
		}
		SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
		WearableAccessorizer.WearableType highestAccessory = this.GetHighestAccessory();
		foreach (KeyValuePair<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> keyValuePair in this.wearables)
		{
			int buildOverridePriority = keyValuePair.Value.buildOverridePriority;
			foreach (KAnimFile kanimFile in keyValuePair.Value.buildAnims)
			{
				KAnim.Build build = kanimFile.GetData().build;
				if (build != null)
				{
					for (int i = 0; i < build.symbols.Length; i++)
					{
						string text = HashCache.Get().Get(build.symbols[i].hash);
						if (keyValuePair.Key == highestAccessory)
						{
							component.AddSymbolOverride(text, build.symbols[i], buildOverridePriority);
							this.animController.SetSymbolVisiblity(text, true);
						}
						else
						{
							component.RemoveSymbolOverride(text, buildOverridePriority);
						}
					}
				}
			}
		}
		this.UpdateVisibleSymbols(highestAccessory);
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x000A2688 File Offset: 0x000A0888
	private void UpdateVisibleSymbols(WearableAccessorizer.WearableType wearableType)
	{
		bool flag = wearableType == WearableAccessorizer.WearableType.Basic;
		bool flag2 = base.GetComponent<Accessorizer>().GetAccessory(Db.Get().AccessorySlots.Hat) != null;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = true;
		if (this.wearables.ContainsKey(wearableType))
		{
			List<KAnimHashedString> list = this.wearables[wearableType].buildAnims.SelectMany((KAnimFile x) => x.GetData().build.symbols.Select((KAnim.Build.Symbol s) => s.hash)).ToList<KAnimHashedString>();
			flag = flag || list.Contains(Db.Get().AccessorySlots.Belt.targetSymbolId);
			flag3 = list.Contains(Db.Get().AccessorySlots.Skirt.targetSymbolId);
			flag4 = list.Contains(Db.Get().AccessorySlots.Necklace.targetSymbolId);
			flag5 = list.Contains(Db.Get().AccessorySlots.ArmLower.targetSymbolId);
		}
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Belt.targetSymbolId, flag);
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Necklace.targetSymbolId, flag4);
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.ArmLower.targetSymbolId, flag5);
		WearableAccessorizer.SkirtAccessory(this.animController, flag3);
		WearableAccessorizer.UpdateHairBasedOnHat(this.animController, flag2);
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x000A27FC File Offset: 0x000A09FC
	public static void UpdateHairBasedOnHat(KAnimControllerBase kbac, bool hasHat)
	{
		if (hasHat)
		{
			kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, false);
			kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, true);
			kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hat.targetSymbolId, true);
			return;
		}
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, true);
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, false);
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hat.targetSymbolId, false);
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x000A28AF File Offset: 0x000A0AAF
	public static void SkirtAccessory(KAnimControllerBase kbac, bool show_skirt)
	{
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Skirt.targetSymbolId, show_skirt);
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Leg.targetSymbolId, !show_skirt);
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x000A28EC File Offset: 0x000A0AEC
	private void RemoveAnimBuild(KAnimFile animFile, int override_priority)
	{
		SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
		KAnim.Build build = ((animFile != null) ? animFile.GetData().build : null);
		if (build != null)
		{
			for (int i = 0; i < build.symbols.Length; i++)
			{
				string text = HashCache.Get().Get(build.symbols[i].hash);
				component.RemoveSymbolOverride(text, override_priority);
			}
		}
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x000A2954 File Offset: 0x000A0B54
	private void UnequippedItem(object data)
	{
		KPrefabID kprefabID = data as KPrefabID;
		if (kprefabID != null)
		{
			Equippable component = kprefabID.GetComponent<Equippable>();
			WearableAccessorizer.WearableType wearableType;
			if (component != null && Enum.TryParse<WearableAccessorizer.WearableType>(component.def.Slot, out wearableType))
			{
				if (this.wearables.ContainsKey(wearableType))
				{
					this.RemoveAnimBuild(component.GetBuildOverride(), this.wearables[wearableType].buildOverridePriority);
					this.wearables.Remove(wearableType);
				}
				this.ApplyWearable();
			}
		}
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x000A29D4 File Offset: 0x000A0BD4
	public void ApplyClothingItem(ClothingItemResource clothingItem)
	{
		if (!this.clothingItems.Exists((ResourceRef<ClothingItemResource> x) => x.Get().IdHash == clothingItem.IdHash))
		{
			if (this.wearables.ContainsKey(WearableAccessorizer.WearableType.CustomClothing))
			{
				foreach (ResourceRef<ClothingItemResource> resourceRef in this.clothingItems.FindAll((ResourceRef<ClothingItemResource> x) => x.Get().Category == clothingItem.Category))
				{
					this.RemoveClothingItem(resourceRef.Get());
				}
			}
			this.clothingItems.Add(new ResourceRef<ClothingItemResource>(clothingItem));
		}
		if (!this.wearables.ContainsKey(WearableAccessorizer.WearableType.CustomClothing))
		{
			this.wearables[WearableAccessorizer.WearableType.CustomClothing] = new WearableAccessorizer.Wearable(new List<KAnimFile>(), 4);
		}
		this.wearables[WearableAccessorizer.WearableType.CustomClothing].buildAnims.Add(clothingItem.AnimFile);
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x000A2AD0 File Offset: 0x000A0CD0
	public void RemoveClothingItem(ClothingItemResource clothing_item)
	{
		this.clothingItems.RemoveAll((ResourceRef<ClothingItemResource> x) => x.Get().IdHash == clothing_item.IdHash);
		if (this.wearables.ContainsKey(WearableAccessorizer.WearableType.CustomClothing))
		{
			if (this.wearables[WearableAccessorizer.WearableType.CustomClothing].buildAnims.Remove(clothing_item.AnimFile))
			{
				this.RemoveAnimBuild(clothing_item.AnimFile, this.wearables[WearableAccessorizer.WearableType.CustomClothing].buildOverridePriority);
			}
			if (this.wearables[WearableAccessorizer.WearableType.CustomClothing].buildAnims.Count <= 0)
			{
				this.wearables.Remove(WearableAccessorizer.WearableType.CustomClothing);
			}
		}
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x000A2B7C File Offset: 0x000A0D7C
	public void ApplyClothingOutfit(ClothingOutfitResource outfit)
	{
		IEnumerable<ClothingItemResource> enumerable = outfit.itemsInOutfit.Select((string itemId) => Db.Get().Permits.ClothingItems.Get(itemId));
		this.ApplyClothingItems(enumerable);
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x000A2BBC File Offset: 0x000A0DBC
	public void ApplyClothingItems(IEnumerable<ClothingItemResource> items)
	{
		this.clothingItems.Clear();
		if (this.wearables.ContainsKey(WearableAccessorizer.WearableType.CustomClothing))
		{
			foreach (KAnimFile kanimFile in this.wearables[WearableAccessorizer.WearableType.CustomClothing].buildAnims)
			{
				this.RemoveAnimBuild(kanimFile, this.wearables[WearableAccessorizer.WearableType.CustomClothing].buildOverridePriority);
			}
			this.wearables[WearableAccessorizer.WearableType.CustomClothing].buildAnims.Clear();
			if (items.Count<ClothingItemResource>() <= 0)
			{
				this.wearables.Remove(WearableAccessorizer.WearableType.CustomClothing);
			}
		}
		foreach (ClothingItemResource clothingItemResource in items)
		{
			this.ApplyClothingItem(clothingItemResource);
		}
		this.ApplyWearable();
	}

	// Token: 0x040010FC RID: 4348
	[MyCmpReq]
	private KAnimControllerBase animController;

	// Token: 0x040010FD RID: 4349
	[Serialize]
	private List<ResourceRef<ClothingItemResource>> clothingItems = new List<ResourceRef<ClothingItemResource>>();

	// Token: 0x040010FE RID: 4350
	[Serialize]
	private string joyResponsePermitId;

	// Token: 0x040010FF RID: 4351
	[Serialize]
	private Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> wearables = new Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable>();

	// Token: 0x02001139 RID: 4409
	public enum WearableType
	{
		// Token: 0x04005A49 RID: 23113
		Basic,
		// Token: 0x04005A4A RID: 23114
		CustomClothing,
		// Token: 0x04005A4B RID: 23115
		Outfit,
		// Token: 0x04005A4C RID: 23116
		Suit,
		// Token: 0x04005A4D RID: 23117
		CustomSuit
	}

	// Token: 0x0200113A RID: 4410
	private class Wearable
	{
		// Token: 0x060075F2 RID: 30194 RVA: 0x002B703A File Offset: 0x002B523A
		public Wearable(List<KAnimFile> buildAnims, int buildOverridePriority)
		{
			this.buildAnims = buildAnims;
			this.buildOverridePriority = buildOverridePriority;
		}

		// Token: 0x060075F3 RID: 30195 RVA: 0x002B7050 File Offset: 0x002B5250
		public Wearable(KAnimFile buildAnim, int buildOverridePriority)
		{
			this.buildAnims = new List<KAnimFile> { buildAnim };
			this.buildOverridePriority = buildOverridePriority;
		}

		// Token: 0x060075F4 RID: 30196 RVA: 0x002B7074 File Offset: 0x002B5274
		public void Deserialize()
		{
			for (int i = 0; i < this.buildAnims.Count; i++)
			{
				this.buildAnims[i] = Assets.GetAnim(this.buildAnims[i].name);
			}
		}

		// Token: 0x04005A4E RID: 23118
		public List<KAnimFile> buildAnims;

		// Token: 0x04005A4F RID: 23119
		public int buildOverridePriority;
	}
}
