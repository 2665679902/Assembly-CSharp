using System;
using UnityEngine;

// Token: 0x02000746 RID: 1862
public interface IEntityConfig
{
	// Token: 0x06003358 RID: 13144
	GameObject CreatePrefab();

	// Token: 0x06003359 RID: 13145
	void OnPrefabInit(GameObject inst);

	// Token: 0x0600335A RID: 13146
	void OnSpawn(GameObject inst);

	// Token: 0x0600335B RID: 13147
	string[] GetDlcIds();
}
