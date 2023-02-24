using System;

// Token: 0x020003B2 RID: 946
public class GameplayEventMinionFilter
{
	// Token: 0x04000AA1 RID: 2721
	public string id;

	// Token: 0x04000AA2 RID: 2722
	public GameplayEventMinionFilter.FilterFn filter;

	// Token: 0x02000FC0 RID: 4032
	// (Invoke) Token: 0x06007065 RID: 28773
	public delegate bool FilterFn(MinionIdentity minion);
}
