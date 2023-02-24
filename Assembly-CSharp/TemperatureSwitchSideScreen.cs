using System;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000BE9 RID: 3049
public class TemperatureSwitchSideScreen : SideScreenContent, IRender200ms
{
	// Token: 0x0600600B RID: 24587 RVA: 0x002324B4 File Offset: 0x002306B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.coolerToggle.onClick += delegate
		{
			this.OnConditionButtonClicked(false);
		};
		this.warmerToggle.onClick += delegate
		{
			this.OnConditionButtonClicked(true);
		};
		LocText component = this.coolerToggle.transform.GetChild(0).GetComponent<LocText>();
		TMP_Text component2 = this.warmerToggle.transform.GetChild(0).GetComponent<LocText>();
		component.SetText(UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.COLDER_BUTTON);
		component2.SetText(UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.WARMER_BUTTON);
		Slider.SliderEvent sliderEvent = new Slider.SliderEvent();
		sliderEvent.AddListener(new UnityAction<float>(this.OnTargetTemperatureChanged));
		this.targetTemperatureSlider.onValueChanged = sliderEvent;
	}

	// Token: 0x0600600C RID: 24588 RVA: 0x00232565 File Offset: 0x00230765
	public void Render200ms(float dt)
	{
		if (this.targetTemperatureSwitch == null)
		{
			return;
		}
		this.UpdateLabels();
	}

	// Token: 0x0600600D RID: 24589 RVA: 0x0023257C File Offset: 0x0023077C
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<TemperatureControlledSwitch>() != null;
	}

	// Token: 0x0600600E RID: 24590 RVA: 0x0023258C File Offset: 0x0023078C
	public override void SetTarget(GameObject target)
	{
		if (target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.targetTemperatureSwitch = target.GetComponent<TemperatureControlledSwitch>();
		if (this.targetTemperatureSwitch == null)
		{
			global::Debug.LogError("The gameObject received does not contain a TimedSwitch component");
			return;
		}
		this.UpdateLabels();
		this.UpdateTargetTemperatureLabel();
		this.OnConditionButtonClicked(this.targetTemperatureSwitch.activateOnWarmerThan);
	}

	// Token: 0x0600600F RID: 24591 RVA: 0x002325EF File Offset: 0x002307EF
	private void OnTargetTemperatureChanged(float new_value)
	{
		this.targetTemperatureSwitch.thresholdTemperature = new_value;
		this.UpdateTargetTemperatureLabel();
	}

	// Token: 0x06006010 RID: 24592 RVA: 0x00232604 File Offset: 0x00230804
	private void OnConditionButtonClicked(bool isWarmer)
	{
		this.targetTemperatureSwitch.activateOnWarmerThan = isWarmer;
		if (isWarmer)
		{
			this.coolerToggle.isOn = false;
			this.warmerToggle.isOn = true;
			this.coolerToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Inactive);
			this.warmerToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Active);
			return;
		}
		this.coolerToggle.isOn = true;
		this.warmerToggle.isOn = false;
		this.coolerToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Active);
		this.warmerToggle.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Inactive);
	}

	// Token: 0x06006011 RID: 24593 RVA: 0x00232695 File Offset: 0x00230895
	private void UpdateTargetTemperatureLabel()
	{
		this.targetTemperature.text = GameUtil.GetFormattedTemperature(this.targetTemperatureSwitch.thresholdTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false);
	}

	// Token: 0x06006012 RID: 24594 RVA: 0x002326B6 File Offset: 0x002308B6
	private void UpdateLabels()
	{
		this.currentTemperature.text = string.Format(UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.CURRENT_TEMPERATURE, GameUtil.GetFormattedTemperature(this.targetTemperatureSwitch.GetTemperature(), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
	}

	// Token: 0x040041C9 RID: 16841
	private TemperatureControlledSwitch targetTemperatureSwitch;

	// Token: 0x040041CA RID: 16842
	[SerializeField]
	private LocText currentTemperature;

	// Token: 0x040041CB RID: 16843
	[SerializeField]
	private LocText targetTemperature;

	// Token: 0x040041CC RID: 16844
	[SerializeField]
	private KToggle coolerToggle;

	// Token: 0x040041CD RID: 16845
	[SerializeField]
	private KToggle warmerToggle;

	// Token: 0x040041CE RID: 16846
	[SerializeField]
	private KSlider targetTemperatureSlider;
}
