using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
public static class EventExtensions
{
	// Token: 0x060005E8 RID: 1512 RVA: 0x0001BB6E File Offset: 0x00019D6E
	public static int Subscribe<ComponentType>(this GameObject go, int hash, EventSystem.IntraObjectHandler<ComponentType> handler) where ComponentType : Component
	{
		return KObjectManager.Instance.GetOrCreateObject(go).GetEventSystem().Subscribe<ComponentType>(hash, handler);
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0001BB88 File Offset: 0x00019D88
	public static void Trigger(this GameObject go, int hash, object data = null)
	{
		KObject kobject = KObjectManager.Instance.Get(go);
		if (kobject != null && kobject.hasEventSystem)
		{
			kobject.GetEventSystem().Trigger(go, hash, data);
		}
	}
}
