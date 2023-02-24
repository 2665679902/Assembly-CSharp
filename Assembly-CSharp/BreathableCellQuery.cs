using System;

// Token: 0x020003CC RID: 972
public class BreathableCellQuery : PathFinderQuery
{
	// Token: 0x0600142F RID: 5167 RVA: 0x0006ADA1 File Offset: 0x00068FA1
	public BreathableCellQuery Reset(Brain brain)
	{
		this.breather = brain.GetComponent<OxygenBreather>();
		return this;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x0006ADB0 File Offset: 0x00068FB0
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		return this.breather.IsBreathableElementAtCell(cell, null);
	}

	// Token: 0x04000B35 RID: 2869
	private OxygenBreather breather;
}
