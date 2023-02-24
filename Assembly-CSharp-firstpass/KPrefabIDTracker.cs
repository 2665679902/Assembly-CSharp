using System;
using System.Collections.Generic;

// Token: 0x020000C3 RID: 195
public class KPrefabIDTracker
{
	// Token: 0x06000771 RID: 1905 RVA: 0x0001F4B3 File Offset: 0x0001D6B3
	public static void DestroyInstance()
	{
		KPrefabIDTracker.Instance = null;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0001F4BB File Offset: 0x0001D6BB
	public static KPrefabIDTracker Get()
	{
		if (KPrefabIDTracker.Instance == null)
		{
			KPrefabIDTracker.Instance = new KPrefabIDTracker();
		}
		return KPrefabIDTracker.Instance;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
	public void Register(KPrefabID instance)
	{
		if (instance.InstanceID != -1)
		{
			if (this.prefabIdMap.ContainsKey(instance.InstanceID))
			{
				Debug.LogWarningFormat(instance.gameObject, "KPID instance id {0} was previously used by {1} but we're trying to add it from {2}. Conflict!", new object[]
				{
					instance.InstanceID,
					this.prefabIdMap[instance.InstanceID].gameObject,
					instance.name
				});
			}
			this.prefabIdMap[instance.InstanceID] = instance;
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0001F555 File Offset: 0x0001D755
	public void Unregister(KPrefabID instance)
	{
		this.prefabIdMap.Remove(instance.InstanceID);
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0001F56C File Offset: 0x0001D76C
	public KPrefabID GetInstance(int instance_id)
	{
		KPrefabID kprefabID = null;
		this.prefabIdMap.TryGetValue(instance_id, out kprefabID);
		return kprefabID;
	}

	// Token: 0x040005FB RID: 1531
	private static KPrefabIDTracker Instance;

	// Token: 0x040005FC RID: 1532
	private Dictionary<int, KPrefabID> prefabIdMap = new Dictionary<int, KPrefabID>();
}
