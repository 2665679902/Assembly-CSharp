using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x0200085B RID: 2139
[AddComponentMenu("KMonoBehaviour/scripts/MusicManager")]
public class MusicManager : KMonoBehaviour, ISerializationCallbackReceiver
{
	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x06003D67 RID: 15719 RVA: 0x00157401 File Offset: 0x00155601
	public Dictionary<string, MusicManager.SongInfo> SongMap
	{
		get
		{
			return this.songMap;
		}
	}

	// Token: 0x06003D68 RID: 15720 RVA: 0x0015740C File Offset: 0x0015560C
	public void PlaySong(string song_name, bool canWait = false)
	{
		this.Log("Play: " + song_name);
		if (!AudioDebug.Get().musicEnabled)
		{
			return;
		}
		MusicManager.SongInfo songInfo = null;
		if (!this.songMap.TryGetValue(song_name, out songInfo))
		{
			DebugUtil.LogErrorArgs(new object[] { "Unknown song:", song_name });
			return;
		}
		if (this.activeSongs.ContainsKey(song_name))
		{
			DebugUtil.LogWarningArgs(new object[] { "Trying to play duplicate song:", song_name });
			return;
		}
		if (this.activeSongs.Count == 0)
		{
			songInfo.ev = KFMOD.CreateInstance(songInfo.fmodEvent);
			if (!songInfo.ev.isValid())
			{
				object[] array = new object[1];
				int num = 0;
				string text = "Failed to find FMOD event [";
				EventReference eventReference = songInfo.fmodEvent;
				array[num] = text + eventReference.ToString() + "]";
				DebugUtil.LogWarningArgs(array);
			}
			int num2 = ((songInfo.numberOfVariations > 0) ? UnityEngine.Random.Range(1, songInfo.numberOfVariations + 1) : (-1));
			if (num2 != -1)
			{
				songInfo.ev.setParameterByName("variation", (float)num2, false);
			}
			if (songInfo.dynamic)
			{
				songInfo.ev.setProperty(EVENT_PROPERTY.SCHEDULE_DELAY, 16000f);
				songInfo.ev.setProperty(EVENT_PROPERTY.SCHEDULE_LOOKAHEAD, 48000f);
				this.activeDynamicSong = songInfo;
			}
			songInfo.ev.start();
			this.activeSongs[song_name] = songInfo;
			return;
		}
		List<string> list = new List<string>(this.activeSongs.Keys);
		if (songInfo.interruptsActiveMusic)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (!this.activeSongs[list[i]].interruptsActiveMusic)
				{
					MusicManager.SongInfo songInfo2 = this.activeSongs[list[i]];
					songInfo2.ev.setParameterByName("interrupted_dimmed", 1f, false);
					this.Log("Dimming: " + Assets.GetSimpleSoundEventName(songInfo2.fmodEvent));
					songInfo.songsOnHold.Add(list[i]);
				}
			}
			songInfo.ev = KFMOD.CreateInstance(songInfo.fmodEvent);
			if (!songInfo.ev.isValid())
			{
				object[] array2 = new object[1];
				int num3 = 0;
				string text2 = "Failed to find FMOD event [";
				EventReference eventReference = songInfo.fmodEvent;
				array2[num3] = text2 + eventReference.ToString() + "]";
				DebugUtil.LogWarningArgs(array2);
			}
			songInfo.ev.start();
			songInfo.ev.release();
			this.activeSongs[song_name] = songInfo;
			return;
		}
		int num4 = 0;
		foreach (string text3 in this.activeSongs.Keys)
		{
			MusicManager.SongInfo songInfo3 = this.activeSongs[text3];
			if (!songInfo3.interruptsActiveMusic && songInfo3.priority > num4)
			{
				num4 = songInfo3.priority;
			}
		}
		if (songInfo.priority >= num4)
		{
			for (int j = 0; j < list.Count; j++)
			{
				MusicManager.SongInfo songInfo4 = this.activeSongs[list[j]];
				FMOD.Studio.EventInstance ev = songInfo4.ev;
				if (!songInfo4.interruptsActiveMusic)
				{
					ev.setParameterByName("interrupted_dimmed", 1f, false);
					ev.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
					this.activeSongs.Remove(list[j]);
					list.Remove(list[j]);
				}
			}
			songInfo.ev = KFMOD.CreateInstance(songInfo.fmodEvent);
			if (!songInfo.ev.isValid())
			{
				object[] array3 = new object[1];
				int num5 = 0;
				string text4 = "Failed to find FMOD event [";
				EventReference eventReference = songInfo.fmodEvent;
				array3[num5] = text4 + eventReference.ToString() + "]";
				DebugUtil.LogWarningArgs(array3);
			}
			int num6 = ((songInfo.numberOfVariations > 0) ? UnityEngine.Random.Range(1, songInfo.numberOfVariations + 1) : (-1));
			if (num6 != -1)
			{
				songInfo.ev.setParameterByName("variation", (float)num6, false);
			}
			songInfo.ev.start();
			this.activeSongs[song_name] = songInfo;
		}
	}

	// Token: 0x06003D69 RID: 15721 RVA: 0x0015781C File Offset: 0x00155A1C
	public void StopSong(string song_name, bool shouldLog = true, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
	{
		if (shouldLog)
		{
			this.Log("Stop: " + song_name);
		}
		MusicManager.SongInfo songInfo = null;
		if (!this.songMap.TryGetValue(song_name, out songInfo))
		{
			DebugUtil.LogErrorArgs(new object[] { "Unknown song:", song_name });
			return;
		}
		if (!this.activeSongs.ContainsKey(song_name))
		{
			DebugUtil.LogWarningArgs(new object[] { "Trying to stop a song that isn't playing:", song_name });
			return;
		}
		FMOD.Studio.EventInstance ev = songInfo.ev;
		ev.stop(stopMode);
		ev.release();
		if (songInfo.dynamic)
		{
			this.activeDynamicSong = null;
		}
		if (songInfo.songsOnHold.Count > 0)
		{
			for (int i = 0; i < songInfo.songsOnHold.Count; i++)
			{
				MusicManager.SongInfo songInfo2;
				if (this.activeSongs.TryGetValue(songInfo.songsOnHold[i], out songInfo2) && songInfo2.ev.isValid())
				{
					FMOD.Studio.EventInstance ev2 = songInfo2.ev;
					this.Log("Undimming: " + Assets.GetSimpleSoundEventName(songInfo2.fmodEvent));
					ev2.setParameterByName("interrupted_dimmed", 0f, false);
					songInfo.songsOnHold.Remove(songInfo.songsOnHold[i]);
				}
				else
				{
					songInfo.songsOnHold.Remove(songInfo.songsOnHold[i]);
				}
			}
		}
		this.activeSongs.Remove(song_name);
	}

	// Token: 0x06003D6A RID: 15722 RVA: 0x00157980 File Offset: 0x00155B80
	public void KillAllSongs(FMOD.Studio.STOP_MODE stop_mode = FMOD.Studio.STOP_MODE.IMMEDIATE)
	{
		this.Log("Kill All Songs");
		if (this.DynamicMusicIsActive())
		{
			this.StopDynamicMusic(true);
		}
		List<string> list = new List<string>(this.activeSongs.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			this.StopSong(list[i], true, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
	}

	// Token: 0x06003D6B RID: 15723 RVA: 0x001579D8 File Offset: 0x00155BD8
	public void SetSongParameter(string song_name, string parameter_name, float parameter_value, bool shouldLog = true)
	{
		if (shouldLog)
		{
			this.Log(string.Format("Set Param {0}: {1}, {2}", song_name, parameter_name, parameter_value));
		}
		MusicManager.SongInfo songInfo = null;
		if (!this.activeSongs.TryGetValue(song_name, out songInfo))
		{
			return;
		}
		FMOD.Studio.EventInstance ev = songInfo.ev;
		if (ev.isValid())
		{
			ev.setParameterByName(parameter_name, parameter_value, false);
		}
	}

	// Token: 0x06003D6C RID: 15724 RVA: 0x00157A30 File Offset: 0x00155C30
	public void SetSongParameter(string song_name, string parameter_name, string parameter_lable, bool shouldLog = true)
	{
		if (shouldLog)
		{
			this.Log(string.Format("Set Param {0}: {1}, {2}", song_name, parameter_name, parameter_lable));
		}
		MusicManager.SongInfo songInfo = null;
		if (!this.activeSongs.TryGetValue(song_name, out songInfo))
		{
			return;
		}
		FMOD.Studio.EventInstance ev = songInfo.ev;
		if (ev.isValid())
		{
			ev.setParameterByNameWithLabel(parameter_name, parameter_lable, false);
		}
	}

	// Token: 0x06003D6D RID: 15725 RVA: 0x00157A84 File Offset: 0x00155C84
	public bool SongIsPlaying(string song_name)
	{
		MusicManager.SongInfo songInfo = null;
		return this.activeSongs.TryGetValue(song_name, out songInfo) && songInfo.musicPlaybackState != PLAYBACK_STATE.STOPPED;
	}

	// Token: 0x06003D6E RID: 15726 RVA: 0x00157AB0 File Offset: 0x00155CB0
	private void Update()
	{
		this.ClearFinishedSongs();
		if (this.DynamicMusicIsActive())
		{
			this.SetDynamicMusicZoomLevel();
			this.SetDynamicMusicTimeSinceLastJob();
			if (this.activeDynamicSong.useTimeOfDay)
			{
				this.SetDynamicMusicTimeOfDay();
			}
			if (GameClock.Instance != null && GameClock.Instance.GetCurrentCycleAsPercentage() >= this.duskTimePercentage / 100f)
			{
				this.StopDynamicMusic(false);
			}
		}
	}

	// Token: 0x06003D6F RID: 15727 RVA: 0x00157B18 File Offset: 0x00155D18
	private void ClearFinishedSongs()
	{
		if (this.activeSongs.Count > 0)
		{
			ListPool<string, MusicManager>.PooledList pooledList = ListPool<string, MusicManager>.Allocate();
			foreach (KeyValuePair<string, MusicManager.SongInfo> keyValuePair in this.activeSongs)
			{
				MusicManager.SongInfo value = keyValuePair.Value;
				FMOD.Studio.EventInstance ev = value.ev;
				ev.getPlaybackState(out value.musicPlaybackState);
				if (value.musicPlaybackState == PLAYBACK_STATE.STOPPED || value.musicPlaybackState == PLAYBACK_STATE.STOPPING)
				{
					pooledList.Add(keyValuePair.Key);
					foreach (string text in value.songsOnHold)
					{
						this.SetSongParameter(text, "interrupted_dimmed", 0f, true);
					}
					value.songsOnHold.Clear();
				}
			}
			foreach (string text2 in pooledList)
			{
				this.activeSongs.Remove(text2);
			}
			pooledList.Recycle();
		}
	}

	// Token: 0x06003D70 RID: 15728 RVA: 0x00157C68 File Offset: 0x00155E68
	public void OnEscapeMenu(bool paused)
	{
		foreach (KeyValuePair<string, MusicManager.SongInfo> keyValuePair in this.activeSongs)
		{
			if (keyValuePair.Value != null)
			{
				this.StartFadeToPause(keyValuePair.Value.ev, paused, 0.25f);
			}
		}
	}

	// Token: 0x06003D71 RID: 15729 RVA: 0x00157CD8 File Offset: 0x00155ED8
	public void OnSupplyClosetMenu(bool paused, float fadeTime)
	{
		foreach (KeyValuePair<string, MusicManager.SongInfo> keyValuePair in this.activeSongs)
		{
			if (keyValuePair.Value != null && (paused || !keyValuePair.Value.dynamic))
			{
				this.StartFadeToPause(keyValuePair.Value.ev, paused, fadeTime);
			}
		}
	}

	// Token: 0x06003D72 RID: 15730 RVA: 0x00157D54 File Offset: 0x00155F54
	public void StartFadeToPause(FMOD.Studio.EventInstance inst, bool paused, float fadeTime = 0.25f)
	{
		if (paused)
		{
			base.StartCoroutine(this.FadeToPause(inst, fadeTime));
			return;
		}
		base.StartCoroutine(this.FadeToUnpause(inst, fadeTime));
	}

	// Token: 0x06003D73 RID: 15731 RVA: 0x00157D78 File Offset: 0x00155F78
	private IEnumerator FadeToPause(FMOD.Studio.EventInstance inst, float fadeTime)
	{
		float startVolume;
		float targetVolume;
		inst.getVolume(out startVolume, out targetVolume);
		targetVolume = 0f;
		float lerpTime = 0f;
		while (lerpTime < 1f)
		{
			lerpTime += Time.unscaledDeltaTime / fadeTime;
			float num = Mathf.Lerp(startVolume, targetVolume, lerpTime);
			inst.setVolume(num);
			yield return null;
		}
		inst.setPaused(true);
		yield break;
	}

	// Token: 0x06003D74 RID: 15732 RVA: 0x00157D8E File Offset: 0x00155F8E
	private IEnumerator FadeToUnpause(FMOD.Studio.EventInstance inst, float fadeTime)
	{
		float startVolume;
		float targetVolume;
		inst.getVolume(out startVolume, out targetVolume);
		targetVolume = 1f;
		float lerpTime = 0f;
		inst.setPaused(false);
		while (lerpTime < 1f)
		{
			lerpTime += Time.unscaledDeltaTime / fadeTime;
			float num = Mathf.Lerp(startVolume, targetVolume, lerpTime);
			inst.setVolume(num);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06003D75 RID: 15733 RVA: 0x00157DA4 File Offset: 0x00155FA4
	public void PlayDynamicMusic()
	{
		if (this.DynamicMusicIsActive())
		{
			this.Log("Trying to play DynamicMusic when it is already playing.");
			return;
		}
		string nextDynamicSong = this.GetNextDynamicSong();
		if (nextDynamicSong == "NONE")
		{
			return;
		}
		this.PlaySong(nextDynamicSong, false);
		MusicManager.SongInfo songInfo;
		if (this.activeSongs.TryGetValue(nextDynamicSong, out songInfo))
		{
			this.activeDynamicSong = songInfo;
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().DynamicMusicPlayingSnapshot);
			if (SpeedControlScreen.Instance != null && SpeedControlScreen.Instance.IsPaused)
			{
				this.SetDynamicMusicPaused();
			}
			if (OverlayScreen.Instance != null && OverlayScreen.Instance.mode != OverlayModes.None.ID)
			{
				this.SetDynamicMusicOverlayActive();
			}
			this.SetDynamicMusicPlayHook();
			this.SetDynamicMusicKeySigniture();
			string text = "Volume_Music";
			if (KPlayerPrefs.HasKey(text))
			{
				float @float = KPlayerPrefs.GetFloat(text);
				AudioMixer.instance.SetSnapshotParameter(AudioMixerSnapshots.Get().DynamicMusicPlayingSnapshot, "userVolume_Music", @float, true);
			}
			AudioMixer.instance.SetSnapshotParameter(AudioMixerSnapshots.Get().DynamicMusicPlayingSnapshot, "intensity", songInfo.sfxAttenuationPercentage / 100f, true);
			return;
		}
		this.Log("DynamicMusic song " + nextDynamicSong + " did not start.");
		string text2 = "";
		foreach (KeyValuePair<string, MusicManager.SongInfo> keyValuePair in this.activeSongs)
		{
			text2 = text2 + keyValuePair.Key + ", ";
			global::Debug.Log(text2);
		}
		DebugUtil.DevAssert(false, "Song failed to play: " + nextDynamicSong, null);
	}

	// Token: 0x06003D76 RID: 15734 RVA: 0x00157F48 File Offset: 0x00156148
	public void StopDynamicMusic(bool stopImmediate = false)
	{
		if (this.activeDynamicSong != null)
		{
			FMOD.Studio.STOP_MODE stop_MODE = (stopImmediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			this.Log("Stop DynamicMusic: " + Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent));
			this.StopSong(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), true, stop_MODE);
			this.activeDynamicSong = null;
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().DynamicMusicPlayingSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
	}

	// Token: 0x06003D77 RID: 15735 RVA: 0x00157FBC File Offset: 0x001561BC
	public string GetNextDynamicSong()
	{
		string text = "";
		if (this.alwaysPlayMusic && this.nextMusicType == MusicManager.TypeOfMusic.None)
		{
			while (this.nextMusicType == MusicManager.TypeOfMusic.None)
			{
				this.CycleToNextMusicType();
			}
		}
		switch (this.nextMusicType)
		{
		case MusicManager.TypeOfMusic.DynamicSong:
			text = this.fullSongPlaylist.GetNextSong();
			this.activePlaylist = this.fullSongPlaylist;
			break;
		case MusicManager.TypeOfMusic.MiniSong:
			text = this.miniSongPlaylist.GetNextSong();
			this.activePlaylist = this.miniSongPlaylist;
			break;
		case MusicManager.TypeOfMusic.None:
			text = "NONE";
			this.activePlaylist = null;
			break;
		}
		this.CycleToNextMusicType();
		return text;
	}

	// Token: 0x06003D78 RID: 15736 RVA: 0x00158054 File Offset: 0x00156254
	private void CycleToNextMusicType()
	{
		int num = this.musicTypeIterator + 1;
		this.musicTypeIterator = num;
		this.musicTypeIterator = num % this.musicStyleOrder.Length;
		this.nextMusicType = this.musicStyleOrder[this.musicTypeIterator];
	}

	// Token: 0x06003D79 RID: 15737 RVA: 0x00158094 File Offset: 0x00156294
	public bool DynamicMusicIsActive()
	{
		return this.activeDynamicSong != null;
	}

	// Token: 0x06003D7A RID: 15738 RVA: 0x001580A1 File Offset: 0x001562A1
	public void SetDynamicMusicPaused()
	{
		if (this.DynamicMusicIsActive())
		{
			this.SetSongParameter(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), "Paused", 1f, true);
		}
	}

	// Token: 0x06003D7B RID: 15739 RVA: 0x001580CC File Offset: 0x001562CC
	public void SetDynamicMusicUnpaused()
	{
		if (this.DynamicMusicIsActive())
		{
			this.SetSongParameter(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), "Paused", 0f, true);
		}
	}

	// Token: 0x06003D7C RID: 15740 RVA: 0x001580F8 File Offset: 0x001562F8
	public void SetDynamicMusicZoomLevel()
	{
		if (CameraController.Instance != null)
		{
			float num = 100f - Camera.main.orthographicSize / 20f * 100f;
			this.SetSongParameter(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), "zoomPercentage", num, false);
		}
	}

	// Token: 0x06003D7D RID: 15741 RVA: 0x0015814C File Offset: 0x0015634C
	public void SetDynamicMusicTimeSinceLastJob()
	{
		this.SetSongParameter(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), "secsSinceNewJob", Time.time - Game.Instance.LastTimeWorkStarted, false);
	}

	// Token: 0x06003D7E RID: 15742 RVA: 0x0015817C File Offset: 0x0015637C
	public void SetDynamicMusicTimeOfDay()
	{
		if (this.time >= this.timeOfDayUpdateRate)
		{
			this.SetSongParameter(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), "timeOfDay", GameClock.Instance.GetCurrentCycleAsPercentage(), false);
			this.time = 0f;
		}
		this.time += Time.deltaTime;
	}

	// Token: 0x06003D7F RID: 15743 RVA: 0x001581DA File Offset: 0x001563DA
	public void SetDynamicMusicOverlayActive()
	{
		if (this.DynamicMusicIsActive())
		{
			this.SetSongParameter(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), "overlayActive", 1f, true);
		}
	}

	// Token: 0x06003D80 RID: 15744 RVA: 0x00158205 File Offset: 0x00156405
	public void SetDynamicMusicOverlayInactive()
	{
		if (this.DynamicMusicIsActive())
		{
			this.SetSongParameter(Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent), "overlayActive", 0f, true);
		}
	}

	// Token: 0x06003D81 RID: 15745 RVA: 0x00158230 File Offset: 0x00156430
	public void SetDynamicMusicPlayHook()
	{
		if (this.DynamicMusicIsActive())
		{
			string simpleSoundEventName = Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent);
			this.SetSongParameter(simpleSoundEventName, "playHook", this.activeDynamicSong.playHook ? 1f : 0f, true);
			this.activePlaylist.songMap[simpleSoundEventName].playHook = !this.activePlaylist.songMap[simpleSoundEventName].playHook;
		}
	}

	// Token: 0x06003D82 RID: 15746 RVA: 0x001582AB File Offset: 0x001564AB
	public bool ShouldPlayDynamicMusicLoadedGame()
	{
		return GameClock.Instance.GetCurrentCycleAsPercentage() <= this.loadGameCutoffPercentage / 100f;
	}

	// Token: 0x06003D83 RID: 15747 RVA: 0x001582C8 File Offset: 0x001564C8
	public void SetDynamicMusicKeySigniture()
	{
		if (this.DynamicMusicIsActive())
		{
			string simpleSoundEventName = Assets.GetSimpleSoundEventName(this.activeDynamicSong.fmodEvent);
			string musicKeySigniture = this.activePlaylist.songMap[simpleSoundEventName].musicKeySigniture;
			float num;
			if (musicKeySigniture != null)
			{
				if (musicKeySigniture == "Ab")
				{
					num = 0f;
					goto IL_92;
				}
				if (musicKeySigniture == "Bb")
				{
					num = 1f;
					goto IL_92;
				}
				if (musicKeySigniture == "C")
				{
					num = 2f;
					goto IL_92;
				}
				if (musicKeySigniture == "D")
				{
					num = 3f;
					goto IL_92;
				}
			}
			num = 2f;
			IL_92:
			RuntimeManager.StudioSystem.setParameterByName("MusicInKey", num, false);
		}
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x06003D84 RID: 15748 RVA: 0x0015837C File Offset: 0x0015657C
	public static MusicManager instance
	{
		get
		{
			return MusicManager._instance;
		}
	}

	// Token: 0x06003D85 RID: 15749 RVA: 0x00158383 File Offset: 0x00156583
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (!RuntimeManager.IsInitialized)
		{
			base.enabled = false;
			return;
		}
		if (KPlayerPrefs.HasKey(AudioOptionsScreen.AlwaysPlayMusicKey))
		{
			this.alwaysPlayMusic = KPlayerPrefs.GetInt(AudioOptionsScreen.AlwaysPlayMusicKey) == 1;
		}
	}

	// Token: 0x06003D86 RID: 15750 RVA: 0x001583BD File Offset: 0x001565BD
	protected override void OnPrefabInit()
	{
		MusicManager._instance = this;
		this.ConfigureSongs();
		this.fullSongPlaylist.ResetUnplayedSongs();
		this.miniSongPlaylist.ResetUnplayedSongs();
		this.nextMusicType = this.musicStyleOrder[this.musicTypeIterator];
	}

	// Token: 0x06003D87 RID: 15751 RVA: 0x001583F4 File Offset: 0x001565F4
	protected override void OnCleanUp()
	{
		MusicManager._instance = null;
	}

	// Token: 0x06003D88 RID: 15752 RVA: 0x001583FC File Offset: 0x001565FC
	[ContextMenu("Reload")]
	private void ConfigureSongs()
	{
		this.songMap.Clear();
		foreach (MusicManager.DynamicSong dynamicSong in this.fullSongs)
		{
			if (DlcManager.IsContentActive(dynamicSong.requiredDlcId))
			{
				string simpleSoundEventName = Assets.GetSimpleSoundEventName(dynamicSong.fmodEvent);
				MusicManager.SongInfo songInfo = new MusicManager.SongInfo();
				songInfo.fmodEvent = dynamicSong.fmodEvent;
				songInfo.requiredDlcId = dynamicSong.requiredDlcId;
				songInfo.priority = 100;
				songInfo.interruptsActiveMusic = false;
				songInfo.dynamic = true;
				songInfo.useTimeOfDay = dynamicSong.useTimeOfDay;
				songInfo.numberOfVariations = dynamicSong.numberOfVariations;
				songInfo.musicKeySigniture = dynamicSong.musicKeySigniture;
				songInfo.sfxAttenuationPercentage = this.dynamicMusicSFXAttenuationPercentage;
				this.songMap[simpleSoundEventName] = songInfo;
				this.fullSongPlaylist.songMap[simpleSoundEventName] = songInfo;
			}
		}
		foreach (MusicManager.Minisong minisong in this.miniSongs)
		{
			if (DlcManager.IsContentActive(minisong.requiredDlcId))
			{
				string simpleSoundEventName2 = Assets.GetSimpleSoundEventName(minisong.fmodEvent);
				MusicManager.SongInfo songInfo2 = new MusicManager.SongInfo();
				songInfo2.fmodEvent = minisong.fmodEvent;
				songInfo2.requiredDlcId = minisong.requiredDlcId;
				songInfo2.priority = 100;
				songInfo2.interruptsActiveMusic = false;
				songInfo2.dynamic = true;
				songInfo2.useTimeOfDay = false;
				songInfo2.numberOfVariations = 5;
				songInfo2.musicKeySigniture = minisong.musicKeySigniture;
				songInfo2.sfxAttenuationPercentage = this.miniSongSFXAttenuationPercentage;
				this.songMap[simpleSoundEventName2] = songInfo2;
				this.miniSongPlaylist.songMap[simpleSoundEventName2] = songInfo2;
			}
		}
		foreach (MusicManager.Stinger stinger in this.stingers)
		{
			if (DlcManager.IsContentActive(stinger.requiredDlcId))
			{
				string simpleSoundEventName3 = Assets.GetSimpleSoundEventName(stinger.fmodEvent);
				MusicManager.SongInfo songInfo3 = new MusicManager.SongInfo();
				songInfo3.fmodEvent = stinger.fmodEvent;
				songInfo3.priority = 100;
				songInfo3.interruptsActiveMusic = true;
				songInfo3.dynamic = false;
				songInfo3.useTimeOfDay = false;
				songInfo3.numberOfVariations = 0;
				songInfo3.requiredDlcId = stinger.requiredDlcId;
				this.SongMap[simpleSoundEventName3] = songInfo3;
			}
		}
		foreach (MusicManager.MenuSong menuSong in this.menuSongs)
		{
			if (DlcManager.IsContentActive(menuSong.requiredDlcId))
			{
				string simpleSoundEventName4 = Assets.GetSimpleSoundEventName(menuSong.fmodEvent);
				MusicManager.SongInfo songInfo4 = new MusicManager.SongInfo();
				songInfo4.fmodEvent = menuSong.fmodEvent;
				songInfo4.priority = 100;
				songInfo4.interruptsActiveMusic = true;
				songInfo4.dynamic = false;
				songInfo4.useTimeOfDay = false;
				songInfo4.numberOfVariations = 0;
				songInfo4.requiredDlcId = menuSong.requiredDlcId;
				this.SongMap[simpleSoundEventName4] = songInfo4;
			}
		}
	}

	// Token: 0x06003D89 RID: 15753 RVA: 0x001586DE File Offset: 0x001568DE
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x06003D8A RID: 15754 RVA: 0x001586E0 File Offset: 0x001568E0
	public void OnAfterDeserialize()
	{
	}

	// Token: 0x06003D8B RID: 15755 RVA: 0x001586E2 File Offset: 0x001568E2
	private void Log(string s)
	{
	}

	// Token: 0x0400284E RID: 10318
	private const string VARIATION_ID = "variation";

	// Token: 0x0400284F RID: 10319
	private const string INTERRUPTED_DIMMED_ID = "interrupted_dimmed";

	// Token: 0x04002850 RID: 10320
	private const string MUSIC_KEY = "MusicInKey";

	// Token: 0x04002851 RID: 10321
	private const float DYNAMIC_MUSIC_SCHEDULE_DELAY = 16000f;

	// Token: 0x04002852 RID: 10322
	private const float DYNAMIC_MUSIC_SCHEDULE_LOOKAHEAD = 48000f;

	// Token: 0x04002853 RID: 10323
	[Header("Song Lists")]
	[Tooltip("Play during the daytime. The mix of the song is affected by the player's input, like pausing the sim, activating an overlay, or zooming in and out.")]
	[SerializeField]
	private MusicManager.DynamicSong[] fullSongs;

	// Token: 0x04002854 RID: 10324
	[Tooltip("Simple dynamic songs which are more ambient in nature, which play quietly during \"non-music\" days. These are affected by Pause and OverlayActive.")]
	[SerializeField]
	private MusicManager.Minisong[] miniSongs;

	// Token: 0x04002855 RID: 10325
	[Tooltip("Triggered by in-game events, such as completing research or night-time falling. They will temporarily interrupt a dynamicSong, fading the dynamicSong back in after the stinger is complete.")]
	[SerializeField]
	private MusicManager.Stinger[] stingers;

	// Token: 0x04002856 RID: 10326
	[Tooltip("Generally songs that don't play during gameplay, while a menu is open. For example, the ESC menu or the Starmap.")]
	[SerializeField]
	private MusicManager.MenuSong[] menuSongs;

	// Token: 0x04002857 RID: 10327
	private Dictionary<string, MusicManager.SongInfo> songMap = new Dictionary<string, MusicManager.SongInfo>();

	// Token: 0x04002858 RID: 10328
	public Dictionary<string, MusicManager.SongInfo> activeSongs = new Dictionary<string, MusicManager.SongInfo>();

	// Token: 0x04002859 RID: 10329
	[Space]
	[Header("Tuning Values")]
	[Tooltip("Just before night-time (88%), dynamic music fades out. At which point of the day should the music fade?")]
	[SerializeField]
	private float duskTimePercentage = 85f;

	// Token: 0x0400285A RID: 10330
	[Tooltip("If we load into a save and the day is almost over, we shouldn't play music because it will stop soon anyway. At what point of the day should we not play music?")]
	[SerializeField]
	private float loadGameCutoffPercentage = 50f;

	// Token: 0x0400285B RID: 10331
	[Tooltip("When dynamic music is active, we play a snapshot which attenuates the ambience and SFX. What intensity should that snapshot be applied?")]
	[SerializeField]
	private float dynamicMusicSFXAttenuationPercentage = 65f;

	// Token: 0x0400285C RID: 10332
	[Tooltip("When mini songs are active, we play a snapshot which attenuates the ambience and SFX. What intensity should that snapshot be applied?")]
	[SerializeField]
	private float miniSongSFXAttenuationPercentage;

	// Token: 0x0400285D RID: 10333
	[SerializeField]
	private MusicManager.TypeOfMusic[] musicStyleOrder;

	// Token: 0x0400285E RID: 10334
	[NonSerialized]
	public bool alwaysPlayMusic;

	// Token: 0x0400285F RID: 10335
	private MusicManager.DynamicSongPlaylist fullSongPlaylist = new MusicManager.DynamicSongPlaylist();

	// Token: 0x04002860 RID: 10336
	private MusicManager.DynamicSongPlaylist miniSongPlaylist = new MusicManager.DynamicSongPlaylist();

	// Token: 0x04002861 RID: 10337
	[NonSerialized]
	public MusicManager.SongInfo activeDynamicSong;

	// Token: 0x04002862 RID: 10338
	[NonSerialized]
	public MusicManager.DynamicSongPlaylist activePlaylist;

	// Token: 0x04002863 RID: 10339
	private MusicManager.TypeOfMusic nextMusicType;

	// Token: 0x04002864 RID: 10340
	private int musicTypeIterator;

	// Token: 0x04002865 RID: 10341
	private float time;

	// Token: 0x04002866 RID: 10342
	private float timeOfDayUpdateRate = 2f;

	// Token: 0x04002867 RID: 10343
	private static MusicManager _instance;

	// Token: 0x04002868 RID: 10344
	[NonSerialized]
	public List<string> MusicDebugLog = new List<string>();

	// Token: 0x02001612 RID: 5650
	[DebuggerDisplay("{fmodEvent}")]
	[Serializable]
	public class SongInfo
	{
		// Token: 0x040068E9 RID: 26857
		public EventReference fmodEvent;

		// Token: 0x040068EA RID: 26858
		[NonSerialized]
		public int priority;

		// Token: 0x040068EB RID: 26859
		[NonSerialized]
		public bool interruptsActiveMusic;

		// Token: 0x040068EC RID: 26860
		[NonSerialized]
		public bool dynamic;

		// Token: 0x040068ED RID: 26861
		[NonSerialized]
		public string requiredDlcId = "";

		// Token: 0x040068EE RID: 26862
		[NonSerialized]
		public bool useTimeOfDay;

		// Token: 0x040068EF RID: 26863
		[NonSerialized]
		public int numberOfVariations;

		// Token: 0x040068F0 RID: 26864
		[NonSerialized]
		public string musicKeySigniture = "C";

		// Token: 0x040068F1 RID: 26865
		[NonSerialized]
		public FMOD.Studio.EventInstance ev;

		// Token: 0x040068F2 RID: 26866
		[NonSerialized]
		public List<string> songsOnHold = new List<string>();

		// Token: 0x040068F3 RID: 26867
		[NonSerialized]
		public PLAYBACK_STATE musicPlaybackState;

		// Token: 0x040068F4 RID: 26868
		[NonSerialized]
		public bool playHook = true;

		// Token: 0x040068F5 RID: 26869
		[NonSerialized]
		public float sfxAttenuationPercentage = 65f;
	}

	// Token: 0x02001613 RID: 5651
	[DebuggerDisplay("{fmodEvent}")]
	[Serializable]
	public class DynamicSong
	{
		// Token: 0x040068F6 RID: 26870
		public EventReference fmodEvent;

		// Token: 0x040068F7 RID: 26871
		[Tooltip("Some songs are set up to have Morning, Daytime, Hook, and Intro sections. Toggle this ON if this song has those sections.")]
		[SerializeField]
		public bool useTimeOfDay;

		// Token: 0x040068F8 RID: 26872
		[Tooltip("Some songs have different possible start locations. Enter how many start locations this song is set up to support.")]
		[SerializeField]
		public int numberOfVariations;

		// Token: 0x040068F9 RID: 26873
		[Tooltip("Some songs have different key signitures. Enter the key this music is in.")]
		[SerializeField]
		public string musicKeySigniture = "";

		// Token: 0x040068FA RID: 26874
		[Tooltip("Should playback of this song be limited to an active DLC?")]
		[SerializeField]
		public string requiredDlcId = "";
	}

	// Token: 0x02001614 RID: 5652
	[DebuggerDisplay("{fmodEvent}")]
	[Serializable]
	public class Stinger
	{
		// Token: 0x040068FB RID: 26875
		public EventReference fmodEvent;

		// Token: 0x040068FC RID: 26876
		[Tooltip("Should playback of this song be limited to an active DLC?")]
		[SerializeField]
		public string requiredDlcId = "";
	}

	// Token: 0x02001615 RID: 5653
	[DebuggerDisplay("{fmodEvent}")]
	[Serializable]
	public class MenuSong
	{
		// Token: 0x040068FD RID: 26877
		public EventReference fmodEvent;

		// Token: 0x040068FE RID: 26878
		[Tooltip("Should playback of this song be limited to an active DLC?")]
		[SerializeField]
		public string requiredDlcId = "";
	}

	// Token: 0x02001616 RID: 5654
	[DebuggerDisplay("{fmodEvent}")]
	[Serializable]
	public class Minisong
	{
		// Token: 0x040068FF RID: 26879
		public EventReference fmodEvent;

		// Token: 0x04006900 RID: 26880
		[Tooltip("Some songs have different key signitures. Enter the key this music is in.")]
		[SerializeField]
		public string musicKeySigniture = "";

		// Token: 0x04006901 RID: 26881
		[Tooltip("Should playback of this song be limited to an active DLC?")]
		[SerializeField]
		public string requiredDlcId = "";
	}

	// Token: 0x02001617 RID: 5655
	public enum TypeOfMusic
	{
		// Token: 0x04006903 RID: 26883
		DynamicSong,
		// Token: 0x04006904 RID: 26884
		MiniSong,
		// Token: 0x04006905 RID: 26885
		None
	}

	// Token: 0x02001618 RID: 5656
	public class DynamicSongPlaylist
	{
		// Token: 0x060086B3 RID: 34483 RVA: 0x002EFA70 File Offset: 0x002EDC70
		public string GetNextSong()
		{
			string text;
			if (this.unplayedSongs.Count > 0)
			{
				int num = UnityEngine.Random.Range(0, this.unplayedSongs.Count);
				text = this.unplayedSongs[num];
				this.unplayedSongs.RemoveAt(num);
			}
			else
			{
				this.ResetUnplayedSongs();
				bool flag = this.unplayedSongs.Count > 1;
				if (flag)
				{
					for (int i = 0; i < this.unplayedSongs.Count; i++)
					{
						if (this.unplayedSongs[i] == this.lastSongPlayed)
						{
							this.unplayedSongs.Remove(this.unplayedSongs[i]);
							break;
						}
					}
				}
				int num2 = UnityEngine.Random.Range(0, this.unplayedSongs.Count);
				text = this.unplayedSongs[num2];
				this.unplayedSongs.RemoveAt(num2);
				if (flag)
				{
					this.unplayedSongs.Add(this.lastSongPlayed);
				}
			}
			this.lastSongPlayed = text;
			global::Debug.Assert(this.songMap.ContainsKey(text), "Missing song " + text);
			return Assets.GetSimpleSoundEventName(this.songMap[text].fmodEvent);
		}

		// Token: 0x060086B4 RID: 34484 RVA: 0x002EFB9C File Offset: 0x002EDD9C
		public void ResetUnplayedSongs()
		{
			this.unplayedSongs.Clear();
			foreach (KeyValuePair<string, MusicManager.SongInfo> keyValuePair in this.songMap)
			{
				if (DlcManager.IsContentActive(keyValuePair.Value.requiredDlcId))
				{
					this.unplayedSongs.Add(keyValuePair.Key);
				}
			}
		}

		// Token: 0x04006906 RID: 26886
		public Dictionary<string, MusicManager.SongInfo> songMap = new Dictionary<string, MusicManager.SongInfo>();

		// Token: 0x04006907 RID: 26887
		public List<string> unplayedSongs = new List<string>();

		// Token: 0x04006908 RID: 26888
		private string lastSongPlayed = "";
	}
}
