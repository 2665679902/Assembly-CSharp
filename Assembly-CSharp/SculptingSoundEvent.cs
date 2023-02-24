using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000426 RID: 1062
public class SculptingSoundEvent : SoundEvent
{
	// Token: 0x060016BF RID: 5823 RVA: 0x00076280 File Offset: 0x00074480
	private static string BaseSoundName(string sound_name)
	{
		int num = sound_name.IndexOf(":");
		if (num > 0)
		{
			return sound_name.Substring(0, num);
		}
		return sound_name;
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000762A8 File Offset: 0x000744A8
	public SculptingSoundEvent(string file_name, string sound_name, int frame, bool do_load, bool is_looping, float min_interval, bool is_dynamic)
		: base(file_name, SculptingSoundEvent.BaseSoundName(sound_name), frame, do_load, is_looping, min_interval, is_dynamic)
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

	// Token: 0x060016C1 RID: 5825 RVA: 0x000763CC File Offset: 0x000745CC
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
			float num3 = 1f;
			if (base.objectIsSelectedAndVisible)
			{
				vector = SoundEvent.AudioHighlightListenerPosition(vector);
				num3 = SoundEvent.GetVolume(base.objectIsSelectedAndVisible);
			}
			else
			{
				vector.z = 0f;
			}
			string text = GlobalAssets.GetSound("Hammer_sculpture", false);
			Worker component = behaviour.GetComponent<Worker>();
			if (component != null)
			{
				Workable workable = component.workable;
				if (workable != null)
				{
					Building component2 = workable.GetComponent<Building>();
					if (component2 != null)
					{
						string name = component2.Def.name;
						if (name != null)
						{
							if (!(name == "MetalSculpture"))
							{
								if (name == "MarbleSculpture")
								{
									text = GlobalAssets.GetSound("Hammer_sculpture_marble", false);
								}
							}
							else
							{
								text = GlobalAssets.GetSound("Hammer_sculpture_metal", false);
							}
						}
					}
				}
			}
			EventInstance eventInstance = SoundEvent.BeginOneShot(text, vector, num3, false);
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

	// Token: 0x060016C2 RID: 5826 RVA: 0x000765BF File Offset: 0x000747BF
	private void ParseParameter(string param)
	{
		this.counterModulus = int.Parse(param);
		if (this.counterModulus != -1 && this.counterModulus < 2)
		{
			throw new ArgumentException("CountedSoundEvent modulus must be 2 or larger");
		}
	}

	// Token: 0x04000CAA RID: 3242
	private const int COUNTER_MODULUS_INVALID = -2147483648;

	// Token: 0x04000CAB RID: 3243
	private const int COUNTER_MODULUS_CLEAR = -1;

	// Token: 0x04000CAC RID: 3244
	private int counterModulus = int.MinValue;
}
