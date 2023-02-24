using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000916 RID: 2326
public class NoFreeRocketInterior : SelectModuleCondition
{
	// Token: 0x060043AE RID: 17326 RVA: 0x0017E2D0 File Offset: 0x0017C4D0
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		int num = 0;
		using (IEnumerator<WorldContainer> enumerator = ClusterManager.Instance.WorldContainers.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsModuleInterior)
				{
					num++;
				}
			}
		}
		return num < ClusterManager.MAX_ROCKET_INTERIOR_COUNT;
	}

	// Token: 0x060043AF RID: 17327 RVA: 0x0017E330 File Offset: 0x0017C530
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.PASSENGER_MODULE_AVAILABLE.COMPLETE;
		}
		return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.PASSENGER_MODULE_AVAILABLE.FAILED;
	}
}
