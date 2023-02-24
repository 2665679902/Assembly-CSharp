using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000499 RID: 1177
[AddComponentMenu("KMonoBehaviour/scripts/LoopingSoundManager")]
public class LoopingSoundManager : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06001A6E RID: 6766 RVA: 0x0008CE60 File Offset: 0x0008B060
	public static void DestroyInstance()
	{
		LoopingSoundManager.instance = null;
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x0008CE68 File Offset: 0x0008B068
	protected override void OnPrefabInit()
	{
		LoopingSoundManager.instance = this;
		this.CollectParameterUpdaters();
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x0008CE78 File Offset: 0x0008B078
	protected override void OnSpawn()
	{
		if (SpeedControlScreen.Instance != null && Game.Instance != null)
		{
			Game.Instance.Subscribe(-1788536802, new Action<object>(LoopingSoundManager.instance.OnPauseChanged));
		}
		Game.Instance.Subscribe(1983128072, delegate(object worlds)
		{
			this.OnActiveWorldChanged();
		});
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x0008CEDB File Offset: 0x0008B0DB
	private void OnActiveWorldChanged()
	{
		this.StopAllSounds();
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x0008CEE4 File Offset: 0x0008B0E4
	private void CollectParameterUpdaters()
	{
		foreach (Type type in App.GetCurrentDomainTypes())
		{
			if (!type.IsAbstract)
			{
				bool flag = false;
				Type type2 = type.BaseType;
				while (type2 != null)
				{
					if (type2 == typeof(LoopingSoundParameterUpdater))
					{
						flag = true;
						break;
					}
					type2 = type2.BaseType;
				}
				if (flag)
				{
					LoopingSoundParameterUpdater loopingSoundParameterUpdater = (LoopingSoundParameterUpdater)Activator.CreateInstance(type);
					DebugUtil.Assert(!this.parameterUpdaters.ContainsKey(loopingSoundParameterUpdater.parameter));
					this.parameterUpdaters[loopingSoundParameterUpdater.parameter] = loopingSoundParameterUpdater;
				}
			}
		}
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x0008CFAC File Offset: 0x0008B1AC
	public void UpdateFirstParameter(HandleVector<int>.Handle handle, HashedString parameter, float value)
	{
		LoopingSoundManager.Sound data = this.sounds.GetData(handle);
		data.firstParameterValue = value;
		data.firstParameter = parameter;
		if (data.IsPlaying)
		{
			data.ev.setParameterByID(this.GetSoundDescription(data.path).GetParameterId(parameter), value, false);
		}
		this.sounds.SetData(handle, data);
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x0008D010 File Offset: 0x0008B210
	public void UpdateSecondParameter(HandleVector<int>.Handle handle, HashedString parameter, float value)
	{
		LoopingSoundManager.Sound data = this.sounds.GetData(handle);
		data.secondParameterValue = value;
		data.secondParameter = parameter;
		if (data.IsPlaying)
		{
			data.ev.setParameterByID(this.GetSoundDescription(data.path).GetParameterId(parameter), value, false);
		}
		this.sounds.SetData(handle, data);
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x0008D074 File Offset: 0x0008B274
	public void UpdateObjectSelection(HandleVector<int>.Handle handle, Vector3 sound_pos, float vol, bool objectIsSelectedAndVisible)
	{
		LoopingSoundManager.Sound data = this.sounds.GetData(handle);
		data.pos = sound_pos;
		data.vol = vol;
		data.objectIsSelectedAndVisible = objectIsSelectedAndVisible;
		ATTRIBUTES_3D attributes_3D = sound_pos.To3DAttributes();
		if (data.IsPlaying)
		{
			data.ev.set3DAttributes(attributes_3D);
			data.ev.setVolume(vol);
		}
		this.sounds.SetData(handle, data);
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x0008D0E0 File Offset: 0x0008B2E0
	public void UpdateVelocity(HandleVector<int>.Handle handle, Vector2 velocity)
	{
		LoopingSoundManager.Sound data = this.sounds.GetData(handle);
		data.velocity = velocity;
		this.sounds.SetData(handle, data);
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x0008D110 File Offset: 0x0008B310
	public void RenderEveryTick(float dt)
	{
		ListPool<LoopingSoundManager.Sound, LoopingSoundManager>.PooledList pooledList = ListPool<LoopingSoundManager.Sound, LoopingSoundManager>.Allocate();
		ListPool<int, LoopingSoundManager>.PooledList pooledList2 = ListPool<int, LoopingSoundManager>.Allocate();
		ListPool<int, LoopingSoundManager>.PooledList pooledList3 = ListPool<int, LoopingSoundManager>.Allocate();
		List<LoopingSoundManager.Sound> dataList = this.sounds.GetDataList();
		bool flag = Time.timeScale == 0f;
		SoundCuller soundCuller = CameraController.Instance.soundCuller;
		for (int i = 0; i < dataList.Count; i++)
		{
			LoopingSoundManager.Sound sound = dataList[i];
			if (sound.objectIsSelectedAndVisible)
			{
				sound.pos = SoundEvent.AudioHighlightListenerPosition(sound.transform.GetPosition());
				sound.vol = 1f;
			}
			else if (sound.transform != null)
			{
				sound.pos = sound.transform.GetPosition();
				sound.pos.z = 0f;
			}
			if (sound.animController != null)
			{
				Vector3 offset = sound.animController.Offset;
				sound.pos.x = sound.pos.x + offset.x;
				sound.pos.y = sound.pos.y + offset.y;
			}
			bool flag2 = !sound.IsCullingEnabled || (sound.ShouldCameraScalePosition && soundCuller.IsAudible(sound.pos, sound.falloffDistanceSq)) || soundCuller.IsAudibleNoCameraScaling(sound.pos, sound.falloffDistanceSq);
			bool isPlaying = sound.IsPlaying;
			if (flag2)
			{
				pooledList.Add(sound);
				if (!isPlaying)
				{
					SoundDescription soundDescription = this.GetSoundDescription(sound.path);
					sound.ev = KFMOD.CreateInstance(soundDescription.path);
					dataList[i] = sound;
					pooledList2.Add(i);
				}
			}
			else if (isPlaying)
			{
				pooledList3.Add(i);
			}
		}
		foreach (int num in pooledList2)
		{
			LoopingSoundManager.Sound sound2 = dataList[num];
			SoundDescription soundDescription2 = this.GetSoundDescription(sound2.path);
			sound2.ev.setPaused(flag && sound2.ShouldPauseOnGamePaused);
			sound2.pos.z = 0f;
			Vector3 pos = sound2.pos;
			if (sound2.objectIsSelectedAndVisible)
			{
				sound2.pos = SoundEvent.AudioHighlightListenerPosition(sound2.transform.GetPosition());
				sound2.vol = 1f;
			}
			else if (sound2.transform != null)
			{
				sound2.pos = sound2.transform.GetPosition();
			}
			sound2.ev.set3DAttributes(pos.To3DAttributes());
			sound2.ev.setVolume(sound2.vol);
			sound2.ev.start();
			sound2.flags |= LoopingSoundManager.Sound.Flags.PLAYING;
			if (sound2.firstParameter != HashedString.Invalid)
			{
				sound2.ev.setParameterByID(soundDescription2.GetParameterId(sound2.firstParameter), sound2.firstParameterValue, false);
			}
			if (sound2.secondParameter != HashedString.Invalid)
			{
				sound2.ev.setParameterByID(soundDescription2.GetParameterId(sound2.secondParameter), sound2.secondParameterValue, false);
			}
			LoopingSoundParameterUpdater.Sound sound3 = new LoopingSoundParameterUpdater.Sound
			{
				ev = sound2.ev,
				path = sound2.path,
				description = soundDescription2,
				transform = sound2.transform,
				objectIsSelectedAndVisible = false
			};
			foreach (SoundDescription.Parameter parameter in soundDescription2.parameters)
			{
				LoopingSoundParameterUpdater loopingSoundParameterUpdater = null;
				if (this.parameterUpdaters.TryGetValue(parameter.name, out loopingSoundParameterUpdater))
				{
					loopingSoundParameterUpdater.Add(sound3);
				}
			}
			dataList[num] = sound2;
		}
		pooledList2.Recycle();
		foreach (int num2 in pooledList3)
		{
			LoopingSoundManager.Sound sound4 = dataList[num2];
			SoundDescription soundDescription3 = this.GetSoundDescription(sound4.path);
			LoopingSoundParameterUpdater.Sound sound5 = new LoopingSoundParameterUpdater.Sound
			{
				ev = sound4.ev,
				path = sound4.path,
				description = soundDescription3,
				transform = sound4.transform,
				objectIsSelectedAndVisible = false
			};
			foreach (SoundDescription.Parameter parameter2 in soundDescription3.parameters)
			{
				LoopingSoundParameterUpdater loopingSoundParameterUpdater2 = null;
				if (this.parameterUpdaters.TryGetValue(parameter2.name, out loopingSoundParameterUpdater2))
				{
					loopingSoundParameterUpdater2.Remove(sound5);
				}
			}
			if (sound4.ShouldCameraScalePosition)
			{
				sound4.ev.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			}
			else
			{
				sound4.ev.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			}
			sound4.flags &= ~LoopingSoundManager.Sound.Flags.PLAYING;
			sound4.ev.release();
			dataList[num2] = sound4;
		}
		pooledList3.Recycle();
		float velocityScale = TuningData<LoopingSoundManager.Tuning>.Get().velocityScale;
		foreach (LoopingSoundManager.Sound sound6 in pooledList)
		{
			ATTRIBUTES_3D attributes_3D = SoundEvent.GetCameraScaledPosition(sound6.pos, sound6.objectIsSelectedAndVisible).To3DAttributes();
			attributes_3D.velocity = (sound6.velocity * velocityScale).ToFMODVector();
			EventInstance ev = sound6.ev;
			ev.set3DAttributes(attributes_3D);
		}
		foreach (KeyValuePair<HashedString, LoopingSoundParameterUpdater> keyValuePair in this.parameterUpdaters)
		{
			keyValuePair.Value.Update(dt);
		}
		pooledList.Recycle();
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x0008D74C File Offset: 0x0008B94C
	public static LoopingSoundManager Get()
	{
		return LoopingSoundManager.instance;
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x0008D754 File Offset: 0x0008B954
	public void StopAllSounds()
	{
		foreach (LoopingSoundManager.Sound sound in this.sounds.GetDataList())
		{
			if (sound.IsPlaying)
			{
				EventInstance eventInstance = sound.ev;
				eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
				eventInstance = sound.ev;
				eventInstance.release();
			}
		}
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x0008D7D0 File Offset: 0x0008B9D0
	private SoundDescription GetSoundDescription(HashedString path)
	{
		return KFMOD.GetSoundEventDescription(path);
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x0008D7D8 File Offset: 0x0008B9D8
	public HandleVector<int>.Handle Add(string path, Vector3 pos, Transform transform = null, bool pause_on_game_pause = true, bool enable_culling = true, bool enable_camera_scaled_position = true, float vol = 1f, bool objectIsSelectedAndVisible = false)
	{
		SoundDescription soundEventDescription = KFMOD.GetSoundEventDescription(path);
		LoopingSoundManager.Sound.Flags flags = (LoopingSoundManager.Sound.Flags)0;
		if (pause_on_game_pause)
		{
			flags |= LoopingSoundManager.Sound.Flags.PAUSE_ON_GAME_PAUSED;
		}
		if (enable_culling)
		{
			flags |= LoopingSoundManager.Sound.Flags.ENABLE_CULLING;
		}
		if (enable_camera_scaled_position)
		{
			flags |= LoopingSoundManager.Sound.Flags.ENABLE_CAMERA_SCALED_POSITION;
		}
		KBatchedAnimController kbatchedAnimController = null;
		if (transform != null)
		{
			kbatchedAnimController = transform.GetComponent<KBatchedAnimController>();
		}
		LoopingSoundManager.Sound sound = new LoopingSoundManager.Sound
		{
			transform = transform,
			animController = kbatchedAnimController,
			falloffDistanceSq = soundEventDescription.falloffDistanceSq,
			path = path,
			pos = pos,
			flags = flags,
			firstParameter = HashedString.Invalid,
			secondParameter = HashedString.Invalid,
			vol = vol,
			objectIsSelectedAndVisible = objectIsSelectedAndVisible
		};
		return this.sounds.Allocate(sound);
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x0008D898 File Offset: 0x0008BA98
	public static HandleVector<int>.Handle StartSound(EventReference event_ref, Vector3 pos, bool pause_on_game_pause = true, bool enable_culling = true)
	{
		return LoopingSoundManager.StartSound(KFMOD.GetEventReferencePath(event_ref), pos, pause_on_game_pause, enable_culling);
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x0008D8A8 File Offset: 0x0008BAA8
	public static HandleVector<int>.Handle StartSound(string path, Vector3 pos, bool pause_on_game_pause = true, bool enable_culling = true)
	{
		if (string.IsNullOrEmpty(path))
		{
			global::Debug.LogWarning("Missing sound");
			return HandleVector<int>.InvalidHandle;
		}
		return LoopingSoundManager.Get().Add(path, pos, null, pause_on_game_pause, enable_culling, true, 1f, false);
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x0008D8E4 File Offset: 0x0008BAE4
	public static void StopSound(HandleVector<int>.Handle handle)
	{
		if (LoopingSoundManager.Get() == null)
		{
			return;
		}
		LoopingSoundManager.Sound data = LoopingSoundManager.Get().sounds.GetData(handle);
		if (data.IsPlaying)
		{
			data.ev.stop(LoopingSoundManager.Get().GameIsPaused ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			data.ev.release();
			SoundDescription soundEventDescription = KFMOD.GetSoundEventDescription(data.path);
			foreach (SoundDescription.Parameter parameter in soundEventDescription.parameters)
			{
				LoopingSoundParameterUpdater loopingSoundParameterUpdater = null;
				if (LoopingSoundManager.Get().parameterUpdaters.TryGetValue(parameter.name, out loopingSoundParameterUpdater))
				{
					LoopingSoundParameterUpdater.Sound sound = new LoopingSoundParameterUpdater.Sound
					{
						ev = data.ev,
						path = data.path,
						description = soundEventDescription,
						transform = data.transform,
						objectIsSelectedAndVisible = false
					};
					loopingSoundParameterUpdater.Remove(sound);
				}
			}
		}
		LoopingSoundManager.Get().sounds.Free(handle);
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x0008D9EC File Offset: 0x0008BBEC
	public static void PauseSound(HandleVector<int>.Handle handle, bool paused)
	{
		LoopingSoundManager.Sound data = LoopingSoundManager.Get().sounds.GetData(handle);
		if (data.IsPlaying)
		{
			data.ev.setPaused(paused);
		}
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x0008DA24 File Offset: 0x0008BC24
	private void OnPauseChanged(object data)
	{
		bool flag = (bool)data;
		this.GameIsPaused = flag;
		foreach (LoopingSoundManager.Sound sound in this.sounds.GetDataList())
		{
			if (sound.IsPlaying)
			{
				EventInstance ev = sound.ev;
				ev.setPaused(flag && sound.ShouldPauseOnGamePaused);
			}
		}
	}

	// Token: 0x04000EAE RID: 3758
	private static LoopingSoundManager instance;

	// Token: 0x04000EAF RID: 3759
	private bool GameIsPaused;

	// Token: 0x04000EB0 RID: 3760
	private Dictionary<HashedString, LoopingSoundParameterUpdater> parameterUpdaters = new Dictionary<HashedString, LoopingSoundParameterUpdater>();

	// Token: 0x04000EB1 RID: 3761
	private KCompactedVector<LoopingSoundManager.Sound> sounds = new KCompactedVector<LoopingSoundManager.Sound>(0);

	// Token: 0x020010DB RID: 4315
	public class Tuning : TuningData<LoopingSoundManager.Tuning>
	{
		// Token: 0x040058E8 RID: 22760
		public float velocityScale;
	}

	// Token: 0x020010DC RID: 4316
	public struct Sound
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060074B6 RID: 29878 RVA: 0x002B3FF4 File Offset: 0x002B21F4
		public bool IsPlaying
		{
			get
			{
				return (this.flags & LoopingSoundManager.Sound.Flags.PLAYING) > (LoopingSoundManager.Sound.Flags)0;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x002B4001 File Offset: 0x002B2201
		public bool ShouldPauseOnGamePaused
		{
			get
			{
				return (this.flags & LoopingSoundManager.Sound.Flags.PAUSE_ON_GAME_PAUSED) > (LoopingSoundManager.Sound.Flags)0;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060074B8 RID: 29880 RVA: 0x002B400E File Offset: 0x002B220E
		public bool IsCullingEnabled
		{
			get
			{
				return (this.flags & LoopingSoundManager.Sound.Flags.ENABLE_CULLING) > (LoopingSoundManager.Sound.Flags)0;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060074B9 RID: 29881 RVA: 0x002B401B File Offset: 0x002B221B
		public bool ShouldCameraScalePosition
		{
			get
			{
				return (this.flags & LoopingSoundManager.Sound.Flags.ENABLE_CAMERA_SCALED_POSITION) > (LoopingSoundManager.Sound.Flags)0;
			}
		}

		// Token: 0x040058E9 RID: 22761
		public EventInstance ev;

		// Token: 0x040058EA RID: 22762
		public Transform transform;

		// Token: 0x040058EB RID: 22763
		public KBatchedAnimController animController;

		// Token: 0x040058EC RID: 22764
		public float falloffDistanceSq;

		// Token: 0x040058ED RID: 22765
		public HashedString path;

		// Token: 0x040058EE RID: 22766
		public Vector3 pos;

		// Token: 0x040058EF RID: 22767
		public Vector2 velocity;

		// Token: 0x040058F0 RID: 22768
		public HashedString firstParameter;

		// Token: 0x040058F1 RID: 22769
		public HashedString secondParameter;

		// Token: 0x040058F2 RID: 22770
		public float firstParameterValue;

		// Token: 0x040058F3 RID: 22771
		public float secondParameterValue;

		// Token: 0x040058F4 RID: 22772
		public float vol;

		// Token: 0x040058F5 RID: 22773
		public bool objectIsSelectedAndVisible;

		// Token: 0x040058F6 RID: 22774
		public LoopingSoundManager.Sound.Flags flags;

		// Token: 0x02001F71 RID: 8049
		[Flags]
		public enum Flags
		{
			// Token: 0x04008BC2 RID: 35778
			PLAYING = 1,
			// Token: 0x04008BC3 RID: 35779
			PAUSE_ON_GAME_PAUSED = 2,
			// Token: 0x04008BC4 RID: 35780
			ENABLE_CULLING = 4,
			// Token: 0x04008BC5 RID: 35781
			ENABLE_CAMERA_SCALED_POSITION = 8
		}
	}
}
