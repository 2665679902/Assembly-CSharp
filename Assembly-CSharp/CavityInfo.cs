using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F7 RID: 2295
public class CavityInfo
{
	// Token: 0x06004252 RID: 16978 RVA: 0x001760E0 File Offset: 0x001742E0
	public CavityInfo()
	{
		this.handle = HandleVector<int>.InvalidHandle;
		this.dirty = true;
	}

	// Token: 0x06004253 RID: 16979 RVA: 0x00176131 File Offset: 0x00174331
	public void AddBuilding(KPrefabID bc)
	{
		this.buildings.Add(bc);
		this.dirty = true;
	}

	// Token: 0x06004254 RID: 16980 RVA: 0x00176146 File Offset: 0x00174346
	public void AddPlants(KPrefabID plant)
	{
		this.plants.Add(plant);
		this.dirty = true;
	}

	// Token: 0x06004255 RID: 16981 RVA: 0x0017615C File Offset: 0x0017435C
	public void RemoveFromCavity(KPrefabID id, List<KPrefabID> listToRemove)
	{
		int num = -1;
		for (int i = 0; i < listToRemove.Count; i++)
		{
			if (id.InstanceID == listToRemove[i].InstanceID)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			listToRemove.RemoveAt(num);
		}
	}

	// Token: 0x06004256 RID: 16982 RVA: 0x001761A0 File Offset: 0x001743A0
	public void OnEnter(object data)
	{
		foreach (KPrefabID kprefabID in this.buildings)
		{
			if (kprefabID != null)
			{
				kprefabID.Trigger(-832141045, data);
			}
		}
	}

	// Token: 0x06004257 RID: 16983 RVA: 0x00176204 File Offset: 0x00174404
	public Vector3 GetCenter()
	{
		return new Vector3((float)(this.minX + (this.maxX - this.minX) / 2), (float)(this.minY + (this.maxY - this.minY) / 2));
	}

	// Token: 0x04002C49 RID: 11337
	public HandleVector<int>.Handle handle;

	// Token: 0x04002C4A RID: 11338
	public bool dirty;

	// Token: 0x04002C4B RID: 11339
	public int numCells;

	// Token: 0x04002C4C RID: 11340
	public int maxX;

	// Token: 0x04002C4D RID: 11341
	public int maxY;

	// Token: 0x04002C4E RID: 11342
	public int minX;

	// Token: 0x04002C4F RID: 11343
	public int minY;

	// Token: 0x04002C50 RID: 11344
	public Room room;

	// Token: 0x04002C51 RID: 11345
	public List<KPrefabID> buildings = new List<KPrefabID>();

	// Token: 0x04002C52 RID: 11346
	public List<KPrefabID> plants = new List<KPrefabID>();

	// Token: 0x04002C53 RID: 11347
	public List<KPrefabID> creatures = new List<KPrefabID>();

	// Token: 0x04002C54 RID: 11348
	public List<KPrefabID> eggs = new List<KPrefabID>();
}
