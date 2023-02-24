using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F9 RID: 1529
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicTemperatureSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x1700026F RID: 623
	// (get) Token: 0x0600277D RID: 10109 RVA: 0x000D368C File Offset: 0x000D188C
	public float StructureTemperature
	{
		get
		{
			return GameComps.StructureTemperatures.GetPayload(this.structureTemperature).Temperature;
		}
	}

	// Token: 0x0600277E RID: 10110 RVA: 0x000D36B1 File Offset: 0x000D18B1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicTemperatureSensor>(-905833192, LogicTemperatureSensor.OnCopySettingsDelegate);
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x000D36CC File Offset: 0x000D18CC
	private void OnCopySettings(object data)
	{
		LogicTemperatureSensor component = ((GameObject)data).GetComponent<LogicTemperatureSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x000D3708 File Offset: 0x000D1908
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x000D375C File Offset: 0x000D195C
	public void Sim200ms(float dt)
	{
		if (this.simUpdateCounter < 8 && !this.dirty)
		{
			int num = Grid.PosToCell(this);
			if (Grid.Mass[num] > 0f)
			{
				this.temperatures[this.simUpdateCounter] = Grid.Temperature[num];
				this.simUpdateCounter++;
			}
			return;
		}
		this.simUpdateCounter = 0;
		this.dirty = false;
		this.averageTemp = 0f;
		for (int i = 0; i < 8; i++)
		{
			this.averageTemp += this.temperatures[i];
		}
		this.averageTemp /= 8f;
		if (this.activateOnWarmerThan)
		{
			if ((this.averageTemp > this.thresholdTemperature && !base.IsSwitchedOn) || (this.averageTemp <= this.thresholdTemperature && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.averageTemp >= this.thresholdTemperature && base.IsSwitchedOn) || (this.averageTemp < this.thresholdTemperature && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06002782 RID: 10114 RVA: 0x000D3873 File Offset: 0x000D1A73
	public float GetTemperature()
	{
		return this.averageTemp;
	}

	// Token: 0x06002783 RID: 10115 RVA: 0x000D387B File Offset: 0x000D1A7B
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateVisualState(false);
		this.UpdateLogicCircuit();
	}

	// Token: 0x06002784 RID: 10116 RVA: 0x000D388A File Offset: 0x000D1A8A
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x000D38A8 File Offset: 0x000D1AA8
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

	// Token: 0x06002786 RID: 10118 RVA: 0x000D3930 File Offset: 0x000D1B30
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06002787 RID: 10119 RVA: 0x000D3983 File Offset: 0x000D1B83
	// (set) Token: 0x06002788 RID: 10120 RVA: 0x000D398B File Offset: 0x000D1B8B
	public float Threshold
	{
		get
		{
			return this.thresholdTemperature;
		}
		set
		{
			this.thresholdTemperature = value;
			this.dirty = true;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06002789 RID: 10121 RVA: 0x000D399B File Offset: 0x000D1B9B
	// (set) Token: 0x0600278A RID: 10122 RVA: 0x000D39A3 File Offset: 0x000D1BA3
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnWarmerThan;
		}
		set
		{
			this.activateOnWarmerThan = value;
			this.dirty = true;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x0600278B RID: 10123 RVA: 0x000D39B3 File Offset: 0x000D1BB3
	public float CurrentValue
	{
		get
		{
			return this.GetTemperature();
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x0600278C RID: 10124 RVA: 0x000D39BB File Offset: 0x000D1BBB
	public float RangeMin
	{
		get
		{
			return this.minTemp;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x0600278D RID: 10125 RVA: 0x000D39C3 File Offset: 0x000D1BC3
	public float RangeMax
	{
		get
		{
			return this.maxTemp;
		}
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x000D39CB File Offset: 0x000D1BCB
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x000D39D9 File Offset: 0x000D1BD9
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06002790 RID: 10128 RVA: 0x000D39E7 File Offset: 0x000D1BE7
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06002791 RID: 10129 RVA: 0x000D39EE File Offset: 0x000D1BEE
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06002792 RID: 10130 RVA: 0x000D39F5 File Offset: 0x000D1BF5
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06002793 RID: 10131 RVA: 0x000D3A01 File Offset: 0x000D1C01
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x000D3A0D File Offset: 0x000D1C0D
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedTemperature(value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, units, true);
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x000D3A19 File Offset: 0x000D1C19
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x000D3A21 File Offset: 0x000D1C21
	public float ProcessedInputValue(float input)
	{
		return GameUtil.GetTemperatureConvertedToKelvin(input);
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x000D3A2C File Offset: 0x000D1C2C
	public LocString ThresholdValueUnits()
	{
		LocString locString = null;
		switch (GameUtil.temperatureUnit)
		{
		case GameUtil.TemperatureUnit.Celsius:
			locString = UI.UNITSUFFIXES.TEMPERATURE.CELSIUS;
			break;
		case GameUtil.TemperatureUnit.Fahrenheit:
			locString = UI.UNITSUFFIXES.TEMPERATURE.FAHRENHEIT;
			break;
		case GameUtil.TemperatureUnit.Kelvin:
			locString = UI.UNITSUFFIXES.TEMPERATURE.KELVIN;
			break;
		}
		return locString;
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06002798 RID: 10136 RVA: 0x000D3A6C File Offset: 0x000D1C6C
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06002799 RID: 10137 RVA: 0x000D3A6F File Offset: 0x000D1C6F
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x0600279A RID: 10138 RVA: 0x000D3A74 File Offset: 0x000D1C74
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(25f, 260f),
				new NonLinearSlider.Range(50f, 400f),
				new NonLinearSlider.Range(12f, 1500f),
				new NonLinearSlider.Range(13f, 10000f)
			};
		}
	}

	// Token: 0x0400174F RID: 5967
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x04001750 RID: 5968
	private int simUpdateCounter;

	// Token: 0x04001751 RID: 5969
	[Serialize]
	public float thresholdTemperature = 280f;

	// Token: 0x04001752 RID: 5970
	[Serialize]
	public bool activateOnWarmerThan;

	// Token: 0x04001753 RID: 5971
	[Serialize]
	private bool dirty = true;

	// Token: 0x04001754 RID: 5972
	public float minTemp;

	// Token: 0x04001755 RID: 5973
	public float maxTemp = 373.15f;

	// Token: 0x04001756 RID: 5974
	private const int NumFrameDelay = 8;

	// Token: 0x04001757 RID: 5975
	private float[] temperatures = new float[8];

	// Token: 0x04001758 RID: 5976
	private float averageTemp;

	// Token: 0x04001759 RID: 5977
	private bool wasOn;

	// Token: 0x0400175A RID: 5978
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400175B RID: 5979
	private static readonly EventSystem.IntraObjectHandler<LogicTemperatureSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicTemperatureSensor>(delegate(LogicTemperatureSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
