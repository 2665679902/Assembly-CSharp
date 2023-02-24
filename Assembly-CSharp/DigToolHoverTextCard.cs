﻿using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020009FD RID: 2557
public class DigToolHoverTextCard : HoverTextConfiguration
{
	// Token: 0x06004CCA RID: 19658 RVA: 0x001B0570 File Offset: 0x001AE770
	public override void UpdateHoverElements(List<KSelectable> selected)
	{
		HoverTextScreen instance = HoverTextScreen.Instance;
		HoverTextDrawer hoverTextDrawer = instance.BeginDrawing();
		int num = Grid.PosToCell(Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()));
		if (!Grid.IsValidCell(num) || (int)Grid.WorldIdx[num] != ClusterManager.Instance.activeWorldId)
		{
			hoverTextDrawer.EndDrawing();
			return;
		}
		hoverTextDrawer.BeginShadowBar(false);
		if (Grid.IsVisible(num))
		{
			base.DrawTitle(instance, hoverTextDrawer);
			base.DrawInstructions(HoverTextScreen.Instance, hoverTextDrawer);
			Element element = Grid.Element[num];
			bool flag = false;
			if (Grid.Solid[num] && Diggable.IsDiggable(num))
			{
				flag = true;
			}
			if (flag)
			{
				hoverTextDrawer.NewLine(26);
				hoverTextDrawer.DrawText(element.nameUpperCase, this.Styles_Title.Standard);
				hoverTextDrawer.NewLine(26);
				hoverTextDrawer.DrawIcon(instance.GetSprite("dash"), 18);
				hoverTextDrawer.DrawText(element.GetMaterialCategoryTag().ProperName(), this.Styles_BodyText.Standard);
				hoverTextDrawer.NewLine(26);
				hoverTextDrawer.DrawIcon(instance.GetSprite("dash"), 18);
				string[] array = HoverTextHelper.MassStringsReadOnly(num);
				hoverTextDrawer.DrawText(array[0], this.Styles_Values.Property.Standard);
				hoverTextDrawer.DrawText(array[1], this.Styles_Values.Property_Decimal.Standard);
				hoverTextDrawer.DrawText(array[2], this.Styles_Values.Property.Standard);
				hoverTextDrawer.DrawText(array[3], this.Styles_Values.Property.Standard);
				hoverTextDrawer.NewLine(26);
				hoverTextDrawer.DrawIcon(instance.GetSprite("dash"), 18);
				hoverTextDrawer.DrawText(GameUtil.GetHardnessString(Grid.Element[num], true), this.Styles_BodyText.Standard);
			}
		}
		else
		{
			hoverTextDrawer.DrawIcon(instance.GetSprite("iconWarning"), 18);
			hoverTextDrawer.DrawText(UI.TOOLS.GENERIC.UNKNOWN, this.Styles_BodyText.Standard);
		}
		hoverTextDrawer.EndShadowBar();
		hoverTextDrawer.EndDrawing();
	}

	// Token: 0x0400329A RID: 12954
	private DigToolHoverTextCard.HoverScreenFields hoverScreenElements;

	// Token: 0x0200180C RID: 6156
	private struct HoverScreenFields
	{
		// Token: 0x04006ED5 RID: 28373
		public GameObject UnknownAreaLine;

		// Token: 0x04006ED6 RID: 28374
		public Image ElementStateIcon;

		// Token: 0x04006ED7 RID: 28375
		public LocText ElementCategory;

		// Token: 0x04006ED8 RID: 28376
		public LocText ElementName;

		// Token: 0x04006ED9 RID: 28377
		public LocText[] ElementMass;

		// Token: 0x04006EDA RID: 28378
		public LocText ElementHardness;

		// Token: 0x04006EDB RID: 28379
		public LocText ElementHardnessDescription;
	}
}
