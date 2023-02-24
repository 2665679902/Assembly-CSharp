using System;
using System.Collections.Generic;
using System.IO;
using STRINGS;
using UnityEngine;

// Token: 0x020004AA RID: 1194
public class Navigator : StateMachineComponent<Navigator.StatesInstance>, ISaveLoadableDetails
{
	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06001AFF RID: 6911 RVA: 0x00090413 File Offset: 0x0008E613
	// (set) Token: 0x06001B00 RID: 6912 RVA: 0x00090420 File Offset: 0x0008E620
	public bool IsFacingLeft
	{
		get
		{
			return this.facing.GetFacing();
		}
		set
		{
			this.facing.SetFacing(value);
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0009042E File Offset: 0x0008E62E
	// (set) Token: 0x06001B02 RID: 6914 RVA: 0x00090436 File Offset: 0x0008E636
	public KMonoBehaviour target { get; set; }

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0009043F File Offset: 0x0008E63F
	// (set) Token: 0x06001B04 RID: 6916 RVA: 0x00090447 File Offset: 0x0008E647
	public CellOffset[] targetOffsets { get; private set; }

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06001B05 RID: 6917 RVA: 0x00090450 File Offset: 0x0008E650
	// (set) Token: 0x06001B06 RID: 6918 RVA: 0x00090458 File Offset: 0x0008E658
	public NavGrid NavGrid { get; private set; }

	// Token: 0x06001B07 RID: 6919 RVA: 0x00090464 File Offset: 0x0008E664
	public void Serialize(BinaryWriter writer)
	{
		byte currentNavType = (byte)this.CurrentNavType;
		writer.Write(currentNavType);
		writer.Write(this.distanceTravelledByNavType.Count);
		foreach (KeyValuePair<NavType, int> keyValuePair in this.distanceTravelledByNavType)
		{
			byte key = (byte)keyValuePair.Key;
			writer.Write(key);
			writer.Write(keyValuePair.Value);
		}
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x000904EC File Offset: 0x0008E6EC
	public void Deserialize(IReader reader)
	{
		NavType navType = (NavType)reader.ReadByte();
		if (!SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 11))
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				NavType navType2 = (NavType)reader.ReadByte();
				int num2 = reader.ReadInt32();
				if (this.distanceTravelledByNavType.ContainsKey(navType2))
				{
					this.distanceTravelledByNavType[navType2] = num2;
				}
			}
		}
		bool flag = false;
		NavType[] validNavTypes = this.NavGrid.ValidNavTypes;
		for (int j = 0; j < validNavTypes.Length; j++)
		{
			if (validNavTypes[j] == navType)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.CurrentNavType = navType;
		}
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x00090594 File Offset: 0x0008E794
	protected override void OnPrefabInit()
	{
		this.transitionDriver = new TransitionDriver(this);
		this.targetLocator = Util.KInstantiate(Assets.GetPrefab(TargetLocator.ID), null, null).GetComponent<KPrefabID>();
		this.targetLocator.gameObject.SetActive(true);
		this.log = new LoggerFSS("Navigator", 35);
		this.simRenderLoadBalance = true;
		this.autoRegisterSimRender = false;
		this.NavGrid = Pathfinding.Instance.GetNavGrid(this.NavGridName);
		base.GetComponent<PathProber>().SetValidNavTypes(this.NavGrid.ValidNavTypes, this.maxProbingRadius);
		this.distanceTravelledByNavType = new Dictionary<NavType, int>();
		for (int i = 0; i < 11; i++)
		{
			this.distanceTravelledByNavType.Add((NavType)i, 0);
		}
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x00090658 File Offset: 0x0008E858
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Navigator>(1623392196, Navigator.OnDefeatedDelegate);
		base.Subscribe<Navigator>(-1506500077, Navigator.OnDefeatedDelegate);
		base.Subscribe<Navigator>(493375141, Navigator.OnRefreshUserMenuDelegate);
		base.Subscribe<Navigator>(-1503271301, Navigator.OnSelectObjectDelegate);
		base.Subscribe<Navigator>(856640610, Navigator.OnStoreDelegate);
		if (this.updateProber)
		{
			SimAndRenderScheduler.instance.Add(this, false);
		}
		this.pathProbeTask = new Navigator.PathProbeTask(this);
		this.SetCurrentNavType(this.CurrentNavType);
		this.SubscribeUnstuckFunctions();
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x000906F2 File Offset: 0x0008E8F2
	private void SubscribeUnstuckFunctions()
	{
		if (this.CurrentNavType == NavType.Tube)
		{
			GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingTileChanged));
		}
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x0009071F File Offset: 0x0008E91F
	private void UnsubscribeUnstuckFunctions()
	{
		GameScenePartitioner.Instance.RemoveGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingTileChanged));
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x00090744 File Offset: 0x0008E944
	private void OnBuildingTileChanged(int cell, object building)
	{
		if (this.CurrentNavType == NavType.Tube && building == null)
		{
			bool flag = cell == Grid.PosToCell(this);
			if (base.smi != null && flag)
			{
				this.SetCurrentNavType(NavType.Floor);
				this.UnsubscribeUnstuckFunctions();
			}
		}
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x00090781 File Offset: 0x0008E981
	protected override void OnCleanUp()
	{
		this.UnsubscribeUnstuckFunctions();
		base.OnCleanUp();
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x0009078F File Offset: 0x0008E98F
	public bool IsMoving()
	{
		return base.smi.IsInsideState(base.smi.sm.normal.moving);
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000907B1 File Offset: 0x0008E9B1
	public bool GoTo(int cell, CellOffset[] offsets = null)
	{
		if (offsets == null)
		{
			offsets = new CellOffset[1];
		}
		this.targetLocator.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
		return this.GoTo(this.targetLocator, offsets, NavigationTactics.ReduceTravelDistance);
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x000907E9 File Offset: 0x0008E9E9
	public bool GoTo(int cell, CellOffset[] offsets, NavTactic tactic)
	{
		if (offsets == null)
		{
			offsets = new CellOffset[1];
		}
		this.targetLocator.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
		return this.GoTo(this.targetLocator, offsets, tactic);
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x0009081D File Offset: 0x0008EA1D
	public void UpdateTarget(int cell)
	{
		this.targetLocator.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x00090838 File Offset: 0x0008EA38
	public bool GoTo(KMonoBehaviour target, CellOffset[] offsets, NavTactic tactic)
	{
		if (tactic == null)
		{
			tactic = NavigationTactics.ReduceTravelDistance;
		}
		base.smi.GoTo(base.smi.sm.normal.moving);
		base.smi.sm.moveTarget.Set(target.gameObject, base.smi, false);
		this.tactic = tactic;
		this.target = target;
		this.targetOffsets = offsets;
		this.ClearReservedCell();
		this.AdvancePath(true);
		return this.IsMoving();
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x000908BA File Offset: 0x0008EABA
	public void BeginTransition(NavGrid.Transition transition)
	{
		this.transitionDriver.EndTransition();
		base.smi.GoTo(base.smi.sm.normal.moving);
		this.transitionDriver.BeginTransition(this, transition, this.defaultSpeed);
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x000908FC File Offset: 0x0008EAFC
	private bool ValidatePath(ref PathFinder.Path path, out bool atNextNode)
	{
		atNextNode = false;
		bool flag = false;
		if (path.IsValid())
		{
			int num = Grid.PosToCell(this.target);
			flag = this.reservedCell != NavigationReservations.InvalidReservation && this.CanReach(this.reservedCell);
			flag &= Grid.IsCellOffsetOf(this.reservedCell, num, this.targetOffsets);
		}
		if (flag)
		{
			int num2 = Grid.PosToCell(this);
			flag = num2 == path.nodes[0].cell && this.CurrentNavType == path.nodes[0].navType;
			flag |= (atNextNode = num2 == path.nodes[1].cell && this.CurrentNavType == path.nodes[1].navType);
		}
		if (!flag)
		{
			return false;
		}
		PathFinderAbilities currentAbilities = this.GetCurrentAbilities();
		return PathFinder.ValidatePath(this.NavGrid, currentAbilities, ref path);
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x000909E4 File Offset: 0x0008EBE4
	public void AdvancePath(bool trigger_advance = true)
	{
		int num = Grid.PosToCell(this);
		if (this.target == null)
		{
			base.Trigger(-766531887, null);
			this.Stop(false, true);
		}
		else if (num == this.reservedCell && this.CurrentNavType != NavType.Tube)
		{
			this.Stop(true, true);
		}
		else
		{
			bool flag2;
			bool flag = !this.ValidatePath(ref this.path, out flag2);
			if (flag2)
			{
				this.path.nodes.RemoveAt(0);
			}
			if (flag)
			{
				int num2 = Grid.PosToCell(this.target);
				int cellPreferences = this.tactic.GetCellPreferences(num2, this.targetOffsets, this);
				this.SetReservedCell(cellPreferences);
				if (this.reservedCell == NavigationReservations.InvalidReservation)
				{
					this.Stop(false, true);
				}
				else
				{
					PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(num, this.CurrentNavType, this.flags);
					PathFinder.UpdatePath(this.NavGrid, this.GetCurrentAbilities(), potentialPath, PathFinderQueries.cellQuery.Reset(this.reservedCell), ref this.path);
				}
			}
			if (this.path.IsValid())
			{
				this.BeginTransition(this.NavGrid.transitions[(int)this.path.nodes[1].transitionId]);
				this.distanceTravelledByNavType[this.CurrentNavType] = Mathf.Max(this.distanceTravelledByNavType[this.CurrentNavType] + 1, this.distanceTravelledByNavType[this.CurrentNavType]);
			}
			else if (this.path.HasArrived())
			{
				this.Stop(true, true);
			}
			else
			{
				this.ClearReservedCell();
				this.Stop(false, true);
			}
		}
		if (trigger_advance)
		{
			base.Trigger(1347184327, null);
		}
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x00090B89 File Offset: 0x0008ED89
	public NavGrid.Transition GetNextTransition()
	{
		return this.NavGrid.transitions[(int)this.path.nodes[1].transitionId];
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x00090BB4 File Offset: 0x0008EDB4
	public void Stop(bool arrived_at_destination = false, bool play_idle = true)
	{
		this.target = null;
		this.targetOffsets = null;
		this.path.Clear();
		base.smi.sm.moveTarget.Set(null, base.smi);
		this.transitionDriver.EndTransition();
		if (play_idle)
		{
			HashedString idleAnim = this.NavGrid.GetIdleAnim(this.CurrentNavType);
			base.GetComponent<KAnimControllerBase>().Play(idleAnim, KAnim.PlayMode.Loop, 1f, 0f);
		}
		if (arrived_at_destination)
		{
			base.smi.GoTo(base.smi.sm.normal.arrived);
			return;
		}
		if (base.smi.GetCurrentState() == base.smi.sm.normal.moving)
		{
			this.ClearReservedCell();
			base.smi.GoTo(base.smi.sm.normal.failed);
		}
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x00090C99 File Offset: 0x0008EE99
	private void SimEveryTick(float dt)
	{
		if (this.IsMoving())
		{
			this.transitionDriver.UpdateTransition(dt);
		}
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x00090CAF File Offset: 0x0008EEAF
	public void Sim4000ms(float dt)
	{
		this.UpdateProbe(true);
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x00090CB8 File Offset: 0x0008EEB8
	public void UpdateProbe(bool forceUpdate = false)
	{
		if (forceUpdate || !this.executePathProbeTaskAsync)
		{
			this.pathProbeTask.Update();
			this.pathProbeTask.Run(null);
		}
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x00090CDC File Offset: 0x0008EEDC
	public void DrawPath()
	{
		if (base.gameObject.activeInHierarchy && this.IsMoving())
		{
			NavPathDrawer.Instance.DrawPath(base.GetComponent<KAnimControllerBase>().GetPivotSymbolPosition(), this.path);
		}
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x00090D0E File Offset: 0x0008EF0E
	public void Pause(string reason)
	{
		base.smi.sm.isPaused.Set(true, base.smi, false);
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x00090D2E File Offset: 0x0008EF2E
	public void Unpause(string reason)
	{
		base.smi.sm.isPaused.Set(false, base.smi, false);
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x00090D4E File Offset: 0x0008EF4E
	private void OnDefeated(object data)
	{
		this.ClearReservedCell();
		this.Stop(false, false);
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x00090D5E File Offset: 0x0008EF5E
	private void ClearReservedCell()
	{
		if (this.reservedCell != NavigationReservations.InvalidReservation)
		{
			NavigationReservations.Instance.RemoveOccupancy(this.reservedCell);
			this.reservedCell = NavigationReservations.InvalidReservation;
		}
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x00090D88 File Offset: 0x0008EF88
	private void SetReservedCell(int cell)
	{
		this.ClearReservedCell();
		this.reservedCell = cell;
		NavigationReservations.Instance.AddOccupancy(cell);
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x00090DA2 File Offset: 0x0008EFA2
	public int GetReservedCell()
	{
		return this.reservedCell;
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x00090DAA File Offset: 0x0008EFAA
	public int GetAnchorCell()
	{
		return this.AnchorCell;
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x00090DB2 File Offset: 0x0008EFB2
	public bool IsValidNavType(NavType nav_type)
	{
		return this.NavGrid.HasNavTypeData(nav_type);
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x00090DC0 File Offset: 0x0008EFC0
	public void SetCurrentNavType(NavType nav_type)
	{
		this.CurrentNavType = nav_type;
		this.AnchorCell = NavTypeHelper.GetAnchorCell(nav_type, Grid.PosToCell(this));
		NavGrid.NavTypeData navTypeData = this.NavGrid.GetNavTypeData(this.CurrentNavType);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		Vector2 one = Vector2.one;
		if (navTypeData.flipX)
		{
			one.x = -1f;
		}
		if (navTypeData.flipY)
		{
			one.y = -1f;
		}
		component.navMatrix = Matrix2x3.Translate(navTypeData.animControllerOffset * 200f) * Matrix2x3.Rotate(navTypeData.rotation) * Matrix2x3.Scale(one);
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x00090E64 File Offset: 0x0008F064
	private void OnRefreshUserMenu(object data)
	{
		if (base.gameObject.HasTag(GameTags.Dead))
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((NavPathDrawer.Instance.GetNavigator() != this) ? new KIconButtonMenu.ButtonInfo("action_navigable_regions", UI.USERMENUACTIONS.DRAWPATHS.NAME, new System.Action(this.OnDrawPaths), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DRAWPATHS.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_navigable_regions", UI.USERMENUACTIONS.DRAWPATHS.NAME_OFF, new System.Action(this.OnDrawPaths), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DRAWPATHS.TOOLTIP_OFF, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 0.1f);
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_follow_cam", UI.USERMENUACTIONS.FOLLOWCAM.NAME, new System.Action(this.OnFollowCam), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.FOLLOWCAM.TOOLTIP, true), 0.3f);
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x00090F67 File Offset: 0x0008F167
	private void OnFollowCam()
	{
		if (CameraController.Instance.followTarget == base.transform)
		{
			CameraController.Instance.ClearFollowTarget();
			return;
		}
		CameraController.Instance.SetFollowTarget(base.transform);
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x00090F9B File Offset: 0x0008F19B
	private void OnDrawPaths()
	{
		if (NavPathDrawer.Instance.GetNavigator() != this)
		{
			NavPathDrawer.Instance.SetNavigator(this);
			return;
		}
		NavPathDrawer.Instance.ClearNavigator();
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x00090FC5 File Offset: 0x0008F1C5
	private void OnSelectObject(object data)
	{
		NavPathDrawer.Instance.ClearNavigator();
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x00090FD1 File Offset: 0x0008F1D1
	public void OnStore(object data)
	{
		if (data is Storage || (data != null && (bool)data))
		{
			this.Stop(false, true);
		}
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x00090FF4 File Offset: 0x0008F1F4
	public PathFinderAbilities GetCurrentAbilities()
	{
		this.abilities.Refresh();
		return this.abilities;
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x00091007 File Offset: 0x0008F207
	public void SetAbilities(PathFinderAbilities abilities)
	{
		this.abilities = abilities;
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x00091010 File Offset: 0x0008F210
	public bool CanReach(IApproachable approachable)
	{
		return this.CanReach(approachable.GetCell(), approachable.GetOffsets());
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x00091024 File Offset: 0x0008F224
	public bool CanReach(int cell, CellOffset[] offsets)
	{
		foreach (CellOffset cellOffset in offsets)
		{
			int num = Grid.OffsetCell(cell, cellOffset);
			if (this.CanReach(num))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x0009105D File Offset: 0x0008F25D
	public bool CanReach(int cell)
	{
		return this.GetNavigationCost(cell) != -1;
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x0009106C File Offset: 0x0008F26C
	public int GetNavigationCost(int cell)
	{
		if (Grid.IsValidCell(cell))
		{
			return this.PathProber.GetCost(cell);
		}
		return -1;
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x00091084 File Offset: 0x0008F284
	public int GetNavigationCostIgnoreProberOffset(int cell, CellOffset[] offsets)
	{
		return this.PathProber.GetNavigationCostIgnoreProberOffset(cell, offsets);
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x00091094 File Offset: 0x0008F294
	public int GetNavigationCost(int cell, CellOffset[] offsets)
	{
		int num = -1;
		int num2 = offsets.Length;
		for (int i = 0; i < num2; i++)
		{
			int num3 = Grid.OffsetCell(cell, offsets[i]);
			int navigationCost = this.GetNavigationCost(num3);
			if (navigationCost != -1 && (num == -1 || navigationCost < num))
			{
				num = navigationCost;
			}
		}
		return num;
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x000910DC File Offset: 0x0008F2DC
	public int GetNavigationCost(IApproachable approachable)
	{
		return this.GetNavigationCost(approachable.GetCell(), approachable.GetOffsets());
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x000910F0 File Offset: 0x0008F2F0
	public void RunQuery(PathFinderQuery query)
	{
		int num = Grid.PosToCell(this);
		PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(num, this.CurrentNavType, this.flags);
		PathFinder.Run(this.NavGrid, this.GetCurrentAbilities(), potentialPath, query);
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x0009112B File Offset: 0x0008F32B
	public void SetFlags(PathFinder.PotentialPath.Flags new_flags)
	{
		this.flags |= new_flags;
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x0009113B File Offset: 0x0008F33B
	public void ClearFlags(PathFinder.PotentialPath.Flags new_flags)
	{
		this.flags &= ~new_flags;
	}

	// Token: 0x04000F01 RID: 3841
	public bool DebugDrawPath;

	// Token: 0x04000F05 RID: 3845
	[MyCmpAdd]
	public PathProber PathProber;

	// Token: 0x04000F06 RID: 3846
	[MyCmpAdd]
	private Facing facing;

	// Token: 0x04000F07 RID: 3847
	[MyCmpGet]
	public AnimEventHandler animEventHandler;

	// Token: 0x04000F08 RID: 3848
	public float defaultSpeed = 1f;

	// Token: 0x04000F09 RID: 3849
	public TransitionDriver transitionDriver;

	// Token: 0x04000F0A RID: 3850
	public string NavGridName;

	// Token: 0x04000F0B RID: 3851
	public bool updateProber;

	// Token: 0x04000F0C RID: 3852
	public int maxProbingRadius;

	// Token: 0x04000F0D RID: 3853
	public PathFinder.PotentialPath.Flags flags;

	// Token: 0x04000F0E RID: 3854
	private LoggerFSS log;

	// Token: 0x04000F0F RID: 3855
	public Dictionary<NavType, int> distanceTravelledByNavType;

	// Token: 0x04000F10 RID: 3856
	public Grid.SceneLayer sceneLayer = Grid.SceneLayer.Move;

	// Token: 0x04000F11 RID: 3857
	private PathFinderAbilities abilities;

	// Token: 0x04000F12 RID: 3858
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04000F13 RID: 3859
	[NonSerialized]
	public PathFinder.Path path;

	// Token: 0x04000F14 RID: 3860
	public NavType CurrentNavType;

	// Token: 0x04000F15 RID: 3861
	private int AnchorCell;

	// Token: 0x04000F16 RID: 3862
	private KPrefabID targetLocator;

	// Token: 0x04000F17 RID: 3863
	private int reservedCell = NavigationReservations.InvalidReservation;

	// Token: 0x04000F18 RID: 3864
	private NavTactic tactic;

	// Token: 0x04000F19 RID: 3865
	public Navigator.PathProbeTask pathProbeTask;

	// Token: 0x04000F1A RID: 3866
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnDefeatedDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnDefeated(data);
	});

	// Token: 0x04000F1B RID: 3867
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04000F1C RID: 3868
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnSelectObjectDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnSelectObject(data);
	});

	// Token: 0x04000F1D RID: 3869
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnStoreDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnStore(data);
	});

	// Token: 0x04000F1E RID: 3870
	public bool executePathProbeTaskAsync;

	// Token: 0x020010E3 RID: 4323
	public class ActiveTransition
	{
		// Token: 0x060074CA RID: 29898 RVA: 0x002B41A8 File Offset: 0x002B23A8
		public void Init(NavGrid.Transition transition, float default_speed)
		{
			this.x = transition.x;
			this.y = transition.y;
			this.isLooping = transition.isLooping;
			this.start = transition.start;
			this.end = transition.end;
			this.preAnim = transition.preAnim;
			this.anim = transition.anim;
			this.speed = default_speed;
			this.animSpeed = transition.animSpeed;
			this.navGridTransition = transition;
		}

		// Token: 0x060074CB RID: 29899 RVA: 0x002B4230 File Offset: 0x002B2430
		public void Copy(Navigator.ActiveTransition other)
		{
			this.x = other.x;
			this.y = other.y;
			this.isLooping = other.isLooping;
			this.start = other.start;
			this.end = other.end;
			this.preAnim = other.preAnim;
			this.anim = other.anim;
			this.speed = other.speed;
			this.animSpeed = other.animSpeed;
			this.navGridTransition = other.navGridTransition;
		}

		// Token: 0x04005901 RID: 22785
		public int x;

		// Token: 0x04005902 RID: 22786
		public int y;

		// Token: 0x04005903 RID: 22787
		public bool isLooping;

		// Token: 0x04005904 RID: 22788
		public NavType start;

		// Token: 0x04005905 RID: 22789
		public NavType end;

		// Token: 0x04005906 RID: 22790
		public HashedString preAnim;

		// Token: 0x04005907 RID: 22791
		public HashedString anim;

		// Token: 0x04005908 RID: 22792
		public float speed;

		// Token: 0x04005909 RID: 22793
		public float animSpeed = 1f;

		// Token: 0x0400590A RID: 22794
		public Func<bool> isCompleteCB;

		// Token: 0x0400590B RID: 22795
		public NavGrid.Transition navGridTransition;
	}

	// Token: 0x020010E4 RID: 4324
	public class StatesInstance : GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.GameInstance
	{
		// Token: 0x060074CD RID: 29901 RVA: 0x002B42C8 File Offset: 0x002B24C8
		public StatesInstance(Navigator master)
			: base(master)
		{
		}
	}

	// Token: 0x020010E5 RID: 4325
	public class States : GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator>
	{
		// Token: 0x060074CE RID: 29902 RVA: 0x002B42D4 File Offset: 0x002B24D4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.normal.stopped;
			this.saveHistory = true;
			this.normal.ParamTransition<bool>(this.isPaused, this.paused, GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.IsTrue).Update("NavigatorProber", delegate(Navigator.StatesInstance smi, float dt)
			{
				smi.master.Sim4000ms(dt);
			}, UpdateRate.SIM_4000ms, false);
			this.normal.moving.Enter(delegate(Navigator.StatesInstance smi)
			{
				smi.Trigger(1027377649, GameHashes.ObjectMovementWakeUp);
			}).Update("UpdateNavigator", delegate(Navigator.StatesInstance smi, float dt)
			{
				smi.master.SimEveryTick(dt);
			}, UpdateRate.SIM_EVERY_TICK, true).Exit(delegate(Navigator.StatesInstance smi)
			{
				smi.Trigger(1027377649, GameHashes.ObjectMovementSleep);
			});
			this.normal.arrived.TriggerOnEnter(GameHashes.DestinationReached, null).GoTo(this.normal.stopped);
			this.normal.failed.TriggerOnEnter(GameHashes.NavigationFailed, null).GoTo(this.normal.stopped);
			this.normal.stopped.Enter(delegate(Navigator.StatesInstance smi)
			{
				smi.master.SubscribeUnstuckFunctions();
			}).DoNothing().Exit(delegate(Navigator.StatesInstance smi)
			{
				smi.master.UnsubscribeUnstuckFunctions();
			});
			this.paused.ParamTransition<bool>(this.isPaused, this.normal, GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.IsFalse);
		}

		// Token: 0x0400590C RID: 22796
		public StateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.TargetParameter moveTarget;

		// Token: 0x0400590D RID: 22797
		public StateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.BoolParameter isPaused = new StateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.BoolParameter(false);

		// Token: 0x0400590E RID: 22798
		public Navigator.States.NormalStates normal;

		// Token: 0x0400590F RID: 22799
		public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State paused;

		// Token: 0x02001F72 RID: 8050
		public class NormalStates : GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State
		{
			// Token: 0x04008BC6 RID: 35782
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State moving;

			// Token: 0x04008BC7 RID: 35783
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State arrived;

			// Token: 0x04008BC8 RID: 35784
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State failed;

			// Token: 0x04008BC9 RID: 35785
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State stopped;
		}
	}

	// Token: 0x020010E6 RID: 4326
	public struct PathProbeTask : IWorkItem<object>
	{
		// Token: 0x060074D0 RID: 29904 RVA: 0x002B4494 File Offset: 0x002B2694
		public PathProbeTask(Navigator navigator)
		{
			this.navigator = navigator;
			this.cell = -1;
		}

		// Token: 0x060074D1 RID: 29905 RVA: 0x002B44A4 File Offset: 0x002B26A4
		public void Update()
		{
			this.cell = Grid.PosToCell(this.navigator);
			this.navigator.abilities.Refresh();
		}

		// Token: 0x060074D2 RID: 29906 RVA: 0x002B44C8 File Offset: 0x002B26C8
		public void Run(object sharedData)
		{
			this.navigator.PathProber.UpdateProbe(this.navigator.NavGrid, this.cell, this.navigator.CurrentNavType, this.navigator.abilities, this.navigator.flags);
		}

		// Token: 0x04005910 RID: 22800
		private int cell;

		// Token: 0x04005911 RID: 22801
		private Navigator navigator;
	}
}
