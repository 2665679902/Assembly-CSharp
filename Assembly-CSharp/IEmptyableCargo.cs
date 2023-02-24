using System;

// Token: 0x02000BC6 RID: 3014
public interface IEmptyableCargo
{
	// Token: 0x06005EBD RID: 24253
	bool CanEmptyCargo();

	// Token: 0x06005EBE RID: 24254
	void EmptyCargo();

	// Token: 0x17000697 RID: 1687
	// (get) Token: 0x06005EBF RID: 24255
	IStateMachineTarget master { get; }

	// Token: 0x17000698 RID: 1688
	// (get) Token: 0x06005EC0 RID: 24256
	bool CanAutoDeploy { get; }

	// Token: 0x17000699 RID: 1689
	// (get) Token: 0x06005EC1 RID: 24257
	// (set) Token: 0x06005EC2 RID: 24258
	bool AutoDeploy { get; set; }

	// Token: 0x1700069A RID: 1690
	// (get) Token: 0x06005EC3 RID: 24259
	bool ChooseDuplicant { get; }

	// Token: 0x1700069B RID: 1691
	// (get) Token: 0x06005EC4 RID: 24260
	bool ModuleDeployed { get; }

	// Token: 0x1700069C RID: 1692
	// (get) Token: 0x06005EC5 RID: 24261
	// (set) Token: 0x06005EC6 RID: 24262
	MinionIdentity ChosenDuplicant { get; set; }
}
