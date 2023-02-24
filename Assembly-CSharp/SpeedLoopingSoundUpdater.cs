using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000C00 RID: 3072
public class SpeedLoopingSoundUpdater : LoopingSoundParameterUpdater
{
	// Token: 0x0600613D RID: 24893 RVA: 0x0023C4B9 File Offset: 0x0023A6B9
	public SpeedLoopingSoundUpdater()
		: base("Speed")
	{
	}

	// Token: 0x0600613E RID: 24894 RVA: 0x0023C4D8 File Offset: 0x0023A6D8
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		SpeedLoopingSoundUpdater.Entry entry = new SpeedLoopingSoundUpdater.Entry
		{
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x0600613F RID: 24895 RVA: 0x0023C524 File Offset: 0x0023A724
	public override void Update(float dt)
	{
		float speedParameterValue = SpeedLoopingSoundUpdater.GetSpeedParameterValue();
		foreach (SpeedLoopingSoundUpdater.Entry entry in this.entries)
		{
			EventInstance ev = entry.ev;
			ev.setParameterByID(entry.parameterId, speedParameterValue, false);
		}
	}

	// Token: 0x06006140 RID: 24896 RVA: 0x0023C590 File Offset: 0x0023A790
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

	// Token: 0x06006141 RID: 24897 RVA: 0x0023C5E8 File Offset: 0x0023A7E8
	public static float GetSpeedParameterValue()
	{
		return Time.timeScale * 1f;
	}

	// Token: 0x04004302 RID: 17154
	private List<SpeedLoopingSoundUpdater.Entry> entries = new List<SpeedLoopingSoundUpdater.Entry>();

	// Token: 0x02001A9C RID: 6812
	private struct Entry
	{
		// Token: 0x04007811 RID: 30737
		public EventInstance ev;

		// Token: 0x04007812 RID: 30738
		public PARAMETER_ID parameterId;
	}
}
