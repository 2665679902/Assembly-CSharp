using System;

// Token: 0x02000842 RID: 2114
[Serializable]
public struct SicknessExposureInfo
{
	// Token: 0x06003CF9 RID: 15609 RVA: 0x0015479B File Offset: 0x0015299B
	public SicknessExposureInfo(string id, string infection_source_info)
	{
		this.sicknessID = id;
		this.sourceInfo = infection_source_info;
	}

	// Token: 0x040027DA RID: 10202
	public string sicknessID;

	// Token: 0x040027DB RID: 10203
	public string sourceInfo;
}
