using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A0F RID: 2575
public class MopToolHoverTextCard : HoverTextConfiguration
{
	// Token: 0x06004DAD RID: 19885 RVA: 0x001B6A80 File Offset: 0x001B4C80
	public override void UpdateHoverElements(List<KSelectable> selected)
	{
		int num = Grid.PosToCell(Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()));
		HoverTextScreen instance = HoverTextScreen.Instance;
		HoverTextDrawer hoverTextDrawer = instance.BeginDrawing();
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
			if (element.IsLiquid)
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
			}
		}
		else
		{
			hoverTextDrawer.DrawIcon(instance.GetSprite("iconWarning"), 18);
			hoverTextDrawer.DrawText(UI.TOOLS.GENERIC.UNKNOWN.ToString().ToUpper(), this.Styles_BodyText.Standard);
		}
		hoverTextDrawer.EndShadowBar();
		hoverTextDrawer.EndDrawing();
	}

	// Token: 0x04003332 RID: 13106
	private MopToolHoverTextCard.HoverScreenFields hoverScreenElements;

	// Token: 0x02001828 RID: 6184
	private struct HoverScreenFields
	{
		// Token: 0x04006F4F RID: 28495
		public GameObject UnknownAreaLine;

		// Token: 0x04006F50 RID: 28496
		public Image ElementStateIcon;

		// Token: 0x04006F51 RID: 28497
		public LocText ElementCategory;

		// Token: 0x04006F52 RID: 28498
		public LocText ElementName;

		// Token: 0x04006F53 RID: 28499
		public LocText[] ElementMass;

		// Token: 0x04006F54 RID: 28500
		public LocText ElementHardness;

		// Token: 0x04006F55 RID: 28501
		public LocText ElementHardnessDescription;
	}
}
