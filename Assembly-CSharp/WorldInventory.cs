using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KSerialization;
using UnityEngine;

// Token: 0x020009D8 RID: 2520
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/WorldInventory")]
public class WorldInventory : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x170005A1 RID: 1441
	// (get) Token: 0x06004B41 RID: 19265 RVA: 0x001A583F File Offset: 0x001A3A3F
	public WorldContainer WorldContainer
	{
		get
		{
			if (this.m_worldContainer == null)
			{
				this.m_worldContainer = base.GetComponent<WorldContainer>();
			}
			return this.m_worldContainer;
		}
	}

	// Token: 0x170005A2 RID: 1442
	// (get) Token: 0x06004B42 RID: 19266 RVA: 0x001A5861 File Offset: 0x001A3A61
	public bool HasValidCount
	{
		get
		{
			return this.hasValidCount;
		}
	}

	// Token: 0x170005A3 RID: 1443
	// (get) Token: 0x06004B43 RID: 19267 RVA: 0x001A586C File Offset: 0x001A3A6C
	private int worldId
	{
		get
		{
			WorldContainer worldContainer = this.WorldContainer;
			if (!(worldContainer != null))
			{
				return -1;
			}
			return worldContainer.id;
		}
	}

	// Token: 0x06004B44 RID: 19268 RVA: 0x001A5894 File Offset: 0x001A3A94
	protected override void OnPrefabInit()
	{
		base.Subscribe(Game.Instance.gameObject, -1588644844, new Action<object>(this.OnAddedFetchable));
		base.Subscribe(Game.Instance.gameObject, -1491270284, new Action<object>(this.OnRemovedFetchable));
		base.Subscribe<WorldInventory>(631075836, WorldInventory.OnNewDayDelegate);
		this.m_worldContainer = base.GetComponent<WorldContainer>();
	}

	// Token: 0x06004B45 RID: 19269 RVA: 0x001A5904 File Offset: 0x001A3B04
	protected override void OnCleanUp()
	{
		base.Unsubscribe(Game.Instance.gameObject, -1588644844, new Action<object>(this.OnAddedFetchable));
		base.Unsubscribe(Game.Instance.gameObject, -1491270284, new Action<object>(this.OnRemovedFetchable));
		base.OnCleanUp();
	}

	// Token: 0x06004B46 RID: 19270 RVA: 0x001A595C File Offset: 0x001A3B5C
	private void GenerateInventoryReport(object data)
	{
		int num = 0;
		int num2 = 0;
		foreach (Brain brain in Components.Brains.GetWorldItems(this.worldId, false))
		{
			CreatureBrain creatureBrain = brain as CreatureBrain;
			if (creatureBrain != null)
			{
				if (creatureBrain.HasTag(GameTags.Creatures.Wild))
				{
					num++;
					ReportManager.Instance.ReportValue(ReportManager.ReportType.WildCritters, 1f, creatureBrain.GetProperName(), creatureBrain.GetProperName());
				}
				else
				{
					num2++;
					ReportManager.Instance.ReportValue(ReportManager.ReportType.DomesticatedCritters, 1f, creatureBrain.GetProperName(), creatureBrain.GetProperName());
				}
			}
		}
		if (DlcManager.IsExpansion1Active())
		{
			WorldContainer component = base.GetComponent<WorldContainer>();
			if (component != null && component.IsModuleInterior)
			{
				Clustercraft clustercraft = component.GetComponent<ClusterGridEntity>() as Clustercraft;
				if (clustercraft != null && clustercraft.Status != Clustercraft.CraftStatus.Grounded)
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.RocketsInFlight, 1f, clustercraft.Name, null);
					return;
				}
			}
		}
		else
		{
			foreach (Spacecraft spacecraft in SpacecraftManager.instance.GetSpacecraft())
			{
				if (spacecraft.state != Spacecraft.MissionState.Grounded && spacecraft.state != Spacecraft.MissionState.Destroyed)
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.RocketsInFlight, 1f, spacecraft.rocketName, null);
				}
			}
		}
	}

	// Token: 0x06004B47 RID: 19271 RVA: 0x001A5AEC File Offset: 0x001A3CEC
	protected override void OnSpawn()
	{
		this.Prober = MinionGroupProber.Get();
		base.StartCoroutine(this.InitialRefresh());
	}

	// Token: 0x06004B48 RID: 19272 RVA: 0x001A5B06 File Offset: 0x001A3D06
	private IEnumerator InitialRefresh()
	{
		int num;
		for (int i = 0; i < 1; i = num)
		{
			yield return null;
			num = i + 1;
		}
		for (int j = 0; j < Components.Pickupables.Count; j++)
		{
			Pickupable pickupable = Components.Pickupables[j];
			if (pickupable != null)
			{
				ReachabilityMonitor.Instance smi = pickupable.GetSMI<ReachabilityMonitor.Instance>();
				if (smi != null)
				{
					smi.UpdateReachability();
				}
			}
		}
		yield break;
	}

	// Token: 0x06004B49 RID: 19273 RVA: 0x001A5B0E File Offset: 0x001A3D0E
	public bool IsReachable(Pickupable pickupable)
	{
		return this.Prober.IsReachable(pickupable);
	}

	// Token: 0x06004B4A RID: 19274 RVA: 0x001A5B1C File Offset: 0x001A3D1C
	public float GetTotalAmount(Tag tag, bool includeRelatedWorlds)
	{
		float num = 0f;
		this.accessibleAmounts.TryGetValue(tag, out num);
		return num;
	}

	// Token: 0x06004B4B RID: 19275 RVA: 0x001A5B40 File Offset: 0x001A3D40
	public ICollection<Pickupable> GetPickupables(Tag tag, bool includeRelatedWorlds = false)
	{
		if (!includeRelatedWorlds)
		{
			HashSet<Pickupable> hashSet = null;
			this.Inventory.TryGetValue(tag, out hashSet);
			return hashSet;
		}
		return ClusterUtil.GetPickupablesFromRelatedWorlds(this, tag);
	}

	// Token: 0x06004B4C RID: 19276 RVA: 0x001A5B6C File Offset: 0x001A3D6C
	public List<Pickupable> CreatePickupablesList(Tag tag)
	{
		HashSet<Pickupable> hashSet = null;
		this.Inventory.TryGetValue(tag, out hashSet);
		if (hashSet == null)
		{
			return null;
		}
		return hashSet.ToList<Pickupable>();
	}

	// Token: 0x06004B4D RID: 19277 RVA: 0x001A5B98 File Offset: 0x001A3D98
	public float GetAmount(Tag tag, bool includeRelatedWorlds)
	{
		float num;
		if (!includeRelatedWorlds)
		{
			num = this.GetTotalAmount(tag, includeRelatedWorlds);
			num -= MaterialNeeds.GetAmount(tag, this.worldId, includeRelatedWorlds);
		}
		else
		{
			num = ClusterUtil.GetAmountFromRelatedWorlds(this, tag);
		}
		return Mathf.Max(num, 0f);
	}

	// Token: 0x06004B4E RID: 19278 RVA: 0x001A5BE0 File Offset: 0x001A3DE0
	public int GetCountWithAdditionalTag(Tag tag, Tag additionalTag, bool includeRelatedWorlds = false)
	{
		ICollection<Pickupable> collection;
		if (!includeRelatedWorlds)
		{
			collection = this.GetPickupables(tag, false);
		}
		else
		{
			ICollection<Pickupable> pickupablesFromRelatedWorlds = ClusterUtil.GetPickupablesFromRelatedWorlds(this, tag);
			collection = pickupablesFromRelatedWorlds;
		}
		ICollection<Pickupable> collection2 = collection;
		int num = 0;
		if (collection2 != null)
		{
			if (additionalTag.IsValid)
			{
				using (IEnumerator<Pickupable> enumerator = collection2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasTag(additionalTag))
						{
							num++;
						}
					}
					return num;
				}
			}
			num = collection2.Count;
		}
		return num;
	}

	// Token: 0x06004B4F RID: 19279 RVA: 0x001A5C5C File Offset: 0x001A3E5C
	public float GetAmountWithoutTag(Tag tag, bool includeRelatedWorlds = false, Tag[] forbiddenTags = null)
	{
		if (forbiddenTags == null)
		{
			return this.GetAmount(tag, includeRelatedWorlds);
		}
		float num = 0f;
		ICollection<Pickupable> collection;
		if (!includeRelatedWorlds)
		{
			collection = this.GetPickupables(tag, false);
		}
		else
		{
			ICollection<Pickupable> pickupablesFromRelatedWorlds = ClusterUtil.GetPickupablesFromRelatedWorlds(this, tag);
			collection = pickupablesFromRelatedWorlds;
		}
		ICollection<Pickupable> collection2 = collection;
		if (collection2 != null)
		{
			foreach (Pickupable pickupable in collection2)
			{
				if (pickupable != null && !pickupable.HasTag(GameTags.StoredPrivate) && !pickupable.HasAnyTags(forbiddenTags))
				{
					num += pickupable.TotalAmount;
				}
			}
		}
		return num;
	}

	// Token: 0x06004B50 RID: 19280 RVA: 0x001A5CF8 File Offset: 0x001A3EF8
	private void Update()
	{
		int num = 0;
		Dictionary<Tag, HashSet<Pickupable>>.Enumerator enumerator = this.Inventory.GetEnumerator();
		int worldId = this.worldId;
		while (enumerator.MoveNext())
		{
			KeyValuePair<Tag, HashSet<Pickupable>> keyValuePair = enumerator.Current;
			if (num == this.accessibleUpdateIndex || this.firstUpdate)
			{
				Tag key = keyValuePair.Key;
				IEnumerable<Pickupable> value = keyValuePair.Value;
				float num2 = 0f;
				foreach (Pickupable pickupable in value)
				{
					if (pickupable != null && pickupable.GetMyWorldId() == worldId && !pickupable.HasTag(GameTags.StoredPrivate))
					{
						num2 += pickupable.TotalAmount;
					}
				}
				if (!this.hasValidCount && this.accessibleUpdateIndex + 1 >= this.Inventory.Count)
				{
					this.hasValidCount = true;
					if (this.worldId == ClusterManager.Instance.activeWorldId)
					{
						this.hasValidCount = true;
						PinnedResourcesPanel.Instance.ClearExcessiveNewItems();
						PinnedResourcesPanel.Instance.Refresh();
					}
				}
				this.accessibleAmounts[key] = num2;
				this.accessibleUpdateIndex = (this.accessibleUpdateIndex + 1) % this.Inventory.Count;
				break;
			}
			num++;
		}
		this.firstUpdate = false;
	}

	// Token: 0x06004B51 RID: 19281 RVA: 0x001A5E4C File Offset: 0x001A404C
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
	}

	// Token: 0x06004B52 RID: 19282 RVA: 0x001A5E54 File Offset: 0x001A4054
	private void OnAddedFetchable(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject.GetComponent<Navigator>() != null)
		{
			return;
		}
		Pickupable component = gameObject.GetComponent<Pickupable>();
		if (component.GetMyWorldId() != this.worldId)
		{
			return;
		}
		KPrefabID component2 = component.GetComponent<KPrefabID>();
		Tag tag = component2.PrefabID();
		if (!this.Inventory.ContainsKey(tag))
		{
			Tag categoryForEntity = DiscoveredResources.GetCategoryForEntity(component2);
			DebugUtil.DevAssertArgs(categoryForEntity.IsValid, new object[] { component.name, "was found by worldinventory but doesn't have a category! Add it to the element definition." });
			DiscoveredResources.Instance.Discover(tag, categoryForEntity);
		}
		HashSet<Pickupable> hashSet;
		if (!this.Inventory.TryGetValue(tag, out hashSet))
		{
			hashSet = new HashSet<Pickupable>();
			this.Inventory[tag] = hashSet;
		}
		hashSet.Add(component);
		foreach (Tag tag2 in component2.Tags)
		{
			if (!this.Inventory.TryGetValue(tag2, out hashSet))
			{
				hashSet = new HashSet<Pickupable>();
				this.Inventory[tag2] = hashSet;
			}
			hashSet.Add(component);
		}
	}

	// Token: 0x06004B53 RID: 19283 RVA: 0x001A5F80 File Offset: 0x001A4180
	private void OnRemovedFetchable(object data)
	{
		Pickupable component = ((GameObject)data).GetComponent<Pickupable>();
		KPrefabID kprefabID = component.KPrefabID;
		HashSet<Pickupable> hashSet;
		if (this.Inventory.TryGetValue(kprefabID.PrefabTag, out hashSet))
		{
			hashSet.Remove(component);
		}
		foreach (Tag tag in kprefabID.Tags)
		{
			if (this.Inventory.TryGetValue(tag, out hashSet))
			{
				hashSet.Remove(component);
			}
		}
	}

	// Token: 0x06004B54 RID: 19284 RVA: 0x001A6018 File Offset: 0x001A4218
	public Dictionary<Tag, float> GetAccessibleAmounts()
	{
		return this.accessibleAmounts;
	}

	// Token: 0x04003151 RID: 12625
	private WorldContainer m_worldContainer;

	// Token: 0x04003152 RID: 12626
	[Serialize]
	public List<Tag> pinnedResources = new List<Tag>();

	// Token: 0x04003153 RID: 12627
	[Serialize]
	public List<Tag> notifyResources = new List<Tag>();

	// Token: 0x04003154 RID: 12628
	private Dictionary<Tag, HashSet<Pickupable>> Inventory = new Dictionary<Tag, HashSet<Pickupable>>();

	// Token: 0x04003155 RID: 12629
	private MinionGroupProber Prober;

	// Token: 0x04003156 RID: 12630
	private Dictionary<Tag, float> accessibleAmounts = new Dictionary<Tag, float>();

	// Token: 0x04003157 RID: 12631
	private bool hasValidCount;

	// Token: 0x04003158 RID: 12632
	private static readonly EventSystem.IntraObjectHandler<WorldInventory> OnNewDayDelegate = new EventSystem.IntraObjectHandler<WorldInventory>(delegate(WorldInventory component, object data)
	{
		component.GenerateInventoryReport(data);
	});

	// Token: 0x04003159 RID: 12633
	private int accessibleUpdateIndex;

	// Token: 0x0400315A RID: 12634
	private bool firstUpdate = true;
}
