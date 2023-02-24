using System;

// Token: 0x0200042D RID: 1069
public class UIAnimationSoundEvent : SoundEvent
{
	// Token: 0x0600170B RID: 5899 RVA: 0x000778D4 File Offset: 0x00075AD4
	public UIAnimationSoundEvent(string file_name, string sound_name, int frame, bool looping)
		: base(file_name, sound_name, frame, true, looping, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x000778E9 File Offset: 0x00075AE9
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		this.PlaySound(behaviour);
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x000778F4 File Offset: 0x00075AF4
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component == null)
			{
				Debug.Log(behaviour.name + " (UI Object) is missing LoopingSounds component.");
				return;
			}
			if (!component.StartSound(base.sound, false, false, false))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", base.sound, behaviour.name) });
				return;
			}
		}
		else
		{
			try
			{
				if (SoundListenerController.Instance == null)
				{
					KFMOD.PlayUISound(base.sound);
				}
				else
				{
					KFMOD.PlayOneShot(base.sound, SoundListenerController.Instance.transform.GetPosition(), 1f);
				}
			}
			catch
			{
				DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + base.sound + "]" });
			}
		}
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x000779E0 File Offset: 0x00075BE0
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StopSound(base.sound);
			}
		}
	}
}
