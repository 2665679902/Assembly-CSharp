using System;

// Token: 0x02000439 RID: 1081
internal abstract class UserVolumeOneShotUpdater : OneShotSoundParameterUpdater
{
	// Token: 0x06001754 RID: 5972 RVA: 0x000793E4 File Offset: 0x000775E4
	public UserVolumeOneShotUpdater(string parameter, string player_pref)
		: base(parameter)
	{
		this.playerPref = player_pref;
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x000793FC File Offset: 0x000775FC
	public override void Play(OneShotSoundParameterUpdater.Sound sound)
	{
		if (!string.IsNullOrEmpty(this.playerPref))
		{
			float @float = KPlayerPrefs.GetFloat(this.playerPref);
			sound.ev.setParameterByID(sound.description.GetParameterId(base.parameter), @float, false);
		}
	}

	// Token: 0x04000CF4 RID: 3316
	private string playerPref;
}
