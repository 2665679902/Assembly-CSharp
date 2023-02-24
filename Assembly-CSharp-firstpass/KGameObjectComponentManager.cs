using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public abstract class KGameObjectComponentManager<T> : KComponentManager<T> where T : new()
{
	// Token: 0x060006D0 RID: 1744 RVA: 0x0001DD0C File Offset: 0x0001BF0C
	public HandleVector<int>.Handle Add(GameObject go, T cmp)
	{
		return base.InternalAddComponent(go, cmp);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0001DD18 File Offset: 0x0001BF18
	public virtual void Remove(GameObject go)
	{
		HandleVector<int>.Handle handle = this.GetHandle(go);
		KComponentManager<T>.CleanupInfo cleanupInfo = new KComponentManager<T>.CleanupInfo(go, handle);
		if (!KComponentCleanUp.InCleanUpPhase)
		{
			base.AddToCleanupList(cleanupInfo);
			return;
		}
		base.RemoveFromCleanupList(go);
		this.OnCleanUp(handle);
		base.InternalRemoveComponent(cleanupInfo);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0001DD5A File Offset: 0x0001BF5A
	public HandleVector<int>.Handle GetHandle(GameObject obj)
	{
		return base.GetHandle(obj);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0001DD63 File Offset: 0x0001BF63
	public HandleVector<int>.Handle GetHandle(MonoBehaviour obj)
	{
		return base.GetHandle(obj.gameObject);
	}
}
