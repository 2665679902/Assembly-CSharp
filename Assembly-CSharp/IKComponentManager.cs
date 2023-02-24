using System;
using UnityEngine;

// Token: 0x0200057E RID: 1406
public interface IKComponentManager
{
	// Token: 0x06002232 RID: 8754
	HandleVector<int>.Handle Add(GameObject go);

	// Token: 0x06002233 RID: 8755
	void Remove(GameObject go);
}
