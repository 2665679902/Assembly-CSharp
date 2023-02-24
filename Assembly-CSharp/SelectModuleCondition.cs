using System;
using UnityEngine;

// Token: 0x0200090D RID: 2317
public abstract class SelectModuleCondition
{
	// Token: 0x06004390 RID: 17296
	public abstract bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext);

	// Token: 0x06004391 RID: 17297
	public abstract string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart);

	// Token: 0x06004392 RID: 17298 RVA: 0x0017DB71 File Offset: 0x0017BD71
	public virtual bool IgnoreInSanboxMode()
	{
		return false;
	}

	// Token: 0x020016EF RID: 5871
	public enum SelectionContext
	{
		// Token: 0x04006B6A RID: 27498
		AddModuleAbove,
		// Token: 0x04006B6B RID: 27499
		AddModuleBelow,
		// Token: 0x04006B6C RID: 27500
		ReplaceModule
	}
}
