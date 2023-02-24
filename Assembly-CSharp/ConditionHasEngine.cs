using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200097A RID: 2426
public class ConditionHasEngine : ProcessCondition
{
	// Token: 0x06004809 RID: 18441 RVA: 0x00195022 File Offset: 0x00193222
	public ConditionHasEngine(ILaunchableRocket launchable)
	{
		this.launchable = launchable;
	}

	// Token: 0x0600480A RID: 18442 RVA: 0x00195034 File Offset: 0x00193234
	public override ProcessCondition.Status EvaluateCondition()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.launchable.LaunchableGameObject.GetComponent<AttachableBuilding>()))
		{
			if (gameObject.GetComponent<RocketEngine>() != null || gameObject.GetComponent<RocketEngineCluster>())
			{
				return ProcessCondition.Status.Ready;
			}
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x0600480B RID: 18443 RVA: 0x001950B4 File Offset: 0x001932B4
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x0600480C RID: 18444 RVA: 0x001950F4 File Offset: 0x001932F4
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x0600480D RID: 18445 RVA: 0x00195134 File Offset: 0x00193334
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F7F RID: 12159
	private ILaunchableRocket launchable;
}
