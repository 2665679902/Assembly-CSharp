using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x020006A2 RID: 1698
internal class UpdateDistanceToImpactParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06002E1A RID: 11802 RVA: 0x000F328E File Offset: 0x000F148E
	public UpdateDistanceToImpactParameter()
		: base("distanceToImpact")
	{
	}

	// Token: 0x06002E1B RID: 11803 RVA: 0x000F32AC File Offset: 0x000F14AC
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateDistanceToImpactParameter.Entry entry = new UpdateDistanceToImpactParameter.Entry
		{
			comet = sound.transform.GetComponent<Comet>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06002E1C RID: 11804 RVA: 0x000F3308 File Offset: 0x000F1508
	public override void Update(float dt)
	{
		foreach (UpdateDistanceToImpactParameter.Entry entry in this.entries)
		{
			if (!(entry.comet == null))
			{
				float soundDistance = entry.comet.GetSoundDistance();
				EventInstance ev = entry.ev;
				ev.setParameterByID(entry.parameterId, soundDistance, false);
			}
		}
	}

	// Token: 0x06002E1D RID: 11805 RVA: 0x000F3388 File Offset: 0x000F1588
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

	// Token: 0x04001B73 RID: 7027
	private List<UpdateDistanceToImpactParameter.Entry> entries = new List<UpdateDistanceToImpactParameter.Entry>();

	// Token: 0x0200136F RID: 4975
	private struct Entry
	{
		// Token: 0x04006087 RID: 24711
		public Comet comet;

		// Token: 0x04006088 RID: 24712
		public EventInstance ev;

		// Token: 0x04006089 RID: 24713
		public PARAMETER_ID parameterId;
	}
}
