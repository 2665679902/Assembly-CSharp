using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000429 RID: 1065
internal class UpdateObjectCountParameter : LoopingSoundParameterUpdater
{
	// Token: 0x060016EC RID: 5868 RVA: 0x00076D54 File Offset: 0x00074F54
	public static UpdateObjectCountParameter.Settings GetSettings(HashedString path_hash, SoundDescription description)
	{
		UpdateObjectCountParameter.Settings settings = default(UpdateObjectCountParameter.Settings);
		if (!UpdateObjectCountParameter.settings.TryGetValue(path_hash, out settings))
		{
			settings = default(UpdateObjectCountParameter.Settings);
			EventDescription eventDescription = RuntimeManager.GetEventDescription(description.path);
			USER_PROPERTY user_PROPERTY;
			if (eventDescription.getUserProperty("minObj", out user_PROPERTY) == RESULT.OK)
			{
				settings.minObjects = (float)((short)user_PROPERTY.floatValue());
			}
			else
			{
				settings.minObjects = 1f;
			}
			USER_PROPERTY user_PROPERTY2;
			if (eventDescription.getUserProperty("maxObj", out user_PROPERTY2) == RESULT.OK)
			{
				settings.maxObjects = user_PROPERTY2.floatValue();
			}
			else
			{
				settings.maxObjects = 0f;
			}
			USER_PROPERTY user_PROPERTY3;
			if (eventDescription.getUserProperty("curveType", out user_PROPERTY3) == RESULT.OK && user_PROPERTY3.stringValue() == "exp")
			{
				settings.useExponentialCurve = true;
			}
			settings.parameterId = description.GetParameterId(UpdateObjectCountParameter.parameterHash);
			settings.path = path_hash;
			UpdateObjectCountParameter.settings[path_hash] = settings;
		}
		return settings;
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x00076E3C File Offset: 0x0007503C
	public static void ApplySettings(EventInstance ev, int count, UpdateObjectCountParameter.Settings settings)
	{
		float num = 0f;
		if (settings.maxObjects != settings.minObjects)
		{
			num = ((float)count - settings.minObjects) / (settings.maxObjects - settings.minObjects);
			num = Mathf.Clamp01(num);
		}
		if (settings.useExponentialCurve)
		{
			num *= num;
		}
		ev.setParameterByID(settings.parameterId, num, false);
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x00076E98 File Offset: 0x00075098
	public UpdateObjectCountParameter()
		: base("objectCount")
	{
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x00076EB8 File Offset: 0x000750B8
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateObjectCountParameter.Settings settings = UpdateObjectCountParameter.GetSettings(sound.path, sound.description);
		UpdateObjectCountParameter.Entry entry = new UpdateObjectCountParameter.Entry
		{
			ev = sound.ev,
			settings = settings
		};
		this.entries.Add(entry);
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x00076F04 File Offset: 0x00075104
	public override void Update(float dt)
	{
		DictionaryPool<HashedString, int, LoopingSoundManager>.PooledDictionary pooledDictionary = DictionaryPool<HashedString, int, LoopingSoundManager>.Allocate();
		foreach (UpdateObjectCountParameter.Entry entry in this.entries)
		{
			int num = 0;
			pooledDictionary.TryGetValue(entry.settings.path, out num);
			num = (pooledDictionary[entry.settings.path] = num + 1);
		}
		foreach (UpdateObjectCountParameter.Entry entry2 in this.entries)
		{
			int num2 = pooledDictionary[entry2.settings.path];
			UpdateObjectCountParameter.ApplySettings(entry2.ev, num2, entry2.settings);
		}
		pooledDictionary.Recycle();
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x00076FF0 File Offset: 0x000751F0
	public override void Remove(LoopingSoundParameterUpdater.Sound sound)
	{
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (this.entries[i].ev.handle == sound.ev.handle)
			{
				this.entries.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x00077048 File Offset: 0x00075248
	public static void Clear()
	{
		UpdateObjectCountParameter.settings.Clear();
	}

	// Token: 0x04000CB8 RID: 3256
	private List<UpdateObjectCountParameter.Entry> entries = new List<UpdateObjectCountParameter.Entry>();

	// Token: 0x04000CB9 RID: 3257
	private static Dictionary<HashedString, UpdateObjectCountParameter.Settings> settings = new Dictionary<HashedString, UpdateObjectCountParameter.Settings>();

	// Token: 0x04000CBA RID: 3258
	private static readonly HashedString parameterHash = "objectCount";

	// Token: 0x02001048 RID: 4168
	private struct Entry
	{
		// Token: 0x040056E1 RID: 22241
		public EventInstance ev;

		// Token: 0x040056E2 RID: 22242
		public UpdateObjectCountParameter.Settings settings;
	}

	// Token: 0x02001049 RID: 4169
	public struct Settings
	{
		// Token: 0x040056E3 RID: 22243
		public HashedString path;

		// Token: 0x040056E4 RID: 22244
		public PARAMETER_ID parameterId;

		// Token: 0x040056E5 RID: 22245
		public float minObjects;

		// Token: 0x040056E6 RID: 22246
		public float maxObjects;

		// Token: 0x040056E7 RID: 22247
		public bool useExponentialCurve;
	}
}
