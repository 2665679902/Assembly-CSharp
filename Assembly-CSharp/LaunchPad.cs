using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x0200094C RID: 2380
public class LaunchPad : KMonoBehaviour, ISim1000ms, IListableOption, IProcessConditionSet
{
	// Token: 0x17000529 RID: 1321
	// (get) Token: 0x0600464A RID: 17994 RVA: 0x0018C040 File Offset: 0x0018A240
	public RocketModuleCluster LandedRocket
	{
		get
		{
			GameObject gameObject = null;
			Grid.ObjectLayers[1].TryGetValue(this.RocketBottomPosition, out gameObject);
			if (gameObject != null)
			{
				RocketModuleCluster component = gameObject.GetComponent<RocketModuleCluster>();
				Clustercraft clustercraft = ((component != null && component.CraftInterface != null) ? component.CraftInterface.GetComponent<Clustercraft>() : null);
				if (clustercraft != null && (clustercraft.Status == Clustercraft.CraftStatus.Grounded || clustercraft.Status == Clustercraft.CraftStatus.Landing))
				{
					return component;
				}
			}
			return null;
		}
	}

	// Token: 0x1700052A RID: 1322
	// (get) Token: 0x0600464B RID: 17995 RVA: 0x0018C0B7 File Offset: 0x0018A2B7
	public int RocketBottomPosition
	{
		get
		{
			return Grid.OffsetCell(Grid.PosToCell(this), this.baseModulePosition);
		}
	}

	// Token: 0x0600464C RID: 17996 RVA: 0x0018C0CC File Offset: 0x0018A2CC
	[OnDeserialized]
	private void OnDeserialzed()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 24))
		{
			Building component = base.GetComponent<Building>();
			if (component != null)
			{
				component.RunOnArea(delegate(int cell)
				{
					if (Grid.IsValidCell(cell) && Grid.Solid[cell])
					{
						SimMessages.ReplaceElement(cell, SimHashes.Vacuum, CellEventLogger.Instance.LaunchpadDesolidify, 0f, -1f, byte.MaxValue, 0, -1);
					}
				});
			}
		}
	}

	// Token: 0x0600464D RID: 17997 RVA: 0x0018C128 File Offset: 0x0018A328
	protected override void OnPrefabInit()
	{
		UserNameable component = base.GetComponent<UserNameable>();
		if (component != null)
		{
			component.SetName(GameUtil.GenerateRandomLaunchPadName());
		}
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x0018C150 File Offset: 0x0018A350
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.tower = new LaunchPad.LaunchPadTower(this, this.maxTowerHeight);
		this.OnRocketBuildingChanged(this.GetRocketBaseModule());
		this.partitionerEntry = GameScenePartitioner.Instance.Add("LaunchPad.OnSpawn", base.gameObject, Extents.OneCell(this.RocketBottomPosition), GameScenePartitioner.Instance.objectLayers[1], new Action<object>(this.OnRocketBuildingChanged));
		Components.LaunchPads.Add(this);
		this.CheckLandedRocketPassengerModuleStatus();
		int num = ConditionFlightPathIsClear.PadTopEdgeDistanceToCeilingEdge(base.gameObject);
		if (num < 35)
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.RocketPlatformCloseToCeiling, num);
		}
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x0018C204 File Offset: 0x0018A404
	protected override void OnCleanUp()
	{
		Components.LaunchPads.Remove(this);
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		if (this.lastBaseAttachable != null)
		{
			AttachableBuilding attachableBuilding = this.lastBaseAttachable;
			attachableBuilding.onAttachmentNetworkChanged = (Action<object>)Delegate.Remove(attachableBuilding.onAttachmentNetworkChanged, new Action<object>(this.OnRocketLayoutChanged));
			this.lastBaseAttachable = null;
		}
		this.RebuildLaunchTowerHeightHandler.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x06004650 RID: 18000 RVA: 0x0018C27C File Offset: 0x0018A47C
	private void CheckLandedRocketPassengerModuleStatus()
	{
		if (this.LandedRocket == null)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.landedRocketPassengerModuleStatusItem, false);
			this.landedRocketPassengerModuleStatusItem = Guid.Empty;
			return;
		}
		if (this.LandedRocket.CraftInterface.GetPassengerModule() == null)
		{
			if (this.landedRocketPassengerModuleStatusItem == Guid.Empty)
			{
				this.landedRocketPassengerModuleStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.LandedRocketLacksPassengerModule, null);
				return;
			}
		}
		else if (this.landedRocketPassengerModuleStatusItem != Guid.Empty)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.landedRocketPassengerModuleStatusItem, false);
			this.landedRocketPassengerModuleStatusItem = Guid.Empty;
		}
	}

	// Token: 0x06004651 RID: 18001 RVA: 0x0018C334 File Offset: 0x0018A534
	public bool IsLogicInputConnected()
	{
		int portCell = base.GetComponent<LogicPorts>().GetPortCell(this.triggerPort);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell) != null;
	}

	// Token: 0x06004652 RID: 18002 RVA: 0x0018C368 File Offset: 0x0018A568
	public void Sim1000ms(float dt)
	{
		LogicPorts component = base.gameObject.GetComponent<LogicPorts>();
		RocketModuleCluster landedRocket = this.LandedRocket;
		if (landedRocket != null && this.IsLogicInputConnected())
		{
			if (component.GetInputValue(this.triggerPort) == 1)
			{
				if (landedRocket.CraftInterface.CheckReadyForAutomatedLaunchCommand())
				{
					landedRocket.CraftInterface.TriggerLaunch(true);
				}
				else
				{
					landedRocket.CraftInterface.CancelLaunch();
				}
			}
			else
			{
				landedRocket.CraftInterface.CancelLaunch();
			}
		}
		this.CheckLandedRocketPassengerModuleStatus();
		component.SendSignal(this.landedRocketPort, (landedRocket != null) ? 1 : 0);
		if (landedRocket != null)
		{
			component.SendSignal(this.statusPort, (landedRocket.CraftInterface.CheckReadyForAutomatedLaunch() || landedRocket.CraftInterface.HasTag(GameTags.RocketNotOnGround)) ? 1 : 0);
			return;
		}
		component.SendSignal(this.statusPort, 0);
	}

	// Token: 0x06004653 RID: 18003 RVA: 0x0018C440 File Offset: 0x0018A640
	public GameObject AddBaseModule(BuildingDef moduleDefID, IList<Tag> elements)
	{
		int num = Grid.OffsetCell(Grid.PosToCell(base.gameObject), this.baseModulePosition);
		GameObject gameObject;
		if (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive)
		{
			gameObject = moduleDefID.Build(num, Orientation.Neutral, null, elements, 293.15f, true, GameClock.Instance.GetTime());
		}
		else
		{
			gameObject = moduleDefID.TryPlace(null, Grid.CellToPosCBC(num, moduleDefID.SceneLayer), Orientation.Neutral, elements, 0);
		}
		GameObject gameObject2 = Util.KInstantiate(Assets.GetPrefab("Clustercraft"), null, null);
		gameObject2.SetActive(true);
		Clustercraft component = gameObject2.GetComponent<Clustercraft>();
		component.GetComponent<CraftModuleInterface>().AddModule(gameObject.GetComponent<RocketModuleCluster>());
		component.Init(this.GetMyWorldLocation(), this);
		if (gameObject.GetComponent<BuildingUnderConstruction>() != null)
		{
			this.OnRocketBuildingChanged(gameObject);
		}
		base.Trigger(374403796, null);
		return gameObject;
	}

	// Token: 0x06004654 RID: 18004 RVA: 0x0018C510 File Offset: 0x0018A710
	private void OnRocketBuildingChanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		RocketModuleCluster landedRocket = this.LandedRocket;
		global::Debug.Assert(gameObject == null || landedRocket == null || landedRocket.gameObject == gameObject, "Launch Pad had a rocket land or take off on it twice??");
		Clustercraft clustercraft = ((landedRocket != null && landedRocket.CraftInterface != null) ? landedRocket.CraftInterface.GetComponent<Clustercraft>() : null);
		if (clustercraft != null)
		{
			if (clustercraft.Status == Clustercraft.CraftStatus.Landing)
			{
				base.Trigger(-887025858, landedRocket);
			}
			else if (clustercraft.Status == Clustercraft.CraftStatus.Launching)
			{
				base.Trigger(-1277991738, landedRocket);
				AttachableBuilding component = landedRocket.CraftInterface.ClusterModules[0].Get().GetComponent<AttachableBuilding>();
				component.onAttachmentNetworkChanged = (Action<object>)Delegate.Remove(component.onAttachmentNetworkChanged, new Action<object>(this.OnRocketLayoutChanged));
			}
		}
		this.OnRocketLayoutChanged(null);
	}

	// Token: 0x06004655 RID: 18005 RVA: 0x0018C5F4 File Offset: 0x0018A7F4
	private void OnRocketLayoutChanged(object data)
	{
		if (this.lastBaseAttachable != null)
		{
			AttachableBuilding attachableBuilding = this.lastBaseAttachable;
			attachableBuilding.onAttachmentNetworkChanged = (Action<object>)Delegate.Remove(attachableBuilding.onAttachmentNetworkChanged, new Action<object>(this.OnRocketLayoutChanged));
			this.lastBaseAttachable = null;
		}
		GameObject rocketBaseModule = this.GetRocketBaseModule();
		if (rocketBaseModule != null)
		{
			this.lastBaseAttachable = rocketBaseModule.GetComponent<AttachableBuilding>();
			AttachableBuilding attachableBuilding2 = this.lastBaseAttachable;
			attachableBuilding2.onAttachmentNetworkChanged = (Action<object>)Delegate.Combine(attachableBuilding2.onAttachmentNetworkChanged, new Action<object>(this.OnRocketLayoutChanged));
		}
		this.DirtyTowerHeight();
	}

	// Token: 0x06004656 RID: 18006 RVA: 0x0018C686 File Offset: 0x0018A886
	public bool HasRocket()
	{
		return this.LandedRocket != null;
	}

	// Token: 0x06004657 RID: 18007 RVA: 0x0018C694 File Offset: 0x0018A894
	public bool HasRocketWithCommandModule()
	{
		return this.HasRocket() && this.LandedRocket.CraftInterface.FindLaunchableRocket() != null;
	}

	// Token: 0x06004658 RID: 18008 RVA: 0x0018C6B8 File Offset: 0x0018A8B8
	private GameObject GetRocketBaseModule()
	{
		GameObject gameObject = Grid.Objects[Grid.OffsetCell(Grid.PosToCell(base.gameObject), this.baseModulePosition), 1];
		if (!(gameObject != null) || !(gameObject.GetComponent<RocketModule>() != null))
		{
			return null;
		}
		return gameObject;
	}

	// Token: 0x06004659 RID: 18009 RVA: 0x0018C704 File Offset: 0x0018A904
	public void DirtyTowerHeight()
	{
		if (!this.dirtyTowerHeight)
		{
			this.dirtyTowerHeight = true;
			if (!this.RebuildLaunchTowerHeightHandler.IsValid)
			{
				this.RebuildLaunchTowerHeightHandler = GameScheduler.Instance.ScheduleNextFrame("RebuildLaunchTowerHeight", new Action<object>(this.RebuildLaunchTowerHeight), null, null);
			}
		}
	}

	// Token: 0x0600465A RID: 18010 RVA: 0x0018C750 File Offset: 0x0018A950
	private void RebuildLaunchTowerHeight(object obj)
	{
		RocketModuleCluster landedRocket = this.LandedRocket;
		if (landedRocket != null)
		{
			this.tower.SetTowerHeight(landedRocket.CraftInterface.MaxHeight);
		}
		this.dirtyTowerHeight = false;
		this.RebuildLaunchTowerHeightHandler.ClearScheduler();
	}

	// Token: 0x0600465B RID: 18011 RVA: 0x0018C795 File Offset: 0x0018A995
	public string GetProperName()
	{
		return base.gameObject.GetProperName();
	}

	// Token: 0x0600465C RID: 18012 RVA: 0x0018C7A4 File Offset: 0x0018A9A4
	public List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType)
	{
		RocketProcessConditionDisplayTarget rocketProcessConditionDisplayTarget = null;
		RocketModuleCluster landedRocket = this.LandedRocket;
		if (landedRocket != null)
		{
			for (int i = 0; i < landedRocket.CraftInterface.ClusterModules.Count; i++)
			{
				RocketProcessConditionDisplayTarget component = landedRocket.CraftInterface.ClusterModules[i].Get().GetComponent<RocketProcessConditionDisplayTarget>();
				if (component != null)
				{
					rocketProcessConditionDisplayTarget = component;
					break;
				}
			}
		}
		if (rocketProcessConditionDisplayTarget != null)
		{
			return ((IProcessConditionSet)rocketProcessConditionDisplayTarget).GetConditionSet(conditionType);
		}
		return new List<ProcessCondition>();
	}

	// Token: 0x0600465D RID: 18013 RVA: 0x0018C820 File Offset: 0x0018AA20
	public static List<LaunchPad> GetLaunchPadsForDestination(AxialI destination)
	{
		List<LaunchPad> list = new List<LaunchPad>();
		foreach (object obj in Components.LaunchPads)
		{
			LaunchPad launchPad = (LaunchPad)obj;
			if (launchPad.GetMyWorldLocation() == destination)
			{
				list.Add(launchPad);
			}
		}
		return list;
	}

	// Token: 0x04002E88 RID: 11912
	public HashedString triggerPort;

	// Token: 0x04002E89 RID: 11913
	public HashedString statusPort;

	// Token: 0x04002E8A RID: 11914
	public HashedString landedRocketPort;

	// Token: 0x04002E8B RID: 11915
	private CellOffset baseModulePosition = new CellOffset(0, 2);

	// Token: 0x04002E8C RID: 11916
	private SchedulerHandle RebuildLaunchTowerHeightHandler;

	// Token: 0x04002E8D RID: 11917
	private AttachableBuilding lastBaseAttachable;

	// Token: 0x04002E8E RID: 11918
	private LaunchPad.LaunchPadTower tower;

	// Token: 0x04002E8F RID: 11919
	[Serialize]
	public int maxTowerHeight;

	// Token: 0x04002E90 RID: 11920
	private bool dirtyTowerHeight;

	// Token: 0x04002E91 RID: 11921
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002E92 RID: 11922
	private Guid landedRocketPassengerModuleStatusItem = Guid.Empty;

	// Token: 0x02001743 RID: 5955
	public class LaunchPadTower
	{
		// Token: 0x06008A3D RID: 35389 RVA: 0x002FAED0 File Offset: 0x002F90D0
		public LaunchPadTower(LaunchPad pad, int startHeight)
		{
			this.pad = pad;
			this.SetTowerHeight(startHeight);
		}

		// Token: 0x06008A3E RID: 35390 RVA: 0x002FAF88 File Offset: 0x002F9188
		public void AddTowerRow()
		{
			GameObject gameObject = new GameObject("LaunchPadTowerRow");
			gameObject.SetActive(false);
			gameObject.transform.SetParent(this.pad.transform);
			gameObject.transform.SetLocalPosition(Grid.CellSizeInMeters * Vector3.up * (float)(this.towerAnimControllers.Count + this.pad.baseModulePosition.y));
			gameObject.transform.SetPosition(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Grid.GetLayerZ(Grid.SceneLayer.Backwall)));
			KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
			kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("rocket_launchpad_tower_kanim") };
			gameObject.SetActive(true);
			this.towerAnimControllers.Add(kbatchedAnimController);
			kbatchedAnimController.initialAnim = this.towerBGAnimNames[this.towerAnimControllers.Count % this.towerBGAnimNames.Length] + this.towerBGAnimSuffix_off;
			this.animLink = new KAnimLink(this.pad.GetComponent<KAnimControllerBase>(), kbatchedAnimController);
		}

		// Token: 0x06008A3F RID: 35391 RVA: 0x002FB0AC File Offset: 0x002F92AC
		public void RemoveTowerRow()
		{
		}

		// Token: 0x06008A40 RID: 35392 RVA: 0x002FB0B0 File Offset: 0x002F92B0
		public void SetTowerHeight(int height)
		{
			if (height < 8)
			{
				height = 0;
			}
			this.targetHeight = height;
			this.pad.maxTowerHeight = height;
			while (this.targetHeight > this.towerAnimControllers.Count)
			{
				this.AddTowerRow();
			}
			if (this.activeAnimationRoutine != null)
			{
				this.pad.StopCoroutine(this.activeAnimationRoutine);
			}
			this.activeAnimationRoutine = this.pad.StartCoroutine(this.TowerRoutine());
		}

		// Token: 0x06008A41 RID: 35393 RVA: 0x002FB122 File Offset: 0x002F9322
		private IEnumerator TowerRoutine()
		{
			while (this.currentHeight < this.targetHeight)
			{
				LaunchPad.LaunchPadTower.<>c__DisplayClass15_0 CS$<>8__locals1 = new LaunchPad.LaunchPadTower.<>c__DisplayClass15_0();
				CS$<>8__locals1.animComplete = false;
				this.towerAnimControllers[this.currentHeight].Queue(this.towerBGAnimNames[this.currentHeight % this.towerBGAnimNames.Length] + this.towerBGAnimSuffix_on_pre, KAnim.PlayMode.Once, 1f, 0f);
				this.towerAnimControllers[this.currentHeight].Queue(this.towerBGAnimNames[this.currentHeight % this.towerBGAnimNames.Length] + this.towerBGAnimSuffix_on, KAnim.PlayMode.Once, 1f, 0f);
				this.towerAnimControllers[this.currentHeight].onAnimComplete += delegate(HashedString arg)
				{
					CS$<>8__locals1.animComplete = true;
				};
				float delay = 0.25f;
				while (!CS$<>8__locals1.animComplete && delay > 0f)
				{
					delay -= Time.deltaTime;
					yield return 0;
				}
				this.currentHeight++;
				CS$<>8__locals1 = null;
			}
			while (this.currentHeight > this.targetHeight)
			{
				LaunchPad.LaunchPadTower.<>c__DisplayClass15_1 CS$<>8__locals2 = new LaunchPad.LaunchPadTower.<>c__DisplayClass15_1();
				this.currentHeight--;
				CS$<>8__locals2.animComplete = false;
				this.towerAnimControllers[this.currentHeight].Queue(this.towerBGAnimNames[this.currentHeight % this.towerBGAnimNames.Length] + this.towerBGAnimSuffix_off_pre, KAnim.PlayMode.Once, 1f, 0f);
				this.towerAnimControllers[this.currentHeight].Queue(this.towerBGAnimNames[this.currentHeight % this.towerBGAnimNames.Length] + this.towerBGAnimSuffix_off, KAnim.PlayMode.Once, 1f, 0f);
				this.towerAnimControllers[this.currentHeight].onAnimComplete += delegate(HashedString arg)
				{
					CS$<>8__locals2.animComplete = true;
				};
				float delay = 0.25f;
				while (!CS$<>8__locals2.animComplete && delay > 0f)
				{
					delay -= Time.deltaTime;
					yield return 0;
				}
				CS$<>8__locals2 = null;
			}
			yield return 0;
			yield break;
		}

		// Token: 0x04006C8E RID: 27790
		private LaunchPad pad;

		// Token: 0x04006C8F RID: 27791
		private KAnimLink animLink;

		// Token: 0x04006C90 RID: 27792
		private Coroutine activeAnimationRoutine;

		// Token: 0x04006C91 RID: 27793
		private string[] towerBGAnimNames = new string[] { "A1", "A2", "A3", "B", "C", "D", "E1", "E2", "F1", "F2" };

		// Token: 0x04006C92 RID: 27794
		private string towerBGAnimSuffix_on = "_on";

		// Token: 0x04006C93 RID: 27795
		private string towerBGAnimSuffix_on_pre = "_on_pre";

		// Token: 0x04006C94 RID: 27796
		private string towerBGAnimSuffix_off_pre = "_off_pre";

		// Token: 0x04006C95 RID: 27797
		private string towerBGAnimSuffix_off = "_off";

		// Token: 0x04006C96 RID: 27798
		private List<KBatchedAnimController> towerAnimControllers = new List<KBatchedAnimController>();

		// Token: 0x04006C97 RID: 27799
		private int targetHeight;

		// Token: 0x04006C98 RID: 27800
		private int currentHeight;
	}
}
