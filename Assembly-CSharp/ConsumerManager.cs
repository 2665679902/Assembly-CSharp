using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020006B1 RID: 1713
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ConsumerManager")]
public class ConsumerManager : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06002E9C RID: 11932 RVA: 0x000F61F2 File Offset: 0x000F43F2
	public static void DestroyInstance()
	{
		ConsumerManager.instance = null;
	}

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06002E9D RID: 11933 RVA: 0x000F61FC File Offset: 0x000F43FC
	// (remove) Token: 0x06002E9E RID: 11934 RVA: 0x000F6234 File Offset: 0x000F4434
	public event Action<Tag> OnDiscover;

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000F6269 File Offset: 0x000F4469
	public List<Tag> DefaultForbiddenTagsList
	{
		get
		{
			return this.defaultForbiddenTagsList;
		}
	}

	// Token: 0x06002EA0 RID: 11936 RVA: 0x000F6274 File Offset: 0x000F4474
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ConsumerManager.instance = this;
		this.RefreshDiscovered(null);
		DiscoveredResources.Instance.OnDiscover += this.OnWorldInventoryDiscover;
		Game.Instance.Subscribe(-107300940, new Action<object>(this.RefreshDiscovered));
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x000F62C6 File Offset: 0x000F44C6
	public bool isDiscovered(Tag id)
	{
		return !this.undiscoveredConsumableTags.Contains(id);
	}

	// Token: 0x06002EA2 RID: 11938 RVA: 0x000F62D7 File Offset: 0x000F44D7
	private void OnWorldInventoryDiscover(Tag category_tag, Tag tag)
	{
		if (this.undiscoveredConsumableTags.Contains(tag))
		{
			this.RefreshDiscovered(null);
		}
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x000F62F0 File Offset: 0x000F44F0
	public void RefreshDiscovered(object data = null)
	{
		foreach (EdiblesManager.FoodInfo foodInfo in EdiblesManager.GetAllFoodTypes())
		{
			if (!this.ShouldBeDiscovered(foodInfo.Id.ToTag()) && !this.undiscoveredConsumableTags.Contains(foodInfo.Id.ToTag()))
			{
				this.undiscoveredConsumableTags.Add(foodInfo.Id.ToTag());
				if (this.OnDiscover != null)
				{
					this.OnDiscover("UndiscoveredSomething".ToTag());
				}
			}
			else if (this.undiscoveredConsumableTags.Contains(foodInfo.Id.ToTag()) && this.ShouldBeDiscovered(foodInfo.Id.ToTag()))
			{
				this.undiscoveredConsumableTags.Remove(foodInfo.Id.ToTag());
				if (this.OnDiscover != null)
				{
					this.OnDiscover(foodInfo.Id.ToTag());
				}
				if (!DiscoveredResources.Instance.IsDiscovered(foodInfo.Id.ToTag()))
				{
					if (foodInfo.CaloriesPerUnit == 0f)
					{
						DiscoveredResources.Instance.Discover(foodInfo.Id.ToTag(), GameTags.CookingIngredient);
					}
					else
					{
						DiscoveredResources.Instance.Discover(foodInfo.Id.ToTag(), GameTags.Edible);
					}
				}
			}
		}
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x000F6474 File Offset: 0x000F4674
	private bool ShouldBeDiscovered(Tag food_id)
	{
		if (DiscoveredResources.Instance.IsDiscovered(food_id))
		{
			return true;
		}
		foreach (Recipe recipe in RecipeManager.Get().recipes)
		{
			if (recipe.Result == food_id)
			{
				foreach (string text in recipe.fabricators)
				{
					if (Db.Get().TechItems.IsTechItemComplete(text))
					{
						return true;
					}
				}
			}
		}
		foreach (Crop crop in Components.Crops.Items)
		{
			if (Grid.IsVisible(Grid.PosToCell(crop.gameObject)) && crop.cropId == food_id.Name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04001C20 RID: 7200
	public static ConsumerManager instance;

	// Token: 0x04001C22 RID: 7202
	[Serialize]
	private List<Tag> undiscoveredConsumableTags = new List<Tag>();

	// Token: 0x04001C23 RID: 7203
	[Serialize]
	private List<Tag> defaultForbiddenTagsList = new List<Tag>();
}
