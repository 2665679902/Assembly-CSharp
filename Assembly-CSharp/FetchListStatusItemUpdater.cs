﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000775 RID: 1909
[AddComponentMenu("KMonoBehaviour/scripts/FetchListStatusItemUpdater")]
public class FetchListStatusItemUpdater : KMonoBehaviour, IRender200ms
{
	// Token: 0x0600346D RID: 13421 RVA: 0x0011AC1A File Offset: 0x00118E1A
	public static void DestroyInstance()
	{
		FetchListStatusItemUpdater.instance = null;
	}

	// Token: 0x0600346E RID: 13422 RVA: 0x0011AC22 File Offset: 0x00118E22
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		FetchListStatusItemUpdater.instance = this;
	}

	// Token: 0x0600346F RID: 13423 RVA: 0x0011AC30 File Offset: 0x00118E30
	public void AddFetchList(FetchList2 fetch_list)
	{
		this.fetchLists.Add(fetch_list);
	}

	// Token: 0x06003470 RID: 13424 RVA: 0x0011AC3E File Offset: 0x00118E3E
	public void RemoveFetchList(FetchList2 fetch_list)
	{
		this.fetchLists.Remove(fetch_list);
	}

	// Token: 0x06003471 RID: 13425 RVA: 0x0011AC50 File Offset: 0x00118E50
	public void Render200ms(float dt)
	{
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			int id = worldContainer.id;
			DictionaryPool<int, ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary = DictionaryPool<int, ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList, FetchListStatusItemUpdater>.Allocate();
			int num = Math.Min(this.maxIteratingCount, this.fetchLists.Count - this.currentIterationIndex[id]);
			for (int i = 0; i < num; i++)
			{
				FetchList2 fetchList = this.fetchLists[i + this.currentIterationIndex[id]];
				if (!(fetchList.Destination == null) && fetchList.Destination.gameObject.GetMyWorldId() == id)
				{
					ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList pooledList = null;
					int instanceID = fetchList.Destination.GetInstanceID();
					if (!pooledDictionary.TryGetValue(instanceID, out pooledList))
					{
						pooledList = ListPool<FetchList2, FetchListStatusItemUpdater>.Allocate();
						pooledDictionary[instanceID] = pooledList;
					}
					pooledList.Add(fetchList);
				}
			}
			this.currentIterationIndex[id] += num;
			if (this.currentIterationIndex[id] >= this.fetchLists.Count)
			{
				this.currentIterationIndex[id] = 0;
			}
			DictionaryPool<Tag, float, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary2 = DictionaryPool<Tag, float, FetchListStatusItemUpdater>.Allocate();
			DictionaryPool<Tag, float, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary3 = DictionaryPool<Tag, float, FetchListStatusItemUpdater>.Allocate();
			foreach (KeyValuePair<int, ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList> keyValuePair in pooledDictionary)
			{
				if (!(keyValuePair.Value[0].Destination.GetMyWorld() == null))
				{
					ListPool<Tag, FetchListStatusItemUpdater>.PooledList pooledList2 = ListPool<Tag, FetchListStatusItemUpdater>.Allocate();
					Storage destination = keyValuePair.Value[0].Destination;
					foreach (FetchList2 fetchList2 in keyValuePair.Value)
					{
						fetchList2.UpdateRemaining();
						foreach (KeyValuePair<Tag, float> keyValuePair2 in fetchList2.GetRemaining())
						{
							if (!pooledList2.Contains(keyValuePair2.Key))
							{
								pooledList2.Add(keyValuePair2.Key);
							}
						}
					}
					ListPool<Pickupable, FetchListStatusItemUpdater>.PooledList pooledList3 = ListPool<Pickupable, FetchListStatusItemUpdater>.Allocate();
					foreach (GameObject gameObject in destination.items)
					{
						if (!(gameObject == null))
						{
							Pickupable component = gameObject.GetComponent<Pickupable>();
							if (!(component == null))
							{
								pooledList3.Add(component);
							}
						}
					}
					DictionaryPool<Tag, float, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary4 = DictionaryPool<Tag, float, FetchListStatusItemUpdater>.Allocate();
					foreach (Tag tag in pooledList2)
					{
						float num2 = 0f;
						foreach (Pickupable pickupable in pooledList3)
						{
							if (pickupable.KPrefabID.HasTag(tag))
							{
								num2 += pickupable.TotalAmount;
							}
						}
						pooledDictionary4[tag] = num2;
					}
					foreach (Tag tag2 in pooledList2)
					{
						if (!pooledDictionary2.ContainsKey(tag2))
						{
							pooledDictionary2[tag2] = destination.GetMyWorld().worldInventory.GetTotalAmount(tag2, true);
						}
						if (!pooledDictionary3.ContainsKey(tag2))
						{
							pooledDictionary3[tag2] = destination.GetMyWorld().worldInventory.GetAmount(tag2, true);
						}
					}
					foreach (FetchList2 fetchList3 in keyValuePair.Value)
					{
						bool flag = false;
						bool flag2 = true;
						bool flag3 = false;
						foreach (KeyValuePair<Tag, float> keyValuePair3 in fetchList3.GetRemaining())
						{
							Tag key = keyValuePair3.Key;
							float value = keyValuePair3.Value;
							float num3 = pooledDictionary4[key];
							float num4 = pooledDictionary2[key];
							float num5 = pooledDictionary3[key];
							float num6 = Mathf.Min(value, num4);
							float num7 = num5 + num6;
							float minimumAmount = fetchList3.GetMinimumAmount(key);
							if (num3 + num7 < minimumAmount)
							{
								flag = true;
							}
							if (num7 < value)
							{
								flag2 = false;
							}
							if (num3 + num7 > value && value > num7)
							{
								flag3 = true;
							}
						}
						fetchList3.UpdateStatusItem(Db.Get().BuildingStatusItems.WaitingForMaterials, ref fetchList3.waitingForMaterialsHandle, flag2);
						fetchList3.UpdateStatusItem(Db.Get().BuildingStatusItems.MaterialsUnavailable, ref fetchList3.materialsUnavailableHandle, flag);
						fetchList3.UpdateStatusItem(Db.Get().BuildingStatusItems.MaterialsUnavailableForRefill, ref fetchList3.materialsUnavailableForRefillHandle, flag3);
					}
					pooledDictionary4.Recycle();
					pooledList3.Recycle();
					pooledList2.Recycle();
					keyValuePair.Value.Recycle();
				}
			}
			pooledDictionary3.Recycle();
			pooledDictionary2.Recycle();
			pooledDictionary.Recycle();
		}
	}

	// Token: 0x0400207F RID: 8319
	public static FetchListStatusItemUpdater instance;

	// Token: 0x04002080 RID: 8320
	private List<FetchList2> fetchLists = new List<FetchList2>();

	// Token: 0x04002081 RID: 8321
	private int[] currentIterationIndex = new int[(int)ClusterManager.INVALID_WORLD_IDX];

	// Token: 0x04002082 RID: 8322
	private int maxIteratingCount = 100;
}
