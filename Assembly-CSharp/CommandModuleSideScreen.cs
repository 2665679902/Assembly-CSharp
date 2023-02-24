using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BA2 RID: 2978
public class CommandModuleSideScreen : SideScreenContent
{
	// Token: 0x06005DAE RID: 23982 RVA: 0x00223644 File Offset: 0x00221844
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ScheduleUpdate();
		MultiToggle multiToggle = this.debugVictoryButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			SpaceDestination spaceDestination = SpacecraftManager.instance.destinations.Find((SpaceDestination match) => match.GetDestinationType() == Db.Get().SpaceDestinationTypes.Wormhole);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Clothe8Dupes.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Build4NatureReserves.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.ReachedSpace.Id);
			this.target.Launch(spaceDestination);
		}));
		this.debugVictoryButton.gameObject.SetActive(DebugHandler.InstantBuildMode && this.CheckHydrogenRocket());
	}

	// Token: 0x06005DAF RID: 23983 RVA: 0x002236A4 File Offset: 0x002218A4
	private bool CheckHydrogenRocket()
	{
		RocketModule rocketModule = this.target.rocketModules.Find((RocketModule match) => match.GetComponent<RocketEngine>());
		return rocketModule != null && rocketModule.GetComponent<RocketEngine>().fuelTag == ElementLoader.FindElementByHash(SimHashes.LiquidHydrogen).tag;
	}

	// Token: 0x06005DB0 RID: 23984 RVA: 0x0022370B File Offset: 0x0022190B
	private void ScheduleUpdate()
	{
		this.updateHandle = UIScheduler.Instance.Schedule("RefreshCommandModuleSideScreen", 1f, delegate(object o)
		{
			this.RefreshConditions();
			this.ScheduleUpdate();
		}, null, null);
	}

	// Token: 0x06005DB1 RID: 23985 RVA: 0x00223735 File Offset: 0x00221935
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LaunchConditionManager>() != null;
	}

	// Token: 0x06005DB2 RID: 23986 RVA: 0x00223744 File Offset: 0x00221944
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<LaunchConditionManager>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received does not contain a LaunchConditionManager component");
			return;
		}
		this.ClearConditions();
		this.ConfigureConditions();
		this.debugVictoryButton.gameObject.SetActive(DebugHandler.InstantBuildMode && this.CheckHydrogenRocket());
	}

	// Token: 0x06005DB3 RID: 23987 RVA: 0x002237B8 File Offset: 0x002219B8
	private void ClearConditions()
	{
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair in this.conditionTable)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.conditionTable.Clear();
	}

	// Token: 0x06005DB4 RID: 23988 RVA: 0x0022381C File Offset: 0x00221A1C
	private void ConfigureConditions()
	{
		foreach (ProcessCondition processCondition in this.target.GetLaunchConditionList())
		{
			GameObject gameObject = Util.KInstantiateUI(this.prefabConditionLineItem, this.conditionListContainer, true);
			this.conditionTable.Add(processCondition, gameObject);
		}
		this.RefreshConditions();
	}

	// Token: 0x06005DB5 RID: 23989 RVA: 0x00223894 File Offset: 0x00221A94
	public void RefreshConditions()
	{
		bool flag = false;
		List<ProcessCondition> launchConditionList = this.target.GetLaunchConditionList();
		foreach (ProcessCondition processCondition in launchConditionList)
		{
			if (!this.conditionTable.ContainsKey(processCondition))
			{
				flag = true;
				break;
			}
			GameObject gameObject = this.conditionTable[processCondition];
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			if (processCondition.GetParentCondition() != null && processCondition.GetParentCondition().EvaluateCondition() == ProcessCondition.Status.Failure)
			{
				gameObject.SetActive(false);
			}
			else if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
			ProcessCondition.Status status = processCondition.EvaluateCondition();
			bool flag2 = status == ProcessCondition.Status.Ready;
			component.GetReference<LocText>("Label").text = processCondition.GetStatusMessage(status);
			component.GetReference<LocText>("Label").color = (flag2 ? Color.black : Color.red);
			component.GetReference<Image>("Box").color = (flag2 ? Color.black : Color.red);
			component.GetReference<Image>("Check").gameObject.SetActive(flag2);
			gameObject.GetComponent<ToolTip>().SetSimpleTooltip(processCondition.GetStatusTooltip(status));
		}
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair in this.conditionTable)
		{
			if (!launchConditionList.Contains(keyValuePair.Key))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.ClearConditions();
			this.ConfigureConditions();
		}
		this.destinationButton.gameObject.SetActive(ManagementMenu.StarmapAvailable());
		this.destinationButton.onClick = delegate
		{
			ManagementMenu.Instance.ToggleStarmap();
		};
	}

	// Token: 0x06005DB6 RID: 23990 RVA: 0x00223A94 File Offset: 0x00221C94
	protected override void OnCleanUp()
	{
		this.updateHandle.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x04004003 RID: 16387
	private LaunchConditionManager target;

	// Token: 0x04004004 RID: 16388
	public GameObject conditionListContainer;

	// Token: 0x04004005 RID: 16389
	public GameObject prefabConditionLineItem;

	// Token: 0x04004006 RID: 16390
	public MultiToggle destinationButton;

	// Token: 0x04004007 RID: 16391
	public MultiToggle debugVictoryButton;

	// Token: 0x04004008 RID: 16392
	[Tooltip("This list is indexed by the ProcessCondition.Status enum")]
	public List<Color> statusColors;

	// Token: 0x04004009 RID: 16393
	private Dictionary<ProcessCondition, GameObject> conditionTable = new Dictionary<ProcessCondition, GameObject>();

	// Token: 0x0400400A RID: 16394
	private SchedulerHandle updateHandle;
}
