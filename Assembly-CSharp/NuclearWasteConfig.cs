using System;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class NuclearWasteConfig : IOreConfig
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000D09 RID: 3337 RVA: 0x000488C1 File Offset: 0x00046AC1
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.NuclearWaste;
		}
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x000488C8 File Offset: 0x00046AC8
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x000488D0 File Offset: 0x00046AD0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLiquidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.decayStorage = true;
		sublimates.spawnFXHash = SpawnFXHashes.NuclearWasteDrip;
		sublimates.info = new Sublimates.Info(0.066f, 6.6f, 1000f, 0f, this.ElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
