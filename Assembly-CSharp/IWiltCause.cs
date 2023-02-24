using System;

// Token: 0x020006F5 RID: 1781
public interface IWiltCause
{
	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06003081 RID: 12417
	string WiltStateString { get; }

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06003082 RID: 12418
	WiltCondition.Condition[] Conditions { get; }
}
