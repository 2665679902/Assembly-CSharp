using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public abstract class KGameObjectSplitComponentManager<Header, Payload> : KSplitComponentManager<Header, Payload> where Header : new() where Payload : new()
{
	// Token: 0x060006D5 RID: 1749 RVA: 0x0001DD79 File Offset: 0x0001BF79
	public HandleVector<int>.Handle Add(GameObject go, Header header, ref Payload payload)
	{
		return base.InternalAddComponent(go, header, ref payload);
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0001DD84 File Offset: 0x0001BF84
	public virtual void Remove(GameObject go)
	{
		HandleVector<int>.Handle handle = this.GetHandle(go);
		KSplitComponentManager<Header, Payload>.CleanupInfo cleanupInfo = new KSplitComponentManager<Header, Payload>.CleanupInfo(go, handle);
		if (!KComponentCleanUp.InCleanUpPhase)
		{
			this.cleanupList.Add(cleanupInfo);
			return;
		}
		base.RemoveFromCleanupList(go);
		this.OnCleanUp(handle);
		base.InternalRemoveComponent(cleanupInfo);
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0001DDCB File Offset: 0x0001BFCB
	public HandleVector<int>.Handle GetHandle(GameObject obj)
	{
		return base.GetHandle(obj);
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0001DDD4 File Offset: 0x0001BFD4
	public HandleVector<int>.Handle GetHandle(MonoBehaviour obj)
	{
		return base.GetHandle(obj.gameObject);
	}
}
