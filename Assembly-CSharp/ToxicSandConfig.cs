using System;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class ToxicSandConfig : IOreConfig
{
	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00048A18 File Offset: 0x00046C18
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.ToxicSand;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00048A1F File Offset: 0x00046C1F
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ContaminatedOxygen;
		}
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x00048A26 File Offset: 0x00046C26
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x00048A30 File Offset: 0x00046C30
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.ContaminatedOxygenBubble;
		sublimates.info = new Sublimates.Info(2.0000001E-05f, 0.05f, 1.8f, 0.5f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
