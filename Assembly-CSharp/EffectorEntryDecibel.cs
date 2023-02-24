using System;

// Token: 0x02000733 RID: 1843
internal struct EffectorEntryDecibel
{
	// Token: 0x06003280 RID: 12928 RVA: 0x00110CC5 File Offset: 0x0010EEC5
	public EffectorEntryDecibel(string name, float value)
	{
		this.name = name;
		this.value = value;
		this.count = 1;
	}

	// Token: 0x04001EC4 RID: 7876
	public string name;

	// Token: 0x04001EC5 RID: 7877
	public int count;

	// Token: 0x04001EC6 RID: 7878
	public float value;
}
