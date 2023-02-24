using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000505 RID: 1285
internal class UpdatePercentCompleteParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06001E9B RID: 7835 RVA: 0x000A3BE5 File Offset: 0x000A1DE5
	public UpdatePercentCompleteParameter()
		: base("percentComplete")
	{
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x000A3C04 File Offset: 0x000A1E04
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdatePercentCompleteParameter.Entry entry = new UpdatePercentCompleteParameter.Entry
		{
			worker = sound.transform.GetComponent<Worker>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x000A3C60 File Offset: 0x000A1E60
	public override void Update(float dt)
	{
		foreach (UpdatePercentCompleteParameter.Entry entry in this.entries)
		{
			if (!(entry.worker == null))
			{
				Workable workable = entry.worker.workable;
				if (!(workable == null))
				{
					float percentComplete = workable.GetPercentComplete();
					EventInstance ev = entry.ev;
					ev.setParameterByID(entry.parameterId, percentComplete, false);
				}
			}
		}
	}

	// Token: 0x06001E9E RID: 7838 RVA: 0x000A3CF0 File Offset: 0x000A1EF0
	public override void Remove(LoopingSoundParameterUpdater.Sound sound)
	{
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (this.entries[i].ev.handle == sound.ev.handle)
			{
				this.entries.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x04001131 RID: 4401
	private List<UpdatePercentCompleteParameter.Entry> entries = new List<UpdatePercentCompleteParameter.Entry>();

	// Token: 0x02001141 RID: 4417
	private struct Entry
	{
		// Token: 0x04005A5D RID: 23133
		public Worker worker;

		// Token: 0x04005A5E RID: 23134
		public EventInstance ev;

		// Token: 0x04005A5F RID: 23135
		public PARAMETER_ID parameterId;
	}
}
