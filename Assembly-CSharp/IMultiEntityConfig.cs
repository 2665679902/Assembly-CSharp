using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000747 RID: 1863
public interface IMultiEntityConfig
{
	// Token: 0x0600335C RID: 13148
	List<GameObject> CreatePrefabs();

	// Token: 0x0600335D RID: 13149
	void OnPrefabInit(GameObject inst);

	// Token: 0x0600335E RID: 13150
	void OnSpawn(GameObject inst);
}
