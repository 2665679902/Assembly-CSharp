using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F4 RID: 1524
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicRadiationSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x0600271F RID: 10015 RVA: 0x000D2588 File Offset: 0x000D0788
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicRadiationSensor>(-905833192, LogicRadiationSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x000D25A4 File Offset: 0x000D07A4
	private void OnCopySettings(object data)
	{
		LogicRadiationSensor component = ((GameObject)data).GetComponent<LogicRadiationSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x000D25DE File Offset: 0x000D07DE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x000D2614 File Offset: 0x000D0814
	public void Sim200ms(float dt)
	{
		if (this.simUpdateCounter < 8 && !this.dirty)
		{
			int num = Grid.PosToCell(this);
			this.radHistory[this.simUpdateCounter] = Grid.Radiation[num];
			this.simUpdateCounter++;
			return;
		}
		this.simUpdateCounter = 0;
		this.dirty = false;
		this.averageRads = 0f;
		for (int i = 0; i < 8; i++)
		{
			this.averageRads += this.radHistory[i];
		}
		this.averageRads /= 8f;
		if (this.activateOnWarmerThan)
		{
			if ((this.averageRads > this.thresholdRads && !base.IsSwitchedOn) || (this.averageRads <= this.thresholdRads && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.averageRads >= this.thresholdRads && base.IsSwitchedOn) || (this.averageRads < this.thresholdRads && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x000D2719 File Offset: 0x000D0919
	public float GetAverageRads()
	{
		return this.averageRads;
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x000D2721 File Offset: 0x000D0921
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateVisualState(false);
		this.UpdateLogicCircuit();
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x000D2730 File Offset: 0x000D0930
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x000D2750 File Offset: 0x000D0950
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

	// Token: 0x06002727 RID: 10023 RVA: 0x000D27D8 File Offset: 0x000D09D8
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06002728 RID: 10024 RVA: 0x000D282B File Offset: 0x000D0A2B
	// (set) Token: 0x06002729 RID: 10025 RVA: 0x000D2833 File Offset: 0x000D0A33
	public float Threshold
	{
		get
		{
			return this.thresholdRads;
		}
		set
		{
			this.thresholdRads = value;
			this.dirty = true;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x0600272A RID: 10026 RVA: 0x000D2843 File Offset: 0x000D0A43
	// (set) Token: 0x0600272B RID: 10027 RVA: 0x000D284B File Offset: 0x000D0A4B
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

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x0600272C RID: 10028 RVA: 0x000D285B File Offset: 0x000D0A5B
	public float CurrentValue
	{
		get
		{
			return this.GetAverageRads();
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x0600272D RID: 10029 RVA: 0x000D2863 File Offset: 0x000D0A63
	public float RangeMin
	{
		get
		{
			return this.minRads;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x0600272E RID: 10030 RVA: 0x000D286B File Offset: 0x000D0A6B
	public float RangeMax
	{
		get
		{
			return this.maxRads;
		}
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x000D2873 File Offset: 0x000D0A73
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x000D2881 File Offset: 0x000D0A81
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06002731 RID: 10033 RVA: 0x000D288F File Offset: 0x000D0A8F
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.RADIATIONSWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06002732 RID: 10034 RVA: 0x000D2896 File Offset: 0x000D0A96
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.RADIATION;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06002733 RID: 10035 RVA: 0x000D289D File Offset: 0x000D0A9D
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.RADIATION_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06002734 RID: 10036 RVA: 0x000D28A9 File Offset: 0x000D0AA9
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.RADIATION_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x000D28B5 File Offset: 0x000D0AB5
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedRads(value, GameUtil.TimeSlice.None);
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x000D28BE File Offset: 0x000D0ABE
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x000D28C6 File Offset: 0x000D0AC6
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x000D28C9 File Offset: 0x000D0AC9
	public LocString ThresholdValueUnits()
	{
		return "";
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06002739 RID: 10041 RVA: 0x000D28D5 File Offset: 0x000D0AD5
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x0600273A RID: 10042 RVA: 0x000D28D8 File Offset: 0x000D0AD8
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x0600273B RID: 10043 RVA: 0x000D28DC File Offset: 0x000D0ADC
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(50f, 200f),
				new NonLinearSlider.Range(25f, 1000f),
				new NonLinearSlider.Range(25f, 5000f)
			};
		}
	}

	// Token: 0x0400171B RID: 5915
	private int simUpdateCounter;

	// Token: 0x0400171C RID: 5916
	[Serialize]
	public float thresholdRads = 280f;

	// Token: 0x0400171D RID: 5917
	[Serialize]
	public bool activateOnWarmerThan;

	// Token: 0x0400171E RID: 5918
	[Serialize]
	private bool dirty = true;

	// Token: 0x0400171F RID: 5919
	public float minRads;

	// Token: 0x04001720 RID: 5920
	public float maxRads = 5000f;

	// Token: 0x04001721 RID: 5921
	private const int NumFrameDelay = 8;

	// Token: 0x04001722 RID: 5922
	private float[] radHistory = new float[8];

	// Token: 0x04001723 RID: 5923
	private float averageRads;

	// Token: 0x04001724 RID: 5924
	private bool wasOn;

	// Token: 0x04001725 RID: 5925
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001726 RID: 5926
	private static readonly EventSystem.IntraObjectHandler<LogicRadiationSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicRadiationSensor>(delegate(LogicRadiationSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
