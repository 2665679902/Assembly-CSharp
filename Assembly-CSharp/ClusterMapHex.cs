using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A52 RID: 2642
public class ClusterMapHex : MultiToggle, ICanvasRaycastFilter
{
	// Token: 0x170005D6 RID: 1494
	// (get) Token: 0x06005036 RID: 20534 RVA: 0x001CB77E File Offset: 0x001C997E
	// (set) Token: 0x06005037 RID: 20535 RVA: 0x001CB786 File Offset: 0x001C9986
	public AxialI location { get; private set; }

	// Token: 0x06005038 RID: 20536 RVA: 0x001CB790 File Offset: 0x001C9990
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.rectTransform = base.GetComponent<RectTransform>();
		this.onClick = new System.Action(this.TrySelect);
		this.onDoubleClick = new Func<bool>(this.TryGoTo);
		this.onEnter = new System.Action(this.OnHover);
		this.onExit = new System.Action(this.OnUnhover);
	}

	// Token: 0x06005039 RID: 20537 RVA: 0x001CB7F7 File Offset: 0x001C99F7
	public void SetLocation(AxialI location)
	{
		this.location = location;
	}

	// Token: 0x0600503A RID: 20538 RVA: 0x001CB800 File Offset: 0x001C9A00
	public void SetRevealed(ClusterRevealLevel level)
	{
		this._revealLevel = level;
		switch (level)
		{
		case ClusterRevealLevel.Hidden:
			this.fogOfWar.gameObject.SetActive(true);
			this.peekedTile.gameObject.SetActive(false);
			return;
		case ClusterRevealLevel.Peeked:
			this.fogOfWar.gameObject.SetActive(false);
			this.peekedTile.gameObject.SetActive(true);
			return;
		case ClusterRevealLevel.Visible:
			this.fogOfWar.gameObject.SetActive(false);
			this.peekedTile.gameObject.SetActive(false);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600503B RID: 20539 RVA: 0x001CB88F File Offset: 0x001C9A8F
	public void SetDestinationStatus(string fail_reason)
	{
		this.m_tooltip.ClearMultiStringTooltip();
		this.UpdateHoverColors(string.IsNullOrEmpty(fail_reason));
		if (!string.IsNullOrEmpty(fail_reason))
		{
			this.m_tooltip.AddMultiStringTooltip(fail_reason, this.invalidDestinationTooltipStyle);
		}
	}

	// Token: 0x0600503C RID: 20540 RVA: 0x001CB8C4 File Offset: 0x001C9AC4
	public void SetDestinationStatus(string fail_reason, int pathLength, int rocketRange, bool repeat)
	{
		this.m_tooltip.ClearMultiStringTooltip();
		if (pathLength > 0)
		{
			string text = (repeat ? UI.CLUSTERMAP.TOOLTIP_PATH_LENGTH_RETURN : UI.CLUSTERMAP.TOOLTIP_PATH_LENGTH);
			if (repeat)
			{
				pathLength *= 2;
			}
			text = string.Format(text, pathLength, GameUtil.GetFormattedRocketRange((float)rocketRange, GameUtil.TimeSlice.None, true));
			this.m_tooltip.AddMultiStringTooltip(text, this.informationTooltipStyle);
		}
		this.UpdateHoverColors(string.IsNullOrEmpty(fail_reason));
		if (!string.IsNullOrEmpty(fail_reason))
		{
			this.m_tooltip.AddMultiStringTooltip(fail_reason, this.invalidDestinationTooltipStyle);
		}
	}

	// Token: 0x0600503D RID: 20541 RVA: 0x001CB950 File Offset: 0x001C9B50
	public void UpdateToggleState(ClusterMapHex.ToggleState state)
	{
		int num = -1;
		switch (state)
		{
		case ClusterMapHex.ToggleState.Unselected:
			num = 0;
			break;
		case ClusterMapHex.ToggleState.Selected:
			num = 1;
			break;
		case ClusterMapHex.ToggleState.OrbitHighlight:
			num = 2;
			break;
		}
		base.ChangeState(num);
	}

	// Token: 0x0600503E RID: 20542 RVA: 0x001CB984 File Offset: 0x001C9B84
	private void TrySelect()
	{
		if (DebugHandler.InstantBuildMode)
		{
			SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().RevealLocation(this.location, 0);
		}
		ClusterMapScreen.Instance.SelectHex(this);
	}

	// Token: 0x0600503F RID: 20543 RVA: 0x001CB9B0 File Offset: 0x001C9BB0
	private bool TryGoTo()
	{
		List<WorldContainer> list = (from entity in ClusterGrid.Instance.GetVisibleEntitiesAtCell(this.location)
			select entity.GetComponent<WorldContainer>() into x
			where x != null
			select x).ToList<WorldContainer>();
		if (list.Count == 1)
		{
			CameraController.Instance.ActiveWorldStarWipe(list[0].id, null);
			return true;
		}
		return false;
	}

	// Token: 0x06005040 RID: 20544 RVA: 0x001CBA40 File Offset: 0x001C9C40
	private void OnHover()
	{
		this.m_tooltip.ClearMultiStringTooltip();
		string text = "";
		switch (this._revealLevel)
		{
		case ClusterRevealLevel.Hidden:
			text = UI.CLUSTERMAP.TOOLTIP_HIDDEN_HEX;
			break;
		case ClusterRevealLevel.Peeked:
		{
			List<ClusterGridEntity> hiddenEntitiesOfLayerAtCell = ClusterGrid.Instance.GetHiddenEntitiesOfLayerAtCell(this.location, EntityLayer.Asteroid);
			List<ClusterGridEntity> hiddenEntitiesOfLayerAtCell2 = ClusterGrid.Instance.GetHiddenEntitiesOfLayerAtCell(this.location, EntityLayer.POI);
			text = ((hiddenEntitiesOfLayerAtCell.Count > 0 || hiddenEntitiesOfLayerAtCell2.Count > 0) ? UI.CLUSTERMAP.TOOLTIP_PEEKED_HEX_WITH_OBJECT : UI.CLUSTERMAP.TOOLTIP_HIDDEN_HEX);
			break;
		}
		case ClusterRevealLevel.Visible:
			if (ClusterGrid.Instance.GetEntitiesOnCell(this.location).Count == 0)
			{
				text = UI.CLUSTERMAP.TOOLTIP_EMPTY_HEX;
			}
			break;
		}
		if (!text.IsNullOrWhiteSpace())
		{
			this.m_tooltip.AddMultiStringTooltip(text, this.informationTooltipStyle);
		}
		this.UpdateHoverColors(true);
		ClusterMapScreen.Instance.OnHoverHex(this);
	}

	// Token: 0x06005041 RID: 20545 RVA: 0x001CBB1C File Offset: 0x001C9D1C
	private void OnUnhover()
	{
		ClusterMapScreen.Instance.OnUnhoverHex(this);
	}

	// Token: 0x06005042 RID: 20546 RVA: 0x001CBB2C File Offset: 0x001C9D2C
	private void UpdateHoverColors(bool validDestination)
	{
		Color color = (validDestination ? this.hoverColorValid : this.hoverColorInvalid);
		for (int i = 0; i < this.states.Length; i++)
		{
			this.states[i].color_on_hover = color;
			for (int j = 0; j < this.states[i].additional_display_settings.Length; j++)
			{
				this.states[i].additional_display_settings[j].color_on_hover = color;
			}
		}
		base.RefreshHoverColor();
	}

	// Token: 0x06005043 RID: 20547 RVA: 0x001CBBB4 File Offset: 0x001C9DB4
	public bool IsRaycastLocationValid(Vector2 inputPoint, Camera eventCamera)
	{
		Vector2 vector = this.rectTransform.position;
		float num = Mathf.Abs(inputPoint.x - vector.x);
		float num2 = Mathf.Abs(inputPoint.y - vector.y);
		Vector2 vector2 = this.rectTransform.lossyScale;
		return num <= vector2.x && num2 <= vector2.y && vector2.y * vector2.x - vector2.y / 2f * num - vector2.x * num2 >= 0f;
	}

	// Token: 0x040035E6 RID: 13798
	private RectTransform rectTransform;

	// Token: 0x040035E7 RID: 13799
	public Color hoverColorValid;

	// Token: 0x040035E8 RID: 13800
	public Color hoverColorInvalid;

	// Token: 0x040035E9 RID: 13801
	public Image fogOfWar;

	// Token: 0x040035EA RID: 13802
	public Image peekedTile;

	// Token: 0x040035EB RID: 13803
	public TextStyleSetting invalidDestinationTooltipStyle;

	// Token: 0x040035EC RID: 13804
	public TextStyleSetting informationTooltipStyle;

	// Token: 0x040035ED RID: 13805
	[MyCmpGet]
	private ToolTip m_tooltip;

	// Token: 0x040035EE RID: 13806
	private ClusterRevealLevel _revealLevel;

	// Token: 0x020018DD RID: 6365
	public enum ToggleState
	{
		// Token: 0x04007298 RID: 29336
		Unselected,
		// Token: 0x04007299 RID: 29337
		Selected,
		// Token: 0x0400729A RID: 29338
		OrbitHighlight
	}
}
