using System;
using STRINGS;

// Token: 0x02000983 RID: 2435
public class ConditionRocketHeight : ProcessCondition
{
	// Token: 0x06004837 RID: 18487 RVA: 0x00195BE2 File Offset: 0x00193DE2
	public ConditionRocketHeight(RocketEngineCluster engine)
	{
		this.engine = engine;
	}

	// Token: 0x06004838 RID: 18488 RVA: 0x00195BF1 File Offset: 0x00193DF1
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.engine.maxHeight < this.engine.GetComponent<RocketModuleCluster>().CraftInterface.RocketHeight)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06004839 RID: 18489 RVA: 0x00195C18 File Offset: 0x00193E18
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x0600483A RID: 18490 RVA: 0x00195C58 File Offset: 0x00193E58
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x0600483B RID: 18491 RVA: 0x00195C98 File Offset: 0x00193E98
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F8A RID: 12170
	private RocketEngineCluster engine;
}
