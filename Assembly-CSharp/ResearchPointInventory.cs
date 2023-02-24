using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020008D9 RID: 2265
public class ResearchPointInventory
{
	// Token: 0x06004141 RID: 16705 RVA: 0x0016DF1C File Offset: 0x0016C11C
	public ResearchPointInventory()
	{
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			this.PointsByTypeID.Add(researchType.id, 0f);
		}
	}

	// Token: 0x06004142 RID: 16706 RVA: 0x0016DF98 File Offset: 0x0016C198
	public void AddResearchPoints(string researchTypeID, float points)
	{
		if (!this.PointsByTypeID.ContainsKey(researchTypeID))
		{
			Debug.LogWarning("Research inventory is missing research point key " + researchTypeID);
			return;
		}
		Dictionary<string, float> pointsByTypeID = this.PointsByTypeID;
		pointsByTypeID[researchTypeID] += points;
	}

	// Token: 0x06004143 RID: 16707 RVA: 0x0016DFDD File Offset: 0x0016C1DD
	public void RemoveResearchPoints(string researchTypeID, float points)
	{
		this.AddResearchPoints(researchTypeID, -points);
	}

	// Token: 0x06004144 RID: 16708 RVA: 0x0016DFE8 File Offset: 0x0016C1E8
	[OnDeserialized]
	private void OnDeserialized()
	{
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			if (!this.PointsByTypeID.ContainsKey(researchType.id))
			{
				this.PointsByTypeID.Add(researchType.id, 0f);
			}
		}
	}

	// Token: 0x04002B8D RID: 11149
	public Dictionary<string, float> PointsByTypeID = new Dictionary<string, float>();
}
