using System;

// Token: 0x02000AA7 RID: 2727
[Serializable]
public struct GraphAxis
{
	// Token: 0x17000631 RID: 1585
	// (get) Token: 0x060053A7 RID: 21415 RVA: 0x001E5F04 File Offset: 0x001E4104
	public float range
	{
		get
		{
			return this.max_value - this.min_value;
		}
	}

	// Token: 0x040038C6 RID: 14534
	public string name;

	// Token: 0x040038C7 RID: 14535
	public float min_value;

	// Token: 0x040038C8 RID: 14536
	public float max_value;

	// Token: 0x040038C9 RID: 14537
	public float guide_frequency;
}
