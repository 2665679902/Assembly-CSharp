using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200065A RID: 1626
[SerializationConfig(MemberSerialization.OptIn)]
public class TemperatureControlledSwitch : CircuitSwitch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000E53C4 File Offset: 0x000E35C4
	public float StructureTemperature
	{
		get
		{
			return GameComps.StructureTemperatures.GetPayload(this.structureTemperature).Temperature;
		}
	}

	// Token: 0x06002B9B RID: 11163 RVA: 0x000E53E9 File Offset: 0x000E35E9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
	}

	// Token: 0x06002B9C RID: 11164 RVA: 0x000E5408 File Offset: 0x000E3608
	public void Sim200ms(float dt)
	{
		if (this.simUpdateCounter < 8)
		{
			this.temperatures[this.simUpdateCounter] = Grid.Temperature[Grid.PosToCell(this)];
			this.simUpdateCounter++;
			return;
		}
		this.simUpdateCounter = 0;
		this.averageTemp = 0f;
		for (int i = 0; i < 8; i++)
		{
			this.averageTemp += this.temperatures[i];
		}
		this.averageTemp /= 8f;
		if (this.activateOnWarmerThan)
		{
			if ((this.averageTemp > this.thresholdTemperature && !base.IsSwitchedOn) || (this.averageTemp < this.thresholdTemperature && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.averageTemp > this.thresholdTemperature && base.IsSwitchedOn) || (this.averageTemp < this.thresholdTemperature && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06002B9D RID: 11165 RVA: 0x000E54FC File Offset: 0x000E36FC
	public float GetTemperature()
	{
		return this.averageTemp;
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06002B9E RID: 11166 RVA: 0x000E5504 File Offset: 0x000E3704
	// (set) Token: 0x06002B9F RID: 11167 RVA: 0x000E550C File Offset: 0x000E370C
	public float Threshold
	{
		get
		{
			return this.thresholdTemperature;
		}
		set
		{
			this.thresholdTemperature = value;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000E5515 File Offset: 0x000E3715
	// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x000E551D File Offset: 0x000E371D
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnWarmerThan;
		}
		set
		{
			this.activateOnWarmerThan = value;
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000E5526 File Offset: 0x000E3726
	public float CurrentValue
	{
		get
		{
			return this.GetTemperature();
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000E552E File Offset: 0x000E372E
	public float RangeMin
	{
		get
		{
			return this.minTemp;
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000E5536 File Offset: 0x000E3736
	public float RangeMax
	{
		get
		{
			return this.maxTemp;
		}
	}

	// Token: 0x06002BA5 RID: 11173 RVA: 0x000E553E File Offset: 0x000E373E
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x06002BA6 RID: 11174 RVA: 0x000E554C File Offset: 0x000E374C
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000E555A File Offset: 0x000E375A
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000E5561 File Offset: 0x000E3761
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE;
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000E5568 File Offset: 0x000E3768
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000E5574 File Offset: 0x000E3774
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002BAB RID: 11179 RVA: 0x000E5580 File Offset: 0x000E3780
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedTemperature(value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, units, false);
	}

	// Token: 0x06002BAC RID: 11180 RVA: 0x000E558C File Offset: 0x000E378C
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06002BAD RID: 11181 RVA: 0x000E5594 File Offset: 0x000E3794
	public float ProcessedInputValue(float input)
	{
		return GameUtil.GetTemperatureConvertedToKelvin(input);
	}

	// Token: 0x06002BAE RID: 11182 RVA: 0x000E559C File Offset: 0x000E379C
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

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000E55DC File Offset: 0x000E37DC
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.InputField;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000E55DF File Offset: 0x000E37DF
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x000E55E2 File Offset: 0x000E37E2
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x040019CA RID: 6602
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x040019CB RID: 6603
	private int simUpdateCounter;

	// Token: 0x040019CC RID: 6604
	[Serialize]
	public float thresholdTemperature = 280f;

	// Token: 0x040019CD RID: 6605
	[Serialize]
	public bool activateOnWarmerThan;

	// Token: 0x040019CE RID: 6606
	public float minTemp;

	// Token: 0x040019CF RID: 6607
	public float maxTemp = 373.15f;

	// Token: 0x040019D0 RID: 6608
	private const int NumFrameDelay = 8;

	// Token: 0x040019D1 RID: 6609
	private float[] temperatures = new float[8];

	// Token: 0x040019D2 RID: 6610
	private float averageTemp;
}
