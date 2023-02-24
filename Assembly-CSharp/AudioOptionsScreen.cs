using System;
using System.Collections.Generic;
using FMODUnity;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000A49 RID: 2633
public class AudioOptionsScreen : KModalScreen
{
	// Token: 0x06004FD4 RID: 20436 RVA: 0x001C7268 File Offset: 0x001C5468
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.closeButton.onClick += delegate
		{
			this.OnClose(base.gameObject);
		};
		this.doneButton.onClick += delegate
		{
			this.OnClose(base.gameObject);
		};
		this.sliderPool = new UIPool<SliderContainer>(this.sliderPrefab);
		foreach (KeyValuePair<string, AudioMixer.UserVolumeBus> keyValuePair in AudioMixer.instance.userVolumeSettings)
		{
			SliderContainer newSlider = this.sliderPool.GetFreeElement(this.sliderGroup, true);
			this.sliderBusMap.Add(newSlider.slider, keyValuePair.Key);
			newSlider.slider.value = keyValuePair.Value.busLevel;
			newSlider.nameLabel.text = keyValuePair.Value.labelString;
			newSlider.UpdateSliderLabel(keyValuePair.Value.busLevel);
			newSlider.slider.ClearReleaseHandleEvent();
			newSlider.slider.onValueChanged.AddListener(delegate(float value)
			{
				this.OnReleaseHandle(newSlider.slider);
			});
			if (keyValuePair.Key == "Master")
			{
				newSlider.transform.SetSiblingIndex(2);
				newSlider.slider.onValueChanged.AddListener(new UnityAction<float>(this.CheckMasterValue));
				this.CheckMasterValue(keyValuePair.Value.busLevel);
			}
		}
		HierarchyReferences component = this.alwaysPlayMusicButton.GetComponent<HierarchyReferences>();
		GameObject gameObject = component.GetReference("Button").gameObject;
		gameObject.GetComponent<ToolTip>().SetSimpleTooltip(UI.FRONTEND.AUDIO_OPTIONS_SCREEN.MUSIC_EVERY_CYCLE_TOOLTIP);
		component.GetReference("CheckMark").gameObject.SetActive(MusicManager.instance.alwaysPlayMusic);
		gameObject.GetComponent<KButton>().onClick += delegate
		{
			this.ToggleAlwaysPlayMusic();
		};
		component.GetReference<LocText>("Label").SetText(UI.FRONTEND.AUDIO_OPTIONS_SCREEN.MUSIC_EVERY_CYCLE);
		if (!KPlayerPrefs.HasKey(AudioOptionsScreen.AlwaysPlayAutomation))
		{
			KPlayerPrefs.SetInt(AudioOptionsScreen.AlwaysPlayAutomation, 1);
		}
		HierarchyReferences component2 = this.alwaysPlayAutomationButton.GetComponent<HierarchyReferences>();
		GameObject gameObject2 = component2.GetReference("Button").gameObject;
		gameObject2.GetComponent<ToolTip>().SetSimpleTooltip(UI.FRONTEND.AUDIO_OPTIONS_SCREEN.AUTOMATION_SOUNDS_ALWAYS_TOOLTIP);
		gameObject2.GetComponent<KButton>().onClick += delegate
		{
			this.ToggleAlwaysPlayAutomation();
		};
		component2.GetReference<LocText>("Label").SetText(UI.FRONTEND.AUDIO_OPTIONS_SCREEN.AUTOMATION_SOUNDS_ALWAYS);
		component2.GetReference("CheckMark").gameObject.SetActive(KPlayerPrefs.GetInt(AudioOptionsScreen.AlwaysPlayAutomation) == 1);
		if (!KPlayerPrefs.HasKey(AudioOptionsScreen.MuteOnFocusLost))
		{
			KPlayerPrefs.SetInt(AudioOptionsScreen.MuteOnFocusLost, 0);
		}
		HierarchyReferences component3 = this.muteOnFocusLostToggle.GetComponent<HierarchyReferences>();
		GameObject gameObject3 = component3.GetReference("Button").gameObject;
		gameObject3.GetComponent<ToolTip>().SetSimpleTooltip(UI.FRONTEND.AUDIO_OPTIONS_SCREEN.MUTE_ON_FOCUS_LOST_TOOLTIP);
		gameObject3.GetComponent<KButton>().onClick += delegate
		{
			this.ToggleMuteOnFocusLost();
		};
		component3.GetReference<LocText>("Label").SetText(UI.FRONTEND.AUDIO_OPTIONS_SCREEN.MUTE_ON_FOCUS_LOST);
		component3.GetReference("CheckMark").gameObject.SetActive(KPlayerPrefs.GetInt(AudioOptionsScreen.MuteOnFocusLost) == 1);
	}

	// Token: 0x06004FD5 RID: 20437 RVA: 0x001C75E0 File Offset: 0x001C57E0
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06004FD6 RID: 20438 RVA: 0x001C7602 File Offset: 0x001C5802
	private void CheckMasterValue(float value)
	{
		this.jambell.enabled = value == 0f;
	}

	// Token: 0x06004FD7 RID: 20439 RVA: 0x001C7617 File Offset: 0x001C5817
	private void OnReleaseHandle(KSlider slider)
	{
		AudioMixer.instance.SetUserVolume(this.sliderBusMap[slider], slider.value);
	}

	// Token: 0x06004FD8 RID: 20440 RVA: 0x001C7638 File Offset: 0x001C5838
	private void ToggleAlwaysPlayMusic()
	{
		MusicManager.instance.alwaysPlayMusic = !MusicManager.instance.alwaysPlayMusic;
		this.alwaysPlayMusicButton.GetComponent<HierarchyReferences>().GetReference("CheckMark").gameObject.SetActive(MusicManager.instance.alwaysPlayMusic);
		KPlayerPrefs.SetInt(AudioOptionsScreen.AlwaysPlayMusicKey, MusicManager.instance.alwaysPlayMusic ? 1 : 0);
	}

	// Token: 0x06004FD9 RID: 20441 RVA: 0x001C76A0 File Offset: 0x001C58A0
	private void ToggleAlwaysPlayAutomation()
	{
		KPlayerPrefs.SetInt(AudioOptionsScreen.AlwaysPlayAutomation, (KPlayerPrefs.GetInt(AudioOptionsScreen.AlwaysPlayAutomation) == 1) ? 0 : 1);
		this.alwaysPlayAutomationButton.GetComponent<HierarchyReferences>().GetReference("CheckMark").gameObject.SetActive(KPlayerPrefs.GetInt(AudioOptionsScreen.AlwaysPlayAutomation) == 1);
	}

	// Token: 0x06004FDA RID: 20442 RVA: 0x001C76F8 File Offset: 0x001C58F8
	private void ToggleMuteOnFocusLost()
	{
		KPlayerPrefs.SetInt(AudioOptionsScreen.MuteOnFocusLost, (KPlayerPrefs.GetInt(AudioOptionsScreen.MuteOnFocusLost) == 1) ? 0 : 1);
		this.muteOnFocusLostToggle.GetComponent<HierarchyReferences>().GetReference("CheckMark").gameObject.SetActive(KPlayerPrefs.GetInt(AudioOptionsScreen.MuteOnFocusLost) == 1);
	}

	// Token: 0x06004FDB RID: 20443 RVA: 0x001C7750 File Offset: 0x001C5950
	private void BuildAudioDeviceList()
	{
		this.audioDevices.Clear();
		this.audioDeviceOptions.Clear();
		int num;
		RuntimeManager.CoreSystem.getNumDrivers(out num);
		for (int i = 0; i < num; i++)
		{
			KFMOD.AudioDevice audioDevice = default(KFMOD.AudioDevice);
			string text;
			RuntimeManager.CoreSystem.getDriverInfo(i, out text, 64, out audioDevice.guid, out audioDevice.systemRate, out audioDevice.speakerMode, out audioDevice.speakerModeChannels);
			audioDevice.name = text;
			audioDevice.fmod_id = i;
			this.audioDevices.Add(audioDevice);
			this.audioDeviceOptions.Add(new Dropdown.OptionData(audioDevice.name));
		}
	}

	// Token: 0x06004FDC RID: 20444 RVA: 0x001C77FC File Offset: 0x001C59FC
	private void OnAudioDeviceChanged(int idx)
	{
		RuntimeManager.CoreSystem.setDriver(idx);
		for (int i = 0; i < this.audioDevices.Count; i++)
		{
			if (idx == this.audioDevices[i].fmod_id)
			{
				KFMOD.currentDevice = this.audioDevices[i];
				KPlayerPrefs.SetString("AudioDeviceGuid", KFMOD.currentDevice.guid.ToString());
				return;
			}
		}
	}

	// Token: 0x06004FDD RID: 20445 RVA: 0x001C7873 File Offset: 0x001C5A73
	private void OnClose(GameObject go)
	{
		this.alwaysPlayMusicMetric[AudioOptionsScreen.AlwaysPlayMusicKey] = MusicManager.instance.alwaysPlayMusic;
		ThreadedHttps<KleiMetrics>.Instance.SendEvent(this.alwaysPlayMusicMetric, "AudioOptionsScreen");
		UnityEngine.Object.Destroy(go);
	}

	// Token: 0x04003589 RID: 13705
	[SerializeField]
	private KButton closeButton;

	// Token: 0x0400358A RID: 13706
	[SerializeField]
	private KButton doneButton;

	// Token: 0x0400358B RID: 13707
	[SerializeField]
	private SliderContainer sliderPrefab;

	// Token: 0x0400358C RID: 13708
	[SerializeField]
	private GameObject sliderGroup;

	// Token: 0x0400358D RID: 13709
	[SerializeField]
	private Image jambell;

	// Token: 0x0400358E RID: 13710
	[SerializeField]
	private GameObject alwaysPlayMusicButton;

	// Token: 0x0400358F RID: 13711
	[SerializeField]
	private GameObject alwaysPlayAutomationButton;

	// Token: 0x04003590 RID: 13712
	[SerializeField]
	private GameObject muteOnFocusLostToggle;

	// Token: 0x04003591 RID: 13713
	[SerializeField]
	private Dropdown deviceDropdown;

	// Token: 0x04003592 RID: 13714
	private UIPool<SliderContainer> sliderPool;

	// Token: 0x04003593 RID: 13715
	private Dictionary<KSlider, string> sliderBusMap = new Dictionary<KSlider, string>();

	// Token: 0x04003594 RID: 13716
	public static readonly string AlwaysPlayMusicKey = "AlwaysPlayMusic";

	// Token: 0x04003595 RID: 13717
	public static readonly string AlwaysPlayAutomation = "AlwaysPlayAutomation";

	// Token: 0x04003596 RID: 13718
	public static readonly string MuteOnFocusLost = "MuteOnFocusLost";

	// Token: 0x04003597 RID: 13719
	private Dictionary<string, object> alwaysPlayMusicMetric = new Dictionary<string, object> { 
	{
		AudioOptionsScreen.AlwaysPlayMusicKey,
		null
	} };

	// Token: 0x04003598 RID: 13720
	private List<KFMOD.AudioDevice> audioDevices = new List<KFMOD.AudioDevice>();

	// Token: 0x04003599 RID: 13721
	private List<Dropdown.OptionData> audioDeviceOptions = new List<Dropdown.OptionData>();
}
