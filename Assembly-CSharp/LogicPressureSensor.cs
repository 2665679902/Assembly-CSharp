using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F3 RID: 1523
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicPressureSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x06002701 RID: 9985 RVA: 0x000D218F File Offset: 0x000D038F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicPressureSensor>(-905833192, LogicPressureSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x000D21A8 File Offset: 0x000D03A8
	private void OnCopySettings(object data)
	{
		LogicPressureSensor component = ((GameObject)data).GetComponent<LogicPressureSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x000D21E2 File Offset: 0x000D03E2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x000D2218 File Offset: 0x000D0418
	public void Sim200ms(float dt)
	{
		int num = Grid.PosToCell(this);
		if (this.sampleIdx < 8)
		{
			float num2 = (Grid.Element[num].IsState(this.desiredState) ? Grid.Mass[num] : 0f);
			this.samples[this.sampleIdx] = num2;
			this.sampleIdx++;
			return;
		}
		this.sampleIdx = 0;
		float currentValue = this.CurrentValue;
		if (this.activateAboveThreshold)
		{
			if ((currentValue > this.threshold && !base.IsSwitchedOn) || (currentValue <= this.threshold && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((currentValue > this.threshold && base.IsSwitchedOn) || (currentValue <= this.threshold && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x000D22E0 File Offset: 0x000D04E0
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06002706 RID: 9990 RVA: 0x000D22EF File Offset: 0x000D04EF
	// (set) Token: 0x06002707 RID: 9991 RVA: 0x000D22F7 File Offset: 0x000D04F7
	public float Threshold
	{
		get
		{
			return this.threshold;
		}
		set
		{
			this.threshold = value;
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06002708 RID: 9992 RVA: 0x000D2300 File Offset: 0x000D0500
	// (set) Token: 0x06002709 RID: 9993 RVA: 0x000D2308 File Offset: 0x000D0508
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateAboveThreshold;
		}
		set
		{
			this.activateAboveThreshold = value;
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x0600270A RID: 9994 RVA: 0x000D2314 File Offset: 0x000D0514
	public float CurrentValue
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < 8; i++)
			{
				num += this.samples[i];
			}
			return num / 8f;
		}
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x0600270B RID: 9995 RVA: 0x000D2345 File Offset: 0x000D0545
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x0600270C RID: 9996 RVA: 0x000D234D File Offset: 0x000D054D
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x000D2355 File Offset: 0x000D0555
	public float GetRangeMinInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMin;
		}
		return this.rangeMin * 1000f;
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x000D2373 File Offset: 0x000D0573
	public float GetRangeMaxInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMax;
		}
		return this.rangeMax * 1000f;
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x0600270F RID: 9999 RVA: 0x000D2391 File Offset: 0x000D0591
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06002710 RID: 10000 RVA: 0x000D2398 File Offset: 0x000D0598
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06002711 RID: 10001 RVA: 0x000D23A4 File Offset: 0x000D05A4
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x000D23B0 File Offset: 0x000D05B0
	public string Format(float value, bool units)
	{
		GameUtil.MetricMassFormat metricMassFormat;
		if (this.desiredState == Element.State.Gas)
		{
			metricMassFormat = GameUtil.MetricMassFormat.Gram;
		}
		else
		{
			metricMassFormat = GameUtil.MetricMassFormat.Kilogram;
		}
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, metricMassFormat, units, "{0:0.#}");
	}

	// Token: 0x06002713 RID: 10003 RVA: 0x000D23DC File Offset: 0x000D05DC
	public float ProcessedSliderValue(float input)
	{
		if (this.desiredState == Element.State.Gas)
		{
			input = Mathf.Round(input * 1000f) / 1000f;
		}
		else
		{
			input = Mathf.Round(input);
		}
		return input;
	}

	// Token: 0x06002714 RID: 10004 RVA: 0x000D2406 File Offset: 0x000D0606
	public float ProcessedInputValue(float input)
	{
		if (this.desiredState == Element.State.Gas)
		{
			input /= 1000f;
		}
		return input;
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x000D241B File Offset: 0x000D061B
	public LocString ThresholdValueUnits()
	{
		return GameUtil.GetCurrentMassUnit(this.desiredState == Element.State.Gas);
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06002716 RID: 10006 RVA: 0x000D242B File Offset: 0x000D062B
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06002717 RID: 10007 RVA: 0x000D2432 File Offset: 0x000D0632
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06002718 RID: 10008 RVA: 0x000D2435 File Offset: 0x000D0635
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06002719 RID: 10009 RVA: 0x000D2438 File Offset: 0x000D0638
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x000D2445 File Offset: 0x000D0645
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x000D2464 File Offset: 0x000D0664
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x000D24EC File Offset: 0x000D06EC
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001710 RID: 5904
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x04001711 RID: 5905
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x04001712 RID: 5906
	private bool wasOn;

	// Token: 0x04001713 RID: 5907
	public float rangeMin;

	// Token: 0x04001714 RID: 5908
	public float rangeMax = 1f;

	// Token: 0x04001715 RID: 5909
	public Element.State desiredState = Element.State.Gas;

	// Token: 0x04001716 RID: 5910
	private const int WINDOW_SIZE = 8;

	// Token: 0x04001717 RID: 5911
	private float[] samples = new float[8];

	// Token: 0x04001718 RID: 5912
	private int sampleIdx;

	// Token: 0x04001719 RID: 5913
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400171A RID: 5914
	private static readonly EventSystem.IntraObjectHandler<LogicPressureSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicPressureSensor>(delegate(LogicPressureSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
