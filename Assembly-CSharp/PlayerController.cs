using System;
using System.Collections.Generic;
using Klei.Input;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200089E RID: 2206
[AddComponentMenu("KMonoBehaviour/scripts/PlayerController")]
public class PlayerController : KMonoBehaviour, IInputHandler
{
	// Token: 0x17000464 RID: 1124
	// (get) Token: 0x06003F3A RID: 16186 RVA: 0x00161357 File Offset: 0x0015F557
	public string handlerName
	{
		get
		{
			return "PlayerController";
		}
	}

	// Token: 0x17000465 RID: 1125
	// (get) Token: 0x06003F3B RID: 16187 RVA: 0x0016135E File Offset: 0x0015F55E
	// (set) Token: 0x06003F3C RID: 16188 RVA: 0x00161366 File Offset: 0x0015F566
	public KInputHandler inputHandler { get; set; }

	// Token: 0x17000466 RID: 1126
	// (get) Token: 0x06003F3D RID: 16189 RVA: 0x0016136F File Offset: 0x0015F56F
	public InterfaceTool ActiveTool
	{
		get
		{
			return this.activeTool;
		}
	}

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x06003F3E RID: 16190 RVA: 0x00161377 File Offset: 0x0015F577
	// (set) Token: 0x06003F3F RID: 16191 RVA: 0x0016137E File Offset: 0x0015F57E
	public static PlayerController Instance { get; private set; }

	// Token: 0x06003F40 RID: 16192 RVA: 0x00161386 File Offset: 0x0015F586
	public static void DestroyInstance()
	{
		PlayerController.Instance = null;
	}

	// Token: 0x06003F41 RID: 16193 RVA: 0x00161390 File Offset: 0x0015F590
	protected override void OnPrefabInit()
	{
		PlayerController.Instance = this;
		InterfaceTool.InitializeConfigs(this.defaultConfigKey, this.interfaceConfigs);
		this.vim = UnityEngine.Object.FindObjectOfType<VirtualInputModule>(true);
		for (int i = 0; i < this.tools.Length; i++)
		{
			if (DlcManager.IsDlcListValidForCurrentContent(this.tools[i].DlcIDs))
			{
				GameObject gameObject = Util.KInstantiate(this.tools[i].gameObject, base.gameObject, null);
				this.tools[i] = gameObject.GetComponent<InterfaceTool>();
				this.tools[i].gameObject.SetActive(true);
				this.tools[i].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06003F42 RID: 16194 RVA: 0x00161435 File Offset: 0x0015F635
	protected override void OnSpawn()
	{
		if (this.tools.Length == 0)
		{
			return;
		}
		this.ActivateTool(this.tools[0]);
	}

	// Token: 0x06003F43 RID: 16195 RVA: 0x0016144F File Offset: 0x0015F64F
	private void InitializeConfigs()
	{
	}

	// Token: 0x06003F44 RID: 16196 RVA: 0x00161451 File Offset: 0x0015F651
	private Vector3 GetCursorPos()
	{
		return PlayerController.GetCursorPos(KInputManager.GetMousePos());
	}

	// Token: 0x06003F45 RID: 16197 RVA: 0x00161460 File Offset: 0x0015F660
	public static Vector3 GetCursorPos(Vector3 mouse_pos)
	{
		RaycastHit raycastHit;
		Vector3 vector;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(mouse_pos), out raycastHit, float.PositiveInfinity, Game.BlockSelectionLayerMask))
		{
			vector = raycastHit.point;
		}
		else
		{
			mouse_pos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
			vector = Camera.main.ScreenToWorldPoint(mouse_pos);
		}
		float num = vector.x;
		float num2 = vector.y;
		num = Mathf.Max(num, 0f);
		num = Mathf.Min(num, Grid.WidthInMeters);
		num2 = Mathf.Max(num2, 0f);
		num2 = Mathf.Min(num2, Grid.HeightInMeters);
		vector.x = num;
		vector.y = num2;
		return vector;
	}

	// Token: 0x06003F46 RID: 16198 RVA: 0x00161514 File Offset: 0x0015F714
	private void UpdateHover()
	{
		UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
		if (current != null)
		{
			this.activeTool.OnFocus(!current.IsPointerOverGameObject());
		}
	}

	// Token: 0x06003F47 RID: 16199 RVA: 0x00161544 File Offset: 0x0015F744
	private void Update()
	{
		this.UpdateDrag();
		if (this.activeTool && this.activeTool.enabled)
		{
			this.UpdateHover();
			Vector3 cursorPos = this.GetCursorPos();
			if (cursorPos != this.prevMousePos)
			{
				this.prevMousePos = cursorPos;
				this.activeTool.OnMouseMove(cursorPos);
			}
		}
		if (Input.GetKeyDown(KeyCode.F12) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
		{
			this.DebugHidingCursor = !this.DebugHidingCursor;
			Cursor.visible = !this.DebugHidingCursor;
			HoverTextScreen.Instance.Show(!this.DebugHidingCursor);
		}
	}

	// Token: 0x06003F48 RID: 16200 RVA: 0x001615F3 File Offset: 0x0015F7F3
	private void OnCleanup()
	{
		Global.GetInputManager().usedMenus.Remove(this);
	}

	// Token: 0x06003F49 RID: 16201 RVA: 0x00161606 File Offset: 0x0015F806
	private void LateUpdate()
	{
		if (this.queueStopDrag)
		{
			this.queueStopDrag = false;
			this.dragging = false;
			this.dragAction = global::Action.Invalid;
			this.dragDelta = Vector3.zero;
			this.worldDragDelta = Vector3.zero;
		}
	}

	// Token: 0x06003F4A RID: 16202 RVA: 0x0016163C File Offset: 0x0015F83C
	public void ActivateTool(InterfaceTool tool)
	{
		if (this.activeTool == tool)
		{
			return;
		}
		this.DeactivateTool(tool);
		this.activeTool = tool;
		this.activeTool.enabled = true;
		this.activeTool.gameObject.SetActive(true);
		this.activeTool.ActivateTool();
		this.UpdateHover();
	}

	// Token: 0x06003F4B RID: 16203 RVA: 0x00161694 File Offset: 0x0015F894
	public void ToolDeactivated(InterfaceTool tool)
	{
		if (this.activeTool == tool && this.activeTool != null)
		{
			this.DeactivateTool(null);
		}
		if (this.activeTool == null)
		{
			this.ActivateTool(SelectTool.Instance);
		}
	}

	// Token: 0x06003F4C RID: 16204 RVA: 0x001616D2 File Offset: 0x0015F8D2
	private void DeactivateTool(InterfaceTool new_tool = null)
	{
		if (this.activeTool != null)
		{
			this.activeTool.enabled = false;
			this.activeTool.gameObject.SetActive(false);
			InterfaceTool interfaceTool = this.activeTool;
			this.activeTool = null;
			interfaceTool.DeactivateTool(new_tool);
		}
	}

	// Token: 0x06003F4D RID: 16205 RVA: 0x00161712 File Offset: 0x0015F912
	public bool IsUsingDefaultTool()
	{
		return this.tools.Length != 0 && this.activeTool == this.tools[0];
	}

	// Token: 0x06003F4E RID: 16206 RVA: 0x00161732 File Offset: 0x0015F932
	private void StartDrag(global::Action action)
	{
		if (this.dragAction == global::Action.Invalid)
		{
			this.dragAction = action;
			this.startDragPos = KInputManager.GetMousePos();
			this.startDragTime = Time.unscaledTime;
		}
	}

	// Token: 0x06003F4F RID: 16207 RVA: 0x0016175C File Offset: 0x0015F95C
	private void UpdateDrag()
	{
		this.dragDelta = Vector2.zero;
		Vector3 mousePos = KInputManager.GetMousePos();
		if (!this.dragging && this.CanDrag() && ((mousePos - this.startDragPos).sqrMagnitude > 36f || Time.unscaledTime - this.startDragTime > 0.3f))
		{
			this.dragging = true;
		}
		if (DistributionPlatform.Initialized && KInputManager.currentControllerIsGamepad && this.dragging)
		{
			return;
		}
		if (this.dragging)
		{
			this.dragDelta = mousePos - this.startDragPos;
			this.worldDragDelta = Camera.main.ScreenToWorldPoint(mousePos) - Camera.main.ScreenToWorldPoint(this.startDragPos);
			this.startDragPos = mousePos;
		}
	}

	// Token: 0x06003F50 RID: 16208 RVA: 0x00161822 File Offset: 0x0015FA22
	private void StopDrag(global::Action action)
	{
		if (this.dragAction == action)
		{
			this.queueStopDrag = true;
			if (KInputManager.currentControllerIsGamepad)
			{
				this.dragging = false;
			}
		}
	}

	// Token: 0x06003F51 RID: 16209 RVA: 0x00161844 File Offset: 0x0015FA44
	public void CancelDragging()
	{
		this.queueStopDrag = true;
		if (this.activeTool != null)
		{
			DragTool dragTool = this.activeTool as DragTool;
			if (dragTool != null)
			{
				dragTool.CancelDragging();
			}
		}
	}

	// Token: 0x06003F52 RID: 16210 RVA: 0x00161881 File Offset: 0x0015FA81
	public void OnCancelInput()
	{
		this.CancelDragging();
	}

	// Token: 0x06003F53 RID: 16211 RVA: 0x0016188C File Offset: 0x0015FA8C
	public void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.ToggleScreenshotMode))
		{
			DebugHandler.ToggleScreenshotMode();
			return;
		}
		if (DebugHandler.HideUI && e.TryConsume(global::Action.Escape))
		{
			DebugHandler.ToggleScreenshotMode();
			return;
		}
		bool flag = true;
		if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
		{
			this.StartDrag(global::Action.MouseLeft);
		}
		else if (e.IsAction(global::Action.MouseRight))
		{
			this.StartDrag(global::Action.MouseRight);
		}
		else if (e.IsAction(global::Action.MouseMiddle))
		{
			this.StartDrag(global::Action.MouseMiddle);
		}
		else
		{
			flag = false;
		}
		if (this.activeTool == null || !this.activeTool.enabled)
		{
			return;
		}
		List<RaycastResult> list = new List<RaycastResult>();
		PointerEventData pointerEventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
		pointerEventData.position = KInputManager.GetMousePos();
		UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
		if (current != null)
		{
			current.RaycastAll(pointerEventData, list);
			if (list.Count > 0)
			{
				return;
			}
		}
		if (flag && !this.draggingAllowed)
		{
			e.TryConsume(e.GetAction());
			return;
		}
		if (e.TryConsume(global::Action.MouseLeft) || e.TryConsume(global::Action.ShiftMouseLeft))
		{
			this.activeTool.OnLeftClickDown(this.GetCursorPos());
			return;
		}
		if (e.IsAction(global::Action.MouseRight))
		{
			this.activeTool.OnRightClickDown(this.GetCursorPos(), e);
			return;
		}
		this.activeTool.OnKeyDown(e);
	}

	// Token: 0x06003F54 RID: 16212 RVA: 0x001619C8 File Offset: 0x0015FBC8
	public void OnKeyUp(KButtonEvent e)
	{
		bool flag = true;
		if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
		{
			this.StopDrag(global::Action.MouseLeft);
		}
		else if (e.IsAction(global::Action.MouseRight))
		{
			this.StopDrag(global::Action.MouseRight);
		}
		else if (e.IsAction(global::Action.MouseMiddle))
		{
			this.StopDrag(global::Action.MouseMiddle);
		}
		else
		{
			flag = false;
		}
		if (this.activeTool == null || !this.activeTool.enabled)
		{
			return;
		}
		if (!this.activeTool.hasFocus)
		{
			return;
		}
		if (flag && !this.draggingAllowed)
		{
			e.TryConsume(e.GetAction());
			return;
		}
		if (!KInputManager.currentControllerIsGamepad)
		{
			if (e.TryConsume(global::Action.MouseLeft) || e.TryConsume(global::Action.ShiftMouseLeft))
			{
				this.activeTool.OnLeftClickUp(this.GetCursorPos());
				return;
			}
			if (e.IsAction(global::Action.MouseRight))
			{
				this.activeTool.OnRightClickUp(this.GetCursorPos());
				return;
			}
			this.activeTool.OnKeyUp(e);
			return;
		}
		else
		{
			if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
			{
				this.activeTool.OnLeftClickUp(this.GetCursorPos());
				return;
			}
			if (e.IsAction(global::Action.MouseRight))
			{
				this.activeTool.OnRightClickUp(this.GetCursorPos());
				return;
			}
			this.activeTool.OnKeyUp(e);
			return;
		}
	}

	// Token: 0x06003F55 RID: 16213 RVA: 0x00161AF9 File Offset: 0x0015FCF9
	public bool ConsumeIfNotDragging(KButtonEvent e, global::Action action)
	{
		return (this.dragAction != action || !this.dragging) && e.TryConsume(action);
	}

	// Token: 0x06003F56 RID: 16214 RVA: 0x00161B15 File Offset: 0x0015FD15
	public bool IsDragging()
	{
		return this.dragging && this.CanDrag();
	}

	// Token: 0x06003F57 RID: 16215 RVA: 0x00161B27 File Offset: 0x0015FD27
	public bool CanDrag()
	{
		return this.draggingAllowed && this.dragAction > global::Action.Invalid;
	}

	// Token: 0x06003F58 RID: 16216 RVA: 0x00161B3C File Offset: 0x0015FD3C
	public void AllowDragging(bool allow)
	{
		this.draggingAllowed = allow;
	}

	// Token: 0x06003F59 RID: 16217 RVA: 0x00161B45 File Offset: 0x0015FD45
	public Vector3 GetDragDelta()
	{
		return this.dragDelta;
	}

	// Token: 0x06003F5A RID: 16218 RVA: 0x00161B4D File Offset: 0x0015FD4D
	public Vector3 GetWorldDragDelta()
	{
		if (!this.draggingAllowed)
		{
			return Vector3.zero;
		}
		return this.worldDragDelta;
	}

	// Token: 0x04002986 RID: 10630
	[SerializeField]
	private global::Action defaultConfigKey;

	// Token: 0x04002987 RID: 10631
	[SerializeField]
	private List<InterfaceToolConfig> interfaceConfigs;

	// Token: 0x04002989 RID: 10633
	public InterfaceTool[] tools;

	// Token: 0x0400298A RID: 10634
	private InterfaceTool activeTool;

	// Token: 0x0400298B RID: 10635
	public VirtualInputModule vim;

	// Token: 0x0400298D RID: 10637
	private bool DebugHidingCursor;

	// Token: 0x0400298E RID: 10638
	private Vector3 prevMousePos = new Vector3(float.PositiveInfinity, 0f, 0f);

	// Token: 0x0400298F RID: 10639
	private const float MIN_DRAG_DIST_SQR = 36f;

	// Token: 0x04002990 RID: 10640
	private const float MIN_DRAG_TIME = 0.3f;

	// Token: 0x04002991 RID: 10641
	private global::Action dragAction;

	// Token: 0x04002992 RID: 10642
	private bool draggingAllowed = true;

	// Token: 0x04002993 RID: 10643
	private bool dragging;

	// Token: 0x04002994 RID: 10644
	private bool queueStopDrag;

	// Token: 0x04002995 RID: 10645
	private Vector3 startDragPos;

	// Token: 0x04002996 RID: 10646
	private float startDragTime;

	// Token: 0x04002997 RID: 10647
	private Vector3 dragDelta;

	// Token: 0x04002998 RID: 10648
	private Vector3 worldDragDelta;
}
