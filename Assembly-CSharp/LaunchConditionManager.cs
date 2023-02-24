using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200094B RID: 2379
[AddComponentMenu("KMonoBehaviour/scripts/LaunchConditionManager")]
public class LaunchConditionManager : KMonoBehaviour, ISim4000ms, ISim1000ms
{
	// Token: 0x17000528 RID: 1320
	// (get) Token: 0x06004637 RID: 17975 RVA: 0x0018B937 File Offset: 0x00189B37
	// (set) Token: 0x06004638 RID: 17976 RVA: 0x0018B93F File Offset: 0x00189B3F
	public List<RocketModule> rocketModules { get; private set; }

	// Token: 0x06004639 RID: 17977 RVA: 0x0018B948 File Offset: 0x00189B48
	public void DEBUG_TraceModuleDestruction(string moduleName, string state, string stackTrace)
	{
		if (this.DEBUG_ModuleDestructions == null)
		{
			this.DEBUG_ModuleDestructions = new List<global::Tuple<string, string, string>>();
		}
		this.DEBUG_ModuleDestructions.Add(new global::Tuple<string, string, string>(moduleName, state, stackTrace));
	}

	// Token: 0x0600463A RID: 17978 RVA: 0x0018B970 File Offset: 0x00189B70
	[ContextMenu("Dump Module Destructions")]
	private void DEBUG_DumpModuleDestructions()
	{
		if (this.DEBUG_ModuleDestructions == null || this.DEBUG_ModuleDestructions.Count == 0)
		{
			DebugUtil.LogArgs(new object[] { "Sorry, no logged module destructions. :(" });
			return;
		}
		foreach (global::Tuple<string, string, string> tuple in this.DEBUG_ModuleDestructions)
		{
			DebugUtil.LogArgs(new object[] { tuple.first, ">", tuple.second, "\n", tuple.third, "\nEND MODULE DUMP\n\n" });
		}
	}

	// Token: 0x0600463B RID: 17979 RVA: 0x0018BA24 File Offset: 0x00189C24
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.rocketModules = new List<RocketModule>();
	}

	// Token: 0x0600463C RID: 17980 RVA: 0x0018BA37 File Offset: 0x00189C37
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.launchable = base.GetComponent<ILaunchableRocket>();
		this.FindModules();
		base.GetComponent<AttachableBuilding>().onAttachmentNetworkChanged = delegate(object data)
		{
			this.FindModules();
		};
	}

	// Token: 0x0600463D RID: 17981 RVA: 0x0018BA68 File Offset: 0x00189C68
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600463E RID: 17982 RVA: 0x0018BA70 File Offset: 0x00189C70
	public void Sim1000ms(float dt)
	{
		Spacecraft spacecraftFromLaunchConditionManager = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this);
		if (spacecraftFromLaunchConditionManager == null)
		{
			return;
		}
		global::Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
		SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(spacecraftFromLaunchConditionManager.id);
		if (base.gameObject.GetComponent<LogicPorts>().GetInputValue(this.triggerPort) == 1 && spacecraftDestination != null && spacecraftDestination.id != -1)
		{
			this.Launch(spacecraftDestination);
		}
	}

	// Token: 0x0600463F RID: 17983 RVA: 0x0018BAD8 File Offset: 0x00189CD8
	public void FindModules()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
		{
			RocketModule component = gameObject.GetComponent<RocketModule>();
			if (component != null && component.conditionManager == null)
			{
				component.conditionManager = this;
				component.RegisterWithConditionManager();
			}
		}
	}

	// Token: 0x06004640 RID: 17984 RVA: 0x0018BB54 File Offset: 0x00189D54
	public void RegisterRocketModule(RocketModule module)
	{
		if (!this.rocketModules.Contains(module))
		{
			this.rocketModules.Add(module);
		}
	}

	// Token: 0x06004641 RID: 17985 RVA: 0x0018BB70 File Offset: 0x00189D70
	public void UnregisterRocketModule(RocketModule module)
	{
		this.rocketModules.Remove(module);
	}

	// Token: 0x06004642 RID: 17986 RVA: 0x0018BB80 File Offset: 0x00189D80
	public List<ProcessCondition> GetLaunchConditionList()
	{
		List<ProcessCondition> list = new List<ProcessCondition>();
		foreach (RocketModule rocketModule in this.rocketModules)
		{
			foreach (ProcessCondition processCondition in rocketModule.GetConditionSet(ProcessCondition.ProcessConditionType.RocketPrep))
			{
				list.Add(processCondition);
			}
			foreach (ProcessCondition processCondition2 in rocketModule.GetConditionSet(ProcessCondition.ProcessConditionType.RocketStorage))
			{
				list.Add(processCondition2);
			}
		}
		return list;
	}

	// Token: 0x06004643 RID: 17987 RVA: 0x0018BC60 File Offset: 0x00189E60
	public void Launch(SpaceDestination destination)
	{
		if (destination == null)
		{
			global::Debug.LogError("Null destination passed to launch");
		}
		if (SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this).state != Spacecraft.MissionState.Grounded)
		{
			return;
		}
		if (DebugHandler.InstantBuildMode || (this.CheckReadyToLaunch() && this.CheckAbleToFly()))
		{
			this.launchable.LaunchableGameObject.Trigger(705820818, null);
			SpacecraftManager.instance.SetSpacecraftDestination(this, destination);
			SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this).BeginMission(destination);
		}
	}

	// Token: 0x06004644 RID: 17988 RVA: 0x0018BCD8 File Offset: 0x00189ED8
	public bool CheckReadyToLaunch()
	{
		foreach (RocketModule rocketModule in this.rocketModules)
		{
			using (List<ProcessCondition>.Enumerator enumerator2 = rocketModule.GetConditionSet(ProcessCondition.ProcessConditionType.RocketPrep).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.EvaluateCondition() == ProcessCondition.Status.Failure)
					{
						return false;
					}
				}
			}
			using (List<ProcessCondition>.Enumerator enumerator2 = rocketModule.GetConditionSet(ProcessCondition.ProcessConditionType.RocketStorage).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.EvaluateCondition() == ProcessCondition.Status.Failure)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06004645 RID: 17989 RVA: 0x0018BDB8 File Offset: 0x00189FB8
	public bool CheckAbleToFly()
	{
		foreach (RocketModule rocketModule in this.rocketModules)
		{
			using (List<ProcessCondition>.Enumerator enumerator2 = rocketModule.GetConditionSet(ProcessCondition.ProcessConditionType.RocketFlight).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.EvaluateCondition() == ProcessCondition.Status.Failure)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06004646 RID: 17990 RVA: 0x0018BE4C File Offset: 0x0018A04C
	private void ClearFlightStatuses()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		foreach (KeyValuePair<ProcessCondition, Guid> keyValuePair in this.conditionStatuses)
		{
			component.RemoveStatusItem(keyValuePair.Value, false);
		}
		this.conditionStatuses.Clear();
	}

	// Token: 0x06004647 RID: 17991 RVA: 0x0018BEBC File Offset: 0x0018A0BC
	public void Sim4000ms(float dt)
	{
		bool flag = this.CheckReadyToLaunch();
		LogicPorts component = base.gameObject.GetComponent<LogicPorts>();
		if (flag)
		{
			Spacecraft spacecraftFromLaunchConditionManager = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this);
			if (spacecraftFromLaunchConditionManager.state == Spacecraft.MissionState.Grounded || spacecraftFromLaunchConditionManager.state == Spacecraft.MissionState.Launching)
			{
				component.SendSignal(this.statusPort, 1);
			}
			else
			{
				component.SendSignal(this.statusPort, 0);
			}
			KSelectable component2 = base.GetComponent<KSelectable>();
			using (List<RocketModule>.Enumerator enumerator = this.rocketModules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RocketModule rocketModule = enumerator.Current;
					foreach (ProcessCondition processCondition in rocketModule.GetConditionSet(ProcessCondition.ProcessConditionType.RocketFlight))
					{
						if (processCondition.EvaluateCondition() == ProcessCondition.Status.Failure)
						{
							if (!this.conditionStatuses.ContainsKey(processCondition))
							{
								StatusItem statusItem = processCondition.GetStatusItem(ProcessCondition.Status.Failure);
								this.conditionStatuses[processCondition] = component2.AddStatusItem(statusItem, processCondition);
							}
						}
						else if (this.conditionStatuses.ContainsKey(processCondition))
						{
							component2.RemoveStatusItem(this.conditionStatuses[processCondition], false);
							this.conditionStatuses.Remove(processCondition);
						}
					}
				}
				return;
			}
		}
		this.ClearFlightStatuses();
		component.SendSignal(this.statusPort, 0);
	}

	// Token: 0x04002E82 RID: 11906
	public HashedString triggerPort;

	// Token: 0x04002E83 RID: 11907
	public HashedString statusPort;

	// Token: 0x04002E85 RID: 11909
	private ILaunchableRocket launchable;

	// Token: 0x04002E86 RID: 11910
	[Serialize]
	private List<global::Tuple<string, string, string>> DEBUG_ModuleDestructions;

	// Token: 0x04002E87 RID: 11911
	private Dictionary<ProcessCondition, Guid> conditionStatuses = new Dictionary<ProcessCondition, Guid>();

	// Token: 0x02001742 RID: 5954
	public enum ConditionType
	{
		// Token: 0x04006C8C RID: 27788
		Launch,
		// Token: 0x04006C8D RID: 27789
		Flight
	}
}
