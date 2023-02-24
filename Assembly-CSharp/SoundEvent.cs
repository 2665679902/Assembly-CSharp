using System;
using System.Diagnostics;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000428 RID: 1064
[DebuggerDisplay("{Name}")]
public class SoundEvent : AnimEvent
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00076697 File Offset: 0x00074897
	// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0007669F File Offset: 0x0007489F
	public string sound { get; private set; }

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000766A8 File Offset: 0x000748A8
	// (set) Token: 0x060016CA RID: 5834 RVA: 0x000766B0 File Offset: 0x000748B0
	public HashedString soundHash { get; private set; }

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x060016CB RID: 5835 RVA: 0x000766B9 File Offset: 0x000748B9
	// (set) Token: 0x060016CC RID: 5836 RVA: 0x000766C1 File Offset: 0x000748C1
	public bool looping { get; private set; }

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x060016CD RID: 5837 RVA: 0x000766CA File Offset: 0x000748CA
	// (set) Token: 0x060016CE RID: 5838 RVA: 0x000766D2 File Offset: 0x000748D2
	public bool ignorePause { get; set; }

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x060016CF RID: 5839 RVA: 0x000766DB File Offset: 0x000748DB
	// (set) Token: 0x060016D0 RID: 5840 RVA: 0x000766E3 File Offset: 0x000748E3
	public bool shouldCameraScalePosition { get; set; }

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x060016D1 RID: 5841 RVA: 0x000766EC File Offset: 0x000748EC
	// (set) Token: 0x060016D2 RID: 5842 RVA: 0x000766F4 File Offset: 0x000748F4
	public float minInterval { get; private set; }

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000766FD File Offset: 0x000748FD
	// (set) Token: 0x060016D4 RID: 5844 RVA: 0x00076705 File Offset: 0x00074905
	public bool objectIsSelectedAndVisible { get; set; }

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0007670E File Offset: 0x0007490E
	// (set) Token: 0x060016D6 RID: 5846 RVA: 0x00076716 File Offset: 0x00074916
	public EffectorValues noiseValues { get; set; }

	// Token: 0x060016D7 RID: 5847 RVA: 0x0007671F File Offset: 0x0007491F
	public SoundEvent()
	{
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x00076728 File Offset: 0x00074928
	public SoundEvent(string file_name, string sound_name, int frame, bool do_load, bool is_looping, float min_interval, bool is_dynamic)
		: base(file_name, sound_name, frame)
	{
		this.shouldCameraScalePosition = true;
		if (do_load)
		{
			this.sound = GlobalAssets.GetSound(sound_name, false);
			this.soundHash = new HashedString(this.sound);
			string.IsNullOrEmpty(this.sound);
		}
		this.minInterval = min_interval;
		this.looping = is_looping;
		this.isDynamic = is_dynamic;
		this.noiseValues = SoundEventVolumeCache.instance.GetVolume(file_name, sound_name);
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x0007679D File Offset: 0x0007499D
	public static bool ObjectIsSelectedAndVisible(GameObject go)
	{
		return false;
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x000767A0 File Offset: 0x000749A0
	public static Vector3 AudioHighlightListenerPosition(Vector3 sound_pos)
	{
		Vector3 position = SoundListenerController.Instance.transform.position;
		float num = 1f * sound_pos.x + 0f * position.x;
		float num2 = 1f * sound_pos.y + 0f * position.y;
		float num3 = 0f * position.z;
		return new Vector3(num, num2, num3);
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x00076808 File Offset: 0x00074A08
	public static float GetVolume(bool objectIsSelectedAndVisible)
	{
		float num = 1f;
		if (objectIsSelectedAndVisible)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x00076825 File Offset: 0x00074A25
	public static bool ShouldPlaySound(KBatchedAnimController controller, string sound, bool is_looping, bool is_dynamic)
	{
		return SoundEvent.ShouldPlaySound(controller, sound, sound, is_looping, is_dynamic);
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x00076838 File Offset: 0x00074A38
	public static bool ShouldPlaySound(KBatchedAnimController controller, string sound, HashedString soundHash, bool is_looping, bool is_dynamic)
	{
		CameraController instance = CameraController.Instance;
		if (instance == null)
		{
			return true;
		}
		Vector3 position = controller.transform.GetPosition();
		Vector3 offset = controller.Offset;
		position.x += offset.x;
		position.y += offset.y;
		if (!SoundCuller.IsAudibleWorld(position))
		{
			return false;
		}
		SpeedControlScreen instance2 = SpeedControlScreen.Instance;
		if (is_dynamic)
		{
			return (!(instance2 != null) || !instance2.IsPaused) && instance.IsAudibleSound(position);
		}
		if (sound == null || SoundEvent.IsLowPrioritySound(sound))
		{
			return false;
		}
		if (!instance.IsAudibleSound(position, soundHash))
		{
			if (!is_looping && !GlobalAssets.IsHighPriority(sound))
			{
				return false;
			}
		}
		else if (instance2 != null && instance2.IsPaused)
		{
			return false;
		}
		return true;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x00076904 File Offset: 0x00074B04
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		GameObject gameObject = behaviour.controller.gameObject;
		this.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
		if (this.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, this.sound, this.soundHash, this.looping, this.isDynamic))
		{
			this.PlaySound(behaviour);
		}
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x00076960 File Offset: 0x00074B60
	protected void PlaySound(AnimEventManager.EventPlayerData behaviour, string sound)
	{
		Vector3 vector = behaviour.GetComponent<Transform>().GetPosition();
		vector.z = 0f;
		if (SoundEvent.ObjectIsSelectedAndVisible(behaviour.controller.gameObject))
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		KBatchedAnimController component = behaviour.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			Vector3 offset = component.Offset;
			vector.x += offset.x;
			vector.y += offset.y;
		}
		AudioDebug audioDebug = AudioDebug.Get();
		if (audioDebug != null && audioDebug.debugSoundEvents)
		{
			string[] array = new string[7];
			array[0] = behaviour.name;
			array[1] = ", ";
			array[2] = sound;
			array[3] = ", ";
			array[4] = base.frame.ToString();
			array[5] = ", ";
			int num = 6;
			Vector3 vector2 = vector;
			array[num] = vector2.ToString();
			global::Debug.Log(string.Concat(array));
		}
		try
		{
			if (this.looping)
			{
				LoopingSounds component2 = behaviour.GetComponent<LoopingSounds>();
				if (component2 == null)
				{
					global::Debug.Log(behaviour.name + " is missing LoopingSounds component. ");
				}
				else if (!component2.StartSound(sound, behaviour, this.noiseValues, this.ignorePause, this.shouldCameraScalePosition))
				{
					DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", sound, behaviour.name) });
				}
			}
			else if (!SoundEvent.PlayOneShot(sound, behaviour, this.noiseValues, SoundEvent.GetVolume(this.objectIsSelectedAndVisible), this.objectIsSelectedAndVisible))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", sound, behaviour.name) });
			}
		}
		catch (Exception ex)
		{
			string text = string.Format(("Error trying to trigger sound [{0}] in behaviour [{1}] [{2}]\n{3}" + sound != null) ? sound.ToString() : "null", behaviour.GetType().ToString(), ex.Message, ex.StackTrace);
			global::Debug.LogError(text);
			throw new ArgumentException(text, ex);
		}
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x00076B60 File Offset: 0x00074D60
	public virtual void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		this.PlaySound(behaviour, this.sound);
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x00076B70 File Offset: 0x00074D70
	public static Vector3 GetCameraScaledPosition(Vector3 pos, bool objectIsSelectedAndVisible = false)
	{
		Vector3 vector = Vector3.zero;
		if (CameraController.Instance != null)
		{
			vector = CameraController.Instance.GetVerticallyScaledPosition(pos, objectIsSelectedAndVisible);
		}
		return vector;
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x00076B9E File Offset: 0x00074D9E
	public static FMOD.Studio.EventInstance BeginOneShot(EventReference event_ref, Vector3 pos, float volume = 1f, bool objectIsSelectedAndVisible = false)
	{
		return KFMOD.BeginOneShot(event_ref, SoundEvent.GetCameraScaledPosition(pos, objectIsSelectedAndVisible), volume);
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x00076BAE File Offset: 0x00074DAE
	public static FMOD.Studio.EventInstance BeginOneShot(string ev, Vector3 pos, float volume = 1f, bool objectIsSelectedAndVisible = false)
	{
		return SoundEvent.BeginOneShot(RuntimeManager.PathToEventReference(ev), pos, volume, false);
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x00076BBE File Offset: 0x00074DBE
	public static bool EndOneShot(FMOD.Studio.EventInstance instance)
	{
		return KFMOD.EndOneShot(instance);
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x00076BC8 File Offset: 0x00074DC8
	public static bool PlayOneShot(EventReference event_ref, Vector3 sound_pos, float volume = 1f)
	{
		bool flag = false;
		if (!event_ref.IsNull)
		{
			FMOD.Studio.EventInstance eventInstance = SoundEvent.BeginOneShot(event_ref, sound_pos, volume, false);
			if (eventInstance.isValid())
			{
				flag = SoundEvent.EndOneShot(eventInstance);
			}
		}
		return flag;
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x00076BFB File Offset: 0x00074DFB
	public static bool PlayOneShot(string sound, Vector3 sound_pos, float volume = 1f)
	{
		return SoundEvent.PlayOneShot(RuntimeManager.PathToEventReference(sound), sound_pos, volume);
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x00076C0C File Offset: 0x00074E0C
	public static bool PlayOneShot(string sound, AnimEventManager.EventPlayerData behaviour, EffectorValues noiseValues, float volume = 1f, bool objectIsSelectedAndVisible = false)
	{
		bool flag = false;
		if (!string.IsNullOrEmpty(sound))
		{
			Vector3 vector = behaviour.GetComponent<Transform>().GetPosition();
			vector.z = 0f;
			if (objectIsSelectedAndVisible)
			{
				vector = SoundEvent.AudioHighlightListenerPosition(vector);
			}
			FMOD.Studio.EventInstance eventInstance = SoundEvent.BeginOneShot(sound, vector, volume, false);
			if (eventInstance.isValid())
			{
				flag = SoundEvent.EndOneShot(eventInstance);
			}
		}
		return flag;
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x00076C64 File Offset: 0x00074E64
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (this.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StopSound(this.sound);
			}
		}
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x00076C96 File Offset: 0x00074E96
	protected static bool IsLowPrioritySound(string sound)
	{
		return sound != null && Camera.main != null && Camera.main.orthographicSize > AudioMixer.LOW_PRIORITY_CUTOFF_DISTANCE && !AudioMixer.instance.activeNIS && GlobalAssets.IsLowPriority(sound);
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x00076CD0 File Offset: 0x00074ED0
	protected void PrintSoundDebug(string anim_name, string sound, string sound_name, Vector3 sound_pos)
	{
		if (sound != null)
		{
			string[] array = new string[7];
			array[0] = anim_name;
			array[1] = ", ";
			array[2] = sound_name;
			array[3] = ", ";
			array[4] = base.frame.ToString();
			array[5] = ", ";
			int num = 6;
			Vector3 vector = sound_pos;
			array[num] = vector.ToString();
			global::Debug.Log(string.Concat(array));
			return;
		}
		global::Debug.Log("Missing sound: " + anim_name + ", " + sound_name);
	}

	// Token: 0x04000CAE RID: 3246
	public static int IGNORE_INTERVAL = -1;

	// Token: 0x04000CB7 RID: 3255
	protected bool isDynamic;
}
