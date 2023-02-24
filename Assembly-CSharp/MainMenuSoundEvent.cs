using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class MainMenuSoundEvent : SoundEvent
{
	// Token: 0x060016B2 RID: 5810 RVA: 0x00075D52 File Offset: 0x00073F52
	public MainMenuSoundEvent(string file_name, string sound_name, int frame)
		: base(file_name, sound_name, frame, true, false, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x00075D68 File Offset: 0x00073F68
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		EventInstance eventInstance = KFMOD.BeginOneShot(base.sound, Vector3.zero, 1f);
		if (eventInstance.isValid())
		{
			eventInstance.setParameterByName("frame", (float)base.frame, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}
}
