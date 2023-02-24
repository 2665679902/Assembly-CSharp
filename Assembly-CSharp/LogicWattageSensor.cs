using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005FC RID: 1532
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicWattageSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x060027B2 RID: 10162 RVA: 0x000D3FD4 File Offset: 0x000D21D4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicWattageSensor>(-905833192, LogicWattageSensor.OnCopySettingsDelegate);
	}

	// Token: 0x060027B3 RID: 10163 RVA: 0x000D3FF0 File Offset: 0x000D21F0
	private void OnCopySettings(object data)
	{
		LogicWattageSensor component = ((GameObject)data).GetComponent<LogicWattageSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x060027B4 RID: 10164 RVA: 0x000D402A File Offset: 0x000D222A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
		this.wasOn = this.switchedOn;
	}

	// Token: 0x060027B5 RID: 10165 RVA: 0x000D4060 File Offset: 0x000D2260
	public void Sim200ms(float dt)
	{
		this.currentWattage = Game.Instance.circuitManager.GetWattsUsedByCircuit(Game.Instance.circuitManager.GetCircuitID(Grid.PosToCell(this)));
		this.currentWattage = Mathf.Max(0f, this.currentWattage);
		if (this.activateOnHigherThan)
		{
			if ((this.currentWattage > this.thresholdWattage && !base.IsSwitchedOn) || (this.currentWattage <= this.thresholdWattage && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.currentWattage >= this.thresholdWattage && base.IsSwitchedOn) || (this.currentWattage < this.thresholdWattage && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x000D411A File Offset: 0x000D231A
	public float GetWattageUsed()
	{
		return this.currentWattage;
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x000D4122 File Offset: 0x000D2322
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateVisualState(false);
		this.UpdateLogicCircuit();
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x000D4131 File Offset: 0x000D2331
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x000D4150 File Offset: 0x000D2350
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

	// Token: 0x060027BA RID: 10170 RVA: 0x000D41D8 File Offset: 0x000D23D8
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x060027BB RID: 10171 RVA: 0x000D422B File Offset: 0x000D242B
	// (set) Token: 0x060027BC RID: 10172 RVA: 0x000D4233 File Offset: 0x000D2433
	public float Threshold
	{
		get
		{
			return this.thresholdWattage;
		}
		set
		{
			this.thresholdWattage = value;
			this.dirty = true;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x060027BD RID: 10173 RVA: 0x000D4243 File Offset: 0x000D2443
	// (set) Token: 0x060027BE RID: 10174 RVA: 0x000D424B File Offset: 0x000D244B
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnHigherThan;
		}
		set
		{
			this.activateOnHigherThan = value;
			this.dirty = true;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x060027BF RID: 10175 RVA: 0x000D425B File Offset: 0x000D245B
	public float CurrentValue
	{
		get
		{
			return this.GetWattageUsed();
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000D4263 File Offset: 0x000D2463
	public float RangeMin
	{
		get
		{
			return this.minWattage;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000D426B File Offset: 0x000D246B
	public float RangeMax
	{
		get
		{
			return this.maxWattage;
		}
	}

	// Token: 0x060027C2 RID: 10178 RVA: 0x000D4273 File Offset: 0x000D2473
	public float GetRangeMinInputField()
	{
		return this.minWattage;
	}

	// Token: 0x060027C3 RID: 10179 RVA: 0x000D427B File Offset: 0x000D247B
	public float GetRangeMaxInputField()
	{
		return this.maxWattage;
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000D4283 File Offset: 0x000D2483
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.WATTAGESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000D428A File Offset: 0x000D248A
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.WATTAGE;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000D4291 File Offset: 0x000D2491
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.WATTAGE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000D429D File Offset: 0x000D249D
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.WATTAGE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x060027C8 RID: 10184 RVA: 0x000D42A9 File Offset: 0x000D24A9
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedWattage(value, GameUtil.WattageFormatterUnit.Watts, units);
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x000D42B3 File Offset: 0x000D24B3
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x060027CA RID: 10186 RVA: 0x000D42BB File Offset: 0x000D24BB
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x060027CB RID: 10187 RVA: 0x000D42BE File Offset: 0x000D24BE
	public LocString ThresholdValueUnits()
	{
		return UI.UNITSUFFIXES.ELECTRICAL.WATT;
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x060027CC RID: 10188 RVA: 0x000D42C5 File Offset: 0x000D24C5
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x060027CD RID: 10189 RVA: 0x000D42C8 File Offset: 0x000D24C8
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x060027CE RID: 10190 RVA: 0x000D42CC File Offset: 0x000D24CC
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(5f, 5f),
				new NonLinearSlider.Range(35f, 1000f),
				new NonLinearSlider.Range(50f, 3000f),
				new NonLinearSlider.Range(10f, this.maxWattage)
			};
		}
	}

	// Token: 0x04001768 RID: 5992
	[Serialize]
	public float thresholdWattage;

	// Token: 0x04001769 RID: 5993
	[Serialize]
	public bool activateOnHigherThan;

	// Token: 0x0400176A RID: 5994
	[Serialize]
	public bool dirty = true;

	// Token: 0x0400176B RID: 5995
	private readonly float minWattage;

	// Token: 0x0400176C RID: 5996
	private readonly float maxWattage = 1.5f * Wire.GetMaxWattageAsFloat(Wire.WattageRating.Max50000);

	// Token: 0x0400176D RID: 5997
	private float currentWattage;

	// Token: 0x0400176E RID: 5998
	private bool wasOn;

	// Token: 0x0400176F RID: 5999
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001770 RID: 6000
	private static readonly EventSystem.IntraObjectHandler<LogicWattageSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicWattageSensor>(delegate(LogicWattageSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
