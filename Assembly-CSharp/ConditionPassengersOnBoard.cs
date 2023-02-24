using System;
using STRINGS;

// Token: 0x02000980 RID: 2432
public class ConditionPassengersOnBoard : ProcessCondition
{
	// Token: 0x06004828 RID: 18472 RVA: 0x001958F7 File Offset: 0x00193AF7
	public ConditionPassengersOnBoard(PassengerRocketModule module)
	{
		this.module = module;
	}

	// Token: 0x06004829 RID: 18473 RVA: 0x00195908 File Offset: 0x00193B08
	public override ProcessCondition.Status EvaluateCondition()
	{
		global::Tuple<int, int> crewBoardedFraction = this.module.GetCrewBoardedFraction();
		if (crewBoardedFraction.first != crewBoardedFraction.second)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x0600482A RID: 18474 RVA: 0x00195932 File Offset: 0x00193B32
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.CREW_BOARDED.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.CREW_BOARDED.FAILURE;
	}

	// Token: 0x0600482B RID: 18475 RVA: 0x00195950 File Offset: 0x00193B50
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		global::Tuple<int, int> crewBoardedFraction = this.module.GetCrewBoardedFraction();
		if (status == ProcessCondition.Status.Ready)
		{
			if (crewBoardedFraction.second != 0)
			{
				return string.Format(UI.STARMAP.LAUNCHCHECKLIST.CREW_BOARDED.TOOLTIP.READY, crewBoardedFraction.first, crewBoardedFraction.second);
			}
			return string.Format(UI.STARMAP.LAUNCHCHECKLIST.CREW_BOARDED.TOOLTIP.NONE, crewBoardedFraction.first, crewBoardedFraction.second);
		}
		else
		{
			if (crewBoardedFraction.first == 0)
			{
				return string.Format(UI.STARMAP.LAUNCHCHECKLIST.CREW_BOARDED.TOOLTIP.FAILURE, crewBoardedFraction.first, crewBoardedFraction.second);
			}
			return string.Format(UI.STARMAP.LAUNCHCHECKLIST.CREW_BOARDED.TOOLTIP.WARNING, crewBoardedFraction.first, crewBoardedFraction.second);
		}
	}

	// Token: 0x0600482C RID: 18476 RVA: 0x00195A14 File Offset: 0x00193C14
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F87 RID: 12167
	private PassengerRocketModule module;
}
