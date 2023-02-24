using System;
using System.Collections.Generic;
using FMOD.Studio;
using Rendering;
using STRINGS;
using UnityEngine;

// Token: 0x020007BF RID: 1983
public class BuildTool : DragTool
{
	// Token: 0x0600387D RID: 14461 RVA: 0x00139486 File Offset: 0x00137686
	public static void DestroyInstance()
	{
		BuildTool.Instance = null;
	}

	// Token: 0x0600387E RID: 14462 RVA: 0x0013948E File Offset: 0x0013768E
	protected override void OnPrefabInit()
	{
		BuildTool.Instance = this;
		this.tooltip = base.GetComponent<ToolTip>();
		this.buildingCount = UnityEngine.Random.Range(1, 14);
		this.canChangeDragAxis = false;
	}

	// Token: 0x0600387F RID: 14463 RVA: 0x001394B8 File Offset: 0x001376B8
	protected override void OnActivateTool()
	{
		this.lastDragCell = -1;
		if (this.visualizer != null)
		{
			this.ClearTilePreview();
			UnityEngine.Object.Destroy(this.visualizer);
		}
		this.active = true;
		base.OnActivateTool();
		Vector3 vector = base.ClampPositionToWorld(PlayerController.GetCursorPos(KInputManager.GetMousePos()), ClusterManager.Instance.activeWorld);
		this.visualizer = GameUtil.KInstantiate(this.def.BuildingPreview, vector, Grid.SceneLayer.Ore, null, LayerMask.NameToLayer("Place"));
		KBatchedAnimController component = this.visualizer.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.visibilityType = KAnimControllerBase.VisibilityType.Always;
			component.isMovable = true;
			component.Offset = this.def.GetVisualizerOffset();
			component.name = component.GetComponent<KPrefabID>().GetDebugName() + "_visualizer";
		}
		if (!this.facadeID.IsNullOrWhiteSpace() && this.facadeID != "DEFAULT_FACADE")
		{
			this.visualizer.GetComponent<BuildingFacade>().ApplyBuildingFacade(Db.GetBuildingFacades().Get(this.facadeID));
		}
		Rotatable component2 = this.visualizer.GetComponent<Rotatable>();
		if (component2 != null)
		{
			this.buildingOrientation = this.def.InitialOrientation;
			component2.SetOrientation(this.buildingOrientation);
		}
		this.visualizer.SetActive(true);
		this.UpdateVis(vector);
		base.GetComponent<BuildToolHoverTextCard>().currentDef = this.def;
		ResourceRemainingDisplayScreen.instance.ActivateDisplay(this.visualizer);
		if (component == null)
		{
			this.visualizer.SetLayerRecursively(LayerMask.NameToLayer("Place"));
		}
		else
		{
			component.SetLayer(LayerMask.NameToLayer("Place"));
		}
		GridCompositor.Instance.ToggleMajor(true);
	}

	// Token: 0x06003880 RID: 14464 RVA: 0x00139668 File Offset: 0x00137868
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		this.lastDragCell = -1;
		if (!this.active)
		{
			return;
		}
		this.active = false;
		GridCompositor.Instance.ToggleMajor(false);
		this.buildingOrientation = Orientation.Neutral;
		this.HideToolTip();
		ResourceRemainingDisplayScreen.instance.DeactivateDisplay();
		this.ClearTilePreview();
		UnityEngine.Object.Destroy(this.visualizer);
		if (new_tool == SelectTool.Instance)
		{
			Game.Instance.Trigger(-1190690038, null);
		}
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x001396E3 File Offset: 0x001378E3
	public void Activate(BuildingDef def, IList<Tag> selected_elements)
	{
		this.selectedElements = selected_elements;
		this.def = def;
		this.viewMode = def.ViewMode;
		ResourceRemainingDisplayScreen.instance.SetResources(selected_elements, def.CraftRecipe);
		PlayerController.Instance.ActivateTool(this);
		this.OnActivateTool();
	}

	// Token: 0x06003882 RID: 14466 RVA: 0x00139721 File Offset: 0x00137921
	public void Activate(BuildingDef def, IList<Tag> selected_elements, string facadeID)
	{
		this.facadeID = facadeID;
		this.Activate(def, selected_elements);
	}

	// Token: 0x06003883 RID: 14467 RVA: 0x00139732 File Offset: 0x00137932
	public void Deactivate()
	{
		this.selectedElements = null;
		SelectTool.Instance.Activate();
		this.def = null;
		this.facadeID = null;
		ResourceRemainingDisplayScreen.instance.DeactivateDisplay();
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x06003884 RID: 14468 RVA: 0x0013975D File Offset: 0x0013795D
	public int GetLastCell
	{
		get
		{
			return this.lastCell;
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06003885 RID: 14469 RVA: 0x00139765 File Offset: 0x00137965
	public Orientation GetBuildingOrientation
	{
		get
		{
			return this.buildingOrientation;
		}
	}

	// Token: 0x06003886 RID: 14470 RVA: 0x00139770 File Offset: 0x00137970
	private void ClearTilePreview()
	{
		if (Grid.IsValidBuildingCell(this.lastCell) && this.def.IsTilePiece)
		{
			GameObject gameObject = Grid.Objects[this.lastCell, (int)this.def.TileLayer];
			if (this.visualizer == gameObject)
			{
				Grid.Objects[this.lastCell, (int)this.def.TileLayer] = null;
			}
			if (this.def.isKAnimTile)
			{
				GameObject gameObject2 = null;
				if (this.def.ReplacementLayer != ObjectLayer.NumLayers)
				{
					gameObject2 = Grid.Objects[this.lastCell, (int)this.def.ReplacementLayer];
				}
				if ((gameObject == null || gameObject.GetComponent<Constructable>() == null) && (gameObject2 == null || gameObject2 == this.visualizer))
				{
					World.Instance.blockTileRenderer.RemoveBlock(this.def, false, SimHashes.Void, this.lastCell);
					World.Instance.blockTileRenderer.RemoveBlock(this.def, true, SimHashes.Void, this.lastCell);
					TileVisualizer.RefreshCell(this.lastCell, this.def.TileLayer, this.def.ReplacementLayer);
				}
			}
		}
	}

	// Token: 0x06003887 RID: 14471 RVA: 0x001398B1 File Offset: 0x00137AB1
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		cursorPos = base.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		this.UpdateVis(cursorPos);
	}

	// Token: 0x06003888 RID: 14472 RVA: 0x001398D4 File Offset: 0x00137AD4
	private void UpdateVis(Vector3 pos)
	{
		string text;
		bool flag = this.def.IsValidPlaceLocation(this.visualizer, pos, this.buildingOrientation, out text);
		bool flag2 = this.def.IsValidReplaceLocation(pos, this.buildingOrientation, this.def.ReplacementLayer, this.def.ObjectLayer);
		flag = flag || flag2;
		if (this.visualizer != null)
		{
			Color color = Color.white;
			float num = 0f;
			if (!flag)
			{
				color = Color.red;
				num = 1f;
			}
			this.SetColor(this.visualizer, color, num);
		}
		int num2 = Grid.PosToCell(pos);
		if (this.def != null)
		{
			Vector3 vector = Grid.CellToPosCBC(num2, this.def.SceneLayer);
			this.visualizer.transform.SetPosition(vector);
			base.transform.SetPosition(vector - Vector3.up * 0.5f);
			if (this.def.IsTilePiece)
			{
				this.ClearTilePreview();
				if (Grid.IsValidBuildingCell(num2))
				{
					GameObject gameObject = Grid.Objects[num2, (int)this.def.TileLayer];
					if (gameObject == null)
					{
						Grid.Objects[num2, (int)this.def.TileLayer] = this.visualizer;
					}
					if (this.def.isKAnimTile)
					{
						GameObject gameObject2 = null;
						if (this.def.ReplacementLayer != ObjectLayer.NumLayers)
						{
							gameObject2 = Grid.Objects[num2, (int)this.def.ReplacementLayer];
						}
						if (gameObject == null || (gameObject.GetComponent<Constructable>() == null && gameObject2 == null))
						{
							TileVisualizer.RefreshCell(num2, this.def.TileLayer, this.def.ReplacementLayer);
							if (this.def.BlockTileAtlas != null)
							{
								int num3 = LayerMask.NameToLayer("Overlay");
								BlockTileRenderer blockTileRenderer = World.Instance.blockTileRenderer;
								blockTileRenderer.SetInvalidPlaceCell(num2, !flag);
								if (this.lastCell != num2)
								{
									blockTileRenderer.SetInvalidPlaceCell(this.lastCell, false);
								}
								blockTileRenderer.AddBlock(num3, this.def, flag2, SimHashes.Void, num2);
							}
						}
					}
				}
			}
			if (this.lastCell != num2)
			{
				this.lastCell = num2;
			}
		}
	}

	// Token: 0x06003889 RID: 14473 RVA: 0x00139B18 File Offset: 0x00137D18
	public PermittedRotations? GetPermittedRotations()
	{
		if (this.visualizer == null)
		{
			return null;
		}
		Rotatable component = this.visualizer.GetComponent<Rotatable>();
		if (component == null)
		{
			return null;
		}
		return new PermittedRotations?(component.permittedRotations);
	}

	// Token: 0x0600388A RID: 14474 RVA: 0x00139B67 File Offset: 0x00137D67
	public bool CanRotate()
	{
		return !(this.visualizer == null) && !(this.visualizer.GetComponent<Rotatable>() == null);
	}

	// Token: 0x0600388B RID: 14475 RVA: 0x00139B90 File Offset: 0x00137D90
	public void TryRotate()
	{
		if (this.visualizer == null)
		{
			return;
		}
		Rotatable component = this.visualizer.GetComponent<Rotatable>();
		if (component == null)
		{
			return;
		}
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Rotate", false));
		this.buildingOrientation = component.Rotate();
		if (Grid.IsValidBuildingCell(this.lastCell))
		{
			Vector3 vector = Grid.CellToPosCCC(this.lastCell, Grid.SceneLayer.Building);
			this.UpdateVis(vector);
		}
		if (base.Dragging && this.lastDragCell != -1)
		{
			this.TryBuild(this.lastDragCell);
		}
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x00139C1D File Offset: 0x00137E1D
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.RotateBuilding))
		{
			this.TryRotate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600388D RID: 14477 RVA: 0x00139C3A File Offset: 0x00137E3A
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		this.TryBuild(cell);
	}

	// Token: 0x0600388E RID: 14478 RVA: 0x00139C44 File Offset: 0x00137E44
	private void TryBuild(int cell)
	{
		if (this.visualizer == null)
		{
			return;
		}
		if (cell == this.lastDragCell && this.buildingOrientation == this.lastDragOrientation)
		{
			return;
		}
		if (Grid.PosToCell(this.visualizer) != cell)
		{
			if (this.def.BuildingComplete.GetComponent<LogicPorts>())
			{
				return;
			}
			if (this.def.BuildingComplete.GetComponent<LogicGateBase>())
			{
				return;
			}
		}
		this.lastDragCell = cell;
		this.lastDragOrientation = this.buildingOrientation;
		this.ClearTilePreview();
		Vector3 vector = Grid.CellToPosCBC(cell, Grid.SceneLayer.Building);
		GameObject gameObject = null;
		PlanScreen.Instance.LastSelectedBuildingFacade = this.facadeID;
		bool flag = DebugHandler.InstantBuildMode || (Game.Instance.SandboxModeActive && SandboxToolParameterMenu.instance.settings.InstantBuild);
		string text;
		if (!flag)
		{
			gameObject = this.def.TryPlace(this.visualizer, vector, this.buildingOrientation, this.selectedElements, this.facadeID, 0);
		}
		else if (this.def.IsValidBuildLocation(this.visualizer, vector, this.buildingOrientation, false) && this.def.IsValidPlaceLocation(this.visualizer, vector, this.buildingOrientation, out text))
		{
			gameObject = this.def.Build(cell, this.buildingOrientation, null, this.selectedElements, 293.15f, this.facadeID, false, GameClock.Instance.GetTime());
		}
		if (gameObject == null && this.def.ReplacementLayer != ObjectLayer.NumLayers)
		{
			GameObject replacementCandidate = this.def.GetReplacementCandidate(cell);
			if (replacementCandidate != null && !this.def.IsReplacementLayerOccupied(cell))
			{
				BuildingComplete component = replacementCandidate.GetComponent<BuildingComplete>();
				if (component != null && component.Def.Replaceable && this.def.CanReplace(replacementCandidate) && (component.Def != this.def || this.selectedElements[0] != replacementCandidate.GetComponent<PrimaryElement>().Element.tag))
				{
					string text2;
					if (!flag)
					{
						gameObject = this.def.TryReplaceTile(this.visualizer, vector, this.buildingOrientation, this.selectedElements, this.facadeID, 0);
						Grid.Objects[cell, (int)this.def.ReplacementLayer] = gameObject;
					}
					else if (this.def.IsValidBuildLocation(this.visualizer, vector, this.buildingOrientation, true) && this.def.IsValidPlaceLocation(this.visualizer, vector, this.buildingOrientation, true, out text2))
					{
						gameObject = this.InstantBuildReplace(cell, vector, replacementCandidate);
					}
				}
			}
		}
		this.PostProcessBuild(flag, vector, gameObject);
	}

	// Token: 0x0600388F RID: 14479 RVA: 0x00139EF4 File Offset: 0x001380F4
	private GameObject InstantBuildReplace(int cell, Vector3 pos, GameObject tile)
	{
		if (tile.GetComponent<SimCellOccupier>() == null)
		{
			UnityEngine.Object.Destroy(tile);
			return this.def.Build(cell, this.buildingOrientation, null, this.selectedElements, 293.15f, this.facadeID, false, GameClock.Instance.GetTime());
		}
		tile.GetComponent<SimCellOccupier>().DestroySelf(delegate
		{
			UnityEngine.Object.Destroy(tile);
			GameObject gameObject = this.def.Build(cell, this.buildingOrientation, null, this.selectedElements, 293.15f, this.facadeID, false, GameClock.Instance.GetTime());
			this.PostProcessBuild(true, pos, gameObject);
		});
		return null;
	}

	// Token: 0x06003890 RID: 14480 RVA: 0x00139F94 File Offset: 0x00138194
	private void PostProcessBuild(bool instantBuild, Vector3 pos, GameObject builtItem)
	{
		if (builtItem == null)
		{
			return;
		}
		if (!instantBuild)
		{
			Prioritizable component = builtItem.GetComponent<Prioritizable>();
			if (component != null)
			{
				if (BuildMenu.Instance != null)
				{
					component.SetMasterPriority(BuildMenu.Instance.GetBuildingPriority());
				}
				if (PlanScreen.Instance != null)
				{
					component.SetMasterPriority(PlanScreen.Instance.GetBuildingPriority());
				}
			}
		}
		if (this.def.MaterialsAvailable(this.selectedElements, ClusterManager.Instance.activeWorld) || DebugHandler.InstantBuildMode)
		{
			this.placeSound = GlobalAssets.GetSound("Place_Building_" + this.def.AudioSize, false);
			if (this.placeSound != null)
			{
				this.buildingCount = this.buildingCount % 14 + 1;
				Vector3 vector = pos;
				vector.z = 0f;
				EventInstance eventInstance = SoundEvent.BeginOneShot(this.placeSound, vector, 1f, false);
				if (this.def.AudioSize == "small")
				{
					eventInstance.setParameterByName("tileCount", (float)this.buildingCount, false);
				}
				SoundEvent.EndOneShot(eventInstance);
			}
		}
		else
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, UI.TOOLTIPS.NOMATERIAL, null, pos, 1.5f, false, false);
		}
		Rotatable component2 = builtItem.GetComponent<Rotatable>();
		if (component2 != null)
		{
			component2.SetOrientation(this.buildingOrientation);
		}
		if (this.def.OnePerWorld)
		{
			PlayerController.Instance.ActivateTool(SelectTool.Instance);
		}
	}

	// Token: 0x06003891 RID: 14481 RVA: 0x0013A112 File Offset: 0x00138312
	protected override DragTool.Mode GetMode()
	{
		return DragTool.Mode.Brush;
	}

	// Token: 0x06003892 RID: 14482 RVA: 0x0013A118 File Offset: 0x00138318
	private void SetColor(GameObject root, Color c, float strength)
	{
		KBatchedAnimController component = root.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.TintColour = c;
		}
	}

	// Token: 0x06003893 RID: 14483 RVA: 0x0013A141 File Offset: 0x00138341
	private void ShowToolTip()
	{
		ToolTipScreen.Instance.SetToolTip(this.tooltip);
	}

	// Token: 0x06003894 RID: 14484 RVA: 0x0013A153 File Offset: 0x00138353
	private void HideToolTip()
	{
		ToolTipScreen.Instance.ClearToolTip(this.tooltip);
	}

	// Token: 0x06003895 RID: 14485 RVA: 0x0013A168 File Offset: 0x00138368
	public void Update()
	{
		if (this.active)
		{
			KBatchedAnimController component = this.visualizer.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				component.SetLayer(LayerMask.NameToLayer("Place"));
			}
		}
	}

	// Token: 0x06003896 RID: 14486 RVA: 0x0013A1A2 File Offset: 0x001383A2
	public override string GetDeactivateSound()
	{
		return "HUD_Click_Deselect";
	}

	// Token: 0x06003897 RID: 14487 RVA: 0x0013A1A9 File Offset: 0x001383A9
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
	}

	// Token: 0x06003898 RID: 14488 RVA: 0x0013A1B2 File Offset: 0x001383B2
	public override void OnLeftClickUp(Vector3 cursor_pos)
	{
		base.OnLeftClickUp(cursor_pos);
	}

	// Token: 0x06003899 RID: 14489 RVA: 0x0013A1BC File Offset: 0x001383BC
	public void SetToolOrientation(Orientation orientation)
	{
		if (this.visualizer != null)
		{
			Rotatable component = this.visualizer.GetComponent<Rotatable>();
			if (component != null)
			{
				this.buildingOrientation = orientation;
				component.SetOrientation(orientation);
				if (Grid.IsValidBuildingCell(this.lastCell))
				{
					Vector3 vector = Grid.CellToPosCCC(this.lastCell, Grid.SceneLayer.Building);
					this.UpdateVis(vector);
				}
				if (base.Dragging && this.lastDragCell != -1)
				{
					this.TryBuild(this.lastDragCell);
				}
			}
		}
	}

	// Token: 0x04002599 RID: 9625
	[SerializeField]
	private TextStyleSetting tooltipStyle;

	// Token: 0x0400259A RID: 9626
	private int lastCell = -1;

	// Token: 0x0400259B RID: 9627
	private int lastDragCell = -1;

	// Token: 0x0400259C RID: 9628
	private Orientation lastDragOrientation;

	// Token: 0x0400259D RID: 9629
	private IList<Tag> selectedElements;

	// Token: 0x0400259E RID: 9630
	private BuildingDef def;

	// Token: 0x0400259F RID: 9631
	private Orientation buildingOrientation;

	// Token: 0x040025A0 RID: 9632
	private string facadeID;

	// Token: 0x040025A1 RID: 9633
	private ToolTip tooltip;

	// Token: 0x040025A2 RID: 9634
	public static BuildTool Instance;

	// Token: 0x040025A3 RID: 9635
	private bool active;

	// Token: 0x040025A4 RID: 9636
	private int buildingCount;
}
