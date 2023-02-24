using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007BE RID: 1982
public class BrushTool : InterfaceTool
{
	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x06003863 RID: 14435 RVA: 0x00138E09 File Offset: 0x00137009
	public bool Dragging
	{
		get
		{
			return this.dragging;
		}
	}

	// Token: 0x06003864 RID: 14436 RVA: 0x00138E11 File Offset: 0x00137011
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		this.dragging = false;
	}

	// Token: 0x06003865 RID: 14437 RVA: 0x00138E20 File Offset: 0x00137020
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.radiusIndicatorColor));
		}
	}

	// Token: 0x06003866 RID: 14438 RVA: 0x00138E88 File Offset: 0x00137088
	public virtual void SetBrushSize(int radius)
	{
		if (radius == this.brushRadius)
		{
			return;
		}
		this.brushRadius = radius;
		this.brushOffsets.Clear();
		for (int i = 0; i < this.brushRadius * 2; i++)
		{
			for (int j = 0; j < this.brushRadius * 2; j++)
			{
				if (Vector2.Distance(new Vector2((float)i, (float)j), new Vector2((float)this.brushRadius, (float)this.brushRadius)) < (float)this.brushRadius - 0.8f)
				{
					this.brushOffsets.Add(new Vector2((float)(i - this.brushRadius), (float)(j - this.brushRadius)));
				}
			}
		}
	}

	// Token: 0x06003867 RID: 14439 RVA: 0x00138F29 File Offset: 0x00137129
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x06003868 RID: 14440 RVA: 0x00138F4C File Offset: 0x0013714C
	protected override void OnPrefabInit()
	{
		Game.Instance.Subscribe(1634669191, new Action<object>(this.OnTutorialOpened));
		base.OnPrefabInit();
		if (this.visualizer != null)
		{
			this.visualizer = Util.KInstantiate(this.visualizer, null, null);
		}
		if (this.areaVisualizer != null)
		{
			this.areaVisualizer = Util.KInstantiate(this.areaVisualizer, null, null);
			this.areaVisualizer.SetActive(false);
			this.areaVisualizer.GetComponent<RectTransform>().SetParent(base.transform);
			this.areaVisualizer.GetComponent<Renderer>().material.color = this.areaColour;
		}
	}

	// Token: 0x06003869 RID: 14441 RVA: 0x00138FFF File Offset: 0x001371FF
	protected override void OnCmpEnable()
	{
		this.dragging = false;
	}

	// Token: 0x0600386A RID: 14442 RVA: 0x00139008 File Offset: 0x00137208
	protected override void OnCmpDisable()
	{
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(false);
		}
		if (this.areaVisualizer != null)
		{
			this.areaVisualizer.SetActive(false);
		}
	}

	// Token: 0x0600386B RID: 14443 RVA: 0x0013903E File Offset: 0x0013723E
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		cursor_pos -= this.placementPivot;
		this.dragging = true;
		this.downPos = cursor_pos;
		if (!KInputManager.currentControllerIsGamepad)
		{
			KScreenManager.Instance.SetEventSystemEnabled(false);
		}
		else
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(true, null);
		}
		this.Paint();
	}

	// Token: 0x0600386C RID: 14444 RVA: 0x00139080 File Offset: 0x00137280
	public override void OnLeftClickUp(Vector3 cursor_pos)
	{
		cursor_pos -= this.placementPivot;
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		if (!this.dragging)
		{
			return;
		}
		this.dragging = false;
		BrushTool.DragAxis dragAxis = this.dragAxis;
		if (dragAxis == BrushTool.DragAxis.Horizontal)
		{
			cursor_pos.y = this.downPos.y;
			this.dragAxis = BrushTool.DragAxis.None;
			return;
		}
		if (dragAxis != BrushTool.DragAxis.Vertical)
		{
			return;
		}
		cursor_pos.x = this.downPos.x;
		this.dragAxis = BrushTool.DragAxis.None;
	}

	// Token: 0x0600386D RID: 14445 RVA: 0x00139108 File Offset: 0x00137308
	protected virtual string GetConfirmSound()
	{
		return "Tile_Confirm";
	}

	// Token: 0x0600386E RID: 14446 RVA: 0x0013910F File Offset: 0x0013730F
	protected virtual string GetDragSound()
	{
		return "Tile_Drag";
	}

	// Token: 0x0600386F RID: 14447 RVA: 0x00139116 File Offset: 0x00137316
	public override string GetDeactivateSound()
	{
		return "Tile_Cancel";
	}

	// Token: 0x06003870 RID: 14448 RVA: 0x00139120 File Offset: 0x00137320
	private static int GetGridDistance(int cell, int center_cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = Grid.CellToXY(center_cell);
		Vector2I vector2I3 = vector2I - vector2I2;
		return Math.Abs(vector2I3.x) + Math.Abs(vector2I3.y);
	}

	// Token: 0x06003871 RID: 14449 RVA: 0x00139158 File Offset: 0x00137358
	private void Paint()
	{
		foreach (int num in this.cellsInRadius)
		{
			if (Grid.IsValidCell(num) && (int)Grid.WorldIdx[num] == ClusterManager.Instance.activeWorldId && (!Grid.Foundation[num] || this.affectFoundation))
			{
				this.OnPaintCell(num, Grid.GetCellDistance(this.currentCell, num));
			}
		}
	}

	// Token: 0x06003872 RID: 14450 RVA: 0x001391E8 File Offset: 0x001373E8
	public override void OnMouseMove(Vector3 cursorPos)
	{
		int num = Grid.PosToCell(cursorPos);
		this.currentCell = num;
		base.OnMouseMove(cursorPos);
		this.cellsInRadius.Clear();
		foreach (Vector2 vector in this.brushOffsets)
		{
			int num2 = Grid.OffsetCell(Grid.PosToCell(cursorPos), new CellOffset((int)vector.x, (int)vector.y));
			if (Grid.IsValidCell(num2) && (int)Grid.WorldIdx[num2] == ClusterManager.Instance.activeWorldId)
			{
				this.cellsInRadius.Add(Grid.OffsetCell(Grid.PosToCell(cursorPos), new CellOffset((int)vector.x, (int)vector.y)));
			}
		}
		if (!this.dragging)
		{
			return;
		}
		this.Paint();
	}

	// Token: 0x06003873 RID: 14451 RVA: 0x001392CC File Offset: 0x001374CC
	protected virtual void OnPaintCell(int cell, int distFromOrigin)
	{
	}

	// Token: 0x06003874 RID: 14452 RVA: 0x001392CE File Offset: 0x001374CE
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.DragStraight))
		{
			this.dragAxis = BrushTool.DragAxis.None;
		}
		else if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriortyKeysDown(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06003875 RID: 14453 RVA: 0x00139301 File Offset: 0x00137501
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.DragStraight))
		{
			this.dragAxis = BrushTool.DragAxis.Invalid;
		}
		else if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriorityKeysUp(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06003876 RID: 14454 RVA: 0x00139334 File Offset: 0x00137534
	private void HandlePriortyKeysDown(KButtonEvent e)
	{
		global::Action action = e.GetAction();
		if (global::Action.Plan1 > action || action > global::Action.Plan10 || !e.TryConsume(action))
		{
			return;
		}
		int num = action - global::Action.Plan1 + 1;
		if (num <= 9)
		{
			ToolMenu.Instance.PriorityScreen.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, num), true);
			return;
		}
		ToolMenu.Instance.PriorityScreen.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.topPriority, 1), true);
	}

	// Token: 0x06003877 RID: 14455 RVA: 0x00139398 File Offset: 0x00137598
	private void HandlePriorityKeysUp(KButtonEvent e)
	{
		global::Action action = e.GetAction();
		if (global::Action.Plan1 <= action && action <= global::Action.Plan10)
		{
			e.TryConsume(action);
		}
	}

	// Token: 0x06003878 RID: 14456 RVA: 0x001393BE File Offset: 0x001375BE
	public override void OnFocus(bool focus)
	{
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(focus);
		}
		this.hasFocus = focus;
		base.OnFocus(focus);
	}

	// Token: 0x06003879 RID: 14457 RVA: 0x001393E8 File Offset: 0x001375E8
	private void OnTutorialOpened(object data)
	{
		this.dragging = false;
	}

	// Token: 0x0600387A RID: 14458 RVA: 0x001393F1 File Offset: 0x001375F1
	public override bool ShowHoverUI()
	{
		return this.dragging || base.ShowHoverUI();
	}

	// Token: 0x0600387B RID: 14459 RVA: 0x00139403 File Offset: 0x00137603
	public override void LateUpdate()
	{
		base.LateUpdate();
	}

	// Token: 0x0400258B RID: 9611
	[SerializeField]
	private Texture2D brushCursor;

	// Token: 0x0400258C RID: 9612
	[SerializeField]
	private GameObject areaVisualizer;

	// Token: 0x0400258D RID: 9613
	[SerializeField]
	private Color32 areaColour = new Color(1f, 1f, 1f, 0.5f);

	// Token: 0x0400258E RID: 9614
	protected Color radiusIndicatorColor = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x0400258F RID: 9615
	protected Vector3 placementPivot;

	// Token: 0x04002590 RID: 9616
	protected bool interceptNumberKeysForPriority;

	// Token: 0x04002591 RID: 9617
	protected List<Vector2> brushOffsets = new List<Vector2>();

	// Token: 0x04002592 RID: 9618
	protected bool affectFoundation;

	// Token: 0x04002593 RID: 9619
	private bool dragging;

	// Token: 0x04002594 RID: 9620
	protected int brushRadius = -1;

	// Token: 0x04002595 RID: 9621
	private BrushTool.DragAxis dragAxis = BrushTool.DragAxis.Invalid;

	// Token: 0x04002596 RID: 9622
	protected Vector3 downPos;

	// Token: 0x04002597 RID: 9623
	protected int currentCell;

	// Token: 0x04002598 RID: 9624
	protected HashSet<int> cellsInRadius = new HashSet<int>();

	// Token: 0x0200151E RID: 5406
	private enum DragAxis
	{
		// Token: 0x040065A4 RID: 26020
		Invalid = -1,
		// Token: 0x040065A5 RID: 26021
		None,
		// Token: 0x040065A6 RID: 26022
		Horizontal,
		// Token: 0x040065A7 RID: 26023
		Vertical
	}
}
