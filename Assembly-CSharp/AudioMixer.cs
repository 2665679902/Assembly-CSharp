using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class AudioMixer
{
	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06001733 RID: 5939 RVA: 0x00078849 File Offset: 0x00076A49
	public static AudioMixer instance
	{
		get
		{
			return AudioMixer._instance;
		}
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x00078850 File Offset: 0x00076A50
	public static AudioMixer Create()
	{
		AudioMixer._instance = new AudioMixer();
		AudioMixerSnapshots audioMixerSnapshots = AudioMixerSnapshots.Get();
		if (audioMixerSnapshots != null)
		{
			audioMixerSnapshots.ReloadSnapshots();
		}
		return AudioMixer._instance;
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x00078881 File Offset: 0x00076A81
	public static void Destroy()
	{
		AudioMixer._instance.StopAll(FMOD.Studio.STOP_MODE.IMMEDIATE);
		AudioMixer._instance = null;
	}

	// Token: 0x06001736 RID: 5942 RVA: 0x00078894 File Offset: 0x00076A94
	public EventInstance Start(EventReference event_ref)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return this.Start(text);
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x000788C0 File Offset: 0x00076AC0
	public EventInstance Start(string snapshot)
	{
		EventInstance eventInstance;
		if (!this.activeSnapshots.TryGetValue(snapshot, out eventInstance))
		{
			if (RuntimeManager.IsInitialized)
			{
				eventInstance = KFMOD.CreateInstance(snapshot);
				this.activeSnapshots[snapshot] = eventInstance;
				eventInstance.start();
				eventInstance.setParameterByName("snapshotActive", 1f, false);
			}
			else
			{
				eventInstance = default(EventInstance);
			}
		}
		AudioMixer.instance.Log("Start Snapshot: " + snapshot);
		return eventInstance;
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x00078940 File Offset: 0x00076B40
	public bool Stop(EventReference event_ref, FMOD.Studio.STOP_MODE stop_mode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return this.Stop(text, stop_mode);
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x00078970 File Offset: 0x00076B70
	public bool Stop(HashedString snapshot, FMOD.Studio.STOP_MODE stop_mode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
	{
		bool flag = false;
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshot, out eventInstance))
		{
			eventInstance.setParameterByName("snapshotActive", 0f, false);
			eventInstance.stop(stop_mode);
			eventInstance.release();
			this.activeSnapshots.Remove(snapshot);
			flag = true;
			AudioMixer instance = AudioMixer.instance;
			string[] array = new string[5];
			array[0] = "Stop Snapshot: [";
			int num = 1;
			HashedString hashedString = snapshot;
			array[num] = hashedString.ToString();
			array[2] = "] with fadeout mode: [";
			array[3] = stop_mode.ToString();
			array[4] = "]";
			instance.Log(string.Concat(array));
		}
		else
		{
			AudioMixer instance2 = AudioMixer.instance;
			string text = "Tried to stop snapshot: [";
			HashedString hashedString = snapshot;
			instance2.Log(text + hashedString.ToString() + "] but it wasn't active.");
		}
		return flag;
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x00078A3F File Offset: 0x00076C3F
	public void Reset()
	{
		this.StopAll(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x00078A48 File Offset: 0x00076C48
	public void StopAll(FMOD.Studio.STOP_MODE stop_mode = FMOD.Studio.STOP_MODE.IMMEDIATE)
	{
		List<HashedString> list = new List<HashedString>();
		foreach (KeyValuePair<HashedString, EventInstance> keyValuePair in this.activeSnapshots)
		{
			if (keyValuePair.Key != AudioMixer.UserVolumeSettingsHash)
			{
				list.Add(keyValuePair.Key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			this.Stop(list[i], stop_mode);
		}
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x00078ADC File Offset: 0x00076CDC
	public bool SnapshotIsActive(EventReference event_ref)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return this.SnapshotIsActive(text);
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x00078B0B File Offset: 0x00076D0B
	public bool SnapshotIsActive(HashedString snapshot_name)
	{
		return this.activeSnapshots.ContainsKey(snapshot_name);
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x00078B20 File Offset: 0x00076D20
	public void SetSnapshotParameter(EventReference event_ref, string parameter_name, float parameter_value, bool shouldLog = true)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		this.SetSnapshotParameter(text, parameter_name, parameter_value, shouldLog);
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x00078B50 File Offset: 0x00076D50
	public void SetSnapshotParameter(string snapshot_name, string parameter_name, float parameter_value, bool shouldLog = true)
	{
		if (shouldLog)
		{
			this.Log(string.Format("Set Param {0}: {1}, {2}", snapshot_name, parameter_name, parameter_value));
		}
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshot_name, out eventInstance))
		{
			eventInstance.setParameterByName(parameter_name, parameter_value, false);
			return;
		}
		this.Log(string.Concat(new string[]
		{
			"Tried to set [",
			parameter_name,
			"] to [",
			parameter_value.ToString(),
			"] but [",
			snapshot_name,
			"] is not active."
		}));
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x00078BE0 File Offset: 0x00076DE0
	public void StartPersistentSnapshots()
	{
		this.persistentSnapshotsActive = true;
		this.Start(AudioMixerSnapshots.Get().DuplicantCountAttenuatorMigrated);
		this.Start(AudioMixerSnapshots.Get().DuplicantCountMovingSnapshot);
		this.Start(AudioMixerSnapshots.Get().DuplicantCountSleepingSnapshot);
		this.spaceVisibleInst = this.Start(AudioMixerSnapshots.Get().SpaceVisibleSnapshot);
		this.facilityVisibleInst = this.Start(AudioMixerSnapshots.Get().FacilityVisibleSnapshot);
		this.Start(AudioMixerSnapshots.Get().PulseSnapshot);
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x00078C64 File Offset: 0x00076E64
	public void StopPersistentSnapshots()
	{
		this.persistentSnapshotsActive = false;
		this.Stop(AudioMixerSnapshots.Get().DuplicantCountAttenuatorMigrated, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().DuplicantCountMovingSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().DuplicantCountSleepingSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().SpaceVisibleSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().FacilityVisibleSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.Stop(AudioMixerSnapshots.Get().PulseSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x00078CE4 File Offset: 0x00076EE4
	private string GetSnapshotName(EventReference event_ref)
	{
		string text;
		RuntimeManager.GetEventDescription(event_ref.Guid).getPath(out text);
		return text;
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x00078D08 File Offset: 0x00076F08
	public void UpdatePersistentSnapshotParameters()
	{
		this.SetVisibleDuplicants();
		string snapshotName = this.GetSnapshotName(AudioMixerSnapshots.Get().DuplicantCountMovingSnapshot);
		if (this.activeSnapshots.TryGetValue(snapshotName, out this.duplicantCountMovingInst))
		{
			this.duplicantCountMovingInst.setParameterByName("duplicantCount", (float)Mathf.Max(0, this.visibleDupes["moving"] - AudioMixer.VISIBLE_DUPLICANTS_BEFORE_ATTENUATION), false);
		}
		string snapshotName2 = this.GetSnapshotName(AudioMixerSnapshots.Get().DuplicantCountSleepingSnapshot);
		if (this.activeSnapshots.TryGetValue(snapshotName2, out this.duplicantCountSleepingInst))
		{
			this.duplicantCountSleepingInst.setParameterByName("duplicantCount", (float)Mathf.Max(0, this.visibleDupes["sleeping"] - AudioMixer.VISIBLE_DUPLICANTS_BEFORE_ATTENUATION), false);
		}
		string snapshotName3 = this.GetSnapshotName(AudioMixerSnapshots.Get().DuplicantCountAttenuatorMigrated);
		if (this.activeSnapshots.TryGetValue(snapshotName3, out this.duplicantCountInst))
		{
			this.duplicantCountInst.setParameterByName("duplicantCount", (float)Mathf.Max(0, this.visibleDupes["visible"] - AudioMixer.VISIBLE_DUPLICANTS_BEFORE_ATTENUATION), false);
		}
		string snapshotName4 = this.GetSnapshotName(AudioMixerSnapshots.Get().PulseSnapshot);
		if (this.activeSnapshots.TryGetValue(snapshotName4, out this.pulseInst))
		{
			float num = AudioMixer.PULSE_SNAPSHOT_BPM / 60f;
			int speed = SpeedControlScreen.Instance.GetSpeed();
			if (speed == 1)
			{
				num /= 2f;
			}
			else if (speed == 2)
			{
				num /= 3f;
			}
			float num2 = Mathf.Abs(Mathf.Sin(Time.time * 3.1415927f * num));
			this.pulseInst.setParameterByName("Pulse", num2, false);
		}
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x00078EB7 File Offset: 0x000770B7
	public void UpdateSpaceVisibleSnapshot(float percent)
	{
		this.spaceVisibleInst.setParameterByName("spaceVisible", percent, false);
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x00078ECC File Offset: 0x000770CC
	public void UpdateFacilityVisibleSnapshot(float percent)
	{
		this.facilityVisibleInst.setParameterByName("facilityVisible", percent, false);
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x00078EE4 File Offset: 0x000770E4
	private void SetVisibleDuplicants()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			Vector3 position = Components.LiveMinionIdentities[i].transform.GetPosition();
			if (CameraController.Instance.IsVisiblePos(position))
			{
				num++;
				Navigator component = Components.LiveMinionIdentities[i].GetComponent<Navigator>();
				if (component != null && component.IsMoving())
				{
					num2++;
				}
				else
				{
					StaminaMonitor.Instance smi = Components.LiveMinionIdentities[i].GetComponent<Worker>().GetSMI<StaminaMonitor.Instance>();
					if (smi != null && smi.IsSleeping())
					{
						num3++;
					}
				}
			}
		}
		this.visibleDupes["visible"] = num;
		this.visibleDupes["moving"] = num2;
		this.visibleDupes["sleeping"] = num3;
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x00078FC4 File Offset: 0x000771C4
	public void StartUserVolumesSnapshot()
	{
		this.Start(AudioMixerSnapshots.Get().UserVolumeSettingsSnapshot);
		string snapshotName = this.GetSnapshotName(AudioMixerSnapshots.Get().UserVolumeSettingsSnapshot);
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshotName, out eventInstance))
		{
			EventDescription eventDescription;
			eventInstance.getDescription(out eventDescription);
			USER_PROPERTY user_PROPERTY;
			eventDescription.getUserProperty("buses", out user_PROPERTY);
			string text = user_PROPERTY.stringValue();
			char c = '-';
			string[] array = text.Split(new char[] { c });
			for (int i = 0; i < array.Length; i++)
			{
				float num = 1f;
				string text2 = "Volume_" + array[i];
				if (KPlayerPrefs.HasKey(text2))
				{
					num = KPlayerPrefs.GetFloat(text2);
				}
				AudioMixer.UserVolumeBus userVolumeBus = new AudioMixer.UserVolumeBus();
				userVolumeBus.busLevel = num;
				userVolumeBus.labelString = Strings.Get("STRINGS.UI.FRONTEND.AUDIO_OPTIONS_SCREEN.AUDIO_BUS_" + array[i].ToUpper());
				this.userVolumeSettings.Add(array[i], userVolumeBus);
				this.SetUserVolume(array[i], userVolumeBus.busLevel);
			}
		}
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000790E0 File Offset: 0x000772E0
	public void SetUserVolume(string bus, float value)
	{
		if (!this.userVolumeSettings.ContainsKey(bus))
		{
			global::Debug.LogError("The provided bus doesn't exist. Check yo'self fool!");
			return;
		}
		if (value > 1f)
		{
			value = 1f;
		}
		else if (value < 0f)
		{
			value = 0f;
		}
		this.userVolumeSettings[bus].busLevel = value;
		KPlayerPrefs.SetFloat("Volume_" + bus, value);
		string snapshotName = this.GetSnapshotName(AudioMixerSnapshots.Get().UserVolumeSettingsSnapshot);
		EventInstance eventInstance;
		if (this.activeSnapshots.TryGetValue(snapshotName, out eventInstance))
		{
			eventInstance.setParameterByName("userVolume_" + bus, this.userVolumeSettings[bus].busLevel, false);
		}
		else
		{
			this.Log(string.Concat(new string[]
			{
				"Tried to set [",
				bus,
				"] to [",
				value.ToString(),
				"] but UserVolumeSettingsSnapshot is not active."
			}));
		}
		if (bus == "Music")
		{
			this.SetSnapshotParameter(AudioMixerSnapshots.Get().DynamicMusicPlayingSnapshot, "userVolume_Music", value, true);
		}
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000791F1 File Offset: 0x000773F1
	private void Log(string s)
	{
	}

	// Token: 0x04000CDB RID: 3291
	private static AudioMixer _instance = null;

	// Token: 0x04000CDC RID: 3292
	private const string DUPLICANT_COUNT_ID = "duplicantCount";

	// Token: 0x04000CDD RID: 3293
	private const string PULSE_ID = "Pulse";

	// Token: 0x04000CDE RID: 3294
	private const string SNAPSHOT_ACTIVE_ID = "snapshotActive";

	// Token: 0x04000CDF RID: 3295
	private const string SPACE_VISIBLE_ID = "spaceVisible";

	// Token: 0x04000CE0 RID: 3296
	private const string FACILITY_VISIBLE_ID = "facilityVisible";

	// Token: 0x04000CE1 RID: 3297
	private const string FOCUS_BUS_PATH = "bus:/SFX/Focus";

	// Token: 0x04000CE2 RID: 3298
	public Dictionary<HashedString, EventInstance> activeSnapshots = new Dictionary<HashedString, EventInstance>();

	// Token: 0x04000CE3 RID: 3299
	public List<HashedString> SnapshotDebugLog = new List<HashedString>();

	// Token: 0x04000CE4 RID: 3300
	public bool activeNIS;

	// Token: 0x04000CE5 RID: 3301
	public static float LOW_PRIORITY_CUTOFF_DISTANCE = 10f;

	// Token: 0x04000CE6 RID: 3302
	public static float PULSE_SNAPSHOT_BPM = 120f;

	// Token: 0x04000CE7 RID: 3303
	public static int VISIBLE_DUPLICANTS_BEFORE_ATTENUATION = 2;

	// Token: 0x04000CE8 RID: 3304
	private EventInstance duplicantCountInst;

	// Token: 0x04000CE9 RID: 3305
	private EventInstance pulseInst;

	// Token: 0x04000CEA RID: 3306
	private EventInstance duplicantCountMovingInst;

	// Token: 0x04000CEB RID: 3307
	private EventInstance duplicantCountSleepingInst;

	// Token: 0x04000CEC RID: 3308
	private EventInstance spaceVisibleInst;

	// Token: 0x04000CED RID: 3309
	private EventInstance facilityVisibleInst;

	// Token: 0x04000CEE RID: 3310
	private static readonly HashedString UserVolumeSettingsHash = new HashedString("event:/Snapshots/Mixing/Snapshot_UserVolumeSettings");

	// Token: 0x04000CEF RID: 3311
	public bool persistentSnapshotsActive;

	// Token: 0x04000CF0 RID: 3312
	private Dictionary<string, int> visibleDupes = new Dictionary<string, int>();

	// Token: 0x04000CF1 RID: 3313
	public Dictionary<string, AudioMixer.UserVolumeBus> userVolumeSettings = new Dictionary<string, AudioMixer.UserVolumeBus>();

	// Token: 0x02001056 RID: 4182
	public class UserVolumeBus
	{
		// Token: 0x0400572B RID: 22315
		public string labelString;

		// Token: 0x0400572C RID: 22316
		public float busLevel;
	}
}
