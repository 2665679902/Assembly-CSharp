using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000410 RID: 1040
public class CreatureChewSoundEvent : SoundEvent
{
	// Token: 0x060015A0 RID: 5536 RVA: 0x000702C0 File Offset: 0x0006E4C0
	public CreatureChewSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, false, false, min_interval, true)
	{
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000702D0 File Offset: 0x0006E4D0
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		string sound = GlobalAssets.GetSound(StringFormatter.Combine(base.name, "_", CreatureChewSoundEvent.GetChewSound(behaviour)), false);
		GameObject gameObject = behaviour.controller.gameObject;
		base.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
		if (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, sound, base.looping, this.isDynamic))
		{
			Vector3 vector = behaviour.GetComponent<Transform>().GetPosition();
			vector.z = 0f;
			if (base.objectIsSelectedAndVisible)
			{
				vector = SoundEvent.AudioHighlightListenerPosition(vector);
			}
			EventInstance eventInstance = SoundEvent.BeginOneShot(sound, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible), false);
			if (behaviour.controller.gameObject.GetDef<BabyMonitor.Def>() != null)
			{
				eventInstance.setParameterByName("isBaby", 1f, false);
			}
			SoundEvent.EndOneShot(eventInstance);
		}
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x0007039C File Offset: 0x0006E59C
	private static string GetChewSound(AnimEventManager.EventPlayerData behaviour)
	{
		string text = CreatureChewSoundEvent.DEFAULT_CHEW_SOUND;
		EatStates.Instance smi = behaviour.controller.GetSMI<EatStates.Instance>();
		if (smi != null)
		{
			Element latestMealElement = smi.GetLatestMealElement();
			if (latestMealElement != null)
			{
				string creatureChewSound = latestMealElement.substance.GetCreatureChewSound();
				if (!string.IsNullOrEmpty(creatureChewSound))
				{
					text = creatureChewSound;
				}
			}
		}
		return text;
	}

	// Token: 0x04000C0F RID: 3087
	private static string DEFAULT_CHEW_SOUND = "Rock";

	// Token: 0x04000C10 RID: 3088
	private const string FMOD_PARAM_IS_BABY_ID = "isBaby";
}
