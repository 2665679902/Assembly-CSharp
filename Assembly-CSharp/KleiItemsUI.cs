using System;
using Database;
using STRINGS;
using UnityEngine;

// Token: 0x02000AD5 RID: 2773
public static class KleiItemsUI
{
	// Token: 0x06005508 RID: 21768 RVA: 0x001ECE85 File Offset: 0x001EB085
	public static string WrapAsToolTipTitle(string text)
	{
		return "<b><style=\"KLink\">" + text + "</style></b>";
	}

	// Token: 0x06005509 RID: 21769 RVA: 0x001ECE97 File Offset: 0x001EB097
	public static string WrapWithColor(string text, Color color)
	{
		return string.Concat(new string[]
		{
			"<color=#",
			color.ToHexString(),
			">",
			text,
			"</color>"
		});
	}

	// Token: 0x0600550A RID: 21770 RVA: 0x001ECEC9 File Offset: 0x001EB0C9
	public static Sprite GetNoneOutfitIcon()
	{
		return Assets.GetSprite("NoTraits");
	}

	// Token: 0x0600550B RID: 21771 RVA: 0x001ECEDA File Offset: 0x001EB0DA
	public static Sprite GetNoneClothingItemIcon(PermitCategory category)
	{
		return Assets.GetSprite("NoTraits");
	}

	// Token: 0x0600550C RID: 21772 RVA: 0x001ECEEB File Offset: 0x001EB0EB
	public static Sprite GetNoneBalloonArtistIcon()
	{
		return Assets.GetSprite("NoTraits");
	}

	// Token: 0x0600550D RID: 21773 RVA: 0x001ECEFC File Offset: 0x001EB0FC
	public static string GetNoneClothingItemString(PermitCategory category)
	{
		switch (category)
		{
		case PermitCategory.DupeTops:
			return UI.OUTFIT_DESCRIPTION.NO_DUPE_TOPS;
		case PermitCategory.DupeBottoms:
			return UI.OUTFIT_DESCRIPTION.NO_DUPE_BOTTOMS;
		case PermitCategory.DupeGloves:
			return UI.OUTFIT_DESCRIPTION.NO_DUPE_GLOVES;
		case PermitCategory.DupeShoes:
			return UI.OUTFIT_DESCRIPTION.NO_DUPE_SHOES;
		case PermitCategory.DupeHats:
			return UI.OUTFIT_DESCRIPTION.NO_DUPE_HATS;
		case PermitCategory.DupeAccessories:
			return UI.OUTFIT_DESCRIPTION.NO_DUPE_ACCESSORIES;
		case PermitCategory.JoyResponse:
			return UI.OUTFIT_DESCRIPTION.NO_JOY_RESPONSE;
		}
		DebugUtil.DevAssert(false, string.Format("Couldn't find \"no item\" string for category {0}", category), null);
		return "-";
	}

	// Token: 0x0600550E RID: 21774 RVA: 0x001ECFAC File Offset: 0x001EB1AC
	public static void ConfigureTooltipOn(GameObject gameObject, Option<LocString> tooltipText = default(Option<LocString>))
	{
		KleiItemsUI.ConfigureTooltipOn(gameObject, tooltipText.HasValue ? Option.Some<string>(tooltipText.Value) : Option.None);
	}

	// Token: 0x0600550F RID: 21775 RVA: 0x001ECFDC File Offset: 0x001EB1DC
	public static void ConfigureTooltipOn(GameObject gameObject, Option<string> tooltipText = default(Option<string>))
	{
		ToolTip toolTip = gameObject.GetComponent<ToolTip>();
		if (toolTip.IsNullOrDestroyed())
		{
			toolTip = gameObject.AddComponent<ToolTip>();
			toolTip.tooltipPivot = new Vector2(0.5f, 1f);
			if (gameObject.GetComponent<KButton>())
			{
				toolTip.tooltipPositionOffset = new Vector2(0f, 22f);
			}
			else
			{
				toolTip.tooltipPositionOffset = new Vector2(0f, 0f);
			}
			toolTip.parentPositionAnchor = new Vector2(0.5f, 0f);
			toolTip.toolTipPosition = ToolTip.TooltipPosition.Custom;
		}
		if (!tooltipText.HasValue)
		{
			toolTip.ClearMultiStringTooltip();
			return;
		}
		toolTip.SetSimpleTooltip(tooltipText.Value);
	}

	// Token: 0x06005510 RID: 21776 RVA: 0x001ED088 File Offset: 0x001EB288
	public static string GetTooltipStringFor(PermitResource permit)
	{
		string text = KleiItemsUI.WrapAsToolTipTitle(permit.Name);
		if (!string.IsNullOrWhiteSpace(permit.Description))
		{
			text = text + "\n" + permit.Description;
		}
		string text2 = UI.KLEI_INVENTORY_SCREEN.ITEM_RARITY_DETAILS.Replace("{RarityName}", permit.Rarity.GetLocStringName());
		if (!string.IsNullOrWhiteSpace(text2))
		{
			text = text + "\n\n" + text2;
		}
		if (PermitItems.GetOwnedCount(permit) <= 0)
		{
			text = text + "\n\n" + KleiItemsUI.WrapWithColor(UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWN_NONE, KleiItemsUI.TEXT_COLOR__PERMIT_NOT_OWNED);
		}
		return text;
	}

	// Token: 0x06005511 RID: 21777 RVA: 0x001ED11A File Offset: 0x001EB31A
	public static Color GetColor(string input)
	{
		if (input[0] == '#')
		{
			return Util.ColorFromHex(input.Substring(1));
		}
		return Util.ColorFromHex(input);
	}

	// Token: 0x040039C8 RID: 14792
	public static readonly Color TEXT_COLOR__PERMIT_NOT_OWNED = KleiItemsUI.GetColor("#DD992F");
}
