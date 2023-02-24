using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000915 RID: 2325
public class RocketHeightLimit : SelectModuleCondition
{
	// Token: 0x060043AB RID: 17323 RVA: 0x0017E19C File Offset: 0x0017C39C
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		int num = selectedPart.HeightInCells;
		if (selectionContext == SelectModuleCondition.SelectionContext.ReplaceModule)
		{
			num -= existingModule.GetComponent<Building>().Def.HeightInCells;
		}
		if (existingModule == null)
		{
			return true;
		}
		RocketModuleCluster component = existingModule.GetComponent<RocketModuleCluster>();
		if (component == null)
		{
			return true;
		}
		int num2 = component.CraftInterface.MaxHeight;
		if (num2 <= 0)
		{
			num2 = ROCKETRY.ROCKET_HEIGHT.MAX_MODULE_STACK_HEIGHT;
		}
		RocketEngineCluster component2 = existingModule.GetComponent<RocketEngineCluster>();
		RocketEngineCluster component3 = selectedPart.BuildingComplete.GetComponent<RocketEngineCluster>();
		if (selectionContext == SelectModuleCondition.SelectionContext.ReplaceModule && component2 != null)
		{
			if (component3 != null)
			{
				num2 = component3.maxHeight;
			}
			else
			{
				num2 = ROCKETRY.ROCKET_HEIGHT.MAX_MODULE_STACK_HEIGHT;
			}
		}
		if (component3 != null && selectionContext == SelectModuleCondition.SelectionContext.AddModuleBelow)
		{
			num2 = component3.maxHeight;
		}
		return num2 == -1 || component.CraftInterface.RocketHeight + num <= num2;
	}

	// Token: 0x060043AC RID: 17324 RVA: 0x0017E264 File Offset: 0x0017C464
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		UnityEngine.Object engine = moduleBase.GetComponent<RocketModuleCluster>().CraftInterface.GetEngine();
		RocketEngineCluster component = selectedPart.BuildingComplete.GetComponent<RocketEngineCluster>();
		bool flag = engine != null || component != null;
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.MAX_HEIGHT.COMPLETE;
		}
		if (flag)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.MAX_HEIGHT.FAILED;
		}
		return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.MAX_HEIGHT.FAILED_NO_ENGINE;
	}
}
