using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000425 RID: 1061
[Serializable]
public class RemoteSoundEvent : SoundEvent
{
	// Token: 0x060016BD RID: 5821 RVA: 0x0007617A File Offset: 0x0007437A
	public RemoteSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, true, false, min_interval, false)
	{
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x0007618C File Offset: 0x0007438C
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		Vector3 vector = behaviour.GetComponent<Transform>().GetPosition();
		vector.z = 0f;
		if (SoundEvent.ObjectIsSelectedAndVisible(behaviour.controller.gameObject))
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		Workable workable = behaviour.GetComponent<Worker>().workable;
		if (workable != null)
		{
			Toggleable component = workable.GetComponent<Toggleable>();
			if (component != null)
			{
				IToggleHandler toggleHandlerForWorker = component.GetToggleHandlerForWorker(behaviour.GetComponent<Worker>());
				float num = 1f;
				if (toggleHandlerForWorker != null && toggleHandlerForWorker.IsHandlerOn())
				{
					num = 0f;
				}
				if (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, base.sound, base.soundHash, base.looping, this.isDynamic))
				{
					EventInstance eventInstance = SoundEvent.BeginOneShot(base.sound, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible), false);
					eventInstance.setParameterByName("State", num, false);
					SoundEvent.EndOneShot(eventInstance);
				}
			}
		}
	}

	// Token: 0x04000CA9 RID: 3241
	private const string STATE_PARAMETER = "State";
}
