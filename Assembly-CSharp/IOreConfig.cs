using System;
using UnityEngine;

// Token: 0x0200028C RID: 652
public interface IOreConfig
{
	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000D04 RID: 3332
	SimHashes ElementID { get; }

	// Token: 0x06000D05 RID: 3333
	GameObject CreatePrefab();
}
