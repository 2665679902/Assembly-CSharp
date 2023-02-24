using System;
using STRINGS;

// Token: 0x02000985 RID: 2437
public class ConditionSufficientOxidizer : ProcessCondition
{
	// Token: 0x06004841 RID: 18497 RVA: 0x00195D06 File Offset: 0x00193F06
	public ConditionSufficientOxidizer(OxidizerTank oxidizerTank)
	{
		this.oxidizerTank = oxidizerTank;
	}

	// Token: 0x06004842 RID: 18498 RVA: 0x00195D18 File Offset: 0x00193F18
	public override ProcessCondition.Status EvaluateCondition()
	{
		RocketModuleCluster component = this.oxidizerTank.GetComponent<RocketModuleCluster>();
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
				if (!component2.HasResourcesToMove(1, Clustercraft.CombustionResource.Oxidizer))
				{
					return ProcessCondition.Status.Failure;
				}
				return ProcessCondition.Status.Ready;
			}
			else
			{
				bool flag = component2.HasResourcesToMove(num * 2, Clustercraft.CombustionResource.Oxidizer);
				bool flag2 = component2.HasResourcesToMove(num, Clustercraft.CombustionResource.Oxidizer);
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

	// Token: 0x06004843 RID: 18499 RVA: 0x00195DB8 File Offset: 0x00193FB8
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.SUFFICIENT_OXIDIZER.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.SUFFICIENT_OXIDIZER.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.SUFFICIENT_OXIDIZER.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x06004844 RID: 18500 RVA: 0x00195DF8 File Offset: 0x00193FF8
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.SUFFICIENT_OXIDIZER.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.SUFFICIENT_OXIDIZER.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.SUFFICIENT_OXIDIZER.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x06004845 RID: 18501 RVA: 0x00195E38 File Offset: 0x00194038
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F8C RID: 12172
	private OxidizerTank oxidizerTank;
}
