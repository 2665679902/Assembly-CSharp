using System;
using STRINGS;

// Token: 0x0200097D RID: 2429
public class ConditionHasResource : ProcessCondition
{
	// Token: 0x06004819 RID: 18457 RVA: 0x00195627 File Offset: 0x00193827
	public ConditionHasResource(Storage storage, SimHashes resource, float thresholdMass)
	{
		this.storage = storage;
		this.resource = resource;
		this.thresholdMass = thresholdMass;
	}

	// Token: 0x0600481A RID: 18458 RVA: 0x00195644 File Offset: 0x00193844
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.storage.GetAmountAvailable(this.resource.CreateTag()) < this.thresholdMass)
		{
			return ProcessCondition.Status.Warning;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x0600481B RID: 18459 RVA: 0x00195668 File Offset: 0x00193868
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = string.Format(UI.STARMAP.LAUNCHCHECKLIST.HAS_RESOURCE.STATUS.READY, this.storage.GetProperName(), ElementLoader.GetElement(this.resource.CreateTag()).name);
			}
			else
			{
				text = string.Format(UI.STARMAP.LAUNCHCHECKLIST.HAS_RESOURCE.STATUS.WARNING, this.storage.GetProperName(), ElementLoader.GetElement(this.resource.CreateTag()).name);
			}
		}
		else
		{
			text = string.Format(UI.STARMAP.LAUNCHCHECKLIST.HAS_RESOURCE.STATUS.FAILURE, this.storage.GetProperName(), ElementLoader.GetElement(this.resource.CreateTag()).name);
		}
		return text;
	}

	// Token: 0x0600481C RID: 18460 RVA: 0x00195718 File Offset: 0x00193918
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = string.Format(UI.STARMAP.LAUNCHCHECKLIST.HAS_RESOURCE.TOOLTIP.READY, this.storage.GetProperName(), ElementLoader.GetElement(this.resource.CreateTag()).name);
			}
			else
			{
				text = string.Format(UI.STARMAP.LAUNCHCHECKLIST.HAS_RESOURCE.TOOLTIP.WARNING, this.storage.GetProperName(), GameUtil.GetFormattedMass(this.thresholdMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), ElementLoader.GetElement(this.resource.CreateTag()).name);
			}
		}
		else
		{
			text = string.Format(UI.STARMAP.LAUNCHCHECKLIST.HAS_RESOURCE.TOOLTIP.FAILURE, this.storage.GetProperName(), GameUtil.GetFormattedMass(this.thresholdMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), ElementLoader.GetElement(this.resource.CreateTag()).name);
		}
		return text;
	}

	// Token: 0x0600481D RID: 18461 RVA: 0x001957F0 File Offset: 0x001939F0
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F82 RID: 12162
	private Storage storage;

	// Token: 0x04002F83 RID: 12163
	private SimHashes resource;

	// Token: 0x04002F84 RID: 12164
	private float thresholdMass;
}
