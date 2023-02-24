using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200090F RID: 2319
public class MaterialsAvailable : SelectModuleCondition
{
	// Token: 0x06004398 RID: 17304 RVA: 0x0017DBEE File Offset: 0x0017BDEE
	public override bool IgnoreInSanboxMode()
	{
		return true;
	}

	// Token: 0x06004399 RID: 17305 RVA: 0x0017DBF1 File Offset: 0x0017BDF1
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		return existingModule == null || ProductInfoScreen.MaterialsMet(selectedPart.CraftRecipe);
	}

	// Token: 0x0600439A RID: 17306 RVA: 0x0017DC0C File Offset: 0x0017BE0C
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.MATERIALS_AVAILABLE.COMPLETE;
		}
		string text = UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.MATERIALS_AVAILABLE.FAILED;
		foreach (Recipe.Ingredient ingredient in selectedPart.CraftRecipe.Ingredients)
		{
			string text2 = "\n" + string.Format("{0}{1}: {2}", "    • ", ingredient.tag.ProperName(), GameUtil.GetFormattedMass(ingredient.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			text += text2;
		}
		return text;
	}
}
