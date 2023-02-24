using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000776 RID: 1910
[AddComponentMenu("KMonoBehaviour/scripts/FetchManager")]
public class FetchManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06003473 RID: 13427 RVA: 0x0011B26B File Offset: 0x0011946B
	private static int QuantizeRotValue(float rot_value)
	{
		return (int)(4f * rot_value);
	}

	// Token: 0x06003474 RID: 13428 RVA: 0x0011B275 File Offset: 0x00119475
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name)
	{
	}

	// Token: 0x06003475 RID: 13429 RVA: 0x0011B277 File Offset: 0x00119477
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x06003476 RID: 13430 RVA: 0x0011B279 File Offset: 0x00119479
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name)
	{
	}

	// Token: 0x06003477 RID: 13431 RVA: 0x0011B27B File Offset: 0x0011947B
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x06003478 RID: 13432 RVA: 0x0011B280 File Offset: 0x00119480
	public HandleVector<int>.Handle Add(Pickupable pickupable)
	{
		Tag tag = pickupable.KPrefabID.PrefabID();
		FetchManager.FetchablesByPrefabId fetchablesByPrefabId = null;
		if (!this.prefabIdToFetchables.TryGetValue(tag, out fetchablesByPrefabId))
		{
			fetchablesByPrefabId = new FetchManager.FetchablesByPrefabId(tag);
			this.prefabIdToFetchables[tag] = fetchablesByPrefabId;
		}
		return fetchablesByPrefabId.AddPickupable(pickupable);
	}

	// Token: 0x06003479 RID: 13433 RVA: 0x0011B2C8 File Offset: 0x001194C8
	public void Remove(Tag prefab_tag, HandleVector<int>.Handle fetchable_handle)
	{
		FetchManager.FetchablesByPrefabId fetchablesByPrefabId;
		if (this.prefabIdToFetchables.TryGetValue(prefab_tag, out fetchablesByPrefabId))
		{
			fetchablesByPrefabId.RemovePickupable(fetchable_handle);
		}
	}

	// Token: 0x0600347A RID: 13434 RVA: 0x0011B2EC File Offset: 0x001194EC
	public void UpdateStorage(Tag prefab_tag, HandleVector<int>.Handle fetchable_handle, Storage storage)
	{
		FetchManager.FetchablesByPrefabId fetchablesByPrefabId;
		if (this.prefabIdToFetchables.TryGetValue(prefab_tag, out fetchablesByPrefabId))
		{
			fetchablesByPrefabId.UpdateStorage(fetchable_handle, storage);
		}
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x0011B311 File Offset: 0x00119511
	public void UpdateTags(Tag prefab_tag, HandleVector<int>.Handle fetchable_handle)
	{
		this.prefabIdToFetchables[prefab_tag].UpdateTags(fetchable_handle);
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x0011B328 File Offset: 0x00119528
	public void Sim1000ms(float dt)
	{
		foreach (KeyValuePair<Tag, FetchManager.FetchablesByPrefabId> keyValuePair in this.prefabIdToFetchables)
		{
			keyValuePair.Value.Sim1000ms(dt);
		}
	}

	// Token: 0x0600347D RID: 13437 RVA: 0x0011B384 File Offset: 0x00119584
	public void UpdatePickups(PathProber path_prober, Worker worker)
	{
		Navigator component = worker.GetComponent<Navigator>();
		this.updatePickupsWorkItems.Reset(null);
		foreach (KeyValuePair<Tag, FetchManager.FetchablesByPrefabId> keyValuePair in this.prefabIdToFetchables)
		{
			FetchManager.FetchablesByPrefabId value = keyValuePair.Value;
			value.UpdateOffsetTables();
			this.updatePickupsWorkItems.Add(new FetchManager.UpdatePickupWorkItem
			{
				fetchablesByPrefabId = value,
				pathProber = path_prober,
				navigator = component,
				worker = worker.gameObject
			});
		}
		OffsetTracker.isExecutingWithinJob = true;
		GlobalJobManager.Run(this.updatePickupsWorkItems);
		OffsetTracker.isExecutingWithinJob = false;
		this.pickups.Clear();
		foreach (KeyValuePair<Tag, FetchManager.FetchablesByPrefabId> keyValuePair2 in this.prefabIdToFetchables)
		{
			this.pickups.AddRange(keyValuePair2.Value.finalPickups);
		}
		this.pickups.Sort(FetchManager.ComparerNoPriority);
	}

	// Token: 0x0600347E RID: 13438 RVA: 0x0011B4B0 File Offset: 0x001196B0
	public static bool IsFetchablePickup(Pickupable pickup, FetchChore chore, Storage destination)
	{
		KPrefabID kprefabID = pickup.KPrefabID;
		Storage storage = pickup.storage;
		if (pickup.UnreservedAmount <= 0f)
		{
			return false;
		}
		if (kprefabID == null)
		{
			return false;
		}
		if (chore.criteria == FetchChore.MatchCriteria.MatchID && !chore.tags.Contains(kprefabID.PrefabTag))
		{
			return false;
		}
		if (chore.criteria == FetchChore.MatchCriteria.MatchTags && !kprefabID.HasTag(chore.tagsFirst))
		{
			return false;
		}
		if (chore.requiredTag.IsValid && !kprefabID.HasTag(chore.requiredTag))
		{
			return false;
		}
		if (kprefabID.HasAnyTags(chore.forbiddenTags))
		{
			return false;
		}
		if (storage != null)
		{
			if (!storage.ignoreSourcePriority && destination.ShouldOnlyTransferFromLowerPriority && destination.masterPriority <= storage.masterPriority)
			{
				return false;
			}
			if (destination.storageNetworkID != -1 && destination.storageNetworkID == storage.storageNetworkID)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600347F RID: 13439 RVA: 0x0011B590 File Offset: 0x00119790
	public static Pickupable FindFetchTarget(List<Pickupable> pickupables, Storage destination, FetchChore chore)
	{
		foreach (Pickupable pickupable in pickupables)
		{
			if (FetchManager.IsFetchablePickup(pickupable, chore, destination))
			{
				return pickupable;
			}
		}
		return null;
	}

	// Token: 0x06003480 RID: 13440 RVA: 0x0011B5E8 File Offset: 0x001197E8
	public Pickupable FindFetchTarget(Storage destination, FetchChore chore)
	{
		foreach (FetchManager.Pickup pickup in this.pickups)
		{
			if (FetchManager.IsFetchablePickup(pickup.pickupable, chore, destination))
			{
				return pickup.pickupable;
			}
		}
		return null;
	}

	// Token: 0x06003481 RID: 13441 RVA: 0x0011B650 File Offset: 0x00119850
	public static bool IsFetchablePickup_Exclude(KPrefabID pickup_id, Storage source, float pickup_unreserved_amount, HashSet<Tag> exclude_tags, Tag required_tag, Storage destination)
	{
		if (pickup_unreserved_amount <= 0f)
		{
			return false;
		}
		if (pickup_id == null)
		{
			return false;
		}
		if (exclude_tags.Contains(pickup_id.PrefabTag))
		{
			return false;
		}
		if (!pickup_id.HasTag(required_tag))
		{
			return false;
		}
		if (source != null)
		{
			if (!source.ignoreSourcePriority && destination.ShouldOnlyTransferFromLowerPriority && destination.masterPriority <= source.masterPriority)
			{
				return false;
			}
			if (destination.storageNetworkID != -1 && destination.storageNetworkID == source.storageNetworkID)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003482 RID: 13442 RVA: 0x0011B6DC File Offset: 0x001198DC
	public Pickupable FindEdibleFetchTarget(Storage destination, HashSet<Tag> exclude_tags, Tag required_tag)
	{
		FetchManager.Pickup pickup = new FetchManager.Pickup
		{
			PathCost = ushort.MaxValue,
			foodQuality = int.MinValue
		};
		int num = int.MaxValue;
		foreach (FetchManager.Pickup pickup2 in this.pickups)
		{
			Pickupable pickupable = pickup2.pickupable;
			if (FetchManager.IsFetchablePickup_Exclude(pickupable.KPrefabID, pickupable.storage, pickupable.UnreservedAmount, exclude_tags, required_tag, destination))
			{
				int num2 = (int)pickup2.PathCost + (5 - pickup2.foodQuality) * 50;
				if (num2 < num)
				{
					pickup = pickup2;
					num = num2;
				}
			}
		}
		return pickup.pickupable;
	}

	// Token: 0x04002083 RID: 8323
	public static TagBits disallowedTagBits = new TagBits(GameTags.Preserved);

	// Token: 0x04002084 RID: 8324
	public static TagBits disallowedTagMask = TagBits.MakeComplement(ref FetchManager.disallowedTagBits);

	// Token: 0x04002085 RID: 8325
	private static readonly FetchManager.PickupComparerIncludingPriority ComparerIncludingPriority = new FetchManager.PickupComparerIncludingPriority();

	// Token: 0x04002086 RID: 8326
	private static readonly FetchManager.PickupComparerNoPriority ComparerNoPriority = new FetchManager.PickupComparerNoPriority();

	// Token: 0x04002087 RID: 8327
	private List<FetchManager.Pickup> pickups = new List<FetchManager.Pickup>();

	// Token: 0x04002088 RID: 8328
	public Dictionary<Tag, FetchManager.FetchablesByPrefabId> prefabIdToFetchables = new Dictionary<Tag, FetchManager.FetchablesByPrefabId>();

	// Token: 0x04002089 RID: 8329
	private WorkItemCollection<FetchManager.UpdatePickupWorkItem, object> updatePickupsWorkItems = new WorkItemCollection<FetchManager.UpdatePickupWorkItem, object>();

	// Token: 0x02001469 RID: 5225
	public struct Fetchable
	{
		// Token: 0x0400634E RID: 25422
		public Pickupable pickupable;

		// Token: 0x0400634F RID: 25423
		public int tagBitsHash;

		// Token: 0x04006350 RID: 25424
		public int masterPriority;

		// Token: 0x04006351 RID: 25425
		public int freshness;

		// Token: 0x04006352 RID: 25426
		public int foodQuality;
	}

	// Token: 0x0200146A RID: 5226
	[DebuggerDisplay("{pickupable.name}")]
	public struct Pickup
	{
		// Token: 0x04006353 RID: 25427
		public Pickupable pickupable;

		// Token: 0x04006354 RID: 25428
		public int tagBitsHash;

		// Token: 0x04006355 RID: 25429
		public ushort PathCost;

		// Token: 0x04006356 RID: 25430
		public int masterPriority;

		// Token: 0x04006357 RID: 25431
		public int freshness;

		// Token: 0x04006358 RID: 25432
		public int foodQuality;
	}

	// Token: 0x0200146B RID: 5227
	private class PickupComparerIncludingPriority : IComparer<FetchManager.Pickup>
	{
		// Token: 0x060080FA RID: 33018 RVA: 0x002E032C File Offset: 0x002DE52C
		public int Compare(FetchManager.Pickup a, FetchManager.Pickup b)
		{
			int num = a.tagBitsHash.CompareTo(b.tagBitsHash);
			if (num != 0)
			{
				return num;
			}
			num = b.masterPriority.CompareTo(a.masterPriority);
			if (num != 0)
			{
				return num;
			}
			num = a.PathCost.CompareTo(b.PathCost);
			if (num != 0)
			{
				return num;
			}
			num = b.foodQuality.CompareTo(a.foodQuality);
			if (num != 0)
			{
				return num;
			}
			return b.freshness.CompareTo(a.freshness);
		}
	}

	// Token: 0x0200146C RID: 5228
	private class PickupComparerNoPriority : IComparer<FetchManager.Pickup>
	{
		// Token: 0x060080FC RID: 33020 RVA: 0x002E03B8 File Offset: 0x002DE5B8
		public int Compare(FetchManager.Pickup a, FetchManager.Pickup b)
		{
			int num = a.PathCost.CompareTo(b.PathCost);
			if (num != 0)
			{
				return num;
			}
			num = b.foodQuality.CompareTo(a.foodQuality);
			if (num != 0)
			{
				return num;
			}
			return b.freshness.CompareTo(a.freshness);
		}
	}

	// Token: 0x0200146D RID: 5229
	public class FetchablesByPrefabId
	{
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060080FE RID: 33022 RVA: 0x002E0411 File Offset: 0x002DE611
		// (set) Token: 0x060080FF RID: 33023 RVA: 0x002E0419 File Offset: 0x002DE619
		public Tag prefabId { get; private set; }

		// Token: 0x06008100 RID: 33024 RVA: 0x002E0424 File Offset: 0x002DE624
		public FetchablesByPrefabId(Tag prefab_id)
		{
			this.prefabId = prefab_id;
			this.fetchables = new KCompactedVector<FetchManager.Fetchable>(0);
			this.rotUpdaters = new Dictionary<HandleVector<int>.Handle, Rottable.Instance>();
			this.finalPickups = new List<FetchManager.Pickup>();
		}

		// Token: 0x06008101 RID: 33025 RVA: 0x002E0484 File Offset: 0x002DE684
		public HandleVector<int>.Handle AddPickupable(Pickupable pickupable)
		{
			int num = 5;
			Edible component = pickupable.GetComponent<Edible>();
			if (component != null)
			{
				num = component.GetQuality();
			}
			int num2 = 0;
			if (pickupable.storage != null)
			{
				Prioritizable prioritizable = pickupable.storage.prioritizable;
				if (prioritizable != null)
				{
					num2 = prioritizable.GetMasterPriority().priority_value;
				}
			}
			Rottable.Instance smi = pickupable.GetSMI<Rottable.Instance>();
			int num3 = 0;
			if (!smi.IsNullOrStopped())
			{
				num3 = FetchManager.QuantizeRotValue(smi.RotValue);
			}
			KPrefabID kprefabID = pickupable.KPrefabID;
			TagBits tagBits = new TagBits(ref FetchManager.disallowedTagMask);
			kprefabID.AndTagBits(ref tagBits);
			HandleVector<int>.Handle handle = this.fetchables.Allocate(new FetchManager.Fetchable
			{
				pickupable = pickupable,
				foodQuality = num,
				freshness = num3,
				masterPriority = num2,
				tagBitsHash = tagBits.GetHashCode()
			});
			if (!smi.IsNullOrStopped())
			{
				this.rotUpdaters[handle] = smi;
			}
			return handle;
		}

		// Token: 0x06008102 RID: 33026 RVA: 0x002E057E File Offset: 0x002DE77E
		public void RemovePickupable(HandleVector<int>.Handle fetchable_handle)
		{
			this.fetchables.Free(fetchable_handle);
			this.rotUpdaters.Remove(fetchable_handle);
		}

		// Token: 0x06008103 RID: 33027 RVA: 0x002E059C File Offset: 0x002DE79C
		public void UpdatePickups(PathProber path_prober, Navigator worker_navigator, GameObject worker_go)
		{
			this.GatherPickupablesWhichCanBePickedUp(worker_go);
			this.GatherReachablePickups(worker_navigator);
			this.finalPickups.Sort(FetchManager.ComparerIncludingPriority);
			if (this.finalPickups.Count > 0)
			{
				FetchManager.Pickup pickup = this.finalPickups[0];
				TagBits tagBits = new TagBits(ref FetchManager.disallowedTagMask);
				pickup.pickupable.KPrefabID.AndTagBits(ref tagBits);
				int num = pickup.tagBitsHash;
				int num2 = this.finalPickups.Count;
				int num3 = 0;
				for (int i = 1; i < this.finalPickups.Count; i++)
				{
					bool flag = false;
					FetchManager.Pickup pickup2 = this.finalPickups[i];
					TagBits tagBits2 = default(TagBits);
					int tagBitsHash = pickup2.tagBitsHash;
					if (pickup.masterPriority == pickup2.masterPriority)
					{
						tagBits2 = new TagBits(ref FetchManager.disallowedTagMask);
						pickup2.pickupable.KPrefabID.AndTagBits(ref tagBits2);
						if (pickup2.tagBitsHash == num && tagBits2.AreEqual(ref tagBits))
						{
							flag = true;
						}
					}
					if (flag)
					{
						num2--;
					}
					else
					{
						num3++;
						pickup = pickup2;
						tagBits = tagBits2;
						num = tagBitsHash;
						if (i > num3)
						{
							this.finalPickups[num3] = pickup2;
						}
					}
				}
				this.finalPickups.RemoveRange(num2, this.finalPickups.Count - num2);
			}
		}

		// Token: 0x06008104 RID: 33028 RVA: 0x002E06E8 File Offset: 0x002DE8E8
		private void GatherPickupablesWhichCanBePickedUp(GameObject worker_go)
		{
			this.pickupsWhichCanBePickedUp.Clear();
			foreach (FetchManager.Fetchable fetchable in this.fetchables.GetDataList())
			{
				Pickupable pickupable = fetchable.pickupable;
				if (pickupable.CouldBePickedUpByMinion(worker_go))
				{
					this.pickupsWhichCanBePickedUp.Add(new FetchManager.Pickup
					{
						pickupable = pickupable,
						tagBitsHash = fetchable.tagBitsHash,
						PathCost = ushort.MaxValue,
						masterPriority = fetchable.masterPriority,
						freshness = fetchable.freshness,
						foodQuality = fetchable.foodQuality
					});
				}
			}
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x002E07B0 File Offset: 0x002DE9B0
		public void UpdateOffsetTables()
		{
			foreach (FetchManager.Fetchable fetchable in this.fetchables.GetDataList())
			{
				fetchable.pickupable.GetOffsets(fetchable.pickupable.cachedCell);
			}
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x002E0818 File Offset: 0x002DEA18
		private void GatherReachablePickups(Navigator navigator)
		{
			this.cellCosts.Clear();
			this.finalPickups.Clear();
			foreach (FetchManager.Pickup pickup in this.pickupsWhichCanBePickedUp)
			{
				Pickupable pickupable = pickup.pickupable;
				int num = -1;
				if (!this.cellCosts.TryGetValue(pickupable.cachedCell, out num))
				{
					num = pickupable.GetNavigationCost(navigator, pickupable.cachedCell);
					this.cellCosts[pickupable.cachedCell] = num;
				}
				if (num != -1)
				{
					this.finalPickups.Add(new FetchManager.Pickup
					{
						pickupable = pickupable,
						tagBitsHash = pickup.tagBitsHash,
						PathCost = (ushort)num,
						masterPriority = pickup.masterPriority,
						freshness = pickup.freshness,
						foodQuality = pickup.foodQuality
					});
				}
			}
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x002E091C File Offset: 0x002DEB1C
		public void UpdateStorage(HandleVector<int>.Handle fetchable_handle, Storage storage)
		{
			FetchManager.Fetchable data = this.fetchables.GetData(fetchable_handle);
			int num = 0;
			Pickupable pickupable = data.pickupable;
			if (pickupable.storage != null)
			{
				Prioritizable prioritizable = pickupable.storage.prioritizable;
				if (prioritizable != null)
				{
					num = prioritizable.GetMasterPriority().priority_value;
				}
			}
			data.masterPriority = num;
			this.fetchables.SetData(fetchable_handle, data);
		}

		// Token: 0x06008108 RID: 33032 RVA: 0x002E0988 File Offset: 0x002DEB88
		public void UpdateTags(HandleVector<int>.Handle fetchable_handle)
		{
			FetchManager.Fetchable data = this.fetchables.GetData(fetchable_handle);
			TagBits tagBits = new TagBits(ref FetchManager.disallowedTagMask);
			data.pickupable.KPrefabID.AndTagBits(ref tagBits);
			data.tagBitsHash = tagBits.GetHashCode();
			this.fetchables.SetData(fetchable_handle, data);
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x002E09E0 File Offset: 0x002DEBE0
		public void Sim1000ms(float dt)
		{
			foreach (KeyValuePair<HandleVector<int>.Handle, Rottable.Instance> keyValuePair in this.rotUpdaters)
			{
				HandleVector<int>.Handle key = keyValuePair.Key;
				Rottable.Instance value = keyValuePair.Value;
				FetchManager.Fetchable data = this.fetchables.GetData(key);
				data.freshness = FetchManager.QuantizeRotValue(value.RotValue);
				this.fetchables.SetData(key, data);
			}
		}

		// Token: 0x04006359 RID: 25433
		public KCompactedVector<FetchManager.Fetchable> fetchables;

		// Token: 0x0400635A RID: 25434
		public List<FetchManager.Pickup> finalPickups = new List<FetchManager.Pickup>();

		// Token: 0x0400635B RID: 25435
		private Dictionary<HandleVector<int>.Handle, Rottable.Instance> rotUpdaters;

		// Token: 0x0400635C RID: 25436
		private List<FetchManager.Pickup> pickupsWhichCanBePickedUp = new List<FetchManager.Pickup>();

		// Token: 0x0400635D RID: 25437
		private Dictionary<int, int> cellCosts = new Dictionary<int, int>();
	}

	// Token: 0x0200146E RID: 5230
	private struct UpdatePickupWorkItem : IWorkItem<object>
	{
		// Token: 0x0600810A RID: 33034 RVA: 0x002E0A6C File Offset: 0x002DEC6C
		public void Run(object shared_data)
		{
			this.fetchablesByPrefabId.UpdatePickups(this.pathProber, this.navigator, this.worker);
		}

		// Token: 0x0400635F RID: 25439
		public FetchManager.FetchablesByPrefabId fetchablesByPrefabId;

		// Token: 0x04006360 RID: 25440
		public PathProber pathProber;

		// Token: 0x04006361 RID: 25441
		public Navigator navigator;

		// Token: 0x04006362 RID: 25442
		public GameObject worker;
	}
}
