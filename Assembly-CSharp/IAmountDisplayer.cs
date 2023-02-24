using System;
using Klei.AI;

// Token: 0x02000A32 RID: 2610
public interface IAmountDisplayer
{
	// Token: 0x06004F58 RID: 20312
	string GetValueString(Amount master, AmountInstance instance);

	// Token: 0x06004F59 RID: 20313
	string GetDescription(Amount master, AmountInstance instance);

	// Token: 0x06004F5A RID: 20314
	string GetTooltip(Amount master, AmountInstance instance);

	// Token: 0x170005CA RID: 1482
	// (get) Token: 0x06004F5B RID: 20315
	IAttributeFormatter Formatter { get; }
}
