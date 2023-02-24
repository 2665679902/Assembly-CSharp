using System;
using STRINGS;

// Token: 0x0200097F RID: 2431
public class ConditionOnLaunchPad : ProcessCondition
{
	// Token: 0x06004823 RID: 18467 RVA: 0x0019584D File Offset: 0x00193A4D
	public ConditionOnLaunchPad(CraftModuleInterface craftInterface)
	{
		this.craftInterface = craftInterface;
	}

	// Token: 0x06004824 RID: 18468 RVA: 0x0019585C File Offset: 0x00193A5C
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (!(this.craftInterface.CurrentPad != null))
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06004825 RID: 18469 RVA: 0x00195874 File Offset: 0x00193A74
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x06004826 RID: 18470 RVA: 0x001958B4 File Offset: 0x00193AB4
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x06004827 RID: 18471 RVA: 0x001958F4 File Offset: 0x00193AF4
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F86 RID: 12166
	private CraftModuleInterface craftInterface;
}
