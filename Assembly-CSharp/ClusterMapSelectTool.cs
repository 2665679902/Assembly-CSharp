using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020007C3 RID: 1987
public class ClusterMapSelectTool : InterfaceTool
{
	// Token: 0x060038AF RID: 14511 RVA: 0x0013A5BA File Offset: 0x001387BA
	public static void DestroyInstance()
	{
		ClusterMapSelectTool.Instance = null;
	}

	// Token: 0x060038B0 RID: 14512 RVA: 0x0013A5C2 File Offset: 0x001387C2
	protected override void OnPrefabInit()
	{
		ClusterMapSelectTool.Instance = this;
	}

	// Token: 0x060038B1 RID: 14513 RVA: 0x0013A5CA File Offset: 0x001387CA
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
		ToolMenu.Instance.PriorityScreen.ResetPriority();
		this.Select(null, false);
	}

	// Token: 0x060038B2 RID: 14514 RVA: 0x0013A5EE File Offset: 0x001387EE
	public KSelectable GetSelected()
	{
		return this.m_selected;
	}

	// Token: 0x060038B3 RID: 14515 RVA: 0x0013A5F6 File Offset: 0x001387F6
	public override bool ShowHoverUI()
	{
		return ClusterMapScreen.Instance.HasCurrentHover();
	}

	// Token: 0x060038B4 RID: 14516 RVA: 0x0013A602 File Offset: 0x00138802
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		base.ClearHover();
		this.Select(null, false);
	}

	// Token: 0x060038B5 RID: 14517 RVA: 0x0013A61C File Offset: 0x0013881C
	private void UpdateHoveredSelectables()
	{
		this.m_hoveredSelectables.Clear();
		if (ClusterMapScreen.Instance.HasCurrentHover())
		{
			AxialI currentHoverLocation = ClusterMapScreen.Instance.GetCurrentHoverLocation();
			List<KSelectable> list = (from entity in ClusterGrid.Instance.GetVisibleEntitiesAtCell(currentHoverLocation)
				select entity.GetComponent<KSelectable>() into selectable
				where selectable != null && selectable.IsSelectable
				select selectable).ToList<KSelectable>();
			this.m_hoveredSelectables.AddRange(list);
		}
	}

	// Token: 0x060038B6 RID: 14518 RVA: 0x0013A6B0 File Offset: 0x001388B0
	public override void LateUpdate()
	{
		this.UpdateHoveredSelectables();
		KSelectable kselectable = ((this.m_hoveredSelectables.Count > 0) ? this.m_hoveredSelectables[0] : null);
		base.UpdateHoverElements(this.m_hoveredSelectables);
		if (!this.hasFocus)
		{
			base.ClearHover();
		}
		else if (kselectable != this.hover)
		{
			base.ClearHover();
			this.hover = kselectable;
			if (kselectable != null)
			{
				Game.Instance.Trigger(2095258329, kselectable.gameObject);
				kselectable.Hover(!this.playedSoundThisFrame);
				this.playedSoundThisFrame = true;
			}
		}
		this.playedSoundThisFrame = false;
	}

	// Token: 0x060038B7 RID: 14519 RVA: 0x0013A753 File Offset: 0x00138953
	public void SelectNextFrame(KSelectable new_selected, bool skipSound = false)
	{
		this.delayedNextSelection = new_selected;
		this.delayedSkipSound = skipSound;
		UIScheduler.Instance.ScheduleNextFrame("DelayedSelect", new Action<object>(this.DoSelectNextFrame), null, null);
	}

	// Token: 0x060038B8 RID: 14520 RVA: 0x0013A781 File Offset: 0x00138981
	private void DoSelectNextFrame(object data)
	{
		this.Select(this.delayedNextSelection, this.delayedSkipSound);
		this.delayedNextSelection = null;
	}

	// Token: 0x060038B9 RID: 14521 RVA: 0x0013A79C File Offset: 0x0013899C
	public void Select(KSelectable new_selected, bool skipSound = false)
	{
		if (new_selected == this.m_selected)
		{
			return;
		}
		if (this.m_selected != null)
		{
			this.m_selected.Unselect();
		}
		GameObject gameObject = null;
		if (new_selected != null && new_selected.GetMyWorldId() == -1)
		{
			if (new_selected == this.hover)
			{
				base.ClearHover();
			}
			new_selected.Select();
			gameObject = new_selected.gameObject;
		}
		this.m_selected = ((gameObject == null) ? null : new_selected);
		Game.Instance.Trigger(-1503271301, gameObject);
	}

	// Token: 0x040025A7 RID: 9639
	private List<KSelectable> m_hoveredSelectables = new List<KSelectable>();

	// Token: 0x040025A8 RID: 9640
	private KSelectable m_selected;

	// Token: 0x040025A9 RID: 9641
	public static ClusterMapSelectTool Instance;

	// Token: 0x040025AA RID: 9642
	private KSelectable delayedNextSelection;

	// Token: 0x040025AB RID: 9643
	private bool delayedSkipSound;
}
