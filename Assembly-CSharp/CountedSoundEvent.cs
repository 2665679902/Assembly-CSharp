﻿using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x0200040F RID: 1039
public class CountedSoundEvent : SoundEvent
{
	// Token: 0x0600159C RID: 5532 RVA: 0x0006FFF0 File Offset: 0x0006E1F0
	private static string BaseSoundName(string sound_name)
	{
		int num = sound_name.IndexOf(":");
		if (num > 0)
		{
			return sound_name.Substring(0, num);
		}
		return sound_name;
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x00070018 File Offset: 0x0006E218
	public CountedSoundEvent(string file_name, string sound_name, int frame, bool do_load, bool is_looping, float min_interval, bool is_dynamic)
		: base(file_name, CountedSoundEvent.BaseSoundName(sound_name), frame, do_load, is_looping, min_interval, is_dynamic)
	{
		if (sound_name.Contains(":"))
		{
			string[] array = sound_name.Split(new char[] { ':' });
			if (array.Length != 2)
			{
				DebugUtil.LogErrorArgs(new object[]
				{
					"Invalid CountedSoundEvent parameter for",
					string.Concat(new string[]
					{
						file_name,
						".",
						sound_name,
						".",
						frame.ToString(),
						":"
					}),
					"'" + sound_name + "'"
				});
			}
			for (int i = 1; i < array.Length; i++)
			{
				this.ParseParameter(array[i]);
			}
			return;
		}
		DebugUtil.LogErrorArgs(new object[]
		{
			"CountedSoundEvent for",
			string.Concat(new string[]
			{
				file_name,
				".",
				sound_name,
				".",
				frame.ToString()
			}),
			" - Must specify max number of steps on event: '" + sound_name + "'"
		});
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x0007013C File Offset: 0x0006E33C
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		if (string.IsNullOrEmpty(base.sound))
		{
			return;
		}
		GameObject gameObject = behaviour.controller.gameObject;
		base.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
		if (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, base.sound, base.soundHash, base.looping, this.isDynamic))
		{
			int num = -1;
			if (this.counterModulus >= -1)
			{
				HandleVector<int>.Handle handle = GameComps.WhiteBoards.GetHandle(gameObject);
				if (!handle.IsValid())
				{
					handle = GameComps.WhiteBoards.Add(gameObject);
				}
				num = (GameComps.WhiteBoards.HasValue(handle, base.soundHash) ? ((int)GameComps.WhiteBoards.GetValue(handle, base.soundHash)) : 0);
				int num2 = ((this.counterModulus == -1) ? 0 : ((num + 1) % this.counterModulus));
				GameComps.WhiteBoards.SetValue(handle, base.soundHash, num2);
			}
			Vector3 vector = behaviour.GetComponent<Transform>().GetPosition();
			vector.z = 0f;
			if (base.objectIsSelectedAndVisible)
			{
				vector = SoundEvent.AudioHighlightListenerPosition(vector);
			}
			EventInstance eventInstance = SoundEvent.BeginOneShot(base.sound, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible), false);
			if (eventInstance.isValid())
			{
				if (num >= 0)
				{
					eventInstance.setParameterByName("eventCount", (float)num, false);
				}
				SoundEvent.EndOneShot(eventInstance);
			}
		}
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x00070295 File Offset: 0x0006E495
	private void ParseParameter(string param)
	{
		this.counterModulus = int.Parse(param);
		if (this.counterModulus != -1 && this.counterModulus < 2)
		{
			throw new ArgumentException("CountedSoundEvent modulus must be 2 or larger");
		}
	}

	// Token: 0x04000C0C RID: 3084
	private const int COUNTER_MODULUS_INVALID = -2147483648;

	// Token: 0x04000C0D RID: 3085
	private const int COUNTER_MODULUS_CLEAR = -1;

	// Token: 0x04000C0E RID: 3086
	private int counterModulus = int.MinValue;
}
