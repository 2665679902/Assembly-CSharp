using System;
using System.Collections.Generic;
using System.Linq;
using Klei.Input;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020007CF RID: 1999
[AddComponentMenu("KMonoBehaviour/scripts/InterfaceTool")]
public class InterfaceTool : KMonoBehaviour
{
	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x0600393A RID: 14650 RVA: 0x0013CFFA File Offset: 0x0013B1FA
	public static InterfaceToolConfig ActiveConfig
	{
		get
		{
			if (InterfaceTool.interfaceConfigMap == null)
			{
				InterfaceTool.InitializeConfigs(global::Action.Invalid, null);
			}
			return InterfaceTool.activeConfigs[InterfaceTool.activeConfigs.Count - 1];
		}
	}

	// Token: 0x0600393B RID: 14651 RVA: 0x0013D020 File Offset: 0x0013B220
	public static void ToggleConfig(global::Action configKey)
	{
		if (InterfaceTool.interfaceConfigMap == null)
		{
			InterfaceTool.InitializeConfigs(global::Action.Invalid, null);
		}
		InterfaceToolConfig interfaceToolConfig;
		if (!InterfaceTool.interfaceConfigMap.TryGetValue(configKey, out interfaceToolConfig))
		{
			global::Debug.LogWarning(string.Format("[InterfaceTool] No config is associated with Key: {0}!", configKey) + " Are you sure the configs were initialized properly!");
			return;
		}
		if (InterfaceTool.activeConfigs.BinarySearch(interfaceToolConfig, InterfaceToolConfig.ConfigComparer) <= 0)
		{
			global::Debug.Log(string.Format("[InterfaceTool] Pushing config with key: {0}", configKey));
			InterfaceTool.activeConfigs.Add(interfaceToolConfig);
			InterfaceTool.activeConfigs.Sort(InterfaceToolConfig.ConfigComparer);
			return;
		}
		global::Debug.Log(string.Format("[InterfaceTool] Popping config with key: {0}", configKey));
		InterfaceTool.activeConfigs.Remove(interfaceToolConfig);
	}

	// Token: 0x0600393C RID: 14652 RVA: 0x0013D0D0 File Offset: 0x0013B2D0
	public static void InitializeConfigs(global::Action defaultKey, List<InterfaceToolConfig> configs)
	{
		string text = ((configs == null) ? "null" : configs.Count.ToString());
		global::Debug.Log(string.Format("[InterfaceTool] Initializing configs with values of DefaultKey: {0} Configs: {1}", defaultKey, text));
		if (configs == null || configs.Count == 0)
		{
			InterfaceToolConfig interfaceToolConfig = ScriptableObject.CreateInstance<InterfaceToolConfig>();
			InterfaceTool.interfaceConfigMap = new Dictionary<global::Action, InterfaceToolConfig>();
			InterfaceTool.interfaceConfigMap[interfaceToolConfig.InputAction] = interfaceToolConfig;
			return;
		}
		InterfaceTool.interfaceConfigMap = configs.ToDictionary((InterfaceToolConfig x) => x.InputAction);
		InterfaceTool.ToggleConfig(defaultKey);
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x0600393D RID: 14653 RVA: 0x0013D169 File Offset: 0x0013B369
	public HashedString ViewMode
	{
		get
		{
			return this.viewMode;
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x0600393E RID: 14654 RVA: 0x0013D171 File Offset: 0x0013B371
	public virtual string[] DlcIDs
	{
		get
		{
			return DlcManager.AVAILABLE_ALL_VERSIONS;
		}
	}

	// Token: 0x0600393F RID: 14655 RVA: 0x0013D178 File Offset: 0x0013B378
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.hoverTextConfiguration = base.GetComponent<HoverTextConfiguration>();
	}

	// Token: 0x06003940 RID: 14656 RVA: 0x0013D18C File Offset: 0x0013B38C
	public void ActivateTool()
	{
		this.OnActivateTool();
		this.OnMouseMove(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
		Game.Instance.Trigger(1174281782, this);
	}

	// Token: 0x06003941 RID: 14657 RVA: 0x0013D1B4 File Offset: 0x0013B3B4
	public virtual bool ShowHoverUI()
	{
		if (ManagementMenu.Instance == null || ManagementMenu.Instance.IsFullscreenUIActive())
		{
			return false;
		}
		Vector3 vector = Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos());
		if (OverlayScreen.Instance == null || !ClusterManager.Instance.IsPositionInActiveWorld(vector) || vector.x < 0f || vector.x > Grid.WidthInMeters || vector.y < 0f || vector.y > Grid.HeightInMeters)
		{
			return false;
		}
		UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
		return current != null && !current.IsPointerOverGameObject();
	}

	// Token: 0x06003942 RID: 14658 RVA: 0x0013D258 File Offset: 0x0013B458
	protected virtual void OnActivateTool()
	{
		if (OverlayScreen.Instance != null && this.viewMode != OverlayModes.None.ID && OverlayScreen.Instance.mode != this.viewMode)
		{
			OverlayScreen.Instance.ToggleOverlay(this.viewMode, true);
			InterfaceTool.toolActivatedViewMode = this.viewMode;
		}
		this.SetCursor(this.cursor, this.cursorOffset, CursorMode.Auto);
	}

	// Token: 0x06003943 RID: 14659 RVA: 0x0013D2CC File Offset: 0x0013B4CC
	public void SetCurrentVirtualInputModuleMousMovementMode(bool mouseMovementOnly, Action<VirtualInputModule> extraActions = null)
	{
		UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
		if (current != null && current.currentInputModule != null)
		{
			VirtualInputModule virtualInputModule = current.currentInputModule as VirtualInputModule;
			if (virtualInputModule != null)
			{
				virtualInputModule.mouseMovementOnly = mouseMovementOnly;
				if (extraActions != null)
				{
					extraActions(virtualInputModule);
				}
			}
		}
	}

	// Token: 0x06003944 RID: 14660 RVA: 0x0013D31C File Offset: 0x0013B51C
	public void DeactivateTool(InterfaceTool new_tool = null)
	{
		this.OnDeactivateTool(new_tool);
		if ((new_tool == null || new_tool == SelectTool.Instance) && InterfaceTool.toolActivatedViewMode != OverlayModes.None.ID && InterfaceTool.toolActivatedViewMode == SimDebugView.Instance.GetMode())
		{
			OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, true);
			InterfaceTool.toolActivatedViewMode = OverlayModes.None.ID;
		}
	}

	// Token: 0x06003945 RID: 14661 RVA: 0x0013D387 File Offset: 0x0013B587
	public virtual void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = null;
	}

	// Token: 0x06003946 RID: 14662 RVA: 0x0013D38C File Offset: 0x0013B58C
	protected virtual void OnDeactivateTool(InterfaceTool new_tool)
	{
	}

	// Token: 0x06003947 RID: 14663 RVA: 0x0013D38E File Offset: 0x0013B58E
	private void OnApplicationFocus(bool focusStatus)
	{
		this.isAppFocused = focusStatus;
	}

	// Token: 0x06003948 RID: 14664 RVA: 0x0013D397 File Offset: 0x0013B597
	public virtual string GetDeactivateSound()
	{
		return "Tile_Cancel";
	}

	// Token: 0x06003949 RID: 14665 RVA: 0x0013D3A0 File Offset: 0x0013B5A0
	public virtual void OnMouseMove(Vector3 cursor_pos)
	{
		if (this.visualizer == null || !this.isAppFocused)
		{
			return;
		}
		cursor_pos = Grid.CellToPosCBC(Grid.PosToCell(cursor_pos), this.visualizerLayer);
		cursor_pos.z += -0.15f;
		this.visualizer.transform.SetLocalPosition(cursor_pos);
	}

	// Token: 0x0600394A RID: 14666 RVA: 0x0013D3FC File Offset: 0x0013B5FC
	public virtual void OnKeyDown(KButtonEvent e)
	{
	}

	// Token: 0x0600394B RID: 14667 RVA: 0x0013D3FE File Offset: 0x0013B5FE
	public virtual void OnKeyUp(KButtonEvent e)
	{
	}

	// Token: 0x0600394C RID: 14668 RVA: 0x0013D400 File Offset: 0x0013B600
	public virtual void OnLeftClickDown(Vector3 cursor_pos)
	{
	}

	// Token: 0x0600394D RID: 14669 RVA: 0x0013D402 File Offset: 0x0013B602
	public virtual void OnLeftClickUp(Vector3 cursor_pos)
	{
	}

	// Token: 0x0600394E RID: 14670 RVA: 0x0013D404 File Offset: 0x0013B604
	public virtual void OnRightClickDown(Vector3 cursor_pos, KButtonEvent e)
	{
	}

	// Token: 0x0600394F RID: 14671 RVA: 0x0013D406 File Offset: 0x0013B606
	public virtual void OnRightClickUp(Vector3 cursor_pos)
	{
	}

	// Token: 0x06003950 RID: 14672 RVA: 0x0013D408 File Offset: 0x0013B608
	public virtual void OnFocus(bool focus)
	{
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(focus);
		}
		this.hasFocus = focus;
	}

	// Token: 0x06003951 RID: 14673 RVA: 0x0013D42C File Offset: 0x0013B62C
	protected Vector2 GetRegularizedPos(Vector2 input, bool minimize)
	{
		Vector3 vector = new Vector3(Grid.HalfCellSizeInMeters, Grid.HalfCellSizeInMeters, 0f);
		return Grid.CellToPosCCC(Grid.PosToCell(input), Grid.SceneLayer.Background) + (minimize ? (-vector) : vector);
	}

	// Token: 0x06003952 RID: 14674 RVA: 0x0013D474 File Offset: 0x0013B674
	protected Vector2 GetWorldRestrictedPosition(Vector2 input)
	{
		input.x = Mathf.Clamp(input.x, ClusterManager.Instance.activeWorld.minimumBounds.x, ClusterManager.Instance.activeWorld.maximumBounds.x);
		input.y = Mathf.Clamp(input.y, ClusterManager.Instance.activeWorld.minimumBounds.y, ClusterManager.Instance.activeWorld.maximumBounds.y);
		return input;
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x0013D4F8 File Offset: 0x0013B6F8
	protected void SetCursor(Texture2D new_cursor, Vector2 offset, CursorMode mode)
	{
		if (new_cursor != InterfaceTool.activeCursor && new_cursor != null)
		{
			InterfaceTool.activeCursor = new_cursor;
			try
			{
				Cursor.SetCursor(new_cursor, offset, mode);
				if (PlayerController.Instance.vim != null)
				{
					PlayerController.Instance.vim.SetCursor(new_cursor);
				}
			}
			catch (Exception ex)
			{
				string text = string.Format("SetCursor Failed new_cursor={0} offset={1} mode={2}", new_cursor, offset, mode);
				KCrashReporter.ReportErrorDevNotification("SetCursor Failed", ex.StackTrace, text);
			}
		}
	}

	// Token: 0x06003954 RID: 14676 RVA: 0x0013D58C File Offset: 0x0013B78C
	protected void UpdateHoverElements(List<KSelectable> hits)
	{
		if (this.hoverTextConfiguration != null)
		{
			this.hoverTextConfiguration.UpdateHoverElements(hits);
		}
	}

	// Token: 0x06003955 RID: 14677 RVA: 0x0013D5A8 File Offset: 0x0013B7A8
	public virtual void LateUpdate()
	{
		if (!this.populateHitsList)
		{
			this.UpdateHoverElements(null);
			return;
		}
		if (!this.isAppFocused)
		{
			return;
		}
		if (!Grid.IsValidCell(Grid.PosToCell(Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()))))
		{
			return;
		}
		this.hits.Clear();
		this.GetSelectablesUnderCursor(this.hits);
		KSelectable objectUnderCursor = this.GetObjectUnderCursor<KSelectable>(false, (KSelectable s) => s.GetComponent<KSelectable>().IsSelectable, null);
		this.UpdateHoverElements(this.hits);
		if (!this.hasFocus && this.hoverOverride == null)
		{
			this.ClearHover();
		}
		else if (objectUnderCursor != this.hover)
		{
			this.ClearHover();
			this.hover = objectUnderCursor;
			if (objectUnderCursor != null)
			{
				Game.Instance.Trigger(2095258329, objectUnderCursor.gameObject);
				objectUnderCursor.Hover(!this.playedSoundThisFrame);
				this.playedSoundThisFrame = true;
			}
		}
		this.playedSoundThisFrame = false;
	}

	// Token: 0x06003956 RID: 14678 RVA: 0x0013D6AC File Offset: 0x0013B8AC
	public void GetSelectablesUnderCursor(List<KSelectable> hits)
	{
		if (this.hoverOverride != null)
		{
			hits.Add(this.hoverOverride);
		}
		Camera main = Camera.main;
		Vector3 vector = new Vector3(KInputManager.GetMousePos().x, KInputManager.GetMousePos().y, -main.transform.GetPosition().z);
		Vector3 vector2 = main.ScreenToWorldPoint(vector);
		Vector2 vector3 = new Vector2(vector2.x, vector2.y);
		int num = Grid.PosToCell(vector2);
		if (!Grid.IsValidCell(num) || !Grid.IsVisible(num))
		{
			return;
		}
		Game.Instance.statusItemRenderer.GetIntersections(vector3, hits);
		ListPool<ScenePartitionerEntry, SelectTool>.PooledList pooledList = ListPool<ScenePartitionerEntry, SelectTool>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)vector3.x, (int)vector3.y, 1, 1, GameScenePartitioner.Instance.collisionLayer, pooledList);
		pooledList.Sort((ScenePartitionerEntry x, ScenePartitionerEntry y) => this.SortHoverCards(x, y));
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			KCollider2D kcollider2D = scenePartitionerEntry.obj as KCollider2D;
			if (!(kcollider2D == null) && kcollider2D.Intersects(new Vector2(vector3.x, vector3.y)))
			{
				KSelectable kselectable = kcollider2D.GetComponent<KSelectable>();
				if (kselectable == null)
				{
					kselectable = kcollider2D.GetComponentInParent<KSelectable>();
				}
				if (!(kselectable == null) && kselectable.isActiveAndEnabled && !hits.Contains(kselectable) && kselectable.IsSelectable)
				{
					hits.Add(kselectable);
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06003957 RID: 14679 RVA: 0x0013D850 File Offset: 0x0013BA50
	public void SetLinkCursor(bool set)
	{
		this.SetCursor(set ? Assets.GetTexture("cursor_hand") : this.cursor, set ? Vector2.zero : this.cursorOffset, CursorMode.Auto);
	}

	// Token: 0x06003958 RID: 14680 RVA: 0x0013D880 File Offset: 0x0013BA80
	protected T GetObjectUnderCursor<T>(bool cycleSelection, Func<T, bool> condition = null, Component previous_selection = null) where T : MonoBehaviour
	{
		this.intersections.Clear();
		this.GetObjectUnderCursor2D<T>(this.intersections, condition, this.layerMask);
		this.intersections.RemoveAll(new Predicate<InterfaceTool.Intersection>(InterfaceTool.is_component_null));
		if (this.intersections.Count <= 0)
		{
			this.prevIntersectionGroup.Clear();
			return default(T);
		}
		this.curIntersectionGroup.Clear();
		foreach (InterfaceTool.Intersection intersection in this.intersections)
		{
			this.curIntersectionGroup.Add(intersection.component);
		}
		if (!this.prevIntersectionGroup.Equals(this.curIntersectionGroup))
		{
			this.hitCycleCount = 0;
			this.prevIntersectionGroup = this.curIntersectionGroup;
		}
		this.intersections.Sort((InterfaceTool.Intersection a, InterfaceTool.Intersection b) => this.SortSelectables(a.component as KMonoBehaviour, b.component as KMonoBehaviour));
		int num = 0;
		if (cycleSelection)
		{
			num = this.hitCycleCount % this.intersections.Count;
			if (this.intersections[num].component != previous_selection || previous_selection == null)
			{
				num = 0;
				this.hitCycleCount = 0;
			}
			else
			{
				int num2 = this.hitCycleCount + 1;
				this.hitCycleCount = num2;
				num = num2 % this.intersections.Count;
			}
		}
		return this.intersections[num].component as T;
	}

	// Token: 0x06003959 RID: 14681 RVA: 0x0013DA00 File Offset: 0x0013BC00
	private void GetObjectUnderCursor2D<T>(List<InterfaceTool.Intersection> intersections, Func<T, bool> condition, int layer_mask) where T : MonoBehaviour
	{
		Camera main = Camera.main;
		Vector3 vector = new Vector3(KInputManager.GetMousePos().x, KInputManager.GetMousePos().y, -main.transform.GetPosition().z);
		Vector3 vector2 = main.ScreenToWorldPoint(vector);
		Vector2 vector3 = new Vector2(vector2.x, vector2.y);
		if (this.hoverOverride != null)
		{
			intersections.Add(new InterfaceTool.Intersection
			{
				component = this.hoverOverride,
				distance = -100f
			});
		}
		int num = Grid.PosToCell(vector2);
		if (Grid.IsValidCell(num) && Grid.IsVisible(num))
		{
			Game.Instance.statusItemRenderer.GetIntersections(vector3, intersections);
			ListPool<ScenePartitionerEntry, SelectTool>.PooledList pooledList = ListPool<ScenePartitionerEntry, SelectTool>.Allocate();
			int num2 = 0;
			int num3 = 0;
			Grid.CellToXY(num, out num2, out num3);
			GameScenePartitioner.Instance.GatherEntries(num2, num3, 1, 1, GameScenePartitioner.Instance.collisionLayer, pooledList);
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				KCollider2D kcollider2D = scenePartitionerEntry.obj as KCollider2D;
				if (!(kcollider2D == null) && kcollider2D.Intersects(new Vector2(vector2.x, vector2.y)))
				{
					T t = kcollider2D.GetComponent<T>();
					if (t == null)
					{
						t = kcollider2D.GetComponentInParent<T>();
					}
					if (!(t == null) && ((1 << t.gameObject.layer) & layer_mask) != 0 && !(t == null) && (condition == null || condition(t)))
					{
						float num4 = t.transform.GetPosition().z - vector2.z;
						bool flag = false;
						for (int i = 0; i < intersections.Count; i++)
						{
							InterfaceTool.Intersection intersection = intersections[i];
							if (intersection.component.gameObject == t.gameObject)
							{
								intersection.distance = Mathf.Min(intersection.distance, num4);
								intersections[i] = intersection;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							intersections.Add(new InterfaceTool.Intersection
							{
								component = t,
								distance = num4
							});
						}
					}
				}
			}
			pooledList.Recycle();
		}
	}

	// Token: 0x0600395A RID: 14682 RVA: 0x0013DCA4 File Offset: 0x0013BEA4
	private int SortSelectables(KMonoBehaviour x, KMonoBehaviour y)
	{
		if (x == null && y == null)
		{
			return 0;
		}
		if (x == null)
		{
			return -1;
		}
		if (y == null)
		{
			return 1;
		}
		int num = x.transform.GetPosition().z.CompareTo(y.transform.GetPosition().z);
		if (num != 0)
		{
			return num;
		}
		return x.GetInstanceID().CompareTo(y.GetInstanceID());
	}

	// Token: 0x0600395B RID: 14683 RVA: 0x0013DD1D File Offset: 0x0013BF1D
	public void SetHoverOverride(KSelectable hover_override)
	{
		this.hoverOverride = hover_override;
	}

	// Token: 0x0600395C RID: 14684 RVA: 0x0013DD28 File Offset: 0x0013BF28
	private int SortHoverCards(ScenePartitionerEntry x, ScenePartitionerEntry y)
	{
		KMonoBehaviour kmonoBehaviour = x.obj as KMonoBehaviour;
		KMonoBehaviour kmonoBehaviour2 = y.obj as KMonoBehaviour;
		return this.SortSelectables(kmonoBehaviour, kmonoBehaviour2);
	}

	// Token: 0x0600395D RID: 14685 RVA: 0x0013DD55 File Offset: 0x0013BF55
	private static bool is_component_null(InterfaceTool.Intersection intersection)
	{
		return !intersection.component;
	}

	// Token: 0x0600395E RID: 14686 RVA: 0x0013DD65 File Offset: 0x0013BF65
	protected void ClearHover()
	{
		if (this.hover != null)
		{
			KSelectable kselectable = this.hover;
			this.hover = null;
			kselectable.Unhover();
			Game.Instance.Trigger(-1201923725, null);
		}
	}

	// Token: 0x040025D8 RID: 9688
	private static Dictionary<global::Action, InterfaceToolConfig> interfaceConfigMap = null;

	// Token: 0x040025D9 RID: 9689
	private static List<InterfaceToolConfig> activeConfigs = new List<InterfaceToolConfig>();

	// Token: 0x040025DA RID: 9690
	public const float MaxClickDistance = 0.02f;

	// Token: 0x040025DB RID: 9691
	public const float DepthBias = -0.15f;

	// Token: 0x040025DC RID: 9692
	public GameObject visualizer;

	// Token: 0x040025DD RID: 9693
	public Grid.SceneLayer visualizerLayer = Grid.SceneLayer.Move;

	// Token: 0x040025DE RID: 9694
	public string placeSound;

	// Token: 0x040025DF RID: 9695
	protected bool populateHitsList;

	// Token: 0x040025E0 RID: 9696
	[NonSerialized]
	public bool hasFocus;

	// Token: 0x040025E1 RID: 9697
	[SerializeField]
	protected Texture2D cursor;

	// Token: 0x040025E2 RID: 9698
	public Vector2 cursorOffset = new Vector2(2f, 2f);

	// Token: 0x040025E3 RID: 9699
	public System.Action OnDeactivate;

	// Token: 0x040025E4 RID: 9700
	private static Texture2D activeCursor = null;

	// Token: 0x040025E5 RID: 9701
	private static HashedString toolActivatedViewMode = OverlayModes.None.ID;

	// Token: 0x040025E6 RID: 9702
	protected HashedString viewMode = OverlayModes.None.ID;

	// Token: 0x040025E7 RID: 9703
	private HoverTextConfiguration hoverTextConfiguration;

	// Token: 0x040025E8 RID: 9704
	private KSelectable hoverOverride;

	// Token: 0x040025E9 RID: 9705
	public KSelectable hover;

	// Token: 0x040025EA RID: 9706
	protected int layerMask;

	// Token: 0x040025EB RID: 9707
	protected SelectMarker selectMarker;

	// Token: 0x040025EC RID: 9708
	private List<RaycastResult> castResults = new List<RaycastResult>();

	// Token: 0x040025ED RID: 9709
	private bool isAppFocused = true;

	// Token: 0x040025EE RID: 9710
	private List<KSelectable> hits = new List<KSelectable>();

	// Token: 0x040025EF RID: 9711
	protected bool playedSoundThisFrame;

	// Token: 0x040025F0 RID: 9712
	private List<InterfaceTool.Intersection> intersections = new List<InterfaceTool.Intersection>();

	// Token: 0x040025F1 RID: 9713
	private HashSet<Component> prevIntersectionGroup = new HashSet<Component>();

	// Token: 0x040025F2 RID: 9714
	private HashSet<Component> curIntersectionGroup = new HashSet<Component>();

	// Token: 0x040025F3 RID: 9715
	private int hitCycleCount;

	// Token: 0x02001526 RID: 5414
	public struct Intersection
	{
		// Token: 0x040065CD RID: 26061
		public MonoBehaviour component;

		// Token: 0x040065CE RID: 26062
		public float distance;
	}
}
