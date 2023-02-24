﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000634 RID: 1588
public class ReorderableBuilding : KMonoBehaviour
{
	// Token: 0x060029EC RID: 10732 RVA: 0x000DD1A0 File Offset: 0x000DB3A0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		base.Subscribe(2127324410, new Action<object>(this.OnCancel));
		GameObject gameObject = new GameObject();
		gameObject.name = "ReorderArm";
		gameObject.transform.SetParent(base.transform);
		gameObject.transform.SetLocalPosition(Vector3.up * Grid.CellSizeInMeters * ((float)base.GetComponent<Building>().Def.HeightInCells / 2f - 0.5f));
		gameObject.transform.SetPosition(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Grid.GetLayerZ(Grid.SceneLayer.BuildingBack)));
		gameObject.SetActive(false);
		this.reorderArmController = gameObject.AddComponent<KBatchedAnimController>();
		this.reorderArmController.AnimFiles = new KAnimFile[] { Assets.GetAnim("rocket_module_switching_arm_kanim") };
		this.reorderArmController.initialAnim = "off";
		gameObject.SetActive(true);
		int num = Grid.PosToCell(gameObject);
		this.ShowReorderArm(Grid.IsValidCell(num));
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			LaunchPad currentPad = component.CraftInterface.CurrentPad;
			if (currentPad != null)
			{
				this.m_animLink = new KAnimLink(currentPad.GetComponent<KAnimControllerBase>(), this.reorderArmController);
			}
		}
		if (this.m_animLink == null)
		{
			this.m_animLink = new KAnimLink(base.GetComponent<KAnimControllerBase>(), this.reorderArmController);
		}
	}

	// Token: 0x060029ED RID: 10733 RVA: 0x000DD329 File Offset: 0x000DB529
	private void OnCancel(object data)
	{
		if (base.GetComponent<BuildingUnderConstruction>() != null && !this.cancelShield && !ReorderableBuilding.toBeRemoved.Contains(this))
		{
			ReorderableBuilding.toBeRemoved.Add(this);
		}
	}

	// Token: 0x060029EE RID: 10734 RVA: 0x000DD35C File Offset: 0x000DB55C
	public GameObject AddModule(BuildingDef def, IList<Tag> buildMaterials)
	{
		if (Assets.GetPrefab(base.GetComponent<KPrefabID>().PrefabID()).GetComponent<ReorderableBuilding>().buildConditions.Find((SelectModuleCondition match) => match is TopOnly) == null)
		{
			if (def.BuildingComplete.GetComponent<ReorderableBuilding>().buildConditions.Find((SelectModuleCondition match) => match is EngineOnBottom) == null)
			{
				return this.AddModuleAbove(def, buildMaterials);
			}
		}
		return this.AddModuleBelow(def, buildMaterials);
	}

	// Token: 0x060029EF RID: 10735 RVA: 0x000DD3F0 File Offset: 0x000DB5F0
	private GameObject AddModuleAbove(BuildingDef def, IList<Tag> buildMaterials)
	{
		BuildingAttachPoint component = base.GetComponent<BuildingAttachPoint>();
		if (component == null)
		{
			return null;
		}
		BuildingAttachPoint.HardPoint hardPoint = component.points[0];
		int num = Grid.OffsetCell(Grid.PosToCell(base.gameObject), hardPoint.position);
		int heightInCells = def.HeightInCells;
		if (hardPoint.attachedBuilding != null)
		{
			if (!hardPoint.attachedBuilding.GetComponent<ReorderableBuilding>().CanMoveVertically(heightInCells, null))
			{
				return null;
			}
			hardPoint.attachedBuilding.GetComponent<ReorderableBuilding>().MoveVertical(heightInCells);
		}
		return this.AddModuleCommon(def, buildMaterials, num);
	}

	// Token: 0x060029F0 RID: 10736 RVA: 0x000DD478 File Offset: 0x000DB678
	private GameObject AddModuleBelow(BuildingDef def, IList<Tag> buildMaterials)
	{
		int num = Grid.PosToCell(base.gameObject);
		int heightInCells = def.HeightInCells;
		if (!this.CanMoveVertically(heightInCells, null))
		{
			return null;
		}
		this.MoveVertical(heightInCells);
		return this.AddModuleCommon(def, buildMaterials, num);
	}

	// Token: 0x060029F1 RID: 10737 RVA: 0x000DD4B4 File Offset: 0x000DB6B4
	private GameObject AddModuleCommon(BuildingDef def, IList<Tag> buildMaterials, int cell)
	{
		GameObject gameObject;
		if (DebugHandler.InstantBuildMode || (Game.Instance.SandboxModeActive && SandboxToolParameterMenu.instance.settings.InstantBuild))
		{
			gameObject = def.Build(cell, Orientation.Neutral, null, buildMaterials, 273.15f, true, GameClock.Instance.GetTime());
		}
		else
		{
			gameObject = def.TryPlace(null, Grid.CellToPosCBC(cell, def.SceneLayer), Orientation.Neutral, buildMaterials, 0);
		}
		ReorderableBuilding.RebuildNetworks();
		this.RocketSpecificPostAdd(gameObject, cell);
		return gameObject;
	}

	// Token: 0x060029F2 RID: 10738 RVA: 0x000DD528 File Offset: 0x000DB728
	private void RocketSpecificPostAdd(GameObject obj, int cell)
	{
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		RocketModuleCluster component2 = obj.GetComponent<RocketModuleCluster>();
		if (component != null && component2 != null)
		{
			component.CraftInterface.AddModule(component2);
		}
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x000DD564 File Offset: 0x000DB764
	public void RemoveModule()
	{
		BuildingAttachPoint component = base.GetComponent<BuildingAttachPoint>();
		AttachableBuilding attachableBuilding = null;
		if (component != null)
		{
			attachableBuilding = component.points[0].attachedBuilding;
		}
		int heightInCells = base.GetComponent<Building>().Def.HeightInCells;
		if (base.GetComponent<Deconstructable>() != null)
		{
			base.GetComponent<Deconstructable>().CompleteWork(null);
		}
		if (base.GetComponent<BuildingUnderConstruction>() != null)
		{
			this.DeleteObject();
		}
		Building component2 = base.GetComponent<Building>();
		component2.Def.UnmarkArea(Grid.PosToCell(this), component2.Orientation, component2.Def.ObjectLayer, base.gameObject);
		if (attachableBuilding != null)
		{
			attachableBuilding.GetComponent<ReorderableBuilding>().MoveVertical(-heightInCells);
		}
	}

	// Token: 0x060029F4 RID: 10740 RVA: 0x000DD61C File Offset: 0x000DB81C
	public void LateUpdate()
	{
		this.cancelShield = false;
		ReorderableBuilding.ProcessToBeRemoved();
		if (this.reorderingAnimUnderway)
		{
			float num = 10f;
			if (Mathf.Abs(this.animController.Offset.y) < Time.unscaledDeltaTime * num)
			{
				this.animController.Offset = new Vector3(this.animController.Offset.x, 0f, this.animController.Offset.z);
				this.reorderingAnimUnderway = false;
				string text = base.GetComponent<Building>().Def.WidthInCells.ToString() + "x" + base.GetComponent<Building>().Def.HeightInCells.ToString() + "_ungrab";
				if (!this.reorderArmController.HasAnimation(text))
				{
					text = "3x3_ungrab";
				}
				this.reorderArmController.Play(text, KAnim.PlayMode.Once, 1f, 0f);
				this.reorderArmController.Queue("off", KAnim.PlayMode.Once, 1f, 0f);
				this.loopingSounds.StopSound(GlobalAssets.GetSound(this.reorderSound, false));
			}
			else if (this.animController.Offset.y > 0f)
			{
				this.animController.Offset = new Vector3(this.animController.Offset.x, this.animController.Offset.y - Time.unscaledDeltaTime * num, this.animController.Offset.z);
			}
			else if (this.animController.Offset.y < 0f)
			{
				this.animController.Offset = new Vector3(this.animController.Offset.x, this.animController.Offset.y + Time.unscaledDeltaTime * num, this.animController.Offset.z);
			}
			this.reorderArmController.Offset = this.animController.Offset;
		}
	}

	// Token: 0x060029F5 RID: 10741 RVA: 0x000DD824 File Offset: 0x000DBA24
	private static void ProcessToBeRemoved()
	{
		if (ReorderableBuilding.toBeRemoved.Count > 0)
		{
			ReorderableBuilding.toBeRemoved.Sort(delegate(ReorderableBuilding a, ReorderableBuilding b)
			{
				if (a.transform.position.y < b.transform.position.y)
				{
					return -1;
				}
				return 1;
			});
			for (int i = 0; i < ReorderableBuilding.toBeRemoved.Count; i++)
			{
				ReorderableBuilding.toBeRemoved[i].RemoveModule();
			}
			ReorderableBuilding.toBeRemoved.Clear();
		}
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x000DD898 File Offset: 0x000DBA98
	public void MoveVertical(int amount)
	{
		if (amount == 0)
		{
			return;
		}
		this.cancelShield = true;
		List<GameObject> list = new List<GameObject>();
		list.Add(base.gameObject);
		AttachableBuilding.GetAttachedAbove(base.GetComponent<AttachableBuilding>(), ref list);
		if (amount > 0)
		{
			list.Reverse();
		}
		foreach (GameObject gameObject in list)
		{
			ReorderableBuilding.UnmarkBuilding(gameObject, null);
			int num = Grid.OffsetCell(Grid.PosToCell(gameObject), 0, amount);
			gameObject.transform.SetPosition(Grid.CellToPos(num, CellAlignment.Bottom, Grid.SceneLayer.BuildingFront));
			ReorderableBuilding.MarkBuilding(gameObject, null);
			gameObject.GetComponent<ReorderableBuilding>().ApplyAnimOffset((float)(-(float)amount));
		}
		if (amount > 0)
		{
			foreach (GameObject gameObject2 in list)
			{
				gameObject2.GetComponent<AttachableBuilding>().RegisterWithAttachPoint(true);
			}
		}
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x000DD994 File Offset: 0x000DBB94
	public void SwapWithAbove(bool selectOnComplete = true)
	{
		BuildingAttachPoint component = base.GetComponent<BuildingAttachPoint>();
		if (component == null || component.points[0].attachedBuilding == null)
		{
			return;
		}
		int num = Grid.PosToCell(base.gameObject);
		ReorderableBuilding.UnmarkBuilding(base.gameObject, null);
		AttachableBuilding attachedBuilding = component.points[0].attachedBuilding;
		BuildingAttachPoint component2 = attachedBuilding.GetComponent<BuildingAttachPoint>();
		AttachableBuilding attachableBuilding = ((component2 != null) ? component2.points[0].attachedBuilding : null);
		ReorderableBuilding.UnmarkBuilding(attachedBuilding.gameObject, attachableBuilding);
		Building component3 = attachedBuilding.GetComponent<Building>();
		int num2 = num;
		attachedBuilding.transform.SetPosition(Grid.CellToPos(num2, CellAlignment.Bottom, Grid.SceneLayer.BuildingFront));
		ReorderableBuilding.MarkBuilding(attachedBuilding.gameObject, null);
		int num3 = Grid.OffsetCell(num, 0, component3.Def.HeightInCells);
		base.transform.SetPosition(Grid.CellToPos(num3, CellAlignment.Bottom, Grid.SceneLayer.BuildingFront));
		ReorderableBuilding.MarkBuilding(base.gameObject, attachableBuilding);
		ReorderableBuilding.RebuildNetworks();
		this.ApplyAnimOffset((float)(-(float)component3.Def.HeightInCells));
		Building component4 = base.GetComponent<Building>();
		component3.GetComponent<ReorderableBuilding>().ApplyAnimOffset((float)component4.Def.HeightInCells);
		if (selectOnComplete)
		{
			SelectTool.Instance.Select(component4.GetComponent<KSelectable>(), false);
		}
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x000DDAD7 File Offset: 0x000DBCD7
	protected override void OnCleanUp()
	{
		if (base.GetComponent<BuildingUnderConstruction>() == null && !this.HasTag(GameTags.RocketInSpace))
		{
			this.RemoveModule();
		}
		if (this.m_animLink != null)
		{
			this.m_animLink.Unregister();
		}
		base.OnCleanUp();
	}

	// Token: 0x060029F9 RID: 10745 RVA: 0x000DDB14 File Offset: 0x000DBD14
	private void ApplyAnimOffset(float amount)
	{
		this.animController.Offset = new Vector3(this.animController.Offset.x, this.animController.Offset.y + amount, this.animController.Offset.z);
		this.reorderArmController.Offset = this.animController.Offset;
		string text = base.GetComponent<Building>().Def.WidthInCells.ToString() + "x" + base.GetComponent<Building>().Def.HeightInCells.ToString() + "_grab";
		if (!this.reorderArmController.HasAnimation(text))
		{
			text = "3x3_grab";
		}
		this.reorderArmController.Play(text, KAnim.PlayMode.Once, 1f, 0f);
		this.reorderArmController.onAnimComplete += this.StartReorderingAnim;
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x000DDBFF File Offset: 0x000DBDFF
	private void StartReorderingAnim(HashedString data)
	{
		this.loopingSounds.StartSound(GlobalAssets.GetSound(this.reorderSound, false));
		this.reorderingAnimUnderway = true;
		this.reorderArmController.onAnimComplete -= this.StartReorderingAnim;
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x000DDC38 File Offset: 0x000DBE38
	public void SwapWithBelow(bool selectOnComplete = true)
	{
		if (base.GetComponent<AttachableBuilding>() == null || base.GetComponent<AttachableBuilding>().GetAttachedTo() == null)
		{
			return;
		}
		base.GetComponent<AttachableBuilding>().GetAttachedTo().GetComponent<ReorderableBuilding>()
			.SwapWithAbove(!selectOnComplete);
		if (selectOnComplete)
		{
			SelectTool.Instance.Select(base.GetComponent<KSelectable>(), false);
		}
	}

	// Token: 0x060029FC RID: 10748 RVA: 0x000DDC94 File Offset: 0x000DBE94
	public bool CanMoveVertically(int moveAmount, GameObject ignoreBuilding = null)
	{
		if (moveAmount == 0)
		{
			return true;
		}
		BuildingAttachPoint component = base.GetComponent<BuildingAttachPoint>();
		AttachableBuilding component2 = base.GetComponent<AttachableBuilding>();
		if (moveAmount > 0)
		{
			if (component != null && component.points[0].attachedBuilding != null && component.points[0].attachedBuilding.gameObject != ignoreBuilding && !component.points[0].attachedBuilding.GetComponent<ReorderableBuilding>().CanMoveVertically(moveAmount, null))
			{
				return false;
			}
		}
		else if (component2 != null)
		{
			BuildingAttachPoint attachedTo = component2.GetAttachedTo();
			if (attachedTo != null && attachedTo.gameObject != ignoreBuilding && !component2.GetAttachedTo().GetComponent<ReorderableBuilding>().CanMoveVertically(moveAmount, null))
			{
				return false;
			}
		}
		foreach (CellOffset cellOffset in this.GetOccupiedOffsets())
		{
			if (!ReorderableBuilding.CheckCellClear(Grid.OffsetCell(Grid.OffsetCell(Grid.PosToCell(base.gameObject), cellOffset), 0, moveAmount), base.gameObject))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060029FD RID: 10749 RVA: 0x000DDDA8 File Offset: 0x000DBFA8
	public static bool CheckCellClear(int checkCell, GameObject ignoreObject = null)
	{
		return Grid.IsValidCell(checkCell) && Grid.IsValidBuildingCell(checkCell) && !Grid.Solid[checkCell] && Grid.WorldIdx[checkCell] != ClusterManager.INVALID_WORLD_IDX && (!(Grid.Objects[checkCell, 1] != null) || !(Grid.Objects[checkCell, 1] != ignoreObject) || !(Grid.Objects[checkCell, 1].GetComponent<ReorderableBuilding>() == null));
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x000DDE28 File Offset: 0x000DC028
	public GameObject ConvertModule(BuildingDef toModule, IList<Tag> materials)
	{
		int num = Grid.PosToCell(base.gameObject);
		int num2 = toModule.HeightInCells - base.GetComponent<Building>().Def.HeightInCells;
		base.gameObject.GetComponent<Building>();
		BuildingAttachPoint component = base.gameObject.GetComponent<BuildingAttachPoint>();
		GameObject gameObject = null;
		if (component != null && component.points[0].attachedBuilding != null)
		{
			gameObject = component.points[0].attachedBuilding.gameObject;
			component.points[0].attachedBuilding = null;
			Components.BuildingAttachPoints.Remove(component);
		}
		ReorderableBuilding.UnmarkBuilding(base.gameObject, null);
		if (num2 != 0 && gameObject != null)
		{
			gameObject.GetComponent<ReorderableBuilding>().MoveVertical(num2);
		}
		string text;
		if (!DebugHandler.InstantBuildMode && !toModule.IsValidPlaceLocation(base.gameObject, num, Orientation.Neutral, out text))
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Building, text, base.transform, 1.5f, false);
			if (num2 != 0 && gameObject != null)
			{
				num2 *= -1;
				gameObject.GetComponent<ReorderableBuilding>().MoveVertical(num2);
			}
			ReorderableBuilding.MarkBuilding(base.gameObject, (gameObject != null) ? gameObject.GetComponent<AttachableBuilding>() : null);
			if (component != null && gameObject != null)
			{
				component.points[0].attachedBuilding = gameObject.GetComponent<AttachableBuilding>();
				Components.BuildingAttachPoints.Add(component);
			}
			return null;
		}
		if (materials == null)
		{
			materials = toModule.DefaultElements();
		}
		GameObject gameObject2;
		if (DebugHandler.InstantBuildMode || (Game.Instance.SandboxModeActive && SandboxToolParameterMenu.instance.settings.InstantBuild))
		{
			gameObject2 = toModule.Build(num, Orientation.Neutral, null, materials, 273.15f, true, GameClock.Instance.GetTime());
		}
		else
		{
			gameObject2 = toModule.TryPlace(base.gameObject, Grid.CellToPosCBC(num, toModule.SceneLayer), Orientation.Neutral, materials, 0);
		}
		RocketModuleCluster component2 = base.GetComponent<RocketModuleCluster>();
		RocketModuleCluster component3 = gameObject2.GetComponent<RocketModuleCluster>();
		if (component2 != null && component3 != null)
		{
			component2.CraftInterface.AddModule(component3);
		}
		Deconstructable component4 = base.GetComponent<Deconstructable>();
		if (component4 != null)
		{
			component4.SetAllowDeconstruction(true);
			component4.ForceDestroyAndGetMaterials();
		}
		else
		{
			Util.KDestroyGameObject(base.gameObject);
		}
		return gameObject2;
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x000DE070 File Offset: 0x000DC270
	private CellOffset[] GetOccupiedOffsets()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			return component.OccupiedCellsOffsets;
		}
		return base.GetComponent<BuildingUnderConstruction>().Def.PlacementOffsets;
	}

	// Token: 0x06002A00 RID: 10752 RVA: 0x000DE0A4 File Offset: 0x000DC2A4
	public bool CanChangeModule()
	{
		if (base.GetComponent<BuildingUnderConstruction>() != null)
		{
			string prefabID = base.GetComponent<BuildingUnderConstruction>().Def.PrefabID;
		}
		else
		{
			string prefabID2 = base.GetComponent<Building>().Def.PrefabID;
		}
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			if (component.CraftInterface != null)
			{
				if (component.CraftInterface.GetComponent<Clustercraft>().Status != Clustercraft.CraftStatus.Grounded)
				{
					return false;
				}
			}
			else if (component.conditionManager != null && SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(component.conditionManager).state != Spacecraft.MissionState.Grounded)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x000DE13D File Offset: 0x000DC33D
	public bool CanRemoveModule()
	{
		return true;
	}

	// Token: 0x06002A02 RID: 10754 RVA: 0x000DE140 File Offset: 0x000DC340
	public bool CanSwapUp(bool alsoCheckAboveCanSwapDown = true)
	{
		BuildingAttachPoint component = base.GetComponent<BuildingAttachPoint>();
		if (component == null)
		{
			return false;
		}
		if (base.GetComponent<AttachableBuilding>() == null || base.GetComponent<RocketEngineCluster>() != null)
		{
			return false;
		}
		AttachableBuilding attachedBuilding = component.points[0].attachedBuilding;
		return !(attachedBuilding == null) && !(attachedBuilding.GetComponent<BuildingAttachPoint>() == null) && !attachedBuilding.HasTag(GameTags.NoseRocketModule) && this.CanMoveVertically(attachedBuilding.GetComponent<Building>().Def.HeightInCells, attachedBuilding.gameObject) && (!alsoCheckAboveCanSwapDown || attachedBuilding.GetComponent<ReorderableBuilding>().CanSwapDown(false));
	}

	// Token: 0x06002A03 RID: 10755 RVA: 0x000DE1EC File Offset: 0x000DC3EC
	public bool CanSwapDown(bool alsoCheckBelowCanSwapUp = true)
	{
		if (base.gameObject.HasTag(GameTags.NoseRocketModule))
		{
			return false;
		}
		AttachableBuilding component = base.GetComponent<AttachableBuilding>();
		if (component == null)
		{
			return false;
		}
		BuildingAttachPoint attachedTo = component.GetAttachedTo();
		return !(attachedTo == null) && !(base.GetComponent<BuildingAttachPoint>() == null) && !(attachedTo.GetComponent<AttachableBuilding>() == null) && !(attachedTo.GetComponent<RocketEngineCluster>() != null) && this.CanMoveVertically(attachedTo.GetComponent<Building>().Def.HeightInCells * -1, attachedTo.gameObject) && (!alsoCheckBelowCanSwapUp || attachedTo.GetComponent<ReorderableBuilding>().CanSwapUp(false));
	}

	// Token: 0x06002A04 RID: 10756 RVA: 0x000DE295 File Offset: 0x000DC495
	public void ShowReorderArm(bool show)
	{
		if (this.reorderArmController != null)
		{
			this.reorderArmController.gameObject.SetActive(show);
		}
	}

	// Token: 0x06002A05 RID: 10757 RVA: 0x000DE2B8 File Offset: 0x000DC4B8
	private static void RebuildNetworks()
	{
		Game.Instance.logicCircuitSystem.ForceRebuildNetworks();
		Game.Instance.gasConduitSystem.ForceRebuildNetworks();
		Game.Instance.liquidConduitSystem.ForceRebuildNetworks();
		Game.Instance.electricalConduitSystem.ForceRebuildNetworks();
		Game.Instance.solidConduitSystem.ForceRebuildNetworks();
	}

	// Token: 0x06002A06 RID: 10758 RVA: 0x000DE310 File Offset: 0x000DC510
	private static void UnmarkBuilding(GameObject go, AttachableBuilding aboveBuilding)
	{
		int num = Grid.PosToCell(go);
		Building component = go.GetComponent<Building>();
		component.Def.UnmarkArea(num, component.Orientation, component.Def.ObjectLayer, go);
		AttachableBuilding component2 = go.GetComponent<AttachableBuilding>();
		if (component2 != null)
		{
			component2.RegisterWithAttachPoint(false);
		}
		if (aboveBuilding != null)
		{
			aboveBuilding.RegisterWithAttachPoint(false);
		}
		RocketModule component3 = go.GetComponent<RocketModule>();
		if (component3 != null)
		{
			component3.DeregisterComponents();
		}
		RocketConduitSender[] components = go.GetComponents<RocketConduitSender>();
		if (components.Length != 0)
		{
			RocketConduitSender[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RemoveConduitPortFromNetwork();
			}
		}
		RocketConduitReceiver[] components2 = go.GetComponents<RocketConduitReceiver>();
		if (components2.Length != 0)
		{
			RocketConduitReceiver[] array2 = components2;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].RemoveConduitPortFromNetwork();
			}
		}
	}

	// Token: 0x06002A07 RID: 10759 RVA: 0x000DE3E4 File Offset: 0x000DC5E4
	private static void MarkBuilding(GameObject go, AttachableBuilding aboveBuilding)
	{
		int num = Grid.PosToCell(go);
		Building component = go.GetComponent<Building>();
		component.Def.MarkArea(num, component.Orientation, component.Def.ObjectLayer, go);
		if (component.GetComponent<OccupyArea>() != null)
		{
			component.GetComponent<OccupyArea>().UpdateOccupiedArea();
		}
		LogicPorts component2 = component.GetComponent<LogicPorts>();
		if (component2 && go.GetComponent<BuildingComplete>() != null)
		{
			component2.OnMove();
		}
		component.GetComponent<AttachableBuilding>().RegisterWithAttachPoint(true);
		if (aboveBuilding != null)
		{
			aboveBuilding.RegisterWithAttachPoint(true);
		}
		RocketModule component3 = go.GetComponent<RocketModule>();
		if (component3 != null)
		{
			component3.RegisterComponents();
		}
		VerticalModuleTiler component4 = go.GetComponent<VerticalModuleTiler>();
		if (component4 != null)
		{
			component4.PostReorderMove();
		}
		RocketConduitSender[] components = go.GetComponents<RocketConduitSender>();
		if (components.Length != 0)
		{
			RocketConduitSender[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AddConduitPortToNetwork();
			}
		}
		RocketConduitReceiver[] components2 = go.GetComponents<RocketConduitReceiver>();
		if (components2.Length != 0)
		{
			RocketConduitReceiver[] array2 = components2;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].AddConduitPortToNetwork();
			}
		}
	}

	// Token: 0x040018D3 RID: 6355
	private bool cancelShield;

	// Token: 0x040018D4 RID: 6356
	private bool reorderingAnimUnderway;

	// Token: 0x040018D5 RID: 6357
	private KBatchedAnimController animController;

	// Token: 0x040018D6 RID: 6358
	public List<SelectModuleCondition> buildConditions = new List<SelectModuleCondition>();

	// Token: 0x040018D7 RID: 6359
	private KBatchedAnimController reorderArmController;

	// Token: 0x040018D8 RID: 6360
	private KAnimLink m_animLink;

	// Token: 0x040018D9 RID: 6361
	[MyCmpAdd]
	private LoopingSounds loopingSounds;

	// Token: 0x040018DA RID: 6362
	private string reorderSound = "RocketModuleSwitchingArm_moving_LP";

	// Token: 0x040018DB RID: 6363
	private static List<ReorderableBuilding> toBeRemoved = new List<ReorderableBuilding>();

	// Token: 0x020012C4 RID: 4804
	public enum MoveSource
	{
		// Token: 0x04005EB1 RID: 24241
		Push,
		// Token: 0x04005EB2 RID: 24242
		Pull
	}
}
