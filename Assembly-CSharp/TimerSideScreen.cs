using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BED RID: 3053
public class TimerSideScreen : SideScreenContent, IRenderEveryTick
{
	// Token: 0x0600603B RID: 24635 RVA: 0x00233237 File Offset: 0x00231437
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.labelHeaderOnDuration.text = UI.UISIDESCREENS.TIMER_SIDE_SCREEN.ON;
		this.labelHeaderOffDuration.text = UI.UISIDESCREENS.TIMER_SIDE_SCREEN.OFF;
	}

	// Token: 0x0600603C RID: 24636 RVA: 0x0023326C File Offset: 0x0023146C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.modeButton.onClick += delegate
		{
			this.ToggleMode();
		};
		this.resetButton.onClick += this.ResetTimer;
		this.onDurationNumberInput.onEndEdit += delegate
		{
			this.UpdateDurationValueFromTextInput(this.onDurationNumberInput.currentValue, this.onDurationSlider);
		};
		this.offDurationNumberInput.onEndEdit += delegate
		{
			this.UpdateDurationValueFromTextInput(this.offDurationNumberInput.currentValue, this.offDurationSlider);
		};
		this.onDurationSlider.wholeNumbers = false;
		this.offDurationSlider.wholeNumbers = false;
	}

	// Token: 0x0600603D RID: 24637 RVA: 0x002332F3 File Offset: 0x002314F3
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicTimerSensor>() != null;
	}

	// Token: 0x0600603E RID: 24638 RVA: 0x00233304 File Offset: 0x00231504
	public override void SetTarget(GameObject target)
	{
		this.greenActiveZone.color = GlobalAssets.Instance.colorSet.logicOnSidescreen;
		this.redActiveZone.color = GlobalAssets.Instance.colorSet.logicOffSidescreen;
		base.SetTarget(target);
		this.targetTimedSwitch = target.GetComponent<LogicTimerSensor>();
		this.onDurationSlider.onValueChanged.RemoveAllListeners();
		this.offDurationSlider.onValueChanged.RemoveAllListeners();
		this.cyclesMode = this.targetTimedSwitch.displayCyclesMode;
		this.UpdateVisualsForNewTarget();
		this.ReconfigureRingVisuals();
		this.onDurationSlider.onValueChanged.AddListener(delegate(float value)
		{
			this.ChangeSetting();
		});
		this.offDurationSlider.onValueChanged.AddListener(delegate(float value)
		{
			this.ChangeSetting();
		});
	}

	// Token: 0x0600603F RID: 24639 RVA: 0x002333D8 File Offset: 0x002315D8
	private void UpdateVisualsForNewTarget()
	{
		float onDuration = this.targetTimedSwitch.onDuration;
		float offDuration = this.targetTimedSwitch.offDuration;
		bool displayCyclesMode = this.targetTimedSwitch.displayCyclesMode;
		if (displayCyclesMode)
		{
			this.onDurationSlider.minValue = this.minCycles;
			this.onDurationNumberInput.minValue = this.onDurationSlider.minValue;
			this.onDurationSlider.maxValue = this.maxCycles;
			this.onDurationNumberInput.maxValue = this.onDurationSlider.maxValue;
			this.onDurationNumberInput.decimalPlaces = 2;
			this.offDurationSlider.minValue = this.minCycles;
			this.offDurationNumberInput.minValue = this.offDurationSlider.minValue;
			this.offDurationSlider.maxValue = this.maxCycles;
			this.offDurationNumberInput.maxValue = this.offDurationSlider.maxValue;
			this.offDurationNumberInput.decimalPlaces = 2;
			this.onDurationSlider.value = onDuration / 600f;
			this.offDurationSlider.value = offDuration / 600f;
			this.onDurationNumberInput.SetAmount(onDuration / 600f);
			this.offDurationNumberInput.SetAmount(offDuration / 600f);
		}
		else
		{
			this.onDurationSlider.minValue = this.minSeconds;
			this.onDurationNumberInput.minValue = this.onDurationSlider.minValue;
			this.onDurationSlider.maxValue = this.maxSeconds;
			this.onDurationNumberInput.maxValue = this.onDurationSlider.maxValue;
			this.onDurationNumberInput.decimalPlaces = 1;
			this.offDurationSlider.minValue = this.minSeconds;
			this.offDurationNumberInput.minValue = this.offDurationSlider.minValue;
			this.offDurationSlider.maxValue = this.maxSeconds;
			this.offDurationNumberInput.maxValue = this.offDurationSlider.maxValue;
			this.offDurationNumberInput.decimalPlaces = 1;
			this.onDurationSlider.value = onDuration;
			this.offDurationSlider.value = offDuration;
			this.onDurationNumberInput.SetAmount(onDuration);
			this.offDurationNumberInput.SetAmount(offDuration);
		}
		this.modeButton.GetComponentInChildren<LocText>().text = (displayCyclesMode ? UI.UISIDESCREENS.TIMER_SIDE_SCREEN.MODE_LABEL_CYCLES : UI.UISIDESCREENS.TIMER_SIDE_SCREEN.MODE_LABEL_SECONDS);
	}

	// Token: 0x06006040 RID: 24640 RVA: 0x00233618 File Offset: 0x00231818
	private void ToggleMode()
	{
		this.cyclesMode = !this.cyclesMode;
		this.targetTimedSwitch.displayCyclesMode = this.cyclesMode;
		float num = this.onDurationSlider.value;
		float num2 = this.offDurationSlider.value;
		if (this.cyclesMode)
		{
			num = this.onDurationSlider.value / 600f;
			num2 = this.offDurationSlider.value / 600f;
		}
		else
		{
			num = this.onDurationSlider.value * 600f;
			num2 = this.offDurationSlider.value * 600f;
		}
		this.onDurationSlider.minValue = (this.cyclesMode ? this.minCycles : this.minSeconds);
		this.onDurationNumberInput.minValue = this.onDurationSlider.minValue;
		this.onDurationSlider.maxValue = (this.cyclesMode ? this.maxCycles : this.maxSeconds);
		this.onDurationNumberInput.maxValue = this.onDurationSlider.maxValue;
		this.onDurationNumberInput.decimalPlaces = (this.cyclesMode ? 2 : 1);
		this.offDurationSlider.minValue = (this.cyclesMode ? this.minCycles : this.minSeconds);
		this.offDurationNumberInput.minValue = this.offDurationSlider.minValue;
		this.offDurationSlider.maxValue = (this.cyclesMode ? this.maxCycles : this.maxSeconds);
		this.offDurationNumberInput.maxValue = this.offDurationSlider.maxValue;
		this.offDurationNumberInput.decimalPlaces = (this.cyclesMode ? 2 : 1);
		this.onDurationSlider.value = num;
		this.offDurationSlider.value = num2;
		this.onDurationNumberInput.SetAmount(num);
		this.offDurationNumberInput.SetAmount(num2);
		this.modeButton.GetComponentInChildren<LocText>().text = (this.cyclesMode ? UI.UISIDESCREENS.TIMER_SIDE_SCREEN.MODE_LABEL_CYCLES : UI.UISIDESCREENS.TIMER_SIDE_SCREEN.MODE_LABEL_SECONDS);
	}

	// Token: 0x06006041 RID: 24641 RVA: 0x00233814 File Offset: 0x00231A14
	private void ChangeSetting()
	{
		this.targetTimedSwitch.onDuration = (this.cyclesMode ? (this.onDurationSlider.value * 600f) : this.onDurationSlider.value);
		this.targetTimedSwitch.offDuration = (this.cyclesMode ? (this.offDurationSlider.value * 600f) : this.offDurationSlider.value);
		this.ReconfigureRingVisuals();
		this.onDurationNumberInput.SetDisplayValue(this.cyclesMode ? (this.targetTimedSwitch.onDuration / 600f).ToString("F2") : this.targetTimedSwitch.onDuration.ToString());
		this.offDurationNumberInput.SetDisplayValue(this.cyclesMode ? (this.targetTimedSwitch.offDuration / 600f).ToString("F2") : this.targetTimedSwitch.offDuration.ToString());
		this.onDurationSlider.SetTooltipText(string.Format(UI.UISIDESCREENS.TIMER_SIDE_SCREEN.GREEN_DURATION_TOOLTIP, this.cyclesMode ? GameUtil.GetFormattedCycles(this.targetTimedSwitch.onDuration, "F2", false) : GameUtil.GetFormattedTime(this.targetTimedSwitch.onDuration, "F0")));
		this.offDurationSlider.SetTooltipText(string.Format(UI.UISIDESCREENS.TIMER_SIDE_SCREEN.RED_DURATION_TOOLTIP, this.cyclesMode ? GameUtil.GetFormattedCycles(this.targetTimedSwitch.offDuration, "F2", false) : GameUtil.GetFormattedTime(this.targetTimedSwitch.offDuration, "F0")));
		if (this.phaseLength == 0f)
		{
			this.timeLeft.text = UI.UISIDESCREENS.TIMER_SIDE_SCREEN.DISABLED;
			if (this.targetTimedSwitch.IsSwitchedOn)
			{
				this.greenActiveZone.fillAmount = 1f;
				this.redActiveZone.fillAmount = 0f;
			}
			else
			{
				this.greenActiveZone.fillAmount = 0f;
				this.redActiveZone.fillAmount = 1f;
			}
			this.targetTimedSwitch.timeElapsedInCurrentState = 0f;
			this.currentTimeMarker.rotation = Quaternion.identity;
			this.currentTimeMarker.Rotate(0f, 0f, 0f);
		}
	}

	// Token: 0x06006042 RID: 24642 RVA: 0x00233A5C File Offset: 0x00231C5C
	private void ReconfigureRingVisuals()
	{
		this.phaseLength = this.targetTimedSwitch.onDuration + this.targetTimedSwitch.offDuration;
		this.greenActiveZone.fillAmount = this.targetTimedSwitch.onDuration / this.phaseLength;
		this.redActiveZone.fillAmount = this.targetTimedSwitch.offDuration / this.phaseLength;
	}

	// Token: 0x06006043 RID: 24643 RVA: 0x00233AC0 File Offset: 0x00231CC0
	public void RenderEveryTick(float dt)
	{
		if (this.phaseLength == 0f)
		{
			return;
		}
		float timeElapsedInCurrentState = this.targetTimedSwitch.timeElapsedInCurrentState;
		if (this.cyclesMode)
		{
			this.timeLeft.text = string.Format(UI.UISIDESCREENS.TIMER_SIDE_SCREEN.CURRENT_TIME, GameUtil.GetFormattedCycles(timeElapsedInCurrentState, "F2", false), GameUtil.GetFormattedCycles(this.targetTimedSwitch.IsSwitchedOn ? this.targetTimedSwitch.onDuration : this.targetTimedSwitch.offDuration, "F2", false));
		}
		else
		{
			this.timeLeft.text = string.Format(UI.UISIDESCREENS.TIMER_SIDE_SCREEN.CURRENT_TIME, GameUtil.GetFormattedTime(timeElapsedInCurrentState, "F1"), GameUtil.GetFormattedTime(this.targetTimedSwitch.IsSwitchedOn ? this.targetTimedSwitch.onDuration : this.targetTimedSwitch.offDuration, "F1"));
		}
		this.currentTimeMarker.rotation = Quaternion.identity;
		if (this.targetTimedSwitch.IsSwitchedOn)
		{
			this.currentTimeMarker.Rotate(0f, 0f, this.targetTimedSwitch.timeElapsedInCurrentState / this.phaseLength * -360f);
			return;
		}
		this.currentTimeMarker.Rotate(0f, 0f, (this.targetTimedSwitch.onDuration + this.targetTimedSwitch.timeElapsedInCurrentState) / this.phaseLength * -360f);
	}

	// Token: 0x06006044 RID: 24644 RVA: 0x00233C20 File Offset: 0x00231E20
	private void UpdateDurationValueFromTextInput(float newValue, KSlider slider)
	{
		if (newValue < slider.minValue)
		{
			newValue = slider.minValue;
		}
		if (newValue > slider.maxValue)
		{
			newValue = slider.maxValue;
		}
		slider.value = newValue;
		NonLinearSlider nonLinearSlider = slider as NonLinearSlider;
		if (nonLinearSlider != null)
		{
			slider.value = nonLinearSlider.GetPercentageFromValue(newValue);
			return;
		}
		slider.value = newValue;
	}

	// Token: 0x06006045 RID: 24645 RVA: 0x00233C7B File Offset: 0x00231E7B
	private void ResetTimer()
	{
		this.targetTimedSwitch.ResetTimer();
	}

	// Token: 0x040041E8 RID: 16872
	public Image greenActiveZone;

	// Token: 0x040041E9 RID: 16873
	public Image redActiveZone;

	// Token: 0x040041EA RID: 16874
	private LogicTimerSensor targetTimedSwitch;

	// Token: 0x040041EB RID: 16875
	public KToggle modeButton;

	// Token: 0x040041EC RID: 16876
	public KButton resetButton;

	// Token: 0x040041ED RID: 16877
	public KSlider onDurationSlider;

	// Token: 0x040041EE RID: 16878
	[SerializeField]
	private KNumberInputField onDurationNumberInput;

	// Token: 0x040041EF RID: 16879
	public KSlider offDurationSlider;

	// Token: 0x040041F0 RID: 16880
	[SerializeField]
	private KNumberInputField offDurationNumberInput;

	// Token: 0x040041F1 RID: 16881
	public RectTransform endIndicator;

	// Token: 0x040041F2 RID: 16882
	public RectTransform currentTimeMarker;

	// Token: 0x040041F3 RID: 16883
	public LocText labelHeaderOnDuration;

	// Token: 0x040041F4 RID: 16884
	public LocText labelHeaderOffDuration;

	// Token: 0x040041F5 RID: 16885
	public LocText labelValueOnDuration;

	// Token: 0x040041F6 RID: 16886
	public LocText labelValueOffDuration;

	// Token: 0x040041F7 RID: 16887
	public LocText timeLeft;

	// Token: 0x040041F8 RID: 16888
	public float phaseLength;

	// Token: 0x040041F9 RID: 16889
	private bool cyclesMode;

	// Token: 0x040041FA RID: 16890
	[SerializeField]
	private float minSeconds;

	// Token: 0x040041FB RID: 16891
	[SerializeField]
	private float maxSeconds = 600f;

	// Token: 0x040041FC RID: 16892
	[SerializeField]
	private float minCycles;

	// Token: 0x040041FD RID: 16893
	[SerializeField]
	private float maxCycles = 10f;

	// Token: 0x040041FE RID: 16894
	private const int CYCLEMODE_DECIMALS = 2;

	// Token: 0x040041FF RID: 16895
	private const int SECONDSMODE_DECIMALS = 1;
}
