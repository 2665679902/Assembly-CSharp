using System;
using Database;

// Token: 0x020009E8 RID: 2536
public class EntityModifierSet : ModifierSet
{
	// Token: 0x06004BD5 RID: 19413 RVA: 0x001A9790 File Offset: 0x001A7990
	public override void Initialize()
	{
		base.Initialize();
		this.DuplicantStatusItems = new DuplicantStatusItems(this.Root);
		this.ChoreGroups = new ChoreGroups(this.Root);
		base.LoadTraits();
	}

	// Token: 0x040031E4 RID: 12772
	public DuplicantStatusItems DuplicantStatusItems;

	// Token: 0x040031E5 RID: 12773
	public ChoreGroups ChoreGroups;
}
