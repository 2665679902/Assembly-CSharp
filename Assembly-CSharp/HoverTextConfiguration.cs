using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A02 RID: 2562
[AddComponentMenu("KMonoBehaviour/scripts/HoverTextConfiguration")]
public class HoverTextConfiguration : KMonoBehaviour
{
	// Token: 0x06004CEA RID: 19690 RVA: 0x001B0F87 File Offset: 0x001AF187
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ConfigureHoverScreen();
	}

	// Token: 0x06004CEB RID: 19691 RVA: 0x001B0F95 File Offset: 0x001AF195
	protected virtual void ConfigureTitle(HoverTextScreen screen)
	{
		if (string.IsNullOrEmpty(this.ToolName))
		{
			this.ToolName = Strings.Get(this.ToolNameStringKey).String.ToUpper();
		}
	}

	// Token: 0x06004CEC RID: 19692 RVA: 0x001B0FBF File Offset: 0x001AF1BF
	protected void DrawTitle(HoverTextScreen screen, HoverTextDrawer drawer)
	{
		drawer.DrawText(this.ToolName, this.ToolTitleTextStyle);
	}

	// Token: 0x06004CED RID: 19693 RVA: 0x001B0FD4 File Offset: 0x001AF1D4
	protected void DrawInstructions(HoverTextScreen screen, HoverTextDrawer drawer)
	{
		TextStyleSetting standard = this.Styles_Instruction.Standard;
		drawer.NewLine(26);
		if (KInputManager.currentControllerIsGamepad)
		{
			drawer.DrawIcon(KInputManager.steamInputInterpreter.GetActionSprite(global::Action.MouseLeft, false), 20);
		}
		else
		{
			drawer.DrawIcon(screen.GetSprite("icon_mouse_left"), 20);
		}
		drawer.DrawText(this.ActionName, standard);
		drawer.AddIndent(8);
		if (KInputManager.currentControllerIsGamepad)
		{
			drawer.DrawIcon(KInputManager.steamInputInterpreter.GetActionSprite(global::Action.MouseRight, false), 20);
		}
		else
		{
			drawer.DrawIcon(screen.GetSprite("icon_mouse_right"), 20);
		}
		drawer.DrawText(this.backStr, standard);
	}

	// Token: 0x06004CEE RID: 19694 RVA: 0x001B1078 File Offset: 0x001AF278
	public virtual void ConfigureHoverScreen()
	{
		if (!string.IsNullOrEmpty(this.ActionStringKey))
		{
			this.ActionName = Strings.Get(this.ActionStringKey);
		}
		HoverTextScreen instance = HoverTextScreen.Instance;
		this.ConfigureTitle(instance);
		this.backStr = UI.TOOLS.GENERIC.BACK.ToString().ToUpper();
	}

	// Token: 0x06004CEF RID: 19695 RVA: 0x001B10CC File Offset: 0x001AF2CC
	public virtual void UpdateHoverElements(List<KSelectable> hover_objects)
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
		this.DrawTitle(instance, hoverTextDrawer);
		this.DrawInstructions(HoverTextScreen.Instance, hoverTextDrawer);
		hoverTextDrawer.EndShadowBar();
		hoverTextDrawer.EndDrawing();
	}

	// Token: 0x040032AC RID: 12972
	public TextStyleSetting[] HoverTextStyleSettings;

	// Token: 0x040032AD RID: 12973
	public string ToolNameStringKey = "";

	// Token: 0x040032AE RID: 12974
	public string ActionStringKey = "";

	// Token: 0x040032AF RID: 12975
	[HideInInspector]
	public string ActionName = "";

	// Token: 0x040032B0 RID: 12976
	[HideInInspector]
	public string ToolName;

	// Token: 0x040032B1 RID: 12977
	protected string backStr;

	// Token: 0x040032B2 RID: 12978
	public TextStyleSetting ToolTitleTextStyle;

	// Token: 0x040032B3 RID: 12979
	public HoverTextConfiguration.TextStylePair Styles_Title;

	// Token: 0x040032B4 RID: 12980
	public HoverTextConfiguration.TextStylePair Styles_BodyText;

	// Token: 0x040032B5 RID: 12981
	public HoverTextConfiguration.TextStylePair Styles_Instruction;

	// Token: 0x040032B6 RID: 12982
	public HoverTextConfiguration.TextStylePair Styles_Warning;

	// Token: 0x040032B7 RID: 12983
	public HoverTextConfiguration.ValuePropertyTextStyles Styles_Values;

	// Token: 0x0200180D RID: 6157
	[Serializable]
	public struct TextStylePair
	{
		// Token: 0x04006EDC RID: 28380
		public TextStyleSetting Standard;

		// Token: 0x04006EDD RID: 28381
		public TextStyleSetting Selected;
	}

	// Token: 0x0200180E RID: 6158
	[Serializable]
	public struct ValuePropertyTextStyles
	{
		// Token: 0x04006EDE RID: 28382
		public HoverTextConfiguration.TextStylePair Property;

		// Token: 0x04006EDF RID: 28383
		public HoverTextConfiguration.TextStylePair Property_Decimal;

		// Token: 0x04006EE0 RID: 28384
		public HoverTextConfiguration.TextStylePair Property_Unit;
	}
}
