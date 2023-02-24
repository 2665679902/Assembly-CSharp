using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200058A RID: 1418
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ComplexFabricator")]
public class ComplexFabricator : KMonoBehaviour, ISim200ms, ISim1000ms
{
	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06002290 RID: 8848 RVA: 0x000BB94A File Offset: 0x000B9B4A
	public ComplexFabricatorWorkable Workable
	{
		get
		{
			return this.workable;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06002291 RID: 8849 RVA: 0x000BB952 File Offset: 0x000B9B52
	// (set) Token: 0x06002292 RID: 8850 RVA: 0x000BB95A File Offset: 0x000B9B5A
	public bool ForbidMutantSeeds
	{
		get
		{
			return this.forbidMutantSeeds;
		}
		set
		{
			this.forbidMutantSeeds = value;
			this.ToggleMutantSeedFetches();
			this.UpdateMutantSeedStatusItem();
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06002293 RID: 8851 RVA: 0x000BB96F File Offset: 0x000B9B6F
	public Tag[] ForbiddenTags
	{
		get
		{
			if (!this.forbidMutantSeeds)
			{
				return null;
			}
			return this.forbiddenMutantTags;
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06002294 RID: 8852 RVA: 0x000BB981 File Offset: 0x000B9B81
	public int CurrentOrderIdx
	{
		get
		{
			return this.nextOrderIdx;
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06002295 RID: 8853 RVA: 0x000BB989 File Offset: 0x000B9B89
	public ComplexRecipe CurrentWorkingOrder
	{
		get
		{
			if (!this.HasWorkingOrder)
			{
				return null;
			}
			return this.recipe_list[this.workingOrderIdx];
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06002296 RID: 8854 RVA: 0x000BB9A2 File Offset: 0x000B9BA2
	public ComplexRecipe NextOrder
	{
		get
		{
			if (!this.nextOrderIsWorkable)
			{
				return null;
			}
			return this.recipe_list[this.nextOrderIdx];
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06002297 RID: 8855 RVA: 0x000BB9BB File Offset: 0x000B9BBB
	// (set) Token: 0x06002298 RID: 8856 RVA: 0x000BB9C3 File Offset: 0x000B9BC3
	public float OrderProgress
	{
		get
		{
			return this.orderProgress;
		}
		set
		{
			this.orderProgress = value;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06002299 RID: 8857 RVA: 0x000BB9CC File Offset: 0x000B9BCC
	public bool HasAnyOrder
	{
		get
		{
			return this.HasWorkingOrder || this.hasOpenOrders;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x0600229A RID: 8858 RVA: 0x000BB9DE File Offset: 0x000B9BDE
	public bool HasWorker
	{
		get
		{
			return !this.duplicantOperated || this.workable.worker != null;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x0600229B RID: 8859 RVA: 0x000BB9FB File Offset: 0x000B9BFB
	public bool WaitingForWorker
	{
		get
		{
			return this.HasWorkingOrder && !this.HasWorker;
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x0600229C RID: 8860 RVA: 0x000BBA10 File Offset: 0x000B9C10
	private bool HasWorkingOrder
	{
		get
		{
			return this.workingOrderIdx > -1;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x0600229D RID: 8861 RVA: 0x000BBA1B File Offset: 0x000B9C1B
	public List<FetchList2> DebugFetchLists
	{
		get
		{
			return this.fetchListList;
		}
	}

	// Token: 0x0600229E RID: 8862 RVA: 0x000BBA24 File Offset: 0x000B9C24
	[OnDeserialized]
	protected virtual void OnDeserializedMethod()
	{
		List<string> list = new List<string>();
		foreach (string text in this.recipeQueueCounts.Keys)
		{
			if (ComplexRecipeManager.Get().GetRecipe(text) == null)
			{
				list.Add(text);
			}
		}
		foreach (string text2 in list)
		{
			global::Debug.LogWarningFormat("{1} removing missing recipe from queue: {0}", new object[] { text2, base.name });
			this.recipeQueueCounts.Remove(text2);
		}
	}

	// Token: 0x0600229F RID: 8863 RVA: 0x000BBAF4 File Offset: 0x000B9CF4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.GetRecipes();
		this.simRenderLoadBalance = true;
		this.choreType = Db.Get().ChoreTypes.Fabricate;
		base.Subscribe<ComplexFabricator>(-1957399615, ComplexFabricator.OnDroppedAllDelegate);
		base.Subscribe<ComplexFabricator>(-592767678, ComplexFabricator.OnOperationalChangedDelegate);
		base.Subscribe<ComplexFabricator>(-905833192, ComplexFabricator.OnCopySettingsDelegate);
		base.Subscribe<ComplexFabricator>(-1697596308, ComplexFabricator.OnStorageChangeDelegate);
		base.Subscribe<ComplexFabricator>(-1837862626, ComplexFabricator.OnParticleStorageChangedDelegate);
		this.workable = base.GetComponent<ComplexFabricatorWorkable>();
		Components.ComplexFabricators.Add(this);
		base.Subscribe<ComplexFabricator>(493375141, ComplexFabricator.OnRefreshUserMenuDelegate);
	}

	// Token: 0x060022A0 RID: 8864 RVA: 0x000BBBA8 File Offset: 0x000B9DA8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.InitRecipeQueueCount();
		foreach (string text in this.recipeQueueCounts.Keys)
		{
			if (this.recipeQueueCounts[text] == 100)
			{
				this.recipeQueueCounts[text] = ComplexFabricator.QUEUE_INFINITE;
			}
		}
		this.buildStorage.Transfer(this.inStorage, true, true);
		this.DropExcessIngredients(this.inStorage);
		int num = this.FindRecipeIndex(this.lastWorkingRecipe);
		if (num > -1)
		{
			this.nextOrderIdx = num;
		}
		this.UpdateMutantSeedStatusItem();
	}

	// Token: 0x060022A1 RID: 8865 RVA: 0x000BBC64 File Offset: 0x000B9E64
	protected override void OnCleanUp()
	{
		this.CancelAllOpenOrders();
		this.CancelChore();
		Components.ComplexFabricators.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060022A2 RID: 8866 RVA: 0x000BBC84 File Offset: 0x000B9E84
	private void OnRefreshUserMenu(object data)
	{
		if (DlcManager.IsContentActive("EXPANSION1_ID") && this.HasRecipiesWithSeeds())
		{
			Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_switch_toggle", this.ForbidMutantSeeds ? UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.ACCEPT : UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.REJECT, delegate
			{
				this.ForbidMutantSeeds = !this.ForbidMutantSeeds;
				this.OnRefreshUserMenu(null);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.TOOLTIP, true), 1f);
		}
	}

	// Token: 0x060022A3 RID: 8867 RVA: 0x000BBD04 File Offset: 0x000B9F04
	private bool HasRecipiesWithSeeds()
	{
		bool flag = false;
		ComplexRecipe[] array = this.recipe_list;
		for (int i = 0; i < array.Length; i++)
		{
			ComplexRecipe.RecipeElement[] ingredients = array[i].ingredients;
			for (int j = 0; j < ingredients.Length; j++)
			{
				GameObject prefab = Assets.GetPrefab(ingredients[j].material);
				if (prefab != null && prefab.GetComponent<PlantableSeed>() != null)
				{
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x000BBD74 File Offset: 0x000B9F74
	private void UpdateMutantSeedStatusItem()
	{
		base.gameObject.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.FabricatorAcceptsMutantSeeds, DlcManager.IsContentActive("EXPANSION1_ID") && this.HasRecipiesWithSeeds() && !this.forbidMutantSeeds, null);
	}

	// Token: 0x060022A5 RID: 8869 RVA: 0x000BBDC5 File Offset: 0x000B9FC5
	private void OnOperationalChanged(object data)
	{
		if ((bool)data)
		{
			this.queueDirty = true;
		}
		else
		{
			this.CancelAllOpenOrders();
		}
		this.UpdateChore();
	}

	// Token: 0x060022A6 RID: 8870 RVA: 0x000BBDE4 File Offset: 0x000B9FE4
	public void Sim1000ms(float dt)
	{
		this.RefreshAndStartNextOrder();
		if (this.materialNeedCache.Count > 0 && this.fetchListList.Count == 0)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} has material needs cached, but no open fetches. materialNeedCache={1}, fetchListList={2}", new object[]
			{
				base.gameObject,
				this.materialNeedCache.Count,
				this.fetchListList.Count
			});
			this.queueDirty = true;
		}
	}

	// Token: 0x060022A7 RID: 8871 RVA: 0x000BBE60 File Offset: 0x000BA060
	public void Sim200ms(float dt)
	{
		if (!this.operational.IsOperational)
		{
			return;
		}
		this.operational.SetActive(this.HasWorkingOrder && this.HasWorker, false);
		if (!this.duplicantOperated && this.HasWorkingOrder)
		{
			ComplexRecipe complexRecipe = this.recipe_list[this.workingOrderIdx];
			this.orderProgress += dt / complexRecipe.time;
			if (this.orderProgress >= 1f)
			{
				this.CompleteWorkingOrder();
			}
		}
	}

	// Token: 0x060022A8 RID: 8872 RVA: 0x000BBEDE File Offset: 0x000BA0DE
	private void RefreshAndStartNextOrder()
	{
		if (!this.operational.IsOperational)
		{
			return;
		}
		if (this.queueDirty)
		{
			this.RefreshQueue();
		}
		if (!this.HasWorkingOrder && this.nextOrderIsWorkable)
		{
			this.StartWorkingOrder(this.nextOrderIdx);
		}
	}

	// Token: 0x060022A9 RID: 8873 RVA: 0x000BBF18 File Offset: 0x000BA118
	public void SetQueueDirty()
	{
		this.queueDirty = true;
	}

	// Token: 0x060022AA RID: 8874 RVA: 0x000BBF21 File Offset: 0x000BA121
	private void RefreshQueue()
	{
		this.queueDirty = false;
		this.ValidateWorkingOrder();
		this.ValidateNextOrder();
		this.UpdateOpenOrders();
		this.DropExcessIngredients(this.inStorage);
		base.Trigger(1721324763, this);
	}

	// Token: 0x060022AB RID: 8875 RVA: 0x000BBF54 File Offset: 0x000BA154
	private void StartWorkingOrder(int index)
	{
		global::Debug.Assert(!this.HasWorkingOrder, "machineOrderIdx already set");
		this.workingOrderIdx = index;
		if (this.recipe_list[this.workingOrderIdx].id != this.lastWorkingRecipe)
		{
			this.orderProgress = 0f;
			this.lastWorkingRecipe = this.recipe_list[this.workingOrderIdx].id;
		}
		this.TransferCurrentRecipeIngredientsForBuild();
		global::Debug.Assert(this.openOrderCounts[this.workingOrderIdx] > 0, "openOrderCount invalid");
		List<int> list = this.openOrderCounts;
		int num = this.workingOrderIdx;
		int num2 = list[num];
		list[num] = num2 - 1;
		this.UpdateChore();
		this.AdvanceNextOrder();
	}

	// Token: 0x060022AC RID: 8876 RVA: 0x000BC00B File Offset: 0x000BA20B
	private void CancelWorkingOrder()
	{
		global::Debug.Assert(this.HasWorkingOrder, "machineOrderIdx not set");
		this.buildStorage.Transfer(this.inStorage, true, true);
		this.workingOrderIdx = -1;
		this.orderProgress = 0f;
		this.UpdateChore();
	}

	// Token: 0x060022AD RID: 8877 RVA: 0x000BC048 File Offset: 0x000BA248
	public void CompleteWorkingOrder()
	{
		if (!this.HasWorkingOrder)
		{
			global::Debug.LogWarning("CompleteWorkingOrder called with no working order.", base.gameObject);
			return;
		}
		ComplexRecipe complexRecipe = this.recipe_list[this.workingOrderIdx];
		this.SpawnOrderProduct(complexRecipe);
		float num = this.buildStorage.MassStored();
		if (num != 0f)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} build storage contains mass {1} after order completion.", new object[] { base.gameObject, num });
			this.buildStorage.Transfer(this.inStorage, true, true);
		}
		this.DecrementRecipeQueueCountInternal(complexRecipe, true);
		this.workingOrderIdx = -1;
		this.orderProgress = 0f;
		this.CancelChore();
		if (!this.cancelling)
		{
			this.RefreshAndStartNextOrder();
		}
	}

	// Token: 0x060022AE RID: 8878 RVA: 0x000BC104 File Offset: 0x000BA304
	private void ValidateWorkingOrder()
	{
		if (!this.HasWorkingOrder)
		{
			return;
		}
		ComplexRecipe complexRecipe = this.recipe_list[this.workingOrderIdx];
		if (!this.IsRecipeQueued(complexRecipe))
		{
			this.CancelWorkingOrder();
		}
	}

	// Token: 0x060022AF RID: 8879 RVA: 0x000BC138 File Offset: 0x000BA338
	private void UpdateChore()
	{
		if (!this.duplicantOperated)
		{
			return;
		}
		bool flag = this.operational.IsOperational && this.HasWorkingOrder;
		if (flag && this.chore == null)
		{
			this.CreateChore();
			return;
		}
		if (!flag && this.chore != null)
		{
			this.CancelChore();
		}
	}

	// Token: 0x060022B0 RID: 8880 RVA: 0x000BC188 File Offset: 0x000BA388
	private void AdvanceNextOrder()
	{
		for (int i = 0; i < this.recipe_list.Length; i++)
		{
			this.nextOrderIdx = (this.nextOrderIdx + 1) % this.recipe_list.Length;
			ComplexRecipe complexRecipe = this.recipe_list[this.nextOrderIdx];
			this.nextOrderIsWorkable = this.GetRemainingQueueCount(complexRecipe) > 0 && this.HasIngredients(complexRecipe, this.inStorage);
			if (this.nextOrderIsWorkable)
			{
				break;
			}
		}
	}

	// Token: 0x060022B1 RID: 8881 RVA: 0x000BC1F8 File Offset: 0x000BA3F8
	private void ValidateNextOrder()
	{
		ComplexRecipe complexRecipe = this.recipe_list[this.nextOrderIdx];
		this.nextOrderIsWorkable = this.GetRemainingQueueCount(complexRecipe) > 0 && this.HasIngredients(complexRecipe, this.inStorage);
		if (!this.nextOrderIsWorkable)
		{
			this.AdvanceNextOrder();
		}
	}

	// Token: 0x060022B2 RID: 8882 RVA: 0x000BC244 File Offset: 0x000BA444
	private void CancelAllOpenOrders()
	{
		for (int i = 0; i < this.openOrderCounts.Count; i++)
		{
			this.openOrderCounts[i] = 0;
		}
		this.ClearMaterialNeeds();
		this.CancelFetches();
	}

	// Token: 0x060022B3 RID: 8883 RVA: 0x000BC280 File Offset: 0x000BA480
	private void UpdateOpenOrders()
	{
		ComplexRecipe[] recipes = this.GetRecipes();
		if (recipes.Length != this.openOrderCounts.Count)
		{
			global::Debug.LogErrorFormat(base.gameObject, "Recipe count {0} doesn't match open order count {1}", new object[]
			{
				recipes.Length,
				this.openOrderCounts.Count
			});
		}
		bool flag = false;
		this.hasOpenOrders = false;
		for (int i = 0; i < recipes.Length; i++)
		{
			ComplexRecipe complexRecipe = recipes[i];
			int recipePrefetchCount = this.GetRecipePrefetchCount(complexRecipe);
			if (recipePrefetchCount > 0)
			{
				this.hasOpenOrders = true;
			}
			int num = this.openOrderCounts[i];
			if (num != recipePrefetchCount)
			{
				if (recipePrefetchCount < num)
				{
					flag = true;
				}
				this.openOrderCounts[i] = recipePrefetchCount;
			}
		}
		DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary pooledDictionary = DictionaryPool<Tag, float, ComplexFabricator>.Allocate();
		DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary pooledDictionary2 = DictionaryPool<Tag, float, ComplexFabricator>.Allocate();
		for (int j = 0; j < this.openOrderCounts.Count; j++)
		{
			if (this.openOrderCounts[j] > 0)
			{
				foreach (ComplexRecipe.RecipeElement recipeElement in this.recipe_list[j].ingredients)
				{
					pooledDictionary[recipeElement.material] = this.inStorage.GetAmountAvailable(recipeElement.material);
				}
			}
		}
		for (int l = 0; l < this.recipe_list.Length; l++)
		{
			int num2 = this.openOrderCounts[l];
			if (num2 > 0)
			{
				foreach (ComplexRecipe.RecipeElement recipeElement2 in this.recipe_list[l].ingredients)
				{
					float num3 = recipeElement2.amount * (float)num2;
					float num4 = num3 - pooledDictionary[recipeElement2.material];
					if (num4 > 0f)
					{
						float num5;
						pooledDictionary2.TryGetValue(recipeElement2.material, out num5);
						pooledDictionary2[recipeElement2.material] = num5 + num4;
						pooledDictionary[recipeElement2.material] = 0f;
					}
					else
					{
						DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary pooledDictionary3 = pooledDictionary;
						Tag material = recipeElement2.material;
						pooledDictionary3[material] -= num3;
					}
				}
			}
		}
		if (flag)
		{
			this.CancelFetches();
		}
		if (pooledDictionary2.Count > 0)
		{
			this.UpdateFetches(pooledDictionary2);
		}
		this.UpdateMaterialNeeds(pooledDictionary2);
		pooledDictionary2.Recycle();
		pooledDictionary.Recycle();
	}

	// Token: 0x060022B4 RID: 8884 RVA: 0x000BC4CC File Offset: 0x000BA6CC
	private void UpdateMaterialNeeds(Dictionary<Tag, float> missingAmounts)
	{
		this.ClearMaterialNeeds();
		foreach (KeyValuePair<Tag, float> keyValuePair in missingAmounts)
		{
			MaterialNeeds.UpdateNeed(keyValuePair.Key, keyValuePair.Value, base.gameObject.GetMyWorldId());
			this.materialNeedCache.Add(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x060022B5 RID: 8885 RVA: 0x000BC550 File Offset: 0x000BA750
	private void ClearMaterialNeeds()
	{
		foreach (KeyValuePair<Tag, float> keyValuePair in this.materialNeedCache)
		{
			MaterialNeeds.UpdateNeed(keyValuePair.Key, -keyValuePair.Value, base.gameObject.GetMyWorldId());
		}
		this.materialNeedCache.Clear();
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x000BC5C8 File Offset: 0x000BA7C8
	public int HighestHEPQueued()
	{
		int num = 0;
		foreach (KeyValuePair<string, int> keyValuePair in this.recipeQueueCounts)
		{
			if (keyValuePair.Value > 0)
			{
				num = Math.Max(this.recipe_list[this.FindRecipeIndex(keyValuePair.Key)].consumedHEP, num);
			}
		}
		return num;
	}

	// Token: 0x060022B7 RID: 8887 RVA: 0x000BC644 File Offset: 0x000BA844
	private void OnFetchComplete()
	{
		for (int i = this.fetchListList.Count - 1; i >= 0; i--)
		{
			if (this.fetchListList[i].IsComplete)
			{
				this.fetchListList.RemoveAt(i);
				this.queueDirty = true;
			}
		}
	}

	// Token: 0x060022B8 RID: 8888 RVA: 0x000BC68F File Offset: 0x000BA88F
	private void OnStorageChange(object data)
	{
		this.queueDirty = true;
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x000BC698 File Offset: 0x000BA898
	private void OnDroppedAll(object data)
	{
		if (this.HasWorkingOrder)
		{
			this.CancelWorkingOrder();
		}
		this.CancelAllOpenOrders();
		this.RefreshQueue();
	}

	// Token: 0x060022BA RID: 8890 RVA: 0x000BC6B4 File Offset: 0x000BA8B4
	private void DropExcessIngredients(Storage storage)
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		if (this.keepAdditionalTag != Tag.Invalid)
		{
			hashSet.Add(this.keepAdditionalTag);
		}
		for (int i = 0; i < this.recipe_list.Length; i++)
		{
			ComplexRecipe complexRecipe = this.recipe_list[i];
			if (this.IsRecipeQueued(complexRecipe))
			{
				foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
				{
					hashSet.Add(recipeElement.material);
				}
			}
		}
		for (int k = storage.items.Count - 1; k >= 0; k--)
		{
			GameObject gameObject = storage.items[k];
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && (!this.keepExcessLiquids || !component.Element.IsLiquid))
				{
					KPrefabID component2 = gameObject.GetComponent<KPrefabID>();
					if (component2 && !hashSet.Contains(component2.PrefabID()))
					{
						storage.Drop(gameObject, true);
					}
				}
			}
		}
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x000BC7C4 File Offset: 0x000BA9C4
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		ComplexFabricator component = gameObject.GetComponent<ComplexFabricator>();
		if (component == null)
		{
			return;
		}
		this.ForbidMutantSeeds = component.ForbidMutantSeeds;
		foreach (ComplexRecipe complexRecipe in this.recipe_list)
		{
			int num;
			if (!component.recipeQueueCounts.TryGetValue(complexRecipe.id, out num))
			{
				num = 0;
			}
			this.SetRecipeQueueCountInternal(complexRecipe, num);
		}
		this.RefreshQueue();
	}

	// Token: 0x060022BC RID: 8892 RVA: 0x000BC842 File Offset: 0x000BAA42
	private int CompareRecipe(ComplexRecipe a, ComplexRecipe b)
	{
		if (a.sortOrder != b.sortOrder)
		{
			return a.sortOrder - b.sortOrder;
		}
		return StringComparer.InvariantCulture.Compare(a.id, b.id);
	}

	// Token: 0x060022BD RID: 8893 RVA: 0x000BC878 File Offset: 0x000BAA78
	public ComplexRecipe[] GetRecipes()
	{
		if (this.recipe_list == null)
		{
			Tag prefabTag = base.GetComponent<KPrefabID>().PrefabTag;
			List<ComplexRecipe> recipes = ComplexRecipeManager.Get().recipes;
			List<ComplexRecipe> list = new List<ComplexRecipe>();
			foreach (ComplexRecipe complexRecipe in recipes)
			{
				using (List<Tag>.Enumerator enumerator2 = complexRecipe.fabricators.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current == prefabTag)
						{
							list.Add(complexRecipe);
						}
					}
				}
			}
			this.recipe_list = list.ToArray();
			Array.Sort<ComplexRecipe>(this.recipe_list, new Comparison<ComplexRecipe>(this.CompareRecipe));
		}
		return this.recipe_list;
	}

	// Token: 0x060022BE RID: 8894 RVA: 0x000BC958 File Offset: 0x000BAB58
	private void InitRecipeQueueCount()
	{
		foreach (ComplexRecipe complexRecipe in this.GetRecipes())
		{
			bool flag = false;
			using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.recipeQueueCounts.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == complexRecipe.id)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				this.recipeQueueCounts.Add(complexRecipe.id, 0);
			}
			this.openOrderCounts.Add(0);
		}
	}

	// Token: 0x060022BF RID: 8895 RVA: 0x000BC9F8 File Offset: 0x000BABF8
	private int FindRecipeIndex(string id)
	{
		for (int i = 0; i < this.recipe_list.Length; i++)
		{
			if (this.recipe_list[i].id == id)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060022C0 RID: 8896 RVA: 0x000BCA30 File Offset: 0x000BAC30
	public int GetRecipeQueueCount(ComplexRecipe recipe)
	{
		return this.recipeQueueCounts[recipe.id];
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x000BCA44 File Offset: 0x000BAC44
	public bool IsRecipeQueued(ComplexRecipe recipe)
	{
		int num = this.recipeQueueCounts[recipe.id];
		global::Debug.Assert(num >= 0 || num == ComplexFabricator.QUEUE_INFINITE);
		return num != 0;
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x000BCA7C File Offset: 0x000BAC7C
	public int GetRecipePrefetchCount(ComplexRecipe recipe)
	{
		int remainingQueueCount = this.GetRemainingQueueCount(recipe);
		global::Debug.Assert(remainingQueueCount >= 0);
		return Mathf.Min(2, remainingQueueCount);
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x000BCAA4 File Offset: 0x000BACA4
	private int GetRemainingQueueCount(ComplexRecipe recipe)
	{
		int num = this.recipeQueueCounts[recipe.id];
		global::Debug.Assert(num >= 0 || num == ComplexFabricator.QUEUE_INFINITE);
		if (num == ComplexFabricator.QUEUE_INFINITE)
		{
			return ComplexFabricator.MAX_QUEUE_SIZE;
		}
		if (num > 0)
		{
			if (this.IsCurrentRecipe(recipe))
			{
				num--;
			}
			return num;
		}
		return 0;
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x000BCAF9 File Offset: 0x000BACF9
	private bool IsCurrentRecipe(ComplexRecipe recipe)
	{
		return this.workingOrderIdx >= 0 && this.recipe_list[this.workingOrderIdx].id == recipe.id;
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x000BCB23 File Offset: 0x000BAD23
	public void SetRecipeQueueCount(ComplexRecipe recipe, int count)
	{
		this.SetRecipeQueueCountInternal(recipe, count);
		this.RefreshQueue();
	}

	// Token: 0x060022C6 RID: 8902 RVA: 0x000BCB33 File Offset: 0x000BAD33
	private void SetRecipeQueueCountInternal(ComplexRecipe recipe, int count)
	{
		this.recipeQueueCounts[recipe.id] = count;
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x000BCB48 File Offset: 0x000BAD48
	public void IncrementRecipeQueueCount(ComplexRecipe recipe)
	{
		if (this.recipeQueueCounts[recipe.id] == ComplexFabricator.QUEUE_INFINITE)
		{
			this.recipeQueueCounts[recipe.id] = 0;
		}
		else if (this.recipeQueueCounts[recipe.id] >= ComplexFabricator.MAX_QUEUE_SIZE)
		{
			this.recipeQueueCounts[recipe.id] = ComplexFabricator.QUEUE_INFINITE;
		}
		else
		{
			Dictionary<string, int> dictionary = this.recipeQueueCounts;
			string id = recipe.id;
			int num = dictionary[id];
			dictionary[id] = num + 1;
		}
		this.RefreshQueue();
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x000BCBD5 File Offset: 0x000BADD5
	public void DecrementRecipeQueueCount(ComplexRecipe recipe, bool respectInfinite = true)
	{
		this.DecrementRecipeQueueCountInternal(recipe, respectInfinite);
		this.RefreshQueue();
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x000BCBE8 File Offset: 0x000BADE8
	private void DecrementRecipeQueueCountInternal(ComplexRecipe recipe, bool respectInfinite = true)
	{
		if (!respectInfinite || this.recipeQueueCounts[recipe.id] != ComplexFabricator.QUEUE_INFINITE)
		{
			if (this.recipeQueueCounts[recipe.id] == ComplexFabricator.QUEUE_INFINITE)
			{
				this.recipeQueueCounts[recipe.id] = ComplexFabricator.MAX_QUEUE_SIZE;
				return;
			}
			if (this.recipeQueueCounts[recipe.id] == 0)
			{
				this.recipeQueueCounts[recipe.id] = ComplexFabricator.QUEUE_INFINITE;
				return;
			}
			Dictionary<string, int> dictionary = this.recipeQueueCounts;
			string id = recipe.id;
			int num = dictionary[id];
			dictionary[id] = num - 1;
		}
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x000BCC87 File Offset: 0x000BAE87
	private void CreateChore()
	{
		global::Debug.Assert(this.chore == null, "chore should be null");
		this.chore = this.workable.CreateWorkChore(this.choreType, this.orderProgress);
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x000BCCB9 File Offset: 0x000BAEB9
	private void CancelChore()
	{
		if (this.cancelling)
		{
			return;
		}
		this.cancelling = true;
		if (this.chore != null)
		{
			this.chore.Cancel("order cancelled");
			this.chore = null;
		}
		this.cancelling = false;
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x000BCCF4 File Offset: 0x000BAEF4
	private void UpdateFetches(DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary missingAmounts)
	{
		ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.fetchChoreTypeIdHash);
		foreach (KeyValuePair<Tag, float> keyValuePair in missingAmounts)
		{
			if (keyValuePair.Value >= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT && !this.HasPendingFetch(keyValuePair.Key))
			{
				FetchList2 fetchList = new FetchList2(this.inStorage, byHash);
				FetchList2 fetchList2 = fetchList;
				Tag key = keyValuePair.Key;
				float value = keyValuePair.Value;
				fetchList2.Add(key, this.ForbiddenTags, value, Operational.State.None);
				fetchList.ShowStatusItem = false;
				fetchList.Submit(new System.Action(this.OnFetchComplete), false);
				this.fetchListList.Add(fetchList);
			}
		}
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x000BCDC0 File Offset: 0x000BAFC0
	private bool HasPendingFetch(Tag tag)
	{
		foreach (FetchList2 fetchList in this.fetchListList)
		{
			float num;
			fetchList.MinimumAmount.TryGetValue(tag, out num);
			if (num > 0f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x000BCE28 File Offset: 0x000BB028
	private void CancelFetches()
	{
		foreach (FetchList2 fetchList in this.fetchListList)
		{
			fetchList.Cancel("cancel all orders");
		}
		this.fetchListList.Clear();
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x000BCE88 File Offset: 0x000BB088
	protected virtual void TransferCurrentRecipeIngredientsForBuild()
	{
		ComplexRecipe.RecipeElement[] ingredients = this.recipe_list[this.workingOrderIdx].ingredients;
		int i = 0;
		while (i < ingredients.Length)
		{
			ComplexRecipe.RecipeElement recipeElement = ingredients[i];
			float num;
			for (;;)
			{
				num = recipeElement.amount - this.buildStorage.GetAmountAvailable(recipeElement.material);
				if (num <= 0f)
				{
					break;
				}
				if (this.inStorage.GetAmountAvailable(recipeElement.material) <= 0f)
				{
					goto Block_2;
				}
				this.inStorage.Transfer(this.buildStorage, recipeElement.material, num, false, true);
			}
			IL_9D:
			i++;
			continue;
			Block_2:
			global::Debug.LogWarningFormat("TransferCurrentRecipeIngredientsForBuild ran out of {0} but still needed {1} more.", new object[] { recipeElement.material, num });
			goto IL_9D;
		}
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x000BCF40 File Offset: 0x000BB140
	protected virtual bool HasIngredients(ComplexRecipe recipe, Storage storage)
	{
		ComplexRecipe.RecipeElement[] ingredients = recipe.ingredients;
		if (recipe.consumedHEP > 0)
		{
			HighEnergyParticleStorage component = base.GetComponent<HighEnergyParticleStorage>();
			if (component == null || component.Particles < (float)recipe.consumedHEP)
			{
				return false;
			}
		}
		foreach (ComplexRecipe.RecipeElement recipeElement in ingredients)
		{
			float amountAvailable = storage.GetAmountAvailable(recipeElement.material);
			if (recipeElement.amount - amountAvailable >= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x000BCFB8 File Offset: 0x000BB1B8
	private void ToggleMutantSeedFetches()
	{
		if (this.HasAnyOrder)
		{
			ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.fetchChoreTypeIdHash);
			List<FetchList2> list = new List<FetchList2>();
			foreach (FetchList2 fetchList in this.fetchListList)
			{
				foreach (FetchOrder2 fetchOrder in fetchList.FetchOrders)
				{
					foreach (Tag tag in fetchOrder.Tags)
					{
						GameObject prefab = Assets.GetPrefab(tag);
						if (prefab != null && prefab.GetComponent<PlantableSeed>() != null)
						{
							fetchList.Cancel("MutantSeedTagChanged");
							list.Add(fetchList);
						}
					}
				}
			}
			foreach (FetchList2 fetchList2 in list)
			{
				this.fetchListList.Remove(fetchList2);
				foreach (FetchOrder2 fetchOrder2 in fetchList2.FetchOrders)
				{
					foreach (Tag tag2 in fetchOrder2.Tags)
					{
						FetchList2 fetchList3 = new FetchList2(this.inStorage, byHash);
						FetchList2 fetchList4 = fetchList3;
						Tag tag3 = tag2;
						float totalAmount = fetchOrder2.TotalAmount;
						fetchList4.Add(tag3, this.ForbiddenTags, totalAmount, Operational.State.None);
						fetchList3.ShowStatusItem = false;
						fetchList3.Submit(new System.Action(this.OnFetchComplete), false);
						this.fetchListList.Add(fetchList3);
					}
				}
			}
		}
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x000BD1F8 File Offset: 0x000BB3F8
	protected virtual List<GameObject> SpawnOrderProduct(ComplexRecipe recipe)
	{
		List<GameObject> list = new List<GameObject>();
		SimUtil.DiseaseInfo diseaseInfo;
		diseaseInfo.count = 0;
		diseaseInfo.idx = 0;
		float num = 0f;
		float num2 = 0f;
		string text = null;
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.ingredients)
		{
			num2 += recipeElement.amount;
		}
		Element element = null;
		foreach (ComplexRecipe.RecipeElement recipeElement2 in recipe.ingredients)
		{
			float num3 = recipeElement2.amount / num2;
			if (recipe.ProductHasFacade && text.IsNullOrWhiteSpace())
			{
				RepairableEquipment component = this.buildStorage.FindFirst(recipeElement2.material).GetComponent<RepairableEquipment>();
				if (component != null)
				{
					text = component.facadeID;
				}
			}
			if (recipeElement2.inheritElement)
			{
				element = this.buildStorage.FindFirst(recipeElement2.material).GetComponent<PrimaryElement>().Element;
			}
			float num4;
			SimUtil.DiseaseInfo diseaseInfo2;
			float num5;
			this.buildStorage.ConsumeAndGetDisease(recipeElement2.material, recipeElement2.amount, out num4, out diseaseInfo2, out num5);
			if (diseaseInfo2.count > diseaseInfo.count)
			{
				diseaseInfo = diseaseInfo2;
			}
			num += num5 * num3;
		}
		if (recipe.consumedHEP > 0)
		{
			base.GetComponent<HighEnergyParticleStorage>().ConsumeAndGet((float)recipe.consumedHEP);
		}
		foreach (ComplexRecipe.RecipeElement recipeElement3 in recipe.results)
		{
			GameObject gameObject = this.buildStorage.FindFirst(recipeElement3.material);
			if (gameObject != null)
			{
				Edible component2 = gameObject.GetComponent<Edible>();
				if (component2)
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, -component2.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.CRAFTED_USED, "{0}", component2.GetProperName()), UI.ENDOFDAYREPORT.NOTES.CRAFTED_CONTEXT);
				}
			}
			ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation = recipeElement3.temperatureOperation;
			if (temperatureOperation > ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
			{
				if (temperatureOperation == ComplexRecipe.RecipeElement.TemperatureOperation.Melted)
				{
					if (this.storeProduced || recipeElement3.storeElement)
					{
						float temperature = ElementLoader.GetElement(recipeElement3.material).defaultValues.temperature;
						this.outStorage.AddLiquid(ElementLoader.GetElementID(recipeElement3.material), recipeElement3.amount, temperature, 0, 0, false, true);
					}
				}
			}
			else
			{
				GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab(recipeElement3.material), Grid.SceneLayer.Ore, null, 0);
				int num6 = Grid.PosToCell(this);
				gameObject2.transform.SetPosition(Grid.CellToPosCCC(num6, Grid.SceneLayer.Ore) + this.outputOffset);
				PrimaryElement component3 = gameObject2.GetComponent<PrimaryElement>();
				component3.Units = recipeElement3.amount;
				component3.Temperature = ((recipeElement3.temperatureOperation == ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature) ? num : this.heatedTemperature);
				if (element != null)
				{
					component3.SetElement(element.id, false);
				}
				if (recipe.ProductHasFacade && !text.IsNullOrWhiteSpace())
				{
					Equippable component4 = gameObject2.GetComponent<Equippable>();
					if (component4 != null)
					{
						EquippableFacade.AddFacadeToEquippable(component4, text);
					}
				}
				gameObject2.SetActive(true);
				float num7 = recipeElement3.amount / recipe.TotalResultUnits();
				component3.AddDisease(diseaseInfo.idx, Mathf.RoundToInt((float)diseaseInfo.count * num7), "ComplexFabricator.CompleteOrder");
				if (!recipeElement3.facadeID.IsNullOrWhiteSpace())
				{
					Equippable component5 = gameObject2.GetComponent<Equippable>();
					if (component5 != null)
					{
						EquippableFacade.AddFacadeToEquippable(component5, recipeElement3.facadeID);
					}
				}
				gameObject2.GetComponent<KMonoBehaviour>().Trigger(748399584, null);
				list.Add(gameObject2);
				if (this.storeProduced || recipeElement3.storeElement)
				{
					this.outStorage.Store(gameObject2, false, false, true, false);
				}
			}
			if (list.Count > 0)
			{
				SymbolOverrideController component6 = base.GetComponent<SymbolOverrideController>();
				if (component6 != null)
				{
					KAnim.Build build = list[0].GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build;
					KAnim.Build.Symbol symbol = build.GetSymbol(build.name);
					if (symbol != null)
					{
						component6.TryRemoveSymbolOverride("output_tracker", 0);
						component6.AddSymbolOverride("output_tracker", symbol, 0);
					}
					else
					{
						global::Debug.LogWarning(component6.name + " is missing symbol " + build.name);
					}
				}
			}
		}
		if (recipe.producedHEP > 0)
		{
			base.GetComponent<HighEnergyParticleStorage>().Store((float)recipe.producedHEP);
		}
		return list;
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x000BD650 File Offset: 0x000BB850
	public virtual List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		ComplexRecipe[] recipes = this.GetRecipes();
		if (recipes.Length != 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.PROCESSES, UI.BUILDINGEFFECTS.TOOLTIPS.PROCESSES, Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		foreach (ComplexRecipe complexRecipe in recipes)
		{
			string text = "";
			string uiname = complexRecipe.GetUIName(false);
			foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
			{
				text = text + "• " + string.Format(UI.BUILDINGEFFECTS.PROCESSEDITEM, recipeElement.material.ProperName(), recipeElement.amount) + "\n";
			}
			Descriptor descriptor2 = new Descriptor(uiname, string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.FABRICATOR_INGREDIENTS, text), Descriptor.DescriptorType.Effect, false);
			descriptor2.IncreaseIndent();
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x000BD748 File Offset: 0x000BB948
	public virtual List<Descriptor> AdditionalEffectsForRecipe(ComplexRecipe recipe)
	{
		return new List<Descriptor>();
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x000BD750 File Offset: 0x000BB950
	public string GetConversationTopic()
	{
		if (this.HasWorkingOrder)
		{
			ComplexRecipe complexRecipe = this.recipe_list[this.workingOrderIdx];
			if (complexRecipe != null)
			{
				return complexRecipe.results[0].material.Name;
			}
		}
		return null;
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x000BD78C File Offset: 0x000BB98C
	public bool NeedsMoreHEPForQueuedRecipe()
	{
		if (this.hasOpenOrders)
		{
			HighEnergyParticleStorage component = base.GetComponent<HighEnergyParticleStorage>();
			foreach (KeyValuePair<string, int> keyValuePair in this.recipeQueueCounts)
			{
				if (keyValuePair.Value > 0)
				{
					foreach (ComplexRecipe complexRecipe in this.GetRecipes())
					{
						if (complexRecipe.id == keyValuePair.Key && (float)complexRecipe.consumedHEP > component.Particles)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x040013ED RID: 5101
	private const int MaxPrefetchCount = 2;

	// Token: 0x040013EE RID: 5102
	public bool duplicantOperated = true;

	// Token: 0x040013EF RID: 5103
	protected ComplexFabricatorWorkable workable;

	// Token: 0x040013F0 RID: 5104
	[SerializeField]
	public HashedString fetchChoreTypeIdHash = Db.Get().ChoreTypes.FabricateFetch.IdHash;

	// Token: 0x040013F1 RID: 5105
	[SerializeField]
	public float heatedTemperature;

	// Token: 0x040013F2 RID: 5106
	[SerializeField]
	public bool storeProduced;

	// Token: 0x040013F3 RID: 5107
	public ComplexFabricatorSideScreen.StyleSetting sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;

	// Token: 0x040013F4 RID: 5108
	public bool labelByResult = true;

	// Token: 0x040013F5 RID: 5109
	public Vector3 outputOffset = Vector3.zero;

	// Token: 0x040013F6 RID: 5110
	public ChoreType choreType;

	// Token: 0x040013F7 RID: 5111
	public bool keepExcessLiquids;

	// Token: 0x040013F8 RID: 5112
	public Tag keepAdditionalTag = Tag.Invalid;

	// Token: 0x040013F9 RID: 5113
	public static int MAX_QUEUE_SIZE = 99;

	// Token: 0x040013FA RID: 5114
	public static int QUEUE_INFINITE = -1;

	// Token: 0x040013FB RID: 5115
	[Serialize]
	private Dictionary<string, int> recipeQueueCounts = new Dictionary<string, int>();

	// Token: 0x040013FC RID: 5116
	private int nextOrderIdx;

	// Token: 0x040013FD RID: 5117
	private bool nextOrderIsWorkable;

	// Token: 0x040013FE RID: 5118
	private int workingOrderIdx = -1;

	// Token: 0x040013FF RID: 5119
	[Serialize]
	private string lastWorkingRecipe;

	// Token: 0x04001400 RID: 5120
	[Serialize]
	private float orderProgress;

	// Token: 0x04001401 RID: 5121
	private List<int> openOrderCounts = new List<int>();

	// Token: 0x04001402 RID: 5122
	[Serialize]
	private bool forbidMutantSeeds;

	// Token: 0x04001403 RID: 5123
	private Tag[] forbiddenMutantTags = new Tag[] { GameTags.MutatedSeed };

	// Token: 0x04001404 RID: 5124
	private bool queueDirty = true;

	// Token: 0x04001405 RID: 5125
	private bool hasOpenOrders;

	// Token: 0x04001406 RID: 5126
	private List<FetchList2> fetchListList = new List<FetchList2>();

	// Token: 0x04001407 RID: 5127
	private Chore chore;

	// Token: 0x04001408 RID: 5128
	private bool cancelling;

	// Token: 0x04001409 RID: 5129
	private ComplexRecipe[] recipe_list;

	// Token: 0x0400140A RID: 5130
	private Dictionary<Tag, float> materialNeedCache = new Dictionary<Tag, float>();

	// Token: 0x0400140B RID: 5131
	[SerializeField]
	public Storage inStorage;

	// Token: 0x0400140C RID: 5132
	[SerializeField]
	public Storage buildStorage;

	// Token: 0x0400140D RID: 5133
	[SerializeField]
	public Storage outStorage;

	// Token: 0x0400140E RID: 5134
	[MyCmpAdd]
	private LoopingSounds loopingSounds;

	// Token: 0x0400140F RID: 5135
	[MyCmpReq]
	protected Operational operational;

	// Token: 0x04001410 RID: 5136
	[MyCmpAdd]
	private ComplexFabricatorSM fabricatorSM;

	// Token: 0x04001411 RID: 5137
	private ProgressBar progressBar;

	// Token: 0x04001412 RID: 5138
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001413 RID: 5139
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnParticleStorageChangedDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001414 RID: 5140
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnDroppedAllDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnDroppedAll(data);
	});

	// Token: 0x04001415 RID: 5141
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001416 RID: 5142
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001417 RID: 5143
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
