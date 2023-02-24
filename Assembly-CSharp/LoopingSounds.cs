using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

// Token: 0x0200049A RID: 1178
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/LoopingSounds")]
public class LoopingSounds : KMonoBehaviour
{
	// Token: 0x06001A83 RID: 6787 RVA: 0x0008DAD0 File Offset: 0x0008BCD0
	public bool IsSoundPlaying(string path)
	{
		using (List<LoopingSounds.LoopingSoundEvent>.Enumerator enumerator = this.loopingSounds.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.asset == path)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x0008DB30 File Offset: 0x0008BD30
	public bool StartSound(string asset, AnimEventManager.EventPlayerData behaviour, EffectorValues noiseValues, bool ignore_pause = false, bool enable_camera_scaled_position = true)
	{
		if (asset == null || asset == "")
		{
			global::Debug.LogWarning("Missing sound");
			return false;
		}
		if (!this.IsSoundPlaying(asset))
		{
			LoopingSounds.LoopingSoundEvent loopingSoundEvent = new LoopingSounds.LoopingSoundEvent
			{
				asset = asset
			};
			GameObject gameObject = base.gameObject;
			this.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
			if (this.objectIsSelectedAndVisible)
			{
				this.sound_pos = SoundEvent.AudioHighlightListenerPosition(base.transform.GetPosition());
				this.vol = SoundEvent.GetVolume(this.objectIsSelectedAndVisible);
			}
			else
			{
				this.sound_pos = behaviour.GetComponent<Transform>().GetPosition();
				this.sound_pos.z = 0f;
			}
			loopingSoundEvent.handle = LoopingSoundManager.Get().Add(asset, this.sound_pos, base.transform, !ignore_pause, true, enable_camera_scaled_position, this.vol, this.objectIsSelectedAndVisible);
			this.loopingSounds.Add(loopingSoundEvent);
		}
		return true;
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x0008DC1C File Offset: 0x0008BE1C
	public bool StartSound(EventReference event_ref)
	{
		string eventReferencePath = KFMOD.GetEventReferencePath(event_ref);
		return this.StartSound(eventReferencePath);
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x0008DC38 File Offset: 0x0008BE38
	public bool StartSound(string asset)
	{
		if (asset.IsNullOrWhiteSpace())
		{
			global::Debug.LogWarning("Missing sound");
			return false;
		}
		if (!this.IsSoundPlaying(asset))
		{
			LoopingSounds.LoopingSoundEvent loopingSoundEvent = new LoopingSounds.LoopingSoundEvent
			{
				asset = asset
			};
			GameObject gameObject = base.gameObject;
			this.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
			if (this.objectIsSelectedAndVisible)
			{
				this.sound_pos = SoundEvent.AudioHighlightListenerPosition(base.transform.GetPosition());
				this.vol = SoundEvent.GetVolume(this.objectIsSelectedAndVisible);
			}
			else
			{
				this.sound_pos = base.transform.GetPosition();
				this.sound_pos.z = 0f;
			}
			loopingSoundEvent.handle = LoopingSoundManager.Get().Add(asset, this.sound_pos, base.transform, true, true, true, this.vol, this.objectIsSelectedAndVisible);
			this.loopingSounds.Add(loopingSoundEvent);
		}
		return true;
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x0008DD18 File Offset: 0x0008BF18
	public bool StartSound(string asset, bool pause_on_game_pause = true, bool enable_culling = true, bool enable_camera_scaled_position = true)
	{
		if (asset.IsNullOrWhiteSpace())
		{
			global::Debug.LogWarning("Missing sound");
			return false;
		}
		if (!this.IsSoundPlaying(asset))
		{
			LoopingSounds.LoopingSoundEvent loopingSoundEvent = new LoopingSounds.LoopingSoundEvent
			{
				asset = asset
			};
			GameObject gameObject = base.gameObject;
			this.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
			if (this.objectIsSelectedAndVisible)
			{
				this.sound_pos = SoundEvent.AudioHighlightListenerPosition(base.transform.GetPosition());
				this.vol = SoundEvent.GetVolume(this.objectIsSelectedAndVisible);
			}
			else
			{
				this.sound_pos = base.transform.GetPosition();
				this.sound_pos.z = 0f;
			}
			loopingSoundEvent.handle = LoopingSoundManager.Get().Add(asset, this.sound_pos, base.transform, pause_on_game_pause, enable_culling, enable_camera_scaled_position, this.vol, this.objectIsSelectedAndVisible);
			this.loopingSounds.Add(loopingSoundEvent);
		}
		return true;
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x0008DDF8 File Offset: 0x0008BFF8
	public void UpdateVelocity(string asset, Vector2 value)
	{
		foreach (LoopingSounds.LoopingSoundEvent loopingSoundEvent in this.loopingSounds)
		{
			if (loopingSoundEvent.asset == asset)
			{
				LoopingSoundManager.Get().UpdateVelocity(loopingSoundEvent.handle, value);
				break;
			}
		}
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x0008DE68 File Offset: 0x0008C068
	public void UpdateFirstParameter(string asset, HashedString parameter, float value)
	{
		foreach (LoopingSounds.LoopingSoundEvent loopingSoundEvent in this.loopingSounds)
		{
			if (loopingSoundEvent.asset == asset)
			{
				LoopingSoundManager.Get().UpdateFirstParameter(loopingSoundEvent.handle, parameter, value);
				break;
			}
		}
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x0008DED8 File Offset: 0x0008C0D8
	public void UpdateSecondParameter(string asset, HashedString parameter, float value)
	{
		foreach (LoopingSounds.LoopingSoundEvent loopingSoundEvent in this.loopingSounds)
		{
			if (loopingSoundEvent.asset == asset)
			{
				LoopingSoundManager.Get().UpdateSecondParameter(loopingSoundEvent.handle, parameter, value);
				break;
			}
		}
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x0008DF48 File Offset: 0x0008C148
	private void StopSoundAtIndex(int i)
	{
		LoopingSoundManager.StopSound(this.loopingSounds[i].handle);
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x0008DF60 File Offset: 0x0008C160
	public void StopSound(EventReference event_ref)
	{
		string eventReferencePath = KFMOD.GetEventReferencePath(event_ref);
		this.StopSound(eventReferencePath);
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x0008DF7C File Offset: 0x0008C17C
	public void StopSound(string asset)
	{
		for (int i = 0; i < this.loopingSounds.Count; i++)
		{
			if (this.loopingSounds[i].asset == asset)
			{
				this.StopSoundAtIndex(i);
				this.loopingSounds.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x0008DFCC File Offset: 0x0008C1CC
	public void PauseSound(string asset, bool paused)
	{
		for (int i = 0; i < this.loopingSounds.Count; i++)
		{
			if (this.loopingSounds[i].asset == asset)
			{
				LoopingSoundManager.PauseSound(this.loopingSounds[i].handle, paused);
				return;
			}
		}
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x0008E020 File Offset: 0x0008C220
	public void StopAllSounds()
	{
		for (int i = 0; i < this.loopingSounds.Count; i++)
		{
			this.StopSoundAtIndex(i);
		}
		this.loopingSounds.Clear();
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x0008E055 File Offset: 0x0008C255
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.StopAllSounds();
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x0008E064 File Offset: 0x0008C264
	public void SetParameter(EventReference event_ref, HashedString parameter, float value)
	{
		string eventReferencePath = KFMOD.GetEventReferencePath(event_ref);
		this.SetParameter(eventReferencePath, parameter, value);
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x0008E084 File Offset: 0x0008C284
	public void SetParameter(string path, HashedString parameter, float value)
	{
		foreach (LoopingSounds.LoopingSoundEvent loopingSoundEvent in this.loopingSounds)
		{
			if (loopingSoundEvent.asset == path)
			{
				LoopingSoundManager.Get().UpdateFirstParameter(loopingSoundEvent.handle, parameter, value);
				break;
			}
		}
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x0008E0F4 File Offset: 0x0008C2F4
	public void PlayEvent(GameSoundEvents.Event ev)
	{
		if (AudioDebug.Get().debugGameEventSounds)
		{
			string text = "GameSoundEvent: ";
			HashedString name = ev.Name;
			global::Debug.Log(text + name.ToString());
		}
		List<AnimEvent> events = GameAudioSheets.Get().GetEvents(ev.Name);
		if (events == null)
		{
			return;
		}
		Vector2 vector = base.transform.GetPosition();
		for (int i = 0; i < events.Count; i++)
		{
			SoundEvent soundEvent = events[i] as SoundEvent;
			if (soundEvent == null || soundEvent.sound == null)
			{
				return;
			}
			if (CameraController.Instance.IsAudibleSound(vector, soundEvent.sound))
			{
				if (AudioDebug.Get().debugGameEventSounds)
				{
					global::Debug.Log("GameSound: " + soundEvent.sound);
				}
				float num = 0f;
				if (this.lastTimePlayed.TryGetValue(soundEvent.soundHash, out num))
				{
					if (Time.time - num > soundEvent.minInterval)
					{
						SoundEvent.PlayOneShot(soundEvent.sound, vector, 1f);
					}
				}
				else
				{
					SoundEvent.PlayOneShot(soundEvent.sound, vector, 1f);
				}
				this.lastTimePlayed[soundEvent.soundHash] = Time.time;
			}
		}
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x0008E244 File Offset: 0x0008C444
	public void UpdateObjectSelection(bool selected)
	{
		GameObject gameObject = base.gameObject;
		if (selected && gameObject != null && CameraController.Instance.IsVisiblePos(gameObject.transform.position))
		{
			this.objectIsSelectedAndVisible = true;
			this.sound_pos = SoundEvent.AudioHighlightListenerPosition(this.sound_pos);
			this.vol = 1f;
		}
		else
		{
			this.objectIsSelectedAndVisible = false;
			this.sound_pos = base.transform.GetPosition();
			this.sound_pos.z = 0f;
			this.vol = 1f;
		}
		for (int i = 0; i < this.loopingSounds.Count; i++)
		{
			LoopingSoundManager.Get().UpdateObjectSelection(this.loopingSounds[i].handle, this.sound_pos, this.vol, this.objectIsSelectedAndVisible);
		}
	}

	// Token: 0x04000EB2 RID: 3762
	private List<LoopingSounds.LoopingSoundEvent> loopingSounds = new List<LoopingSounds.LoopingSoundEvent>();

	// Token: 0x04000EB3 RID: 3763
	private Dictionary<HashedString, float> lastTimePlayed = new Dictionary<HashedString, float>();

	// Token: 0x04000EB4 RID: 3764
	[SerializeField]
	public bool updatePosition;

	// Token: 0x04000EB5 RID: 3765
	public float vol = 1f;

	// Token: 0x04000EB6 RID: 3766
	public bool objectIsSelectedAndVisible;

	// Token: 0x04000EB7 RID: 3767
	public Vector3 sound_pos;

	// Token: 0x020010DD RID: 4317
	private struct LoopingSoundEvent
	{
		// Token: 0x040058F7 RID: 22775
		public string asset;

		// Token: 0x040058F8 RID: 22776
		public HandleVector<int>.Handle handle;
	}
}
