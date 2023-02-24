using System;
using System.Collections;
using System.Collections.Generic;
using TemplateClasses;
using UnityEngine;

// Token: 0x020007E2 RID: 2018
public class StampTool : InterfaceTool
{
	// Token: 0x06003A17 RID: 14871 RVA: 0x00141114 File Offset: 0x0013F314
	public static void DestroyInstance()
	{
		StampTool.Instance = null;
		StampTool.previewPool = null;
		StampTool.placerPool = null;
		StampTool.previewPoolTransform = null;
		StampTool.placerPoolTransform = null;
	}

	// Token: 0x06003A18 RID: 14872 RVA: 0x00141134 File Offset: 0x0013F334
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		StampTool.Instance = this;
		StampTool.placerPool = new GameObjectPool(new Func<GameObject>(StampTool.InstantiatePlacer), 0);
		StampTool.previewPool = new HashMapObjectPool<Tag, Building>(new Func<Tag, Building>(StampTool.InstantiatePreview), 0);
	}

	// Token: 0x06003A19 RID: 14873 RVA: 0x00141170 File Offset: 0x0013F370
	private void Update()
	{
		this.RefreshPreview(Grid.PosToCell(this.GetCursorPos()));
	}

	// Token: 0x06003A1A RID: 14874 RVA: 0x00141184 File Offset: 0x0013F384
	public void Activate(TemplateContainer template, bool SelectAffected = false, bool DeactivateOnStamp = false)
	{
		this.selectAffected = SelectAffected;
		this.deactivateOnStamp = DeactivateOnStamp;
		if (this.stampTemplate == template || template == null || template.cells == null)
		{
			return;
		}
		this.stampTemplate = template;
		PlayerController.Instance.ActivateTool(this);
		base.StartCoroutine(this.InitializePlacementVisual());
	}

	// Token: 0x06003A1B RID: 14875 RVA: 0x001411D3 File Offset: 0x0013F3D3
	private Vector3 GetCursorPos()
	{
		return PlayerController.GetCursorPos(KInputManager.GetMousePos());
	}

	// Token: 0x06003A1C RID: 14876 RVA: 0x001411DF File Offset: 0x0013F3DF
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		this.Stamp(cursor_pos);
	}

	// Token: 0x06003A1D RID: 14877 RVA: 0x001411F4 File Offset: 0x0013F3F4
	private void Stamp(Vector2 pos)
	{
		if (!this.ready)
		{
			return;
		}
		int num = Grid.OffsetCell(Grid.PosToCell(pos), Mathf.FloorToInt(-this.stampTemplate.info.size.X / 2f), 0);
		int num2 = Grid.OffsetCell(Grid.PosToCell(pos), Mathf.FloorToInt(this.stampTemplate.info.size.X / 2f), 0);
		int num3 = Grid.OffsetCell(Grid.PosToCell(pos), 0, 1 + Mathf.FloorToInt(-this.stampTemplate.info.size.Y / 2f));
		int num4 = Grid.OffsetCell(Grid.PosToCell(pos), 0, 1 + Mathf.FloorToInt(this.stampTemplate.info.size.Y / 2f));
		if (!Grid.IsValidBuildingCell(num) || !Grid.IsValidBuildingCell(num2) || !Grid.IsValidBuildingCell(num4) || !Grid.IsValidBuildingCell(num3))
		{
			return;
		}
		this.ready = false;
		bool pauseOnComplete = SpeedControlScreen.Instance.IsPaused;
		if (SpeedControlScreen.Instance.IsPaused)
		{
			SpeedControlScreen.Instance.Unpause(true);
		}
		if (this.stampTemplate.cells != null)
		{
			for (int i = 0; i < this.buildingPreviews.Count; i++)
			{
				StampTool.ClearTilePreview(this.buildingPreviews[i]);
			}
			List<GameObject> list = new List<GameObject>();
			for (int j = 0; j < this.stampTemplate.cells.Count; j++)
			{
				for (int k = 0; k < 34; k++)
				{
					GameObject gameObject = Grid.Objects[Grid.XYToCell((int)(pos.x + (float)this.stampTemplate.cells[j].location_x), (int)(pos.y + (float)this.stampTemplate.cells[j].location_y)), k];
					if (gameObject != null && !list.Contains(gameObject))
					{
						list.Add(gameObject);
					}
				}
			}
			if (list != null)
			{
				foreach (GameObject gameObject2 in list)
				{
					if (gameObject2 != null)
					{
						Util.KDestroyGameObject(gameObject2);
					}
				}
			}
		}
		TemplateLoader.Stamp(this.stampTemplate, pos, delegate
		{
			this.CompleteStamp(pauseOnComplete);
		});
		if (this.selectAffected)
		{
			DebugBaseTemplateButton.Instance.ClearSelection();
			if (this.stampTemplate.cells != null)
			{
				for (int l = 0; l < this.stampTemplate.cells.Count; l++)
				{
					DebugBaseTemplateButton.Instance.AddToSelection(Grid.XYToCell((int)(pos.x + (float)this.stampTemplate.cells[l].location_x), (int)(pos.y + (float)this.stampTemplate.cells[l].location_y)));
				}
			}
		}
		if (this.deactivateOnStamp)
		{
			base.DeactivateTool(null);
		}
	}

	// Token: 0x06003A1E RID: 14878 RVA: 0x00141524 File Offset: 0x0013F724
	private void CompleteStamp(bool pause)
	{
		if (pause)
		{
			SpeedControlScreen.Instance.Pause(true, false);
		}
		this.ready = true;
		this.OnDeactivateTool(null);
	}

	// Token: 0x06003A1F RID: 14879 RVA: 0x00141543 File Offset: 0x0013F743
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		if (base.gameObject.activeSelf)
		{
			return;
		}
		this.ReleasePlacementVisual();
		this.placementCell = Grid.InvalidCell;
		this.stampTemplate = null;
	}

	// Token: 0x06003A20 RID: 14880 RVA: 0x00141572 File Offset: 0x0013F772
	private IEnumerator InitializePlacementVisual()
	{
		this.ReleasePlacementVisual();
		this.rootCellPlacer = StampTool.placerPool.GetInstance();
		for (int i = 0; i < this.stampTemplate.cells.Count; i++)
		{
			Cell cell = this.stampTemplate.cells[i];
			if (cell.location_x != 0 || cell.location_y != 0)
			{
				GameObject instance = StampTool.placerPool.GetInstance();
				instance.transform.SetParent(this.rootCellPlacer.transform);
				instance.transform.localPosition = new Vector3((float)cell.location_x, (float)cell.location_y);
				instance.SetActive(true);
				this.childCellPlacers.Add(instance);
			}
		}
		if (this.stampTemplate.buildings != null)
		{
			yield return this.InitializeBuildingPlacementVisuals();
		}
		yield break;
	}

	// Token: 0x06003A21 RID: 14881 RVA: 0x00141581 File Offset: 0x0013F781
	private IEnumerator InitializeBuildingPlacementVisuals()
	{
		foreach (Prefab prefab in this.stampTemplate.buildings)
		{
			Building instance = StampTool.previewPool.GetInstance(prefab.id);
			Rotatable component = instance.GetComponent<Rotatable>();
			if (component != null)
			{
				component.SetOrientation(prefab.rotationOrientation);
			}
			instance.transform.SetParent(this.rootCellPlacer.transform);
			instance.transform.SetLocalPosition(new Vector2((float)prefab.location_x, (float)prefab.location_y));
			instance.gameObject.SetActive(true);
			this.buildingPreviews.Add(instance);
		}
		yield return null;
		for (int i = 0; i < this.stampTemplate.buildings.Count; i++)
		{
			Prefab prefab2 = this.stampTemplate.buildings[i];
			Building building = this.buildingPreviews[i];
			string text = "";
			if ((prefab2.connections & 1) != 0)
			{
				text += "L";
			}
			if ((prefab2.connections & 2) != 0)
			{
				text += "R";
			}
			if ((prefab2.connections & 4) != 0)
			{
				text += "U";
			}
			if ((prefab2.connections & 8) != 0)
			{
				text += "D";
			}
			if (text == "")
			{
				text = "None";
			}
			KBatchedAnimController component2 = building.GetComponent<KBatchedAnimController>();
			if (component2 != null && component2.HasAnimation(text))
			{
				string text2 = text + "_place";
				bool flag = component2.HasAnimation(text2);
				component2.Play(flag ? text2 : text, KAnim.PlayMode.Loop, 1f, 0f);
			}
		}
		yield break;
	}

	// Token: 0x06003A22 RID: 14882 RVA: 0x00141590 File Offset: 0x0013F790
	private void ReleasePlacementVisual()
	{
		if (this.rootCellPlacer == null)
		{
			return;
		}
		this.rootCellPlacer.SetActive(false);
		for (int i = this.childCellPlacers.Count - 1; i >= 0; i--)
		{
			GameObject gameObject = this.childCellPlacers[i];
			gameObject.transform.SetParent(StampTool.placerPoolTransform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.SetActive(false);
			StampTool.placerPool.ReleaseInstance(gameObject);
			this.childCellPlacers.RemoveAt(i);
		}
		for (int j = this.buildingPreviews.Count - 1; j >= 0; j--)
		{
			Building building = this.buildingPreviews[j];
			StampTool.ClearTilePreview(building);
			building.transform.SetParent(StampTool.previewPoolTransform);
			building.transform.localPosition = Vector3.zero;
			building.gameObject.SetActive(false);
			StampTool.previewPool.ReleaseInstance(building.Def.PrefabID, building);
			this.buildingPreviews.RemoveAt(j);
		}
		this.rootCellPlacer.transform.SetParent(StampTool.placerPoolTransform);
		this.rootCellPlacer.transform.position = Vector3.zero;
		StampTool.placerPool.ReleaseInstance(this.rootCellPlacer);
		this.rootCellPlacer = null;
	}

	// Token: 0x06003A23 RID: 14883 RVA: 0x001416DC File Offset: 0x0013F8DC
	private static void ClearTilePreview(Building b)
	{
		int num = Grid.PosToCell(b.transform.position);
		if (!b.Def.IsTilePiece || !Grid.IsValidBuildingCell(num))
		{
			return;
		}
		if (b.gameObject == Grid.Objects[num, (int)b.Def.TileLayer])
		{
			Grid.Objects[num, (int)b.Def.TileLayer] = null;
		}
		if (!b.Def.isKAnimTile)
		{
			return;
		}
		if (b.Def.BlockTileAtlas != null)
		{
			World.Instance.blockTileRenderer.RemoveBlock(b.Def, false, SimHashes.Void, num);
		}
		TileVisualizer.RefreshCell(num, b.Def.TileLayer, ObjectLayer.NumLayers);
	}

	// Token: 0x06003A24 RID: 14884 RVA: 0x0014179C File Offset: 0x0013F99C
	private static void UpdateTileRendering(int newCell, Building b)
	{
		StampTool.ClearTilePreview(b);
		if (!b.Def.IsTilePiece || !Grid.IsValidBuildingCell(newCell))
		{
			return;
		}
		if (Grid.Objects[newCell, (int)b.Def.TileLayer] == null)
		{
			Grid.Objects[newCell, (int)b.Def.TileLayer] = b.gameObject;
		}
		if (!b.Def.isKAnimTile)
		{
			return;
		}
		if (b.Def.BlockTileAtlas != null)
		{
			World.Instance.blockTileRenderer.AddBlock(b.gameObject.layer, b.Def, false, SimHashes.Void, newCell);
		}
		TileVisualizer.RefreshCell(newCell, b.Def.TileLayer, ObjectLayer.NumLayers);
	}

	// Token: 0x06003A25 RID: 14885 RVA: 0x0014185C File Offset: 0x0013FA5C
	public void RefreshPreview(int new_placement_cell)
	{
		if (Grid.IsValidCell(new_placement_cell) && new_placement_cell != this.placementCell)
		{
			for (int i = 0; i < this.buildingPreviews.Count; i++)
			{
				Building building = this.buildingPreviews[i];
				Vector3 localPosition = building.transform.localPosition;
				StampTool.UpdateTileRendering(Grid.OffsetCell(new_placement_cell, (int)localPosition.x, (int)localPosition.y), building);
			}
			this.placementCell = new_placement_cell;
			this.rootCellPlacer.transform.SetPosition(Grid.CellToPosCBC(this.placementCell, this.visualizerLayer));
			this.rootCellPlacer.SetActive(true);
		}
	}

	// Token: 0x06003A26 RID: 14886 RVA: 0x001418FC File Offset: 0x0013FAFC
	private static Building InstantiatePreview(Tag previewId)
	{
		GameObject gameObject = Assets.TryGetPrefab(previewId);
		if (gameObject == null)
		{
			return null;
		}
		Building component = gameObject.GetComponent<Building>();
		if (component == null)
		{
			return null;
		}
		if (StampTool.previewPoolTransform == null)
		{
			StampTool.previewPoolTransform = new GameObject("Preview Pool").transform;
		}
		GameObject gameObject2 = component.Def.BuildingPreview;
		if (gameObject2 == null)
		{
			gameObject2 = BuildingLoader.Instance.CreateBuildingPreview(component.Def);
		}
		int num = LayerMask.NameToLayer("Place");
		Building component2 = GameUtil.KInstantiate(gameObject2, Vector3.zero, Grid.SceneLayer.Ore, null, num).GetComponent<Building>();
		KBatchedAnimController component3 = component2.GetComponent<KBatchedAnimController>();
		if (component3 != null)
		{
			component3.visibilityType = KAnimControllerBase.VisibilityType.Always;
			component3.isMovable = true;
			component3.Offset = component.Def.GetVisualizerOffset();
			component3.name = component3.GetComponent<KPrefabID>().GetDebugName() + "_visualizer";
			component3.TintColour = Color.white;
			component3.SetLayer(num);
		}
		component2.transform.SetParent(StampTool.previewPoolTransform);
		component2.gameObject.SetActive(false);
		return component2;
	}

	// Token: 0x06003A27 RID: 14887 RVA: 0x00141A1C File Offset: 0x0013FC1C
	private static GameObject InstantiatePlacer()
	{
		if (StampTool.placerPoolTransform == null)
		{
			StampTool.placerPoolTransform = new GameObject("Stamp Placer Pool").transform;
		}
		GameObject gameObject = Util.KInstantiate(StampTool.Instance.PlacerPrefab, StampTool.placerPoolTransform.gameObject, null);
		gameObject.SetActive(false);
		return gameObject;
	}

	// Token: 0x0400262C RID: 9772
	public static StampTool Instance;

	// Token: 0x0400262D RID: 9773
	private static HashMapObjectPool<Tag, Building> previewPool;

	// Token: 0x0400262E RID: 9774
	private static GameObjectPool placerPool;

	// Token: 0x0400262F RID: 9775
	private static Transform previewPoolTransform;

	// Token: 0x04002630 RID: 9776
	private static Transform placerPoolTransform;

	// Token: 0x04002631 RID: 9777
	public TemplateContainer stampTemplate;

	// Token: 0x04002632 RID: 9778
	public GameObject PlacerPrefab;

	// Token: 0x04002633 RID: 9779
	private bool ready = true;

	// Token: 0x04002634 RID: 9780
	private int placementCell = Grid.InvalidCell;

	// Token: 0x04002635 RID: 9781
	private bool selectAffected;

	// Token: 0x04002636 RID: 9782
	private bool deactivateOnStamp;

	// Token: 0x04002637 RID: 9783
	private GameObject rootCellPlacer;

	// Token: 0x04002638 RID: 9784
	private List<GameObject> childCellPlacers = new List<GameObject>();

	// Token: 0x04002639 RID: 9785
	private List<Building> buildingPreviews = new List<Building>();
}
