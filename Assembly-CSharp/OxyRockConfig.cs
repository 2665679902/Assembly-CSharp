using System;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class OxyRockConfig : IOreConfig
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00048933 File Offset: 0x00046B33
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.OxyRock;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0004893A File Offset: 0x00046B3A
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.Oxygen;
		}
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x00048941 File Offset: 0x00046B41
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x00048948 File Offset: 0x00046B48
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.OxygenEmissionBubbles;
		sublimates.info = new Sublimates.Info(0.010000001f, 0.0050000004f, 1.8f, 0.7f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
