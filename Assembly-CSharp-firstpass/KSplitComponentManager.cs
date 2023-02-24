using System;
using System.Collections.Generic;

// Token: 0x020000B2 RID: 178
public abstract class KSplitComponentManager<Header, Payload> : KSplitCompactedVector<Header, Payload>, IComponentManager where Header : new() where Payload : new()
{
	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001D5C7 File Offset: 0x0001B7C7
	// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001D5CF File Offset: 0x0001B7CF
	public string Name { get; set; }

	// Token: 0x060006A7 RID: 1703 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
	public KSplitComponentManager()
		: base(0)
	{
		this.Name = base.GetType().Name;
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0001D634 File Offset: 0x0001B834
	public bool Has(object go)
	{
		return !this.cleanupList.Exists((KSplitComponentManager<Header, Payload>.CleanupInfo x) => x.instance == go) && !(this.GetHandle(go) == HandleVector<int>.InvalidHandle);
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0001D684 File Offset: 0x0001B884
	protected HandleVector<int>.Handle InternalAddComponent(object instance, Header header, ref Payload payload)
	{
		HandleVector<int>.Handle handle = HandleVector<int>.InvalidHandle;
		this.RemoveFromCleanupList(instance);
		if (!this.instanceHandleMap.TryGetValue(instance, out handle))
		{
			handle = base.Allocate(header, ref payload);
			this.instanceHandleMap[instance] = handle;
		}
		else
		{
			base.SetData(handle, header, ref payload);
		}
		this.spawnList.Remove(handle);
		this.OnPrefabInit(handle);
		this.spawnList.Add(handle);
		return handle;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
	protected void InternalRemoveComponent(KSplitComponentManager<Header, Payload>.CleanupInfo info)
	{
		if (info.instance != null)
		{
			if (!this.instanceHandleMap.ContainsKey(info.instance))
			{
				DebugUtil.LogErrorArgs(new object[]
				{
					"Tried to remove component of type",
					typeof(Header).ToString(),
					typeof(Payload).ToString(),
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

	// Token: 0x060006AB RID: 1707 RVA: 0x0001D81C File Offset: 0x0001BA1C
	public HandleVector<int>.Handle GetHandle(object instance)
	{
		HandleVector<int>.Handle invalidHandle;
		if (!this.instanceHandleMap.TryGetValue(instance, out invalidHandle))
		{
			invalidHandle = HandleVector<int>.InvalidHandle;
		}
		return invalidHandle;
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0001D840 File Offset: 0x0001BA40
	public void Spawn()
	{
		HashSet<HandleVector<int>.Handle> hashSet = this.spawnList;
		this.spawnList = this.shadowSpawnList;
		this.shadowSpawnList = hashSet;
		this.spawnList.Clear();
		foreach (KSplitComponentManager<Header, Payload>.CleanupInfo cleanupInfo in this.cleanupList)
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

	// Token: 0x060006AD RID: 1709 RVA: 0x0001D91C File Offset: 0x0001BB1C
	public virtual void RenderEveryTick(float dt)
	{
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0001D91E File Offset: 0x0001BB1E
	public virtual void FixedUpdate(float dt)
	{
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0001D920 File Offset: 0x0001BB20
	public virtual void Sim200ms(float dt)
	{
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0001D924 File Offset: 0x0001BB24
	public void CleanUp()
	{
		this.shadowCleanupList.AddRange(this.cleanupList);
		this.cleanupList.Clear();
		foreach (KSplitComponentManager<Header, Payload>.CleanupInfo cleanupInfo in this.shadowCleanupList)
		{
			this.OnCleanUp(cleanupInfo.handle);
			this.InternalRemoveComponent(cleanupInfo);
		}
		this.shadowCleanupList.Clear();
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0001D9AC File Offset: 0x0001BBAC
	protected void RemoveFromCleanupList(object instance)
	{
		for (int i = 0; i < this.cleanupList.Count; i++)
		{
			if (this.cleanupList[i].instance == instance)
			{
				this.cleanupList[i] = this.cleanupList[this.cleanupList.Count - 1];
				this.cleanupList.RemoveAt(this.cleanupList.Count - 1);
				return;
			}
		}
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0001DA20 File Offset: 0x0001BC20
	public override void Clear()
	{
		base.Clear();
		this.spawnList.Clear();
		this.shadowSpawnList.Clear();
		this.cleanupList.Clear();
		this.shadowCleanupList.Clear();
		this.instanceHandleMap.Clear();
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0001DA5F File Offset: 0x0001BC5F
	protected virtual void OnPrefabInit(HandleVector<int>.Handle h)
	{
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0001DA61 File Offset: 0x0001BC61
	protected virtual void OnSpawn(HandleVector<int>.Handle h)
	{
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0001DA63 File Offset: 0x0001BC63
	protected virtual void OnCleanUp(HandleVector<int>.Handle h)
	{
	}

	// Token: 0x040005C4 RID: 1476
	protected Dictionary<object, HandleVector<int>.Handle> instanceHandleMap = new Dictionary<object, HandleVector<int>.Handle>();

	// Token: 0x040005C5 RID: 1477
	private HashSet<HandleVector<int>.Handle> spawnList = new HashSet<HandleVector<int>.Handle>();

	// Token: 0x040005C6 RID: 1478
	private HashSet<HandleVector<int>.Handle> shadowSpawnList = new HashSet<HandleVector<int>.Handle>();

	// Token: 0x040005C8 RID: 1480
	protected List<KSplitComponentManager<Header, Payload>.CleanupInfo> cleanupList = new List<KSplitComponentManager<Header, Payload>.CleanupInfo>();

	// Token: 0x040005C9 RID: 1481
	private List<KSplitComponentManager<Header, Payload>.CleanupInfo> shadowCleanupList = new List<KSplitComponentManager<Header, Payload>.CleanupInfo>();

	// Token: 0x020009E7 RID: 2535
	protected struct CleanupInfo
	{
		// Token: 0x060053C3 RID: 21443 RVA: 0x0009C56F File Offset: 0x0009A76F
		public CleanupInfo(object instance, HandleVector<int>.Handle handle)
		{
			this.instance = instance;
			this.handle = handle;
		}

		// Token: 0x0400222F RID: 8751
		public object instance;

		// Token: 0x04002230 RID: 8752
		public HandleVector<int>.Handle handle;
	}
}
