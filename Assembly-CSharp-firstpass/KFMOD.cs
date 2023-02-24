using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class KFMOD
{
	// Token: 0x060001C4 RID: 452 RVA: 0x0000A274 File Offset: 0x00008474
	public static SoundDescription GetSoundEventDescription(HashedString path)
	{
		if (!KFMOD.soundDescriptions.ContainsKey(path))
		{
			return default(SoundDescription);
		}
		return KFMOD.soundDescriptions[path];
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000A2A4 File Offset: 0x000084A4
	public static string GetEventReferencePath(EventReference event_ref)
	{
		EventDescription eventDescription = RuntimeManager.GetEventDescription(event_ref.Guid);
		string text = "";
		if (eventDescription.isValid())
		{
			eventDescription.getPath(out text);
		}
		return text;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000A2D8 File Offset: 0x000084D8
	public static void Initialize()
	{
		try
		{
			Settings instance = Settings.Instance;
			if (!DlcManager.IsExpansion1Active())
			{
				instance.Banks.RemoveAll((string b) => b.StartsWith("expansion1_"));
			}
			if (UnityEngine.Object.FindObjectsOfType<RuntimeManager>().Length != 0)
			{
				global::Debug.LogError("FMOD got initialized before we tried to initialize it! This will cause bad things to happen!");
			}
			FMOD.Studio.System studioSystem = RuntimeManager.StudioSystem;
			KFMOD.didFmodInitializeSuccessfully = RuntimeManager.IsInitialized;
		}
		catch (Exception ex)
		{
			KFMOD.didFmodInitializeSuccessfully = false;
			if (!(ex.GetType() == typeof(SystemNotInitializedException)))
			{
				throw ex;
			}
			global::Debug.LogWarning(ex);
		}
		KFMOD.CollectParameterUpdaters();
		KFMOD.CollectSoundDescriptions();
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000A388 File Offset: 0x00008588
	public static void PlayOneShot(string sound, Vector3 position, float volume = 1f)
	{
		KFMOD.EndOneShot(KFMOD.BeginOneShot(sound, position, volume));
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000A398 File Offset: 0x00008598
	public static void PlayUISound(EventReference event_ref)
	{
		KFMOD.PlayUISound(KFMOD.GetEventReferencePath(event_ref));
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000A3A5 File Offset: 0x000085A5
	public static void PlayUISound(string sound)
	{
		KFMOD.PlayOneShot(sound, Vector3.zero, 1f);
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000A3B8 File Offset: 0x000085B8
	public static void PlayOneShotWithParameter(string sound, Vector3 position, string parameter, float parameterValue, float volume = 1f)
	{
		EventInstance eventInstance = KFMOD.BeginOneShot(sound, position, volume);
		eventInstance.setParameterByName(parameter, parameterValue, false);
		KFMOD.EndOneShot(eventInstance);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000A3E1 File Offset: 0x000085E1
	public static void PlayUISoundWithParameter(string sound, string parameter, float parameterValue)
	{
		KFMOD.PlayOneShotWithParameter(sound, Vector3.zero, parameter, parameterValue, 1f);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000A3F8 File Offset: 0x000085F8
	public static EventInstance BeginOneShot(EventReference event_ref, Vector3 position, float volume = 1f)
	{
		if (event_ref.IsNull || App.IsExiting || !RuntimeManager.IsInitialized)
		{
			return default(EventInstance);
		}
		EventInstance eventInstance = KFMOD.CreateInstance(event_ref);
		if (!eventInstance.isValid())
		{
			if (KFMODDebugger.instance != null)
			{
				string text;
				RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
			}
			return eventInstance;
		}
		Vector3 vector = new Vector3(position.x, position.y, position.z);
		if (KFMODDebugger.instance != null)
		{
			string text2;
			RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text2);
		}
		ATTRIBUTES_3D attributes_3D = vector.To3DAttributes();
		eventInstance.set3DAttributes(attributes_3D);
		eventInstance.setVolume(volume);
		return eventInstance;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000A4B0 File Offset: 0x000086B0
	public static EventInstance BeginOneShot(string sound, Vector3 position, float volume = 1f)
	{
		if (sound.IsNullOrWhiteSpace())
		{
			return default(EventInstance);
		}
		return KFMOD.BeginOneShot(RuntimeManager.PathToEventReference(sound), position, volume);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000A4DC File Offset: 0x000086DC
	public static bool EndOneShot(EventInstance instance)
	{
		if (!instance.isValid())
		{
			return false;
		}
		instance.start();
		instance.release();
		return true;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000A4FC File Offset: 0x000086FC
	public static EventInstance CreateInstance(EventReference event_ref)
	{
		if (!RuntimeManager.IsInitialized)
		{
			EventInstance eventInstance = default(EventInstance);
			return eventInstance;
		}
		EventInstance eventInstance2;
		try
		{
			eventInstance2 = RuntimeManager.CreateInstance(event_ref);
		}
		catch (EventNotFoundException ex)
		{
			global::Debug.LogWarning(ex);
			EventInstance eventInstance = default(EventInstance);
			return eventInstance;
		}
		EventDescription eventDescription;
		eventInstance2.getDescription(out eventDescription);
		string text;
		eventDescription.getPath(out text);
		HashedString hashedString = text;
		SoundDescription soundEventDescription = KFMOD.GetSoundEventDescription(hashedString);
		OneShotSoundParameterUpdater.Sound sound = new OneShotSoundParameterUpdater.Sound
		{
			ev = eventInstance2,
			path = hashedString,
			description = soundEventDescription
		};
		OneShotSoundParameterUpdater[] oneShotParameterUpdaters = soundEventDescription.oneShotParameterUpdaters;
		for (int i = 0; i < oneShotParameterUpdaters.Length; i++)
		{
			oneShotParameterUpdaters[i].Play(sound);
		}
		return eventInstance2;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000A5BC File Offset: 0x000087BC
	public static EventInstance CreateInstance(string path)
	{
		return KFMOD.CreateInstance(RuntimeManager.PathToEventReference(path));
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000A5CC File Offset: 0x000087CC
	private static void CollectSoundDescriptions()
	{
		Bank[] array = null;
		RuntimeManager.StudioSystem.getBankList(out array);
		foreach (Bank bank in array)
		{
			EventDescription[] array3;
			bank.getEventList(out array3);
			foreach (EventDescription eventDescription in array3)
			{
				string text;
				eventDescription.getPath(out text);
				HashedString hashedString = text;
				SoundDescription soundDescription = default(SoundDescription);
				soundDescription.path = text;
				float num = 0f;
				float num2 = 0f;
				eventDescription.getMinMaxDistance(out num, out num2);
				if (num2 == 0f)
				{
					num2 = 60f;
				}
				soundDescription.falloffDistanceSq = num2 * num2;
				List<OneShotSoundParameterUpdater> list = new List<OneShotSoundParameterUpdater>();
				int num3 = 0;
				eventDescription.getParameterDescriptionCount(out num3);
				SoundDescription.Parameter[] array4 = new SoundDescription.Parameter[num3];
				for (int k = 0; k < num3; k++)
				{
					PARAMETER_DESCRIPTION parameter_DESCRIPTION;
					eventDescription.getParameterDescriptionByIndex(k, out parameter_DESCRIPTION);
					string text2 = parameter_DESCRIPTION.name;
					array4[k] = new SoundDescription.Parameter
					{
						name = new HashedString(text2),
						id = parameter_DESCRIPTION.id
					};
					OneShotSoundParameterUpdater oneShotSoundParameterUpdater = null;
					if (KFMOD.parameterUpdaters.TryGetValue(text2, out oneShotSoundParameterUpdater))
					{
						list.Add(oneShotSoundParameterUpdater);
					}
				}
				soundDescription.parameters = array4;
				soundDescription.oneShotParameterUpdaters = list.ToArray();
				KFMOD.soundDescriptions[hashedString] = soundDescription;
			}
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000A750 File Offset: 0x00008950
	private static void CollectParameterUpdaters()
	{
		foreach (Type type in App.GetCurrentDomainTypes())
		{
			if (!type.IsAbstract)
			{
				bool flag = false;
				Type type2 = type.BaseType;
				while (type2 != null)
				{
					if (type2 == typeof(OneShotSoundParameterUpdater))
					{
						flag = true;
						break;
					}
					type2 = type2.BaseType;
				}
				if (flag)
				{
					OneShotSoundParameterUpdater oneShotSoundParameterUpdater = (OneShotSoundParameterUpdater)Activator.CreateInstance(type);
					DebugUtil.Assert(!KFMOD.parameterUpdaters.ContainsKey(oneShotSoundParameterUpdater.parameter));
					KFMOD.parameterUpdaters[oneShotSoundParameterUpdater.parameter] = oneShotSoundParameterUpdater;
				}
			}
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000A814 File Offset: 0x00008A14
	public static void RenderEveryTick(float dt)
	{
		foreach (KeyValuePair<HashedString, OneShotSoundParameterUpdater> keyValuePair in KFMOD.parameterUpdaters)
		{
			keyValuePair.Value.Update(dt);
		}
	}

	// Token: 0x040000B7 RID: 183
	private static Dictionary<HashedString, SoundDescription> soundDescriptions = new Dictionary<HashedString, SoundDescription>();

	// Token: 0x040000B8 RID: 184
	public static bool didFmodInitializeSuccessfully = true;

	// Token: 0x040000B9 RID: 185
	private static Dictionary<HashedString, OneShotSoundParameterUpdater> parameterUpdaters = new Dictionary<HashedString, OneShotSoundParameterUpdater>();

	// Token: 0x040000BA RID: 186
	public static KFMOD.AudioDevice currentDevice;

	// Token: 0x02000976 RID: 2422
	private struct SoundCountEntry
	{
		// Token: 0x040020CC RID: 8396
		public int count;

		// Token: 0x040020CD RID: 8397
		public float minObjects;

		// Token: 0x040020CE RID: 8398
		public float maxObjects;
	}

	// Token: 0x02000977 RID: 2423
	public struct AudioDevice
	{
		// Token: 0x040020CF RID: 8399
		public int fmod_id;

		// Token: 0x040020D0 RID: 8400
		public string name;

		// Token: 0x040020D1 RID: 8401
		public Guid guid;

		// Token: 0x040020D2 RID: 8402
		public int systemRate;

		// Token: 0x040020D3 RID: 8403
		public SPEAKERMODE speakerMode;

		// Token: 0x040020D4 RID: 8404
		public int speakerModeChannels;

		// Token: 0x040020D5 RID: 8405
		public bool selected;
	}
}
