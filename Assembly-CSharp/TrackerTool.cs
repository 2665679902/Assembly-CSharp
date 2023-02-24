using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020004E0 RID: 1248
public class TrackerTool : KMonoBehaviour
{
	// Token: 0x06001D92 RID: 7570 RVA: 0x0009DB30 File Offset: 0x0009BD30
	protected override void OnSpawn()
	{
		TrackerTool.Instance = this;
		base.OnSpawn();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			this.AddNewWorldTrackers(worldContainer.id);
		}
		foreach (object obj in Components.LiveMinionIdentities)
		{
			this.AddMinionTrackers((MinionIdentity)obj);
		}
		Components.LiveMinionIdentities.OnAdd += this.AddMinionTrackers;
		ClusterManager.Instance.Subscribe(-1280433810, new Action<object>(this.Refresh));
		ClusterManager.Instance.Subscribe(-1078710002, new Action<object>(this.RemoveWorld));
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x0009DC2C File Offset: 0x0009BE2C
	protected override void OnForcedCleanUp()
	{
		TrackerTool.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x0009DC3C File Offset: 0x0009BE3C
	private void AddMinionTrackers(MinionIdentity identity)
	{
		this.minionTrackers.Add(identity, new List<MinionTracker>());
		identity.Subscribe(1969584890, delegate(object data)
		{
			this.minionTrackers.Remove(identity);
		});
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x0009DC90 File Offset: 0x0009BE90
	private void Refresh(object data)
	{
		int num = (int)data;
		this.AddNewWorldTrackers(num);
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x0009DCAC File Offset: 0x0009BEAC
	private void RemoveWorld(object data)
	{
		int world_id = (int)data;
		this.worldTrackers.RemoveAll((WorldTracker match) => match.WorldID == world_id);
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x0009DCE3 File Offset: 0x0009BEE3
	public bool IsRocketInterior(int worldID)
	{
		return ClusterManager.Instance.GetWorld(worldID).IsModuleInterior;
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x0009DCF8 File Offset: 0x0009BEF8
	private void AddNewWorldTrackers(int worldID)
	{
		this.worldTrackers.Add(new StressTracker(worldID));
		this.worldTrackers.Add(new KCalTracker(worldID));
		this.worldTrackers.Add(new IdleTracker(worldID));
		this.worldTrackers.Add(new BreathabilityTracker(worldID));
		this.worldTrackers.Add(new PowerUseTracker(worldID));
		this.worldTrackers.Add(new BatteryTracker(worldID));
		this.worldTrackers.Add(new CropTracker(worldID));
		this.worldTrackers.Add(new WorkingToiletTracker(worldID));
		this.worldTrackers.Add(new RadiationTracker(worldID));
		if (ClusterManager.Instance.GetWorld(worldID).IsModuleInterior)
		{
			this.worldTrackers.Add(new RocketFuelTracker(worldID));
			this.worldTrackers.Add(new RocketOxidizerTracker(worldID));
		}
		for (int i = 0; i < Db.Get().ChoreGroups.Count; i++)
		{
			this.worldTrackers.Add(new WorkTimeTracker(worldID, Db.Get().ChoreGroups[i]));
			this.worldTrackers.Add(new ChoreCountTracker(worldID, Db.Get().ChoreGroups[i]));
		}
		this.worldTrackers.Add(new AllChoresCountTracker(worldID));
		this.worldTrackers.Add(new AllWorkTimeTracker(worldID));
		foreach (Tag tag in GameTags.CalorieCategories)
		{
			this.worldTrackers.Add(new ResourceTracker(worldID, tag));
			foreach (GameObject gameObject in Assets.GetPrefabsWithTag(tag))
			{
				this.AddResourceTracker(worldID, gameObject.GetComponent<KPrefabID>().PrefabTag);
			}
		}
		foreach (Tag tag2 in GameTags.UnitCategories)
		{
			this.worldTrackers.Add(new ResourceTracker(worldID, tag2));
			foreach (GameObject gameObject2 in Assets.GetPrefabsWithTag(tag2))
			{
				this.AddResourceTracker(worldID, gameObject2.GetComponent<KPrefabID>().PrefabTag);
			}
		}
		foreach (Tag tag3 in GameTags.MaterialCategories)
		{
			this.worldTrackers.Add(new ResourceTracker(worldID, tag3));
			foreach (GameObject gameObject3 in Assets.GetPrefabsWithTag(tag3))
			{
				this.AddResourceTracker(worldID, gameObject3.GetComponent<KPrefabID>().PrefabTag);
			}
		}
		foreach (Tag tag4 in GameTags.OtherEntityTags)
		{
			this.worldTrackers.Add(new ResourceTracker(worldID, tag4));
			foreach (GameObject gameObject4 in Assets.GetPrefabsWithTag(tag4))
			{
				this.AddResourceTracker(worldID, gameObject4.GetComponent<KPrefabID>().PrefabTag);
			}
		}
		foreach (GameObject gameObject5 in Assets.GetPrefabsWithTag(GameTags.CookingIngredient))
		{
			this.AddResourceTracker(worldID, gameObject5.GetComponent<KPrefabID>().PrefabTag);
		}
		foreach (EdiblesManager.FoodInfo foodInfo in EdiblesManager.GetAllFoodTypes())
		{
			this.AddResourceTracker(worldID, foodInfo.Id);
		}
		foreach (Element element in ElementLoader.elements)
		{
			this.AddResourceTracker(worldID, element.tag);
		}
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x0009E1A0 File Offset: 0x0009C3A0
	private void AddResourceTracker(int worldID, Tag tag)
	{
		if (this.worldTrackers.Find((WorldTracker match) => match is ResourceTracker && ((ResourceTracker)match).WorldID == worldID && ((ResourceTracker)match).tag == tag) != null)
		{
			return;
		}
		this.worldTrackers.Add(new ResourceTracker(worldID, tag));
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x0009E1F8 File Offset: 0x0009C3F8
	public ResourceTracker GetResourceStatistic(int worldID, Tag tag)
	{
		return (ResourceTracker)this.worldTrackers.Find((WorldTracker match) => match is ResourceTracker && ((ResourceTracker)match).WorldID == worldID && ((ResourceTracker)match).tag == tag);
	}

	// Token: 0x06001D9B RID: 7579 RVA: 0x0009E238 File Offset: 0x0009C438
	public WorldTracker GetWorldTracker<T>(int worldID) where T : WorldTracker
	{
		return (T)((object)this.worldTrackers.Find((WorldTracker match) => match is T && ((T)((object)match)).WorldID == worldID));
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x0009E274 File Offset: 0x0009C474
	public ChoreCountTracker GetChoreGroupTracker(int worldID, ChoreGroup choreGroup)
	{
		return (ChoreCountTracker)this.worldTrackers.Find((WorldTracker match) => match is ChoreCountTracker && ((ChoreCountTracker)match).WorldID == worldID && ((ChoreCountTracker)match).choreGroup == choreGroup);
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x0009E2B4 File Offset: 0x0009C4B4
	public WorkTimeTracker GetWorkTimeTracker(int worldID, ChoreGroup choreGroup)
	{
		return (WorkTimeTracker)this.worldTrackers.Find((WorldTracker match) => match is WorkTimeTracker && ((WorkTimeTracker)match).WorldID == worldID && ((WorkTimeTracker)match).choreGroup == choreGroup);
	}

	// Token: 0x06001D9E RID: 7582 RVA: 0x0009E2F1 File Offset: 0x0009C4F1
	public MinionTracker GetMinionTracker<T>(MinionIdentity identity) where T : MinionTracker
	{
		return (T)((object)this.minionTrackers[identity].Find((MinionTracker match) => match is T));
	}

	// Token: 0x06001D9F RID: 7583 RVA: 0x0009E330 File Offset: 0x0009C530
	public void Update()
	{
		if (SpeedControlScreen.Instance.IsPaused)
		{
			return;
		}
		if (!this.trackerActive)
		{
			return;
		}
		if (this.minionTrackers.Count > 0)
		{
			this.updatingMinionTracker++;
			if (this.updatingMinionTracker >= this.minionTrackers.Count)
			{
				this.updatingMinionTracker = 0;
			}
			KeyValuePair<MinionIdentity, List<MinionTracker>> keyValuePair = this.minionTrackers.ElementAt(this.updatingMinionTracker);
			for (int i = 0; i < keyValuePair.Value.Count; i++)
			{
				keyValuePair.Value[i].UpdateData();
			}
		}
		if (this.worldTrackers.Count > 0)
		{
			for (int j = 0; j < this.numUpdatesPerFrame; j++)
			{
				this.updatingWorldTracker++;
				if (this.updatingWorldTracker >= this.worldTrackers.Count)
				{
					this.updatingWorldTracker = 0;
				}
				this.worldTrackers[this.updatingWorldTracker].UpdateData();
			}
		}
	}

	// Token: 0x040010AD RID: 4269
	public static TrackerTool Instance;

	// Token: 0x040010AE RID: 4270
	private List<WorldTracker> worldTrackers = new List<WorldTracker>();

	// Token: 0x040010AF RID: 4271
	private Dictionary<MinionIdentity, List<MinionTracker>> minionTrackers = new Dictionary<MinionIdentity, List<MinionTracker>>();

	// Token: 0x040010B0 RID: 4272
	private int updatingWorldTracker;

	// Token: 0x040010B1 RID: 4273
	private int updatingMinionTracker;

	// Token: 0x040010B2 RID: 4274
	public bool trackerActive = true;

	// Token: 0x040010B3 RID: 4275
	private int numUpdatesPerFrame = 50;
}
