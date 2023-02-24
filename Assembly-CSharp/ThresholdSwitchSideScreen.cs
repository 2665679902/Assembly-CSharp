using System;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000BEB RID: 3051
public class ThresholdSwitchSideScreen : SideScreenContent, IRender200ms
{
	// Token: 0x0600601D RID: 24605 RVA: 0x00232844 File Offset: 0x00230A44
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.aboveToggle.onClick += delegate
		{
			this.OnConditionButtonClicked(true);
		};
		this.belowToggle.onClick += delegate
		{
			this.OnConditionButtonClicked(false);
		};
		LocText component = this.aboveToggle.transform.GetChild(0).GetComponent<LocText>();
		TMP_Text component2 = this.belowToggle.transform.GetChild(0).GetComponent<LocText>();
		component.SetText(UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.ABOVE_BUTTON);
		component2.SetText(UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.BELOW_BUTTON);
		this.thresholdSlider.onDrag += delegate
		{
			this.ReceiveValueFromSlider(this.thresholdSlider.GetValueForPercentage(GameUtil.GetRoundedTemperatureInKelvin(this.thresholdSlider.value)));
		};
		this.thresholdSlider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider(this.thresholdSlider.GetValueForPercentage(GameUtil.GetRoundedTemperatureInKelvin(this.thresholdSlider.value)));
		};
		this.thresholdSlider.onMove += delegate
		{
			this.ReceiveValueFromSlider(this.thresholdSlider.GetValueForPercentage(GameUtil.GetRoundedTemperatureInKelvin(this.thresholdSlider.value)));
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput(this.numberInput.currentValue);
		};
		this.numberInput.decimalPlaces = 1;
	}

	// Token: 0x0600601E RID: 24606 RVA: 0x00232939 File Offset: 0x00230B39
	public void Render200ms(float dt)
	{
		if (this.target == null)
		{
			this.target = null;
			return;
		}
		this.UpdateLabels();
	}

	// Token: 0x0600601F RID: 24607 RVA: 0x00232957 File Offset: 0x00230B57
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IThresholdSwitch>() != null;
	}

	// Token: 0x06006020 RID: 24608 RVA: 0x00232964 File Offset: 0x00230B64
	public override void SetTarget(GameObject new_target)
	{
		this.target = null;
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target;
		this.thresholdSwitch = this.target.GetComponent<IThresholdSwitch>();
		if (this.thresholdSwitch == null)
		{
			this.target = null;
			global::Debug.LogError("The gameObject received does not contain a IThresholdSwitch component");
			return;
		}
		this.UpdateLabels();
		if (this.target.GetComponent<IThresholdSwitch>().LayoutType == ThresholdScreenLayoutType.SliderBar)
		{
			this.thresholdSlider.gameObject.SetActive(true);
			this.thresholdSlider.minValue = 0f;
			this.thresholdSlider.maxValue = 100f;
			this.thresholdSlider.SetRanges(this.thresholdSwitch.GetRanges);
			this.thresholdSlider.value = this.thresholdSlider.GetPercentageFromValue(this.thresholdSwitch.Threshold);
			this.thresholdSlider.GetComponentInChildren<ToolTip>();
		}
		else
		{
			this.thresholdSlider.gameObject.SetActive(false);
		}
		MultiToggle incrementMinorToggle = this.incrementMinor.GetComponent<MultiToggle>();
		incrementMinorToggle.onClick = delegate
		{
			this.UpdateThresholdValue(this.thresholdSwitch.Threshold + (float)this.thresholdSwitch.IncrementScale);
			incrementMinorToggle.ChangeState(1);
		};
		incrementMinorToggle.onStopHold = delegate
		{
			incrementMinorToggle.ChangeState(0);
		};
		MultiToggle incrementMajorToggle = this.incrementMajor.GetComponent<MultiToggle>();
		incrementMajorToggle.onClick = delegate
		{
			this.UpdateThresholdValue(this.thresholdSwitch.Threshold + 10f * (float)this.thresholdSwitch.IncrementScale);
			incrementMajorToggle.ChangeState(1);
		};
		incrementMajorToggle.onStopHold = delegate
		{
			incrementMajorToggle.ChangeState(0);
		};
		MultiToggle decrementMinorToggle = this.decrementMinor.GetComponent<MultiToggle>();
		decrementMinorToggle.onClick = delegate
		{
			this.UpdateThresholdValue(this.thresholdSwitch.Threshold - (float)this.thresholdSwitch.IncrementScale);
			decrementMinorToggle.ChangeState(1);
		};
		decrementMinorToggle.onStopHold = delegate
		{
			decrementMinorToggle.ChangeState(0);
		};
		MultiToggle decrementMajorToggle = this.decrementMajor.GetComponent<MultiToggle>();
		decrementMajorToggle.onClick = delegate
		{
			this.UpdateThresholdValue(this.thresholdSwitch.Threshold - 10f * (float)this.thresholdSwitch.IncrementScale);
			decrementMajorToggle.ChangeState(1);
		};
		decrementMajorToggle.onStopHold = delegate
		{
			decrementMajorToggle.ChangeState(0);
		};
		this.unitsLabel.text = this.thresholdSwitch.ThresholdValueUnits();
		this.numberInput.minValue = this.thresholdSwitch.GetRangeMinInputField();
		this.numberInput.maxValue = this.thresholdSwitch.GetRangeMaxInputField();
		this.numberInput.Activate();
		this.UpdateTargetThresholdLabel();
		this.OnConditionButtonClicked(this.thresholdSwitch.ActivateAboveThreshold);
	}

	// Token: 0x06006021 RID: 24609 RVA: 0x00232BCF File Offset: 0x00230DCF
	private void OnThresholdValueChanged(float new_value)
	{
		this.thresholdSwitch.Threshold = new_value;
		this.UpdateTargetThresholdLabel();
	}

	// Token: 0x06006022 RID: 24610 RVA: 0x00232BE4 File Offset: 0x00230DE4
	private void OnConditionButtonClicked(bool activate_above_threshold)
	{
		this.thresholdSwitch.ActivateAboveThreshold = activate_above_threshold;
		if (activate_above_threshold)
		{
			this.belowToggle.isOn = true;
			this.aboveToggle.isOn = false;
			this.belowToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Inactive);
			this.aboveToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Active);
		}
		else
		{
			this.belowToggle.isOn = false;
			this.aboveToggle.isOn = true;
			this.belowToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Active);
			this.aboveToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Inactive);
		}
		this.UpdateTargetThresholdLabel();
	}

	// Token: 0x06006023 RID: 24611 RVA: 0x00232C7C File Offset: 0x00230E7C
	private void UpdateTargetThresholdLabel()
	{
		this.numberInput.SetDisplayValue(this.thresholdSwitch.Format(this.thresholdSwitch.Threshold, false) + this.thresholdSwitch.ThresholdValueUnits());
		if (this.thresholdSwitch.ActivateAboveThreshold)
		{
			this.thresholdSlider.GetComponentInChildren<ToolTip>().SetSimpleTooltip(string.Format(this.thresholdSwitch.AboveToolTip, this.thresholdSwitch.Format(this.thresholdSwitch.Threshold, true)));
			this.thresholdSlider.GetComponentInChildren<ToolTip>().tooltipPositionOffset = new Vector2(0f, 25f);
			return;
		}
		this.thresholdSlider.GetComponentInChildren<ToolTip>().SetSimpleTooltip(string.Format(this.thresholdSwitch.BelowToolTip, this.thresholdSwitch.Format(this.thresholdSwitch.Threshold, true)));
		this.thresholdSlider.GetComponentInChildren<ToolTip>().tooltipPositionOffset = new Vector2(0f, 25f);
	}

	// Token: 0x06006024 RID: 24612 RVA: 0x00232D7A File Offset: 0x00230F7A
	private void ReceiveValueFromSlider(float newValue)
	{
		this.UpdateThresholdValue(this.thresholdSwitch.ProcessedSliderValue(newValue));
	}

	// Token: 0x06006025 RID: 24613 RVA: 0x00232D8E File Offset: 0x00230F8E
	private void ReceiveValueFromInput(float newValue)
	{
		this.UpdateThresholdValue(this.thresholdSwitch.ProcessedInputValue(newValue));
	}

	// Token: 0x06006026 RID: 24614 RVA: 0x00232DA4 File Offset: 0x00230FA4
	private void UpdateThresholdValue(float newValue)
	{
		if (newValue < this.thresholdSwitch.RangeMin)
		{
			newValue = this.thresholdSwitch.RangeMin;
		}
		if (newValue > this.thresholdSwitch.RangeMax)
		{
			newValue = this.thresholdSwitch.RangeMax;
		}
		this.thresholdSwitch.Threshold = newValue;
		NonLinearSlider nonLinearSlider = this.thresholdSlider;
		if (nonLinearSlider != null)
		{
			this.thresholdSlider.value = nonLinearSlider.GetPercentageFromValue(newValue);
		}
		else
		{
			this.thresholdSlider.value = newValue;
		}
		this.UpdateTargetThresholdLabel();
	}

	// Token: 0x06006027 RID: 24615 RVA: 0x00232E29 File Offset: 0x00231029
	private void UpdateLabels()
	{
		this.currentValue.text = string.Format(UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.CURRENT_VALUE, this.thresholdSwitch.ThresholdValueName, this.thresholdSwitch.Format(this.thresholdSwitch.CurrentValue, true));
	}

	// Token: 0x06006028 RID: 24616 RVA: 0x00232E67 File Offset: 0x00231067
	public override string GetTitle()
	{
		if (this.target != null)
		{
			return this.thresholdSwitch.Title;
		}
		return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
	}

	// Token: 0x040041D0 RID: 16848
	private GameObject target;

	// Token: 0x040041D1 RID: 16849
	private IThresholdSwitch thresholdSwitch;

	// Token: 0x040041D2 RID: 16850
	[SerializeField]
	private LocText currentValue;

	// Token: 0x040041D3 RID: 16851
	[SerializeField]
	private LocText tresholdValue;

	// Token: 0x040041D4 RID: 16852
	[SerializeField]
	private KToggle aboveToggle;

	// Token: 0x040041D5 RID: 16853
	[SerializeField]
	private KToggle belowToggle;

	// Token: 0x040041D6 RID: 16854
	[Header("Slider")]
	[SerializeField]
	private NonLinearSlider thresholdSlider;

	// Token: 0x040041D7 RID: 16855
	[Header("Number Input")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x040041D8 RID: 16856
	[SerializeField]
	private LocText unitsLabel;

	// Token: 0x040041D9 RID: 16857
	[Header("Increment Buttons")]
	[SerializeField]
	private GameObject incrementMinor;

	// Token: 0x040041DA RID: 16858
	[SerializeField]
	private GameObject incrementMajor;

	// Token: 0x040041DB RID: 16859
	[SerializeField]
	private GameObject decrementMinor;

	// Token: 0x040041DC RID: 16860
	[SerializeField]
	private GameObject decrementMajor;
}
