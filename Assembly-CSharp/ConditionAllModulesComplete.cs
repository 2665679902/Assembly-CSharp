using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000974 RID: 2420
public class ConditionAllModulesComplete : ProcessCondition
{
	// Token: 0x060047E3 RID: 18403 RVA: 0x00194713 File Offset: 0x00192913
	public ConditionAllModulesComplete(ILaunchableRocket launchable)
	{
		this.launchable = launchable;
	}

	// Token: 0x060047E4 RID: 18404 RVA: 0x00194724 File Offset: 0x00192924
	public override ProcessCondition.Status EvaluateCondition()
	{
		using (List<GameObject>.Enumerator enumerator = AttachableBuilding.GetAttachedNetwork(this.launchable.LaunchableGameObject.GetComponent<AttachableBuilding>()).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetComponent<Constructable>() != null)
				{
					return ProcessCondition.Status.Failure;
				}
			}
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x060047E5 RID: 18405 RVA: 0x00194794 File Offset: 0x00192994
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x060047E6 RID: 18406 RVA: 0x001947D4 File Offset: 0x001929D4
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x060047E7 RID: 18407 RVA: 0x00194814 File Offset: 0x00192A14
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F74 RID: 12148
	private ILaunchableRocket launchable;
}
