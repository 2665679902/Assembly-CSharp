using System;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020007CA RID: 1994
public class DragTool : InterfaceTool
{
	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x060038F8 RID: 14584 RVA: 0x0013B9B1 File Offset: 0x00139BB1
	public bool Dragging
	{
		get
		{
			return this.dragging;
		}
	}

	// Token: 0x060038F9 RID: 14585 RVA: 0x0013B9B9 File Offset: 0x00139BB9
	protected virtual DragTool.Mode GetMode()
	{
		return this.mode;
	}

	// Token: 0x060038FA RID: 14586 RVA: 0x0013B9C1 File Offset: 0x00139BC1
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		this.dragging = false;
		this.SetMode(this.mode);
	}

	// Token: 0x060038FB RID: 14587 RVA: 0x0013B9DC File Offset: 0x00139BDC
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		this.RemoveCurrentAreaText();
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x0013BA08 File Offset: 0x00139C08
	protected override void OnPrefabInit()
	{
		Game.Instance.Subscribe(1634669191, new Action<object>(this.OnTutorialOpened));
		base.OnPrefabInit();
		if (this.visualizer != null)
		{
			this.visualizer = global::Util.KInstantiate(this.visualizer, null, null);
		}
		if (this.areaVisualizer != null)
		{
			this.areaVisualizer = global::Util.KInstantiate(this.areaVisualizer, null, null);
			this.areaVisualizer.SetActive(false);
			this.areaVisualizerSpriteRenderer = this.areaVisualizer.GetComponent<SpriteRenderer>();
			this.areaVisualizer.transform.SetParent(base.transform);
			this.areaVisualizer.GetComponent<Renderer>().material.color = this.areaColour;
		}
	}

	// Token: 0x060038FD RID: 14589 RVA: 0x0013BACC File Offset: 0x00139CCC
	protected override void OnCmpEnable()
	{
		this.dragging = false;
	}

	// Token: 0x060038FE RID: 14590 RVA: 0x0013BAD5 File Offset: 0x00139CD5
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

	// Token: 0x060038FF RID: 14591 RVA: 0x0013BB0C File Offset: 0x00139D0C
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		cursor_pos = this.ClampPositionToWorld(cursor_pos, ClusterManager.Instance.activeWorld);
		this.dragging = true;
		this.downPos = cursor_pos;
		this.previousCursorPos = cursor_pos;
		if (this.currentVirtualInputInUse != null)
		{
			this.currentVirtualInputInUse.mouseMovementOnly = false;
			this.currentVirtualInputInUse = null;
		}
		if (!KInputManager.currentControllerIsGamepad)
		{
			KScreenManager.Instance.SetEventSystemEnabled(false);
		}
		else
		{
			UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
			base.SetCurrentVirtualInputModuleMousMovementMode(true, delegate(VirtualInputModule module)
			{
				this.currentVirtualInputInUse = module;
			});
		}
		this.hasFocus = true;
		this.RemoveCurrentAreaText();
		if (this.areaVisualizerTextPrefab != null)
		{
			this.areaVisualizerText = NameDisplayScreen.Instance.AddWorldText("", this.areaVisualizerTextPrefab);
			NameDisplayScreen.Instance.GetWorldText(this.areaVisualizerText).GetComponent<LocText>().color = this.areaColour;
		}
		DragTool.Mode mode = this.GetMode();
		if (mode == DragTool.Mode.Brush)
		{
			if (this.visualizer != null)
			{
				this.AddDragPoint(cursor_pos);
				return;
			}
		}
		else if (mode == DragTool.Mode.Box || mode == DragTool.Mode.Line)
		{
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(false);
			}
			if (this.areaVisualizer != null)
			{
				this.areaVisualizer.SetActive(true);
				this.areaVisualizer.transform.SetPosition(cursor_pos);
				this.areaVisualizerSpriteRenderer.size = new Vector2(0.01f, 0.01f);
			}
		}
	}

	// Token: 0x06003900 RID: 14592 RVA: 0x0013BC72 File Offset: 0x00139E72
	public void RemoveCurrentAreaText()
	{
		if (this.areaVisualizerText != Guid.Empty)
		{
			NameDisplayScreen.Instance.RemoveWorldText(this.areaVisualizerText);
			this.areaVisualizerText = Guid.Empty;
		}
	}

	// Token: 0x06003901 RID: 14593 RVA: 0x0013BCA4 File Offset: 0x00139EA4
	public void CancelDragging()
	{
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (this.currentVirtualInputInUse != null)
		{
			this.currentVirtualInputInUse.mouseMovementOnly = false;
			this.currentVirtualInputInUse = null;
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		this.dragAxis = DragTool.DragAxis.Invalid;
		if (!this.dragging)
		{
			return;
		}
		this.dragging = false;
		this.RemoveCurrentAreaText();
		DragTool.Mode mode = this.GetMode();
		if ((mode == DragTool.Mode.Box || mode == DragTool.Mode.Line) && this.areaVisualizer != null)
		{
			this.areaVisualizer.SetActive(false);
		}
	}

	// Token: 0x06003902 RID: 14594 RVA: 0x0013BD34 File Offset: 0x00139F34
	public override void OnLeftClickUp(Vector3 cursor_pos)
	{
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (this.currentVirtualInputInUse != null)
		{
			this.currentVirtualInputInUse.mouseMovementOnly = false;
			this.currentVirtualInputInUse = null;
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		this.dragAxis = DragTool.DragAxis.Invalid;
		if (!this.dragging)
		{
			return;
		}
		this.dragging = false;
		cursor_pos = this.ClampPositionToWorld(cursor_pos, ClusterManager.Instance.activeWorld);
		this.RemoveCurrentAreaText();
		DragTool.Mode mode = this.GetMode();
		if (mode == DragTool.Mode.Line)
		{
			cursor_pos = this.SnapToLine(cursor_pos);
		}
		if ((mode == DragTool.Mode.Box || mode == DragTool.Mode.Line) && this.areaVisualizer != null)
		{
			this.areaVisualizer.SetActive(false);
			int num;
			int num2;
			Grid.PosToXY(this.downPos, out num, out num2);
			int num3 = num;
			int num4 = num2;
			int num5;
			int num6;
			Grid.PosToXY(cursor_pos, out num5, out num6);
			if (num5 < num)
			{
				global::Util.Swap<int>(ref num, ref num5);
			}
			if (num6 < num2)
			{
				global::Util.Swap<int>(ref num2, ref num6);
			}
			for (int i = num2; i <= num6; i++)
			{
				for (int j = num; j <= num5; j++)
				{
					int num7 = Grid.XYToCell(j, i);
					if (Grid.IsValidCell(num7) && Grid.IsVisible(num7))
					{
						int num8 = i - num4;
						int num9 = j - num3;
						num8 = Mathf.Abs(num8);
						num9 = Mathf.Abs(num9);
						this.OnDragTool(num7, num8 + num9);
					}
				}
			}
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound(this.GetConfirmSound(), false));
			this.OnDragComplete(this.downPos, cursor_pos);
		}
	}

	// Token: 0x06003903 RID: 14595 RVA: 0x0013BEAB File Offset: 0x0013A0AB
	protected virtual string GetConfirmSound()
	{
		return "Tile_Confirm";
	}

	// Token: 0x06003904 RID: 14596 RVA: 0x0013BEB2 File Offset: 0x0013A0B2
	protected virtual string GetDragSound()
	{
		return "Tile_Drag";
	}

	// Token: 0x06003905 RID: 14597 RVA: 0x0013BEB9 File Offset: 0x0013A0B9
	public override string GetDeactivateSound()
	{
		return "Tile_Cancel";
	}

	// Token: 0x06003906 RID: 14598 RVA: 0x0013BEC0 File Offset: 0x0013A0C0
	protected Vector3 ClampPositionToWorld(Vector3 position, WorldContainer world)
	{
		position.x = Mathf.Clamp(position.x, world.minimumBounds.x, world.maximumBounds.x);
		position.y = Mathf.Clamp(position.y, world.minimumBounds.y, world.maximumBounds.y);
		return position;
	}

	// Token: 0x06003907 RID: 14599 RVA: 0x0013BF20 File Offset: 0x0013A120
	protected Vector3 SnapToLine(Vector3 cursorPos)
	{
		Vector3 vector = cursorPos - this.downPos;
		if (this.canChangeDragAxis || this.dragAxis == DragTool.DragAxis.Invalid)
		{
			this.dragAxis = DragTool.DragAxis.Invalid;
			if (vector.sqrMagnitude > 0.707f)
			{
				if (Mathf.Abs(vector.x) < Mathf.Abs(vector.y))
				{
					this.dragAxis = DragTool.DragAxis.Vertical;
				}
				else
				{
					this.dragAxis = DragTool.DragAxis.Horizontal;
				}
			}
		}
		DragTool.DragAxis dragAxis = this.dragAxis;
		if (dragAxis != DragTool.DragAxis.Horizontal)
		{
			if (dragAxis == DragTool.DragAxis.Vertical)
			{
				cursorPos.x = this.downPos.x;
				if (this.lineModeMaxLength != -1)
				{
					cursorPos.y = this.downPos.y + Mathf.Sign(vector.y) * (float)(this.lineModeMaxLength - 1);
				}
			}
		}
		else
		{
			cursorPos.y = this.downPos.y;
			if (this.lineModeMaxLength != -1)
			{
				cursorPos.x = this.downPos.x + Mathf.Sign(vector.x) * (float)(this.lineModeMaxLength - 1);
			}
		}
		return cursorPos;
	}

	// Token: 0x06003908 RID: 14600 RVA: 0x0013C028 File Offset: 0x0013A228
	public override void OnMouseMove(Vector3 cursorPos)
	{
		cursorPos = this.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		if (this.dragging && (Input.GetKey((KeyCode)Global.GetInputManager().GetDefaultController().GetInputForAction(global::Action.DragStraight)) || this.GetMode() == DragTool.Mode.Line))
		{
			cursorPos = this.SnapToLine(cursorPos);
		}
		else
		{
			this.dragAxis = DragTool.DragAxis.Invalid;
		}
		base.OnMouseMove(cursorPos);
		if (!this.dragging)
		{
			return;
		}
		DragTool.Mode mode = this.GetMode();
		if (mode != DragTool.Mode.Brush)
		{
			if (mode - DragTool.Mode.Box <= 1)
			{
				Vector2 vector = Vector3.Max(this.downPos, cursorPos);
				Vector2 vector2 = Vector3.Min(this.downPos, cursorPos);
				vector = base.GetWorldRestrictedPosition(vector);
				vector2 = base.GetWorldRestrictedPosition(vector2);
				vector = base.GetRegularizedPos(vector, false);
				vector2 = base.GetRegularizedPos(vector2, true);
				Vector2 vector3 = vector - vector2;
				Vector2 vector4 = (vector + vector2) * 0.5f;
				this.areaVisualizer.transform.SetPosition(new Vector2(vector4.x, vector4.y));
				int num = (int)(vector.x - vector2.x + (vector.y - vector2.y) - 1f);
				if (this.areaVisualizerSpriteRenderer.size != vector3)
				{
					string sound = GlobalAssets.GetSound(this.GetDragSound(), false);
					if (sound != null)
					{
						Vector3 position = this.areaVisualizer.transform.GetPosition();
						position.z = 0f;
						EventInstance eventInstance = SoundEvent.BeginOneShot(sound, position, 1f, false);
						eventInstance.setParameterByName("tileCount", (float)num, false);
						SoundEvent.EndOneShot(eventInstance);
					}
				}
				this.areaVisualizerSpriteRenderer.size = vector3;
				if (this.areaVisualizerText != Guid.Empty)
				{
					Vector2I vector2I = new Vector2I(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
					LocText component = NameDisplayScreen.Instance.GetWorldText(this.areaVisualizerText).GetComponent<LocText>();
					component.text = string.Format(UI.TOOLS.TOOL_AREA_FMT, vector2I.x, vector2I.y, vector2I.x * vector2I.y);
					Vector2 vector5 = vector4;
					component.transform.SetPosition(vector5);
				}
			}
		}
		else
		{
			this.AddDragPoints(cursorPos, this.previousCursorPos);
			if (this.areaVisualizerText != Guid.Empty)
			{
				int dragLength = this.GetDragLength();
				LocText component2 = NameDisplayScreen.Instance.GetWorldText(this.areaVisualizerText).GetComponent<LocText>();
				component2.text = string.Format(UI.TOOLS.TOOL_LENGTH_FMT, dragLength);
				Vector3 vector6 = Grid.CellToPos(Grid.PosToCell(cursorPos));
				vector6 += new Vector3(0f, 1f, 0f);
				component2.transform.SetPosition(vector6);
			}
		}
		this.previousCursorPos = cursorPos;
	}

	// Token: 0x06003909 RID: 14601 RVA: 0x0013C30E File Offset: 0x0013A50E
	protected virtual void OnDragTool(int cell, int distFromOrigin)
	{
	}

	// Token: 0x0600390A RID: 14602 RVA: 0x0013C310 File Offset: 0x0013A510
	protected virtual void OnDragComplete(Vector3 cursorDown, Vector3 cursorUp)
	{
	}

	// Token: 0x0600390B RID: 14603 RVA: 0x0013C312 File Offset: 0x0013A512
	protected virtual int GetDragLength()
	{
		return 0;
	}

	// Token: 0x0600390C RID: 14604 RVA: 0x0013C318 File Offset: 0x0013A518
	private void AddDragPoint(Vector3 cursorPos)
	{
		cursorPos = this.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		int num = Grid.PosToCell(cursorPos);
		if (Grid.IsValidCell(num) && Grid.IsVisible(num))
		{
			this.OnDragTool(num, 0);
		}
	}

	// Token: 0x0600390D RID: 14605 RVA: 0x0013C358 File Offset: 0x0013A558
	private void AddDragPoints(Vector3 cursorPos, Vector3 previousCursorPos)
	{
		cursorPos = this.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		Vector3 vector = cursorPos - previousCursorPos;
		float magnitude = vector.magnitude;
		float num = Grid.CellSizeInMeters * 0.25f;
		int num2 = 1 + (int)(magnitude / num);
		vector.Normalize();
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector2 = previousCursorPos + vector * ((float)i * num);
			this.AddDragPoint(vector2);
		}
	}

	// Token: 0x0600390E RID: 14606 RVA: 0x0013C3CD File Offset: 0x0013A5CD
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriortyKeysDown(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x0600390F RID: 14607 RVA: 0x0013C3ED File Offset: 0x0013A5ED
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriorityKeysUp(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06003910 RID: 14608 RVA: 0x0013C410 File Offset: 0x0013A610
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

	// Token: 0x06003911 RID: 14609 RVA: 0x0013C474 File Offset: 0x0013A674
	private void HandlePriorityKeysUp(KButtonEvent e)
	{
		global::Action action = e.GetAction();
		if (global::Action.Plan1 <= action && action <= global::Action.Plan10)
		{
			e.TryConsume(action);
		}
	}

	// Token: 0x06003912 RID: 14610 RVA: 0x0013C49C File Offset: 0x0013A69C
	protected void SetMode(DragTool.Mode newMode)
	{
		this.mode = newMode;
		switch (this.mode)
		{
		case DragTool.Mode.Brush:
			if (this.areaVisualizer != null)
			{
				this.areaVisualizer.SetActive(false);
			}
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(true);
			}
			base.SetCursor(this.cursor, this.cursorOffset, CursorMode.Auto);
			return;
		case DragTool.Mode.Box:
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(true);
			}
			this.mode = DragTool.Mode.Box;
			base.SetCursor(this.boxCursor, this.cursorOffset, CursorMode.Auto);
			return;
		case DragTool.Mode.Line:
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(true);
			}
			this.mode = DragTool.Mode.Line;
			base.SetCursor(this.boxCursor, this.cursorOffset, CursorMode.Auto);
			return;
		default:
			return;
		}
	}

	// Token: 0x06003913 RID: 14611 RVA: 0x0013C57C File Offset: 0x0013A77C
	public override void OnFocus(bool focus)
	{
		DragTool.Mode mode = this.GetMode();
		if (mode == DragTool.Mode.Brush)
		{
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(focus);
			}
			this.hasFocus = focus;
			return;
		}
		if (mode - DragTool.Mode.Box > 1)
		{
			return;
		}
		if (this.visualizer != null && !this.dragging)
		{
			this.visualizer.SetActive(focus);
		}
		this.hasFocus = focus || this.dragging;
	}

	// Token: 0x06003914 RID: 14612 RVA: 0x0013C5F0 File Offset: 0x0013A7F0
	private void OnTutorialOpened(object data)
	{
		this.dragging = false;
	}

	// Token: 0x06003915 RID: 14613 RVA: 0x0013C5F9 File Offset: 0x0013A7F9
	public override bool ShowHoverUI()
	{
		return this.dragging || base.ShowHoverUI();
	}

	// Token: 0x040025BB RID: 9659
	[SerializeField]
	private Texture2D boxCursor;

	// Token: 0x040025BC RID: 9660
	[SerializeField]
	private GameObject areaVisualizer;

	// Token: 0x040025BD RID: 9661
	[SerializeField]
	private GameObject areaVisualizerTextPrefab;

	// Token: 0x040025BE RID: 9662
	[SerializeField]
	private Color32 areaColour = new Color(1f, 1f, 1f, 0.5f);

	// Token: 0x040025BF RID: 9663
	protected SpriteRenderer areaVisualizerSpriteRenderer;

	// Token: 0x040025C0 RID: 9664
	protected Guid areaVisualizerText;

	// Token: 0x040025C1 RID: 9665
	protected Vector3 placementPivot;

	// Token: 0x040025C2 RID: 9666
	protected bool interceptNumberKeysForPriority;

	// Token: 0x040025C3 RID: 9667
	private bool dragging;

	// Token: 0x040025C4 RID: 9668
	private Vector3 previousCursorPos;

	// Token: 0x040025C5 RID: 9669
	private DragTool.Mode mode = DragTool.Mode.Box;

	// Token: 0x040025C6 RID: 9670
	private DragTool.DragAxis dragAxis = DragTool.DragAxis.Invalid;

	// Token: 0x040025C7 RID: 9671
	protected bool canChangeDragAxis = true;

	// Token: 0x040025C8 RID: 9672
	protected int lineModeMaxLength = -1;

	// Token: 0x040025C9 RID: 9673
	protected Vector3 downPos;

	// Token: 0x040025CA RID: 9674
	private VirtualInputModule currentVirtualInputInUse;

	// Token: 0x02001524 RID: 5412
	private enum DragAxis
	{
		// Token: 0x040065C5 RID: 26053
		Invalid = -1,
		// Token: 0x040065C6 RID: 26054
		None,
		// Token: 0x040065C7 RID: 26055
		Horizontal,
		// Token: 0x040065C8 RID: 26056
		Vertical
	}

	// Token: 0x02001525 RID: 5413
	public enum Mode
	{
		// Token: 0x040065CA RID: 26058
		Brush,
		// Token: 0x040065CB RID: 26059
		Box,
		// Token: 0x040065CC RID: 26060
		Line
	}
}
