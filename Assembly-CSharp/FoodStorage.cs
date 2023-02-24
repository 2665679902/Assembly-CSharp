using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000482 RID: 1154
public class FoodStorage : KMonoBehaviour
{
	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0008A753 File Offset: 0x00088953
	// (set) Token: 0x060019C3 RID: 6595 RVA: 0x0008A75B File Offset: 0x0008895B
	public FilteredStorage FilteredStorage { get; set; }

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0008A764 File Offset: 0x00088964
	// (set) Token: 0x060019C5 RID: 6597 RVA: 0x0008A76C File Offset: 0x0008896C
	public bool SpicedFoodOnly
	{
		get
		{
			return this.onlyStoreSpicedFood;
		}
		set
		{
			this.onlyStoreSpicedFood = value;
			base.Trigger(1163645216, this.onlyStoreSpicedFood);
			if (this.onlyStoreSpicedFood)
			{
				this.FilteredStorage.AddForbiddenTag(GameTags.UnspicedFood);
				this.storage.DropUnlessHasTag(GameTags.SpicedFood);
				return;
			}
			this.FilteredStorage.RemoveForbiddenTag(GameTags.UnspicedFood);
		}
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x0008A7CF File Offset: 0x000889CF
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<FoodStorage>(-905833192, FoodStorage.OnCopySettingsDelegate);
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x0008A7E8 File Offset: 0x000889E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x0008A7F0 File Offset: 0x000889F0
	private void OnCopySettings(object data)
	{
		FoodStorage component = ((GameObject)data).GetComponent<FoodStorage>();
		if (component != null)
		{
			this.SpicedFoodOnly = component.SpicedFoodOnly;
		}
	}

	// Token: 0x04000E77 RID: 3703
	[Serialize]
	private bool onlyStoreSpicedFood;

	// Token: 0x04000E78 RID: 3704
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04000E7A RID: 3706
	private static readonly EventSystem.IntraObjectHandler<FoodStorage> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<FoodStorage>(delegate(FoodStorage component, object data)
	{
		component.OnCopySettings(data);
	});
}
