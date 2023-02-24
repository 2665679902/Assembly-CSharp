﻿using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000A4C RID: 2636
public class BuildToolRotateButtonUI : MonoBehaviour
{
	// Token: 0x0600500C RID: 20492 RVA: 0x001CA4B8 File Offset: 0x001C86B8
	private void Awake()
	{
		this.tooltip.refreshWhileHovering = true;
		this.tooltip.SizingSetting = ToolTip.ToolTipSizeSetting.MaxWidthWrapContent;
		this.button.onClick += delegate
		{
			BuildTool.Instance.TryRotate();
		};
		this.UpdateTooltip(false);
	}

	// Token: 0x0600500D RID: 20493 RVA: 0x001CA510 File Offset: 0x001C8710
	private void Update()
	{
		bool flag = BuildTool.Instance.CanRotate();
		this.UpdateTooltip(flag);
		if (this.button.isInteractable != flag)
		{
			this.button.isInteractable = flag;
		}
	}

	// Token: 0x0600500E RID: 20494 RVA: 0x001CA54C File Offset: 0x001C874C
	private void UpdateTooltip(bool can_rotate)
	{
		PermittedRotations? permittedRotations = BuildTool.Instance.GetPermittedRotations();
		if (permittedRotations == null)
		{
			can_rotate = false;
		}
		if (can_rotate)
		{
			LocString locString = UI.BUILDTOOL_ROTATE;
			string feedbackString = this.GetFeedbackString(permittedRotations.Value, BuildTool.Instance.GetBuildingOrientation);
			if (feedbackString != null)
			{
				locString = locString + "\n\n " + feedbackString;
			}
			this.tooltip.SetSimpleTooltip(locString);
			return;
		}
		this.tooltip.SetSimpleTooltip(UI.BUILDTOOL_CANT_ROTATE);
	}

	// Token: 0x0600500F RID: 20495 RVA: 0x001CA5D4 File Offset: 0x001C87D4
	private string GetFeedbackString(PermittedRotations permitted_rotations, Orientation current_rotation)
	{
		switch (permitted_rotations)
		{
		case PermittedRotations.R90:
			if (current_rotation == Orientation.Neutral)
			{
				return UI.BUILDTOOL_ROTATE_CURRENT_UPRIGHT;
			}
			if (current_rotation == Orientation.R90)
			{
				return UI.BUILDTOOL_ROTATE_CURRENT_ON_SIDE;
			}
			break;
		case PermittedRotations.R360:
			switch (current_rotation)
			{
			case Orientation.Neutral:
				return UI.BUILDTOOL_ROTATE_CURRENT_DEGREES.ToString().Replace("{Degrees}", "0");
			case Orientation.R90:
				return UI.BUILDTOOL_ROTATE_CURRENT_DEGREES.ToString().Replace("{Degrees}", "90");
			case Orientation.R180:
				return UI.BUILDTOOL_ROTATE_CURRENT_DEGREES.ToString().Replace("{Degrees}", "180");
			case Orientation.R270:
				return UI.BUILDTOOL_ROTATE_CURRENT_DEGREES.ToString().Replace("{Degrees}", "270");
			}
			break;
		case PermittedRotations.FlipH:
			if (current_rotation == Orientation.Neutral)
			{
				return UI.BUILDTOOL_ROTATE_CURRENT_RIGHT;
			}
			if (current_rotation == Orientation.FlipH)
			{
				return UI.BUILDTOOL_ROTATE_CURRENT_LEFT;
			}
			break;
		case PermittedRotations.FlipV:
			if (current_rotation == Orientation.Neutral)
			{
				return UI.BUILDTOOL_ROTATE_CURRENT_UP;
			}
			if (current_rotation == Orientation.FlipV)
			{
				return UI.BUILDTOOL_ROTATE_CURRENT_DOWN;
			}
			break;
		}
		DebugUtil.DevLogError(string.Format("Unexpected rotation value for tooltip (permitted_rotations: {0}, current_rotation: {1})", permitted_rotations, current_rotation));
		return null;
	}

	// Token: 0x040035C4 RID: 13764
	[SerializeField]
	protected KButton button;

	// Token: 0x040035C5 RID: 13765
	[SerializeField]
	protected ToolTip tooltip;
}
