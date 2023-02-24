using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x0200097C RID: 2428
public class ConditionHasNosecone : ProcessCondition
{
	// Token: 0x06004814 RID: 18452 RVA: 0x0019552F File Offset: 0x0019372F
	public ConditionHasNosecone(LaunchableRocketCluster launchable)
	{
		this.launchable = launchable;
	}

	// Token: 0x06004815 RID: 18453 RVA: 0x00195540 File Offset: 0x00193740
	public override ProcessCondition.Status EvaluateCondition()
	{
		using (IEnumerator<Ref<RocketModuleCluster>> enumerator = this.launchable.parts.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Get().HasTag(GameTags.NoseRocketModule))
				{
					return ProcessCondition.Status.Ready;
				}
			}
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06004816 RID: 18454 RVA: 0x001955A4 File Offset: 0x001937A4
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x06004817 RID: 18455 RVA: 0x001955E4 File Offset: 0x001937E4
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x06004818 RID: 18456 RVA: 0x00195624 File Offset: 0x00193824
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F81 RID: 12161
	private LaunchableRocketCluster launchable;
}
