using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000434 RID: 1076
internal abstract class UserVolumeLoopingUpdater : LoopingSoundParameterUpdater
{
	// Token: 0x0600174C RID: 5964 RVA: 0x00079258 File Offset: 0x00077458
	public UserVolumeLoopingUpdater(string parameter, string player_pref)
		: base(parameter)
	{
		this.playerPref = player_pref;
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x00079278 File Offset: 0x00077478
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UserVolumeLoopingUpdater.Entry entry = new UserVolumeLoopingUpdater.Entry
		{
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000792C4 File Offset: 0x000774C4
	public override void Update(float dt)
	{
		if (string.IsNullOrEmpty(this.playerPref))
		{
			return;
		}
		float @float = KPlayerPrefs.GetFloat(this.playerPref);
		foreach (UserVolumeLoopingUpdater.Entry entry in this.entries)
		{
			EventInstance ev = entry.ev;
			ev.setParameterByID(entry.parameterId, @float, false);
		}
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x00079344 File Offset: 0x00077544
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

	// Token: 0x04000CF2 RID: 3314
	private List<UserVolumeLoopingUpdater.Entry> entries = new List<UserVolumeLoopingUpdater.Entry>();

	// Token: 0x04000CF3 RID: 3315
	private string playerPref;

	// Token: 0x02001057 RID: 4183
	private struct Entry
	{
		// Token: 0x0400572D RID: 22317
		public EventInstance ev;

		// Token: 0x0400572E RID: 22318
		public PARAMETER_ID parameterId;
	}
}
