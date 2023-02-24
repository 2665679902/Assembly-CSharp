using System;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class SlimeMoldConfig : IOreConfig
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000D12 RID: 3346 RVA: 0x000489A4 File Offset: 0x00046BA4
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.SlimeMold;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000D13 RID: 3347 RVA: 0x000489AB File Offset: 0x00046BAB
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ContaminatedOxygen;
		}
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x000489B2 File Offset: 0x00046BB2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x000489BC File Offset: 0x00046BBC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.ContaminatedOxygenBubble;
		sublimates.info = new Sublimates.Info(0.025f, 0.125f, 1.8f, 0f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
