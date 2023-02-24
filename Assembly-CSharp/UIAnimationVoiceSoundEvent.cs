using System;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class UIAnimationVoiceSoundEvent : SoundEvent
{
	// Token: 0x0600170F RID: 5903 RVA: 0x00077A12 File Offset: 0x00075C12
	public UIAnimationVoiceSoundEvent(string file_name, string sound_name, int frame, bool looping)
		: base(file_name, sound_name, frame, true, looping, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
		this.actualSoundName = sound_name;
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x00077A2E File Offset: 0x00075C2E
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		this.PlaySound(behaviour);
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x00077A38 File Offset: 0x00075C38
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		string soundPath = MinionVoice.ByObject(behaviour.controller).UnwrapOr(MinionVoice.Random(), string.Format("Couldn't find MinionVoice on UI {0}, falling back to random voice", behaviour.controller)).GetSoundPath(this.actualSoundName);
		if (this.actualSoundName.Contains(":"))
		{
			float num = float.Parse(this.actualSoundName.Split(new char[] { ':' })[1]);
			if ((float)UnityEngine.Random.Range(0, 100) > num)
			{
				return;
			}
		}
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component == null)
			{
				global::Debug.Log(behaviour.name + " (UI Object) is missing LoopingSounds component.");
			}
			else if (!component.StartSound(soundPath, false, false, false))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", soundPath, behaviour.name) });
			}
			this.lastPlayedLoopingSoundPath = soundPath;
			return;
		}
		try
		{
			if (SoundListenerController.Instance == null)
			{
				KFMOD.PlayUISound(soundPath);
			}
			else
			{
				KFMOD.PlayOneShot(soundPath, SoundListenerController.Instance.transform.GetPosition(), 1f);
			}
		}
		catch
		{
			DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + soundPath + "]" });
		}
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x00077B84 File Offset: 0x00075D84
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null && this.lastPlayedLoopingSoundPath != null)
			{
				component.StopSound(this.lastPlayedLoopingSoundPath);
			}
		}
		this.lastPlayedLoopingSoundPath = null;
	}

	// Token: 0x04000CC3 RID: 3267
	private string actualSoundName;

	// Token: 0x04000CC4 RID: 3268
	private string lastPlayedLoopingSoundPath;
}
