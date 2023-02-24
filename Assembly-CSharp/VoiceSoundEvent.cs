using System;
using FMOD.Studio;
using Klei.AI;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class VoiceSoundEvent : SoundEvent
{
	// Token: 0x06001713 RID: 5907 RVA: 0x00077BC5 File Offset: 0x00075DC5
	public VoiceSoundEvent(string file_name, string sound_name, int frame, bool is_looping)
		: base(file_name, sound_name, frame, false, is_looping, (float)SoundEvent.IGNORE_INTERVAL, true)
	{
		base.noiseValues = SoundEventVolumeCache.instance.GetVolume("VoiceSoundEvent", sound_name);
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x00077BFB File Offset: 0x00075DFB
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		VoiceSoundEvent.PlayVoice(base.name, behaviour.controller, this.intervalBetweenSpeaking, base.looping, false);
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x00077C1C File Offset: 0x00075E1C
	public static EventInstance PlayVoice(string name, KBatchedAnimController controller, float interval_between_speaking, bool looping, bool objectIsSelectedAndVisible = false)
	{
		EventInstance eventInstance = default(EventInstance);
		MinionIdentity component = controller.GetComponent<MinionIdentity>();
		if (component == null || (name.Contains("state") && Time.time - component.timeLastSpoke < interval_between_speaking))
		{
			return eventInstance;
		}
		if (name.Contains(":"))
		{
			float num = float.Parse(name.Split(new char[] { ':' })[1]);
			if ((float)UnityEngine.Random.Range(0, 100) > num)
			{
				return eventInstance;
			}
		}
		Worker component2 = controller.GetComponent<Worker>();
		string assetName = VoiceSoundEvent.GetAssetName(name, component2);
		StaminaMonitor.Instance smi = component2.GetSMI<StaminaMonitor.Instance>();
		if (!name.Contains("sleep_") && smi != null && smi.IsSleeping())
		{
			return eventInstance;
		}
		Vector3 vector = component2.transform.GetPosition();
		vector.z = 0f;
		if (SoundEvent.ObjectIsSelectedAndVisible(controller.gameObject))
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		string sound = GlobalAssets.GetSound(assetName, true);
		if (!SoundEvent.ShouldPlaySound(controller, sound, looping, false))
		{
			return eventInstance;
		}
		if (sound != null)
		{
			if (looping)
			{
				LoopingSounds component3 = controller.GetComponent<LoopingSounds>();
				if (component3 == null)
				{
					global::Debug.Log(controller.name + " is missing LoopingSounds component. ");
				}
				else if (!component3.StartSound(sound))
				{
					DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", sound, controller.name) });
				}
			}
			else
			{
				eventInstance = SoundEvent.BeginOneShot(sound, vector, 1f, false);
				if (sound.Contains("sleep_") && controller.GetComponent<Traits>().HasTrait("Snorer"))
				{
					eventInstance.setParameterByName("snoring", 1f, false);
				}
				SoundEvent.EndOneShot(eventInstance);
				component.timeLastSpoke = Time.time;
			}
		}
		else if (AudioDebug.Get().debugVoiceSounds)
		{
			global::Debug.LogWarning("Missing voice sound: " + assetName);
		}
		return eventInstance;
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x00077DE8 File Offset: 0x00075FE8
	private static string GetAssetName(string name, Component cmp)
	{
		string text = "F01";
		if (cmp != null)
		{
			MinionIdentity component = cmp.GetComponent<MinionIdentity>();
			if (component != null)
			{
				text = component.GetVoiceId();
			}
		}
		string text2 = name;
		if (name.Contains(":"))
		{
			text2 = name.Split(new char[] { ':' })[0];
		}
		return StringFormatter.Combine("DupVoc_", text, "_", text2);
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x00077E50 File Offset: 0x00076050
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				string sound = GlobalAssets.GetSound(VoiceSoundEvent.GetAssetName(base.name, component), true);
				component.StopSound(sound);
			}
		}
	}

	// Token: 0x04000CC5 RID: 3269
	public static float locomotionSoundProb = 50f;

	// Token: 0x04000CC6 RID: 3270
	public float timeLastSpoke;

	// Token: 0x04000CC7 RID: 3271
	public float intervalBetweenSpeaking = 10f;
}
