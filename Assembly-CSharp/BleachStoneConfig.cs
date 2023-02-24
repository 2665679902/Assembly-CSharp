using System;
using UnityEngine;

// Token: 0x0200028A RID: 650
public class BleachStoneConfig : IOreConfig
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0004856E File Offset: 0x0004676E
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.BleachStone;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00048575 File Offset: 0x00046775
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ChlorineGas;
		}
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0004857C File Offset: 0x0004677C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x00048584 File Offset: 0x00046784
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.BleachStoneEmissionBubbles;
		sublimates.info = new Sublimates.Info(0.00020000001f, 0.0025000002f, 1.8f, 0.5f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
