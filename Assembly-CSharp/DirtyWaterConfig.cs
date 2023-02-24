using System;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class DirtyWaterConfig : IOreConfig
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000CFF RID: 3327 RVA: 0x000485E0 File Offset: 0x000467E0
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.DirtyWater;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000D00 RID: 3328 RVA: 0x000485E7 File Offset: 0x000467E7
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ContaminatedOxygen;
		}
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x000485EE File Offset: 0x000467EE
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x000485F8 File Offset: 0x000467F8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLiquidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.ContaminatedOxygenBubbleWater;
		sublimates.info = new Sublimates.Info(4.0000006E-05f, 0.025f, 1.8f, 1f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
