using System;

// Token: 0x02000421 RID: 1057
public class MouthFlapSoundEvent : SoundEvent
{
	// Token: 0x060016B4 RID: 5812 RVA: 0x00075DB0 File Offset: 0x00073FB0
	public MouthFlapSoundEvent(string file_name, string sound_name, int frame, bool is_looping)
		: base(file_name, sound_name, frame, false, is_looping, (float)SoundEvent.IGNORE_INTERVAL, true)
	{
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x00075DC5 File Offset: 0x00073FC5
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		behaviour.controller.GetSMI<SpeechMonitor.Instance>().PlaySpeech(base.name, null);
	}
}
