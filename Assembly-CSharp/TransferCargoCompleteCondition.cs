using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200098A RID: 2442
public class TransferCargoCompleteCondition : ProcessCondition
{
	// Token: 0x0600485A RID: 18522 RVA: 0x00196113 File Offset: 0x00194313
	public TransferCargoCompleteCondition(GameObject target)
	{
		this.target = target;
	}

	// Token: 0x0600485B RID: 18523 RVA: 0x00196124 File Offset: 0x00194324
	public override ProcessCondition.Status EvaluateCondition()
	{
		LaunchPad component = this.target.GetComponent<LaunchPad>();
		CraftModuleInterface craftModuleInterface;
		if (component == null)
		{
			craftModuleInterface = this.target.GetComponent<Clustercraft>().ModuleInterface;
		}
		else
		{
			RocketModuleCluster landedRocket = component.LandedRocket;
			if (landedRocket == null)
			{
				return ProcessCondition.Status.Ready;
			}
			craftModuleInterface = landedRocket.CraftInterface;
		}
		if (!craftModuleInterface.HasCargoModule)
		{
			return ProcessCondition.Status.Ready;
		}
		if (!this.target.HasTag(GameTags.TransferringCargoComplete))
		{
			return ProcessCondition.Status.Warning;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x0600485C RID: 18524 RVA: 0x00196193 File Offset: 0x00194393
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.CARGO_TRANSFER_COMPLETE.STATUS.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.CARGO_TRANSFER_COMPLETE.STATUS.WARNING;
	}

	// Token: 0x0600485D RID: 18525 RVA: 0x001961AE File Offset: 0x001943AE
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.CARGO_TRANSFER_COMPLETE.TOOLTIP.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.CARGO_TRANSFER_COMPLETE.TOOLTIP.WARNING;
	}

	// Token: 0x0600485E RID: 18526 RVA: 0x001961C9 File Offset: 0x001943C9
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F95 RID: 12181
	private GameObject target;
}
