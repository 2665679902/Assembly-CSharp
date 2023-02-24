using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B78 RID: 2936
[Serializable]
public class SaveConfigurationScreen
{
	// Token: 0x06005C48 RID: 23624 RVA: 0x0021C268 File Offset: 0x0021A468
	public void ToggleDisabledContent(bool enable)
	{
		if (enable)
		{
			this.disabledContentPanel.SetActive(true);
			this.disabledContentWarning.SetActive(false);
			this.perSaveWarning.SetActive(true);
			return;
		}
		this.disabledContentPanel.SetActive(false);
		this.disabledContentWarning.SetActive(true);
		this.perSaveWarning.SetActive(false);
	}

	// Token: 0x06005C49 RID: 23625 RVA: 0x0021C2C4 File Offset: 0x0021A4C4
	public void Init()
	{
		this.autosaveFrequencySlider.minValue = 0f;
		this.autosaveFrequencySlider.maxValue = (float)(this.sliderValueToCycleCount.Length - 1);
		this.autosaveFrequencySlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnAutosaveValueChanged(Mathf.FloorToInt(val));
		});
		this.autosaveFrequencySlider.value = (float)this.CycleCountToSlider(SaveGame.Instance.AutoSaveCycleInterval);
		this.timelapseResolutionSlider.minValue = 0f;
		this.timelapseResolutionSlider.maxValue = (float)(this.sliderValueToResolution.Length - 1);
		this.timelapseResolutionSlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnTimelapseValueChanged(Mathf.FloorToInt(val));
		});
		this.timelapseResolutionSlider.value = (float)this.ResolutionToSliderValue(SaveGame.Instance.TimelapseResolution);
		this.OnTimelapseValueChanged(Mathf.FloorToInt(this.timelapseResolutionSlider.value));
	}

	// Token: 0x06005C4A RID: 23626 RVA: 0x0021C3A4 File Offset: 0x0021A5A4
	public void Show(bool show)
	{
		if (show)
		{
			this.autosaveFrequencySlider.value = (float)this.CycleCountToSlider(SaveGame.Instance.AutoSaveCycleInterval);
			this.timelapseResolutionSlider.value = (float)this.ResolutionToSliderValue(SaveGame.Instance.TimelapseResolution);
			this.OnAutosaveValueChanged(Mathf.FloorToInt(this.autosaveFrequencySlider.value));
			this.OnTimelapseValueChanged(Mathf.FloorToInt(this.timelapseResolutionSlider.value));
		}
	}

	// Token: 0x06005C4B RID: 23627 RVA: 0x0021C418 File Offset: 0x0021A618
	private void OnTimelapseValueChanged(int sliderValue)
	{
		Vector2I vector2I = this.SliderValueToResolution(sliderValue);
		if (vector2I.x <= 0)
		{
			this.timelapseDescriptionLabel.SetText(UI.FRONTEND.COLONY_SAVE_OPTIONS_SCREEN.TIMELAPSE_DISABLED_DESCRIPTION);
		}
		else
		{
			this.timelapseDescriptionLabel.SetText(string.Format(UI.FRONTEND.COLONY_SAVE_OPTIONS_SCREEN.TIMELAPSE_RESOLUTION_DESCRIPTION, vector2I.x, vector2I.y));
		}
		SaveGame.Instance.TimelapseResolution = vector2I;
		Game.Instance.Trigger(75424175, null);
	}

	// Token: 0x06005C4C RID: 23628 RVA: 0x0021C498 File Offset: 0x0021A698
	private void OnAutosaveValueChanged(int sliderValue)
	{
		int num = this.SliderValueToCycleCount(sliderValue);
		if (sliderValue == 0)
		{
			this.autosaveDescriptionLabel.SetText(UI.FRONTEND.COLONY_SAVE_OPTIONS_SCREEN.AUTOSAVE_NEVER);
		}
		else
		{
			this.autosaveDescriptionLabel.SetText(string.Format(UI.FRONTEND.COLONY_SAVE_OPTIONS_SCREEN.AUTOSAVE_FREQUENCY_DESCRIPTION, num));
		}
		SaveGame.Instance.AutoSaveCycleInterval = num;
	}

	// Token: 0x06005C4D RID: 23629 RVA: 0x0021C4F2 File Offset: 0x0021A6F2
	private int SliderValueToCycleCount(int sliderValue)
	{
		return this.sliderValueToCycleCount[sliderValue];
	}

	// Token: 0x06005C4E RID: 23630 RVA: 0x0021C4FC File Offset: 0x0021A6FC
	private int CycleCountToSlider(int count)
	{
		for (int i = 0; i < this.sliderValueToCycleCount.Length; i++)
		{
			if (this.sliderValueToCycleCount[i] == count)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06005C4F RID: 23631 RVA: 0x0021C52A File Offset: 0x0021A72A
	private Vector2I SliderValueToResolution(int sliderValue)
	{
		return this.sliderValueToResolution[sliderValue];
	}

	// Token: 0x06005C50 RID: 23632 RVA: 0x0021C538 File Offset: 0x0021A738
	private int ResolutionToSliderValue(Vector2I resolution)
	{
		for (int i = 0; i < this.sliderValueToResolution.Length; i++)
		{
			if (this.sliderValueToResolution[i] == resolution)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x04003F07 RID: 16135
	[SerializeField]
	private KSlider autosaveFrequencySlider;

	// Token: 0x04003F08 RID: 16136
	[SerializeField]
	private LocText timelapseDescriptionLabel;

	// Token: 0x04003F09 RID: 16137
	[SerializeField]
	private KSlider timelapseResolutionSlider;

	// Token: 0x04003F0A RID: 16138
	[SerializeField]
	private LocText autosaveDescriptionLabel;

	// Token: 0x04003F0B RID: 16139
	private int[] sliderValueToCycleCount = new int[] { -1, 50, 20, 10, 5, 2, 1 };

	// Token: 0x04003F0C RID: 16140
	private Vector2I[] sliderValueToResolution = new Vector2I[]
	{
		new Vector2I(-1, -1),
		new Vector2I(256, 384),
		new Vector2I(512, 768),
		new Vector2I(1024, 1536),
		new Vector2I(2048, 3072),
		new Vector2I(4096, 6144),
		new Vector2I(8192, 12288)
	};

	// Token: 0x04003F0D RID: 16141
	[SerializeField]
	private GameObject disabledContentPanel;

	// Token: 0x04003F0E RID: 16142
	[SerializeField]
	private GameObject disabledContentWarning;

	// Token: 0x04003F0F RID: 16143
	[SerializeField]
	private GameObject perSaveWarning;
}
