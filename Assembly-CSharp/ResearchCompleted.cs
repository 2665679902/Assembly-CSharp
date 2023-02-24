using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200090E RID: 2318
public class ResearchCompleted : SelectModuleCondition
{
	// Token: 0x06004394 RID: 17300 RVA: 0x0017DB7C File Offset: 0x0017BD7C
	public override bool IgnoreInSanboxMode()
	{
		return true;
	}

	// Token: 0x06004395 RID: 17301 RVA: 0x0017DB80 File Offset: 0x0017BD80
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		if (existingModule == null)
		{
			return true;
		}
		TechItem techItem = Db.Get().TechItems.TryGet(selectedPart.PrefabID);
		return DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || techItem == null || techItem.IsComplete();
	}

	// Token: 0x06004396 RID: 17302 RVA: 0x0017DBCC File Offset: 0x0017BDCC
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.RESEARCHED.COMPLETE;
		}
		return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.RESEARCHED.FAILED;
	}
}
