using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public abstract class KComponentManager<T> : KCompactedVector<T>, IComponentManager where T : new()
{
	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001D0F8 File Offset: 0x0001B2F8
	// (set) Token: 0x06000693 RID: 1683 RVA: 0x0001D100 File Offset: 0x0001B300
	public string Name { get; set; }

	// Token: 0x06000694 RID: 1684 RVA: 0x0001D10C File Offset: 0x0001B30C
	public KComponentManager()
		: base(0)
	{
		this.Name = base.GetType().Name;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0001D173 File Offset: 0x0001B373
	protected void AddToCleanupList(KComponentManager<T>.CleanupInfo info)
	{
		this.cleanupMap.Add(info.instance);
		this.cleanupList.Add(info);
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0001D193 File Offset: 0x0001B393
	protected bool IsInCleanupList(GameObject go)
	{
		return this.cleanupMap.Contains(go);
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0001D1A1 File Offset: 0x0001B3A1
	public bool Has(object go)
	{
		return !this.cleanupMap.Contains(go) && !(this.GetHandle(go) == HandleVector<int>.InvalidHandle);
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0001D1CC File Offset: 0x0001B3CC
	protected HandleVector<int>.Handle InternalAddComponent(object instance, T cmp_values)
	{
		HandleVector<int>.Handle handle = HandleVector<int>.InvalidHandle;
		this.RemoveFromCleanupList(instance);
		if (!this.instanceHandleMap.TryGetValue(instance, out handle))
		{
			handle = base.Allocate(cmp_values);
			this.instanceHandleMap[instance] = handle;
		}
		else
		{
			base.SetData(handle, cmp_values);
		}
		this.spawnList.Remove(handle);
		this.OnPrefabInit(handle);
		this.spawnList.Add(handle);
		return handle;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0001D238 File Offset: 0x0001B438
	protected void InternalRemoveComponent(KComponentManager<T>.CleanupInfo info)
	{
		if (info.instance != null)
		{
			if (!this.instanceHandleMap.ContainsKey(info.instance))
			{
				DebugUtil.LogErrorArgs(new object[]
				{
					"Tried to remove component of type",
					typeof(T).ToString(),
					"on instance",
					info.instance.ToString(),
					"but instance has not been registered yet. Handle:",
					info.handle
				});
				return;
			}
			this.instanceHandleMap.Remove(info.instance);
		}
		else
		{
			foreach (KeyValuePair<object, HandleVector<int>.Handle> keyValuePair in this.instanceHandleMap)
			{
				if (keyValuePair.Value == info.handle)
				{
					this.instanceHandleMap.Remove(keyValuePair.Key);
				}
			}
		}
		base.Free(info.handle);
		this.spawnList.Remove(info.handle);
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0001D34C File Offset: 0x0001B54C
	public HandleVector<int>.Handle GetHandle(object instance)
	{
		HandleVector<int>.Handle invalidHandle;
		if (!this.instanceHandleMap.TryGetValue(instance, out invalidHandle))
		{
			invalidHandle = HandleVector<int>.InvalidHandle;
		}
		return invalidHandle;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0001D370 File Offset: 0x0001B570
	public void Spawn()
	{
		HashSet<HandleVector<int>.Handle> hashSet = this.spawnList;
		this.spawnList = this.shadowSpawnList;
		this.shadowSpawnList = hashSet;
		this.spawnList.Clear();
		foreach (KComponentManager<T>.CleanupInfo cleanupInfo in this.cleanupList)
		{
			HandleVector<int>.Handle handle = this.GetHandle(cleanupInfo);
			this.shadowSpawnList.Remove(handle);
		}
		foreach (HandleVector<int>.Handle handle2 in this.shadowSpawnList)
		{
			this.OnSpawn(handle2);
		}
		this.shadowSpawnList.Clear();
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0001D44C File Offset: 0x0001B64C
	public virtual void RenderEveryTick(float dt)
	{
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0001D44E File Offset: 0x0001B64E
	public virtual void FixedUpdate(float dt)
	{
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0001D450 File Offset: 0x0001B650
	public virtual void Sim200ms(float dt)
	{
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0001D454 File Offset: 0x0001B654
	public void CleanUp()
	{
		this.shadowCleanupList.AddRange(this.cleanupList);
		this.cleanupList.Clear();
		this.cleanupMap.Clear();
		foreach (KComponentManager<T>.CleanupInfo cleanupInfo in this.shadowCleanupList)
		{
			this.OnCleanUp(cleanupInfo.handle);
			this.InternalRemoveComponent(cleanupInfo);
		}
		this.shadowCleanupList.Clear();
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0001D4E8 File Offset: 0x0001B6E8
	protected void RemoveFromCleanupList(object instance)
	{
		for (int i = 0; i < this.cleanupList.Count; i++)
		{
			if (this.cleanupList[i].instance == instance)
			{
				this.cleanupMap.Remove(instance);
				this.cleanupList[i] = this.cleanupList[this.cleanupList.Count - 1];
				this.cleanupList.RemoveAt(this.cleanupList.Count - 1);
				return;
			}
		}
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0001D56C File Offset: 0x0001B76C
	public override void Clear()
	{
		base.Clear();
		this.spawnList.Clear();
		this.shadowSpawnList.Clear();
		this.cleanupList.Clear();
		this.cleanupMap.Clear();
		this.shadowCleanupList.Clear();
		this.instanceHandleMap.Clear();
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0001D5C1 File Offset: 0x0001B7C1
	protected virtual void OnPrefabInit(HandleVector<int>.Handle h)
	{
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0001D5C3 File Offset: 0x0001B7C3
	protected virtual void OnSpawn(HandleVector<int>.Handle h)
	{
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0001D5C5 File Offset: 0x0001B7C5
	protected virtual void OnCleanUp(HandleVector<int>.Handle h)
	{
	}

	// Token: 0x040005BD RID: 1469
	protected Dictionary<object, HandleVector<int>.Handle> instanceHandleMap = new Dictionary<object, HandleVector<int>.Handle>();

	// Token: 0x040005BE RID: 1470
	private HashSet<HandleVector<int>.Handle> spawnList = new HashSet<HandleVector<int>.Handle>();

	// Token: 0x040005BF RID: 1471
	private HashSet<HandleVector<int>.Handle> shadowSpawnList = new HashSet<HandleVector<int>.Handle>();

	// Token: 0x040005C1 RID: 1473
	private List<KComponentManager<T>.CleanupInfo> cleanupList = new List<KComponentManager<T>.CleanupInfo>();

	// Token: 0x040005C2 RID: 1474
	private HashSet<object> cleanupMap = new HashSet<object>();

	// Token: 0x040005C3 RID: 1475
	private List<KComponentManager<T>.CleanupInfo> shadowCleanupList = new List<KComponentManager<T>.CleanupInfo>();

	// Token: 0x020009E6 RID: 2534
	protected struct CleanupInfo
	{
		// Token: 0x060053C2 RID: 21442 RVA: 0x0009C55F File Offset: 0x0009A75F
		public CleanupInfo(object instance, HandleVector<int>.Handle handle)
		{
			this.instance = instance;
			this.handle = handle;
		}

		// Token: 0x0400222D RID: 8749
		public object instance;

		// Token: 0x0400222E RID: 8750
		public HandleVector<int>.Handle handle;
	}
}
