using System;
using System.Runtime.Serialization;
using FMOD.Studio;
using KSerialization;
using UnityEngine;

// Token: 0x020008D2 RID: 2258
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/TimeOfDay")]
public class TimeOfDay : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x060040E0 RID: 16608 RVA: 0x0016B7A8 File Offset: 0x001699A8
	public static void DestroyInstance()
	{
		TimeOfDay.Instance = null;
	}

	// Token: 0x060040E1 RID: 16609 RVA: 0x0016B7B0 File Offset: 0x001699B0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		TimeOfDay.Instance = this;
	}

	// Token: 0x060040E2 RID: 16610 RVA: 0x0016B7BE File Offset: 0x001699BE
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		TimeOfDay.Instance = null;
	}

	// Token: 0x060040E3 RID: 16611 RVA: 0x0016B7CC File Offset: 0x001699CC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.timeRegion = this.GetCurrentTimeRegion();
		this.UpdateSunlightIntensity();
	}

	// Token: 0x060040E4 RID: 16612 RVA: 0x0016B7E7 File Offset: 0x001699E7
	[OnDeserialized]
	private void OnDeserialized()
	{
		this.UpdateVisuals();
	}

	// Token: 0x060040E5 RID: 16613 RVA: 0x0016B7EF File Offset: 0x001699EF
	public TimeOfDay.TimeRegion GetCurrentTimeRegion()
	{
		if (GameClock.Instance.IsNighttime())
		{
			return TimeOfDay.TimeRegion.Night;
		}
		return TimeOfDay.TimeRegion.Day;
	}

	// Token: 0x060040E6 RID: 16614 RVA: 0x0016B800 File Offset: 0x00169A00
	private void Update()
	{
		this.UpdateVisuals();
		this.UpdateAudio();
	}

	// Token: 0x060040E7 RID: 16615 RVA: 0x0016B810 File Offset: 0x00169A10
	private void UpdateVisuals()
	{
		float num = 0.875f;
		float num2 = 0.2f;
		float num3 = 1f;
		float num4 = 0f;
		if (GameClock.Instance.GetCurrentCycleAsPercentage() >= num)
		{
			num4 = num3;
		}
		this.scale = Mathf.Lerp(this.scale, num4, Time.deltaTime * num2);
		float num5 = this.UpdateSunlightIntensity();
		Shader.SetGlobalVector("_TimeOfDay", new Vector4(this.scale, num5, 0f, 0f));
	}

	// Token: 0x060040E8 RID: 16616 RVA: 0x0016B888 File Offset: 0x00169A88
	private void UpdateAudio()
	{
		TimeOfDay.TimeRegion currentTimeRegion = this.GetCurrentTimeRegion();
		if (currentTimeRegion != this.timeRegion)
		{
			this.TriggerSoundChange(currentTimeRegion);
			this.timeRegion = currentTimeRegion;
			base.Trigger(1791086652, null);
		}
	}

	// Token: 0x060040E9 RID: 16617 RVA: 0x0016B8BF File Offset: 0x00169ABF
	public void Sim4000ms(float dt)
	{
		this.UpdateSunlightIntensity();
	}

	// Token: 0x060040EA RID: 16618 RVA: 0x0016B8C8 File Offset: 0x00169AC8
	public void SetEclipse(bool eclipse)
	{
		this.isEclipse = eclipse;
	}

	// Token: 0x060040EB RID: 16619 RVA: 0x0016B8D4 File Offset: 0x00169AD4
	private float UpdateSunlightIntensity()
	{
		float daytimeDurationInPercentage = GameClock.Instance.GetDaytimeDurationInPercentage();
		float num = GameClock.Instance.GetCurrentCycleAsPercentage() / daytimeDurationInPercentage;
		if (num >= 1f || this.isEclipse)
		{
			num = 0f;
		}
		float num2 = Mathf.Sin(num * 3.1415927f);
		Game.Instance.currentFallbackSunlightIntensity = num2 * 80000f;
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			worldContainer.currentSunlightIntensity = num2 * (float)worldContainer.sunlight;
			worldContainer.currentCosmicIntensity = (float)worldContainer.cosmicRadiation;
		}
		return num2;
	}

	// Token: 0x060040EC RID: 16620 RVA: 0x0016B98C File Offset: 0x00169B8C
	private void TriggerSoundChange(TimeOfDay.TimeRegion new_region)
	{
		if (new_region == TimeOfDay.TimeRegion.Day)
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().NightStartedMigrated, STOP_MODE.ALLOWFADEOUT);
			if (MusicManager.instance.SongIsPlaying("Stinger_Loop_Night"))
			{
				MusicManager.instance.StopSong("Stinger_Loop_Night", true, STOP_MODE.ALLOWFADEOUT);
			}
			MusicManager.instance.PlaySong("Stinger_Day", false);
			MusicManager.instance.PlayDynamicMusic();
			return;
		}
		if (new_region != TimeOfDay.TimeRegion.Night)
		{
			return;
		}
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().NightStartedMigrated);
		MusicManager.instance.PlaySong("Stinger_Loop_Night", false);
	}

	// Token: 0x060040ED RID: 16621 RVA: 0x0016BA1A File Offset: 0x00169C1A
	public void SetScale(float new_scale)
	{
		this.scale = new_scale;
	}

	// Token: 0x04002B4B RID: 11083
	[Serialize]
	private float scale;

	// Token: 0x04002B4C RID: 11084
	private TimeOfDay.TimeRegion timeRegion;

	// Token: 0x04002B4D RID: 11085
	private EventInstance nightLPEvent;

	// Token: 0x04002B4E RID: 11086
	public static TimeOfDay Instance;

	// Token: 0x04002B4F RID: 11087
	private bool isEclipse;

	// Token: 0x02001695 RID: 5781
	public enum TimeRegion
	{
		// Token: 0x04006A3F RID: 27199
		Invalid,
		// Token: 0x04006A40 RID: 27200
		Day,
		// Token: 0x04006A41 RID: 27201
		Night
	}
}
