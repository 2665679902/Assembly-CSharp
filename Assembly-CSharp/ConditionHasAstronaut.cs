using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x02000977 RID: 2423
public class ConditionHasAstronaut : ProcessCondition
{
	// Token: 0x060047FA RID: 18426 RVA: 0x00194E4B File Offset: 0x0019304B
	public ConditionHasAstronaut(CommandModule module)
	{
		this.module = module;
	}

	// Token: 0x060047FB RID: 18427 RVA: 0x00194E5C File Offset: 0x0019305C
	public override ProcessCondition.Status EvaluateCondition()
	{
		List<MinionStorage.Info> storedMinionInfo = this.module.GetComponent<MinionStorage>().GetStoredMinionInfo();
		if (storedMinionInfo.Count > 0 && storedMinionInfo[0].serializedMinion != null)
		{
			return ProcessCondition.Status.Ready;
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x060047FC RID: 18428 RVA: 0x00194E94 File Offset: 0x00193094
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.ASTRONAUT_TITLE;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.ASTRONAUGHT;
	}

	// Token: 0x060047FD RID: 18429 RVA: 0x00194EAF File Offset: 0x001930AF
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HASASTRONAUT;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.ASTRONAUGHT;
	}

	// Token: 0x060047FE RID: 18430 RVA: 0x00194ECA File Offset: 0x001930CA
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F7C RID: 12156
	private CommandModule module;
}
