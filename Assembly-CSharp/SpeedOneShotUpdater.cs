using System;

// Token: 0x02000C01 RID: 3073
public class SpeedOneShotUpdater : OneShotSoundParameterUpdater
{
	// Token: 0x06006142 RID: 24898 RVA: 0x0023C5F5 File Offset: 0x0023A7F5
	public SpeedOneShotUpdater()
		: base("Speed")
	{
	}

	// Token: 0x06006143 RID: 24899 RVA: 0x0023C607 File Offset: 0x0023A807
	public override void Play(OneShotSoundParameterUpdater.Sound sound)
	{
		sound.ev.setParameterByID(sound.description.GetParameterId(base.parameter), SpeedLoopingSoundUpdater.GetSpeedParameterValue(), false);
	}
}
