using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000466 RID: 1126
internal class UpdateConsumedMassParameter : LoopingSoundParameterUpdater
{
	// Token: 0x060018FC RID: 6396 RVA: 0x000858E7 File Offset: 0x00083AE7
	public UpdateConsumedMassParameter()
		: base("consumedMass")
	{
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x00085904 File Offset: 0x00083B04
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateConsumedMassParameter.Entry entry = new UpdateConsumedMassParameter.Entry
		{
			creatureCalorieMonitor = sound.transform.GetSMI<CreatureCalorieMonitor.Instance>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x060018FE RID: 6398 RVA: 0x00085960 File Offset: 0x00083B60
	public override void Update(float dt)
	{
		foreach (UpdateConsumedMassParameter.Entry entry in this.entries)
		{
			if (!entry.creatureCalorieMonitor.IsNullOrStopped())
			{
				float fullness = entry.creatureCalorieMonitor.stomach.GetFullness();
				EventInstance ev = entry.ev;
				ev.setParameterByID(entry.parameterId, fullness, false);
			}
		}
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x000859E4 File Offset: 0x00083BE4
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

	// Token: 0x04000DFB RID: 3579
	private List<UpdateConsumedMassParameter.Entry> entries = new List<UpdateConsumedMassParameter.Entry>();

	// Token: 0x02001098 RID: 4248
	private struct Entry
	{
		// Token: 0x04005829 RID: 22569
		public CreatureCalorieMonitor.Instance creatureCalorieMonitor;

		// Token: 0x0400582A RID: 22570
		public EventInstance ev;

		// Token: 0x0400582B RID: 22571
		public PARAMETER_ID parameterId;
	}
}
