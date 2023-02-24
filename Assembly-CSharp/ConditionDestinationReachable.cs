using System;
using STRINGS;

// Token: 0x02000975 RID: 2421
public class ConditionDestinationReachable : ProcessCondition
{
	// Token: 0x060047E8 RID: 18408 RVA: 0x00194817 File Offset: 0x00192A17
	public ConditionDestinationReachable(RocketModule module)
	{
		this.module = module;
		this.craftRegisterType = module.GetComponent<ILaunchableRocket>().registerType;
	}

	// Token: 0x060047E9 RID: 18409 RVA: 0x00194838 File Offset: 0x00192A38
	public override ProcessCondition.Status EvaluateCondition()
	{
		ProcessCondition.Status status = ProcessCondition.Status.Failure;
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft)
			{
				if (!this.module.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<RocketClusterDestinationSelector>().IsAtDestination())
				{
					status = ProcessCondition.Status.Ready;
				}
			}
		}
		else
		{
			int id = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.module.GetComponent<LaunchConditionManager>()).id;
			SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(id);
			if (spacecraftDestination != null && this.CanReachSpacecraftDestination(spacecraftDestination) && spacecraftDestination.GetDestinationType().visitable)
			{
				status = ProcessCondition.Status.Ready;
			}
		}
		return status;
	}

	// Token: 0x060047EA RID: 18410 RVA: 0x001948BC File Offset: 0x00192ABC
	public bool CanReachSpacecraftDestination(SpaceDestination destination)
	{
		Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
		float rocketMaxDistance = this.module.GetComponent<CommandModule>().rocketStats.GetRocketMaxDistance();
		return (float)destination.OneBasedDistance * 10000f <= rocketMaxDistance;
	}

	// Token: 0x060047EB RID: 18411 RVA: 0x00194900 File Offset: 0x00192B00
	public SpaceDestination GetSpacecraftDestination()
	{
		Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
		int id = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.module.GetComponent<LaunchConditionManager>()).id;
		return SpacecraftManager.instance.GetSpacecraftDestination(id);
	}

	// Token: 0x060047EC RID: 18412 RVA: 0x00194940 File Offset: 0x00192B40
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text = "";
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft)
			{
				text = UI.STARMAP.DESTINATIONSELECTION.REACHABLE;
			}
		}
		else if (status == ProcessCondition.Status.Ready && this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION.REACHABLE;
		}
		else if (this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION.UNREACHABLE;
		}
		else
		{
			text = UI.STARMAP.DESTINATIONSELECTION.NOTSELECTED;
		}
		return text;
	}

	// Token: 0x060047ED RID: 18413 RVA: 0x001949AC File Offset: 0x00192BAC
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text = "";
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft)
			{
				if (status == ProcessCondition.Status.Ready)
				{
					text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.REACHABLE;
				}
				else
				{
					text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.NOTSELECTED;
				}
			}
		}
		else if (status == ProcessCondition.Status.Ready && this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.REACHABLE;
		}
		else if (this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.UNREACHABLE;
		}
		else
		{
			text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.NOTSELECTED;
		}
		return text;
	}

	// Token: 0x060047EE RID: 18414 RVA: 0x00194A27 File Offset: 0x00192C27
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F75 RID: 12149
	private LaunchableRocketRegisterType craftRegisterType;

	// Token: 0x04002F76 RID: 12150
	private RocketModule module;
}
