using System;
using STRINGS;

// Token: 0x02000982 RID: 2434
public class ConditionProperlyFueled : ProcessCondition
{
	// Token: 0x06004832 RID: 18482 RVA: 0x00195A71 File Offset: 0x00193C71
	public ConditionProperlyFueled(IFuelTank fuelTank)
	{
		this.fuelTank = fuelTank;
	}

	// Token: 0x06004833 RID: 18483 RVA: 0x00195A80 File Offset: 0x00193C80
	public override ProcessCondition.Status EvaluateCondition()
	{
		RocketModuleCluster component = ((KMonoBehaviour)this.fuelTank).GetComponent<RocketModuleCluster>();
		if (component != null && component.CraftInterface != null)
		{
			Clustercraft component2 = component.CraftInterface.GetComponent<Clustercraft>();
			ClusterTraveler component3 = component.CraftInterface.GetComponent<ClusterTraveler>();
			if (component2 == null || component3 == null || component3.CurrentPath == null)
			{
				return ProcessCondition.Status.Failure;
			}
			int num = component3.RemainingTravelNodes();
			if (num == 0)
			{
				if (!component2.HasResourcesToMove(1, Clustercraft.CombustionResource.Fuel))
				{
					return ProcessCondition.Status.Failure;
				}
				return ProcessCondition.Status.Ready;
			}
			else
			{
				bool flag = component2.HasResourcesToMove(num * 2, Clustercraft.CombustionResource.Fuel);
				bool flag2 = component2.HasResourcesToMove(num, Clustercraft.CombustionResource.Fuel);
				if (flag)
				{
					return ProcessCondition.Status.Ready;
				}
				if (flag2)
				{
					return ProcessCondition.Status.Warning;
				}
			}
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06004834 RID: 18484 RVA: 0x00195B24 File Offset: 0x00193D24
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.PROPERLY_FUELED.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.PROPERLY_FUELED.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.PROPERLY_FUELED.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x06004835 RID: 18485 RVA: 0x00195B64 File Offset: 0x00193D64
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		Clustercraft component = ((KMonoBehaviour)this.fuelTank).GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				if (component.Destination == component.Location)
				{
					text = UI.STARMAP.LAUNCHCHECKLIST.PROPERLY_FUELED.TOOLTIP.READY_NO_DESTINATION;
				}
				else
				{
					text = UI.STARMAP.LAUNCHCHECKLIST.PROPERLY_FUELED.TOOLTIP.READY;
				}
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.PROPERLY_FUELED.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.PROPERLY_FUELED.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x06004836 RID: 18486 RVA: 0x00195BDF File Offset: 0x00193DDF
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F89 RID: 12169
	private IFuelTank fuelTank;
}
