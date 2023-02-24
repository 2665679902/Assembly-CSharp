using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000912 RID: 2322
public class EngineOnBottom : SelectModuleCondition
{
	// Token: 0x060043A2 RID: 17314 RVA: 0x0017DE70 File Offset: 0x0017C070
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		if (existingModule == null || existingModule.GetComponent<LaunchPad>() != null)
		{
			return true;
		}
		if (selectionContext == SelectModuleCondition.SelectionContext.ReplaceModule)
		{
			return existingModule.GetComponent<AttachableBuilding>().GetAttachedTo() == null;
		}
		return selectionContext == SelectModuleCondition.SelectionContext.AddModuleBelow && existingModule.GetComponent<AttachableBuilding>().GetAttachedTo() == null;
	}

	// Token: 0x060043A3 RID: 17315 RVA: 0x0017DECD File Offset: 0x0017C0CD
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.ENGINE_AT_BOTTOM.COMPLETE;
		}
		return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.ENGINE_AT_BOTTOM.FAILED;
	}
}
