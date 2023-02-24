using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009F8 RID: 2552
public class ClusterMapSelectToolHoverTextCard : HoverTextConfiguration
{
	// Token: 0x06004CA9 RID: 19625 RVA: 0x001AF4FC File Offset: 0x001AD6FC
	public override void ConfigureHoverScreen()
	{
		base.ConfigureHoverScreen();
		HoverTextScreen instance = HoverTextScreen.Instance;
		this.m_iconWarning = instance.GetSprite("iconWarning");
		this.m_iconDash = instance.GetSprite("dash");
		this.m_iconHighlighted = instance.GetSprite("dash_arrow");
	}

	// Token: 0x06004CAA RID: 19626 RVA: 0x001AF548 File Offset: 0x001AD748
	public override void UpdateHoverElements(List<KSelectable> hoverObjects)
	{
		if (this.m_iconWarning == null)
		{
			this.ConfigureHoverScreen();
		}
		HoverTextDrawer hoverTextDrawer = HoverTextScreen.Instance.BeginDrawing();
		foreach (KSelectable kselectable in hoverObjects)
		{
			hoverTextDrawer.BeginShadowBar(ClusterMapSelectTool.Instance.GetSelected() == kselectable);
			string unitFormattedName = GameUtil.GetUnitFormattedName(kselectable.gameObject, true);
			hoverTextDrawer.DrawText(unitFormattedName, this.Styles_Title.Standard);
			foreach (StatusItemGroup.Entry entry in kselectable.GetStatusItemGroup())
			{
				if (entry.category != null && entry.category.Id == "Main")
				{
					TextStyleSetting textStyleSetting = (this.IsStatusItemWarning(entry) ? this.Styles_Warning.Standard : this.Styles_BodyText.Standard);
					Sprite sprite = ((entry.item.sprite != null) ? entry.item.sprite.sprite : this.m_iconWarning);
					Color color = (this.IsStatusItemWarning(entry) ? this.Styles_Warning.Standard.textColor : this.Styles_BodyText.Standard.textColor);
					hoverTextDrawer.NewLine(26);
					hoverTextDrawer.DrawIcon(sprite, color, 18, 2);
					hoverTextDrawer.DrawText(entry.GetName(), textStyleSetting);
				}
			}
			foreach (StatusItemGroup.Entry entry2 in kselectable.GetStatusItemGroup())
			{
				if (entry2.category == null || entry2.category.Id != "Main")
				{
					TextStyleSetting textStyleSetting2 = (this.IsStatusItemWarning(entry2) ? this.Styles_Warning.Standard : this.Styles_BodyText.Standard);
					Sprite sprite2 = ((entry2.item.sprite != null) ? entry2.item.sprite.sprite : this.m_iconWarning);
					Color color2 = (this.IsStatusItemWarning(entry2) ? this.Styles_Warning.Standard.textColor : this.Styles_BodyText.Standard.textColor);
					hoverTextDrawer.NewLine(26);
					hoverTextDrawer.DrawIcon(sprite2, color2, 18, 2);
					hoverTextDrawer.DrawText(entry2.GetName(), textStyleSetting2);
				}
			}
			hoverTextDrawer.EndShadowBar();
		}
		hoverTextDrawer.EndDrawing();
	}

	// Token: 0x06004CAB RID: 19627 RVA: 0x001AF824 File Offset: 0x001ADA24
	private bool IsStatusItemWarning(StatusItemGroup.Entry item)
	{
		return item.item.notificationType == NotificationType.Bad || item.item.notificationType == NotificationType.BadMinor || item.item.notificationType == NotificationType.DuplicantThreatening;
	}

	// Token: 0x0400327F RID: 12927
	private Sprite m_iconWarning;

	// Token: 0x04003280 RID: 12928
	private Sprite m_iconDash;

	// Token: 0x04003281 RID: 12929
	private Sprite m_iconHighlighted;
}
