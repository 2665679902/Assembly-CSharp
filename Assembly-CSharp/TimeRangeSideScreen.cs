using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BEC RID: 3052
public class TimeRangeSideScreen : SideScreenContent, IRender200ms
{
	// Token: 0x06006030 RID: 24624 RVA: 0x00232F28 File Offset: 0x00231128
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.labelHeaderStart.text = UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.ON;
		this.labelHeaderDuration.text = UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.DURATION;
	}

	// Token: 0x06006031 RID: 24625 RVA: 0x00232F5A File Offset: 0x0023115A
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicTimeOfDaySensor>() != null;
	}

	// Token: 0x06006032 RID: 24626 RVA: 0x00232F68 File Offset: 0x00231168
	public override void SetTarget(GameObject target)
	{
		this.imageActiveZone.color = GlobalAssets.Instance.colorSet.logicOnSidescreen;
		this.imageInactiveZone.color = GlobalAssets.Instance.colorSet.logicOffSidescreen;
		base.SetTarget(target);
		this.targetTimedSwitch = target.GetComponent<LogicTimeOfDaySensor>();
		this.duration.onValueChanged.RemoveAllListeners();
		this.startTime.onValueChanged.RemoveAllListeners();
		this.startTime.value = this.targetTimedSwitch.startTime;
		this.duration.value = this.targetTimedSwitch.duration;
		this.ChangeSetting();
		this.startTime.onValueChanged.AddListener(delegate(float value)
		{
			this.ChangeSetting();
		});
		this.duration.onValueChanged.AddListener(delegate(float value)
		{
			this.ChangeSetting();
		});
	}

	// Token: 0x06006033 RID: 24627 RVA: 0x00233050 File Offset: 0x00231250
	private void ChangeSetting()
	{
		this.targetTimedSwitch.startTime = this.startTime.value;
		this.targetTimedSwitch.duration = this.duration.value;
		this.imageActiveZone.rectTransform.rotation = Quaternion.identity;
		this.imageActiveZone.rectTransform.Rotate(0f, 0f, this.NormalizedValueToDegrees(this.startTime.value));
		this.imageActiveZone.fillAmount = this.duration.value;
		this.labelValueStart.text = GameUtil.GetFormattedPercent(this.targetTimedSwitch.startTime * 100f, GameUtil.TimeSlice.None);
		this.labelValueDuration.text = GameUtil.GetFormattedPercent(this.targetTimedSwitch.duration * 100f, GameUtil.TimeSlice.None);
		this.endIndicator.rotation = Quaternion.identity;
		this.endIndicator.Rotate(0f, 0f, this.NormalizedValueToDegrees(this.startTime.value + this.duration.value));
		this.startTime.SetTooltipText(string.Format(UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.ON_TOOLTIP, GameUtil.GetFormattedPercent(this.targetTimedSwitch.startTime * 100f, GameUtil.TimeSlice.None)));
		this.duration.SetTooltipText(string.Format(UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.DURATION_TOOLTIP, GameUtil.GetFormattedPercent(this.targetTimedSwitch.duration * 100f, GameUtil.TimeSlice.None)));
	}

	// Token: 0x06006034 RID: 24628 RVA: 0x002331C7 File Offset: 0x002313C7
	public void Render200ms(float dt)
	{
		this.currentTimeMarker.rotation = Quaternion.identity;
		this.currentTimeMarker.Rotate(0f, 0f, this.NormalizedValueToDegrees(GameClock.Instance.GetCurrentCycleAsPercentage()));
	}

	// Token: 0x06006035 RID: 24629 RVA: 0x002331FE File Offset: 0x002313FE
	private float NormalizedValueToDegrees(float value)
	{
		return 360f * value;
	}

	// Token: 0x06006036 RID: 24630 RVA: 0x00233207 File Offset: 0x00231407
	private float SecondsToDegrees(float seconds)
	{
		return 360f * (seconds / 600f);
	}

	// Token: 0x06006037 RID: 24631 RVA: 0x00233216 File Offset: 0x00231416
	private float DegreesToNormalizedValue(float degrees)
	{
		return degrees / 360f;
	}

	// Token: 0x040041DD RID: 16861
	public Image imageInactiveZone;

	// Token: 0x040041DE RID: 16862
	public Image imageActiveZone;

	// Token: 0x040041DF RID: 16863
	private LogicTimeOfDaySensor targetTimedSwitch;

	// Token: 0x040041E0 RID: 16864
	public KSlider startTime;

	// Token: 0x040041E1 RID: 16865
	public KSlider duration;

	// Token: 0x040041E2 RID: 16866
	public RectTransform endIndicator;

	// Token: 0x040041E3 RID: 16867
	public LocText labelHeaderStart;

	// Token: 0x040041E4 RID: 16868
	public LocText labelHeaderDuration;

	// Token: 0x040041E5 RID: 16869
	public LocText labelValueStart;

	// Token: 0x040041E6 RID: 16870
	public LocText labelValueDuration;

	// Token: 0x040041E7 RID: 16871
	public RectTransform currentTimeMarker;
}
