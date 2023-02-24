using System;

// Token: 0x020006AB RID: 1707
internal abstract class DivisibleTask<SharedData> : IWorkItem<SharedData>
{
	// Token: 0x06002E58 RID: 11864 RVA: 0x000F4694 File Offset: 0x000F2894
	public void Run(SharedData sharedData)
	{
		this.RunDivision(sharedData);
	}

	// Token: 0x06002E59 RID: 11865 RVA: 0x000F469D File Offset: 0x000F289D
	protected DivisibleTask(string name)
	{
		this.name = name;
	}

	// Token: 0x06002E5A RID: 11866
	protected abstract void RunDivision(SharedData sharedData);

	// Token: 0x04001BFD RID: 7165
	public string name;

	// Token: 0x04001BFE RID: 7166
	public int start;

	// Token: 0x04001BFF RID: 7167
	public int end;
}
