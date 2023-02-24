using System;
using UnityEngine;

// Token: 0x02000749 RID: 1865
[AddComponentMenu("KMonoBehaviour/scripts/EntityPrefabs")]
public class EntityPrefabs : KMonoBehaviour
{
	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x06003366 RID: 13158 RVA: 0x00114C84 File Offset: 0x00112E84
	// (set) Token: 0x06003367 RID: 13159 RVA: 0x00114C8B File Offset: 0x00112E8B
	public static EntityPrefabs Instance { get; private set; }

	// Token: 0x06003368 RID: 13160 RVA: 0x00114C93 File Offset: 0x00112E93
	public static void DestroyInstance()
	{
		EntityPrefabs.Instance = null;
	}

	// Token: 0x06003369 RID: 13161 RVA: 0x00114C9B File Offset: 0x00112E9B
	protected override void OnPrefabInit()
	{
		EntityPrefabs.Instance = this;
	}

	// Token: 0x04001F88 RID: 8072
	public GameObject SelectMarker;

	// Token: 0x04001F89 RID: 8073
	public GameObject ForegroundLayer;
}
