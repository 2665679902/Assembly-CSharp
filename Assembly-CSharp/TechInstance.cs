using System;
using UnityEngine;

// Token: 0x020008DE RID: 2270
public class TechInstance
{
	// Token: 0x06004163 RID: 16739 RVA: 0x0016E920 File Offset: 0x0016CB20
	public TechInstance(Tech tech)
	{
		this.tech = tech;
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x0016E93A File Offset: 0x0016CB3A
	public bool IsComplete()
	{
		return this.complete;
	}

	// Token: 0x06004165 RID: 16741 RVA: 0x0016E942 File Offset: 0x0016CB42
	public void Purchased()
	{
		if (!this.complete)
		{
			this.complete = true;
		}
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x0016E953 File Offset: 0x0016CB53
	public float PercentageCompleteResearchType(string type)
	{
		if (!this.tech.RequiresResearchType(type))
		{
			return 1f;
		}
		return Mathf.Clamp01(this.progressInventory.PointsByTypeID[type] / this.tech.costsByResearchTypeID[type]);
	}

	// Token: 0x06004167 RID: 16743 RVA: 0x0016E994 File Offset: 0x0016CB94
	public TechInstance.SaveData Save()
	{
		string[] array = new string[this.progressInventory.PointsByTypeID.Count];
		this.progressInventory.PointsByTypeID.Keys.CopyTo(array, 0);
		float[] array2 = new float[this.progressInventory.PointsByTypeID.Count];
		this.progressInventory.PointsByTypeID.Values.CopyTo(array2, 0);
		return new TechInstance.SaveData
		{
			techId = this.tech.Id,
			complete = this.complete,
			inventoryIDs = array,
			inventoryValues = array2
		};
	}

	// Token: 0x06004168 RID: 16744 RVA: 0x0016EA34 File Offset: 0x0016CC34
	public void Load(TechInstance.SaveData save_data)
	{
		this.complete = save_data.complete;
		for (int i = 0; i < save_data.inventoryIDs.Length; i++)
		{
			this.progressInventory.AddResearchPoints(save_data.inventoryIDs[i], save_data.inventoryValues[i]);
		}
	}

	// Token: 0x04002BA0 RID: 11168
	public Tech tech;

	// Token: 0x04002BA1 RID: 11169
	private bool complete;

	// Token: 0x04002BA2 RID: 11170
	public ResearchPointInventory progressInventory = new ResearchPointInventory();

	// Token: 0x020016A1 RID: 5793
	public struct SaveData
	{
		// Token: 0x04006A6A RID: 27242
		public string techId;

		// Token: 0x04006A6B RID: 27243
		public bool complete;

		// Token: 0x04006A6C RID: 27244
		public string[] inventoryIDs;

		// Token: 0x04006A6D RID: 27245
		public float[] inventoryValues;
	}
}
